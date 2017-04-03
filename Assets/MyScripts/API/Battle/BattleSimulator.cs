using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using FBG.Base;
using FBG.Attack;
using FBG.JSON;
using FBG.Data;

namespace FBG.Battle
{
    public class BattleSimulator : MonoBehaviour
    {
        private static BattleSimulator instance = null;
        public static BattleSimulator Instance { get { return instance; } }

        public int teamSize = 1;
        public int redIndex;
        public int blueIndex;

        private TeamPokemon redTeam;
        private TeamPokemon blueTeam;

        public MoveResults redResult;
        public MoveResults blueResult;
        public static List<battleHistory> moveHistory = new List<battleHistory>();

        public BattleGUI battleGUI;

        public GifRenderer redSprite;
        public GifRenderer blueSprite;

        private bool isRedMoveCalculated;
        private bool isBlueMoveCalculated;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = this;
            }
        }

        // Use this for initialization
        private void Start()
        {
            isRedMoveCalculated = false;
            isBlueMoveCalculated = false;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            redTeam = new TeamPokemon(teamSize);
            blueTeam = new TeamPokemon(teamSize);

            redTeam.assignEnemyTeam(ref blueTeam);
            blueTeam.assignEnemyTeam(ref redTeam);

            redTeam.generateRandomTeam();
            blueTeam.generateRandomTeam();

            //get the enviornment ready
            BattleEnviornment.init();

            battleGUI = this.GetComponent<BattleGUI>();
            battleGUI.checkButtonNames(redTeam.curPokemon);

            battleGUI.changePokemonSprite(redSprite, redTeam.pokemon[redIndex].Name, redTeam.pokemon[redIndex].ID);
            battleGUI.changePokemonSprite(blueSprite, blueTeam.pokemon[blueIndex].Name, blueTeam.pokemon[blueIndex].ID);

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        void Update()
        {
            capPokemonIndex();
            battleGUI.checkButtonNames(redTeam.curPokemon);
            doTurn();
        }

        private void capPokemonIndex()
        {
            if(redIndex > teamSize - 1)
            {
                redIndex = teamSize - 1;
            }
            if(blueIndex > teamSize -1)
            {
                blueIndex = teamSize - 1;
            }

            if(redIndex < 0)
            {
                redIndex = 0;
            }
            if(blueIndex < 0)
            {
                blueIndex = 0;
            }
        }

        //this it the current button method
        public void redTeamAttack(int index)
        {
            //this is so if the blue team's move affects any of our stat stages, we calulate our attack with that already taken into account
            if (redTeam.pokemon[redIndex].Speed < blueTeam.pokemon[blueIndex].Speed && !isBlueMoveCalculated)
            {
                blueTeamAttack();
            }

            string atkName = redTeam.pokemon[redIndex].atkMoves[index];

            if (atkName == "")
            {
                return;
            }
            //print(atkName);

            PokemonBase self = redTeam.pokemon[redIndex];
            PokemonBase tar = blueTeam.pokemon[blueIndex];
            redResult = AtkCalc.calculateAttackEffect(tar, self, atkName);

            //if we go first we add the move to the history of moves
            if (redTeam.pokemon[redIndex].Speed > blueTeam.pokemon[blueIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, redResult.name, redResult);
                moveHistory.Add(hist);
            }
            else
            {
                //if we go seconds and didnt die, we also add the move to the history of moves
                if (redTeam.pokemon[redIndex].curHp - blueResult.dmgReport.damage > 0)
                {
                    battleHistory hist = new battleHistory(self.Name, redResult.name, redResult);
                    moveHistory.Add(hist);
                }
            }
            isRedMoveCalculated = true;
        }

        //this is the random AI attack
        private void blueTeamAttack()
        {
            if (redTeam.pokemon[redIndex].Speed > blueTeam.pokemon[blueIndex].Speed && !isRedMoveCalculated)
            {
                //redTeamAttack();
            }

            int len = blueTeam.pokemon[blueIndex].atkMoves.Count;
            int rnd = Random.Range(0, len);
            string atkName = blueTeam.pokemon[blueIndex].atkMoves[rnd];

            while (atkName == "")
            {
                rnd = Random.Range(0, len);
                atkName = blueTeam.pokemon[blueIndex].atkMoves[rnd];
            }
            PokemonBase self = blueTeam.pokemon[blueIndex];
            PokemonBase tar = redTeam.pokemon[redIndex];
            blueResult = AtkCalc.calculateAttackEffect(tar, self, atkName);

            //if this pokemon went first we add that move to our history of moves
            if (blueTeam.pokemon[blueIndex].Speed > redTeam.pokemon[redIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, blueResult.name, blueResult);
                moveHistory.Add(hist);
            }
            else
            {
                //if we didnt go first we will add this move to the history if we don't die before we use it
                if (blueTeam.pokemon[blueIndex].curHp - redResult.dmgReport.damage > 0)
                {
                    battleHistory hist = new battleHistory(self.Name, blueResult.name, blueResult);
                    moveHistory.Add(hist);
                }
            }
            isBlueMoveCalculated = true;
        }

        private void doTurn()
        {
            if (isRedMoveCalculated && isBlueMoveCalculated)
            {
                isRedMoveCalculated = false;
                isBlueMoveCalculated = false;

                if (redTeam.pokemon[redIndex].Speed >= redTeam.pokemon[blueIndex].Speed)
                {
                    doMove(blueTeam.pokemon[blueIndex], redTeam.pokemon[redIndex], redResult);
                    doMove(redTeam.pokemon[redIndex], blueTeam.pokemon[blueIndex], blueResult);
                }
                else
                {
                    doMove(redTeam.pokemon[redIndex], blueTeam.pokemon[blueIndex], blueResult);
                    doMove(blueTeam.pokemon[blueIndex], redTeam.pokemon[redIndex], redResult);
                }
            }
        }

        /// <summary>
        /// If we are doing a status move that affects any stat stage, that is already calcuated into the attacks, because when we the player attack we check the speed first before calculating thereby giving the other pokemon the chance to reduce one of our stats before we actually do anything. The calculations only return a MoveResults class, which contains all the information about the move, in this method we are simply applying that data in order.
        /// </summary>
        /// <param name="tar">target pokemon</param>
        /// <param name="self">self pokemon</param>
        /// <param name="move">the move results when calculated</param>
        private void doMove(PokemonBase tar, PokemonBase self, MoveResults move)
        {
            //if the current pokemon is dead, then we do not apply damage, heal, or recoil
            if (self.curHp <= 0) { return; }
            if (!checkNVStatus(self))
            {
                return;
            }

            //Damage
            applyDamage(tar, self, move);

            //Heal
            applyHeal(tar, self, move);

            //Recoil
            applyRecoil(tar, self, move);
        }

        private bool checkNVStatus(PokemonBase self)
        {
            if (self.status_A != nonVolitileStatusEffects.none)
            {
                nonVolitleMove nv = Utilities.isMoveHaltedByNV(self);
                if (nv.isAffected)
                {
                    //TODO add text coriutine
                    return true;
                }
            }
            return false;
        }

        private void applyDamage(PokemonBase tar, PokemonBase self, MoveResults move)
        {
            if (move.dmgReport.damage == 0) { return; }
            if (tar.curHp - move.dmgReport.damage <= 0)
            {
                move.dmgReport.damage = tar.curHp;
            }
            tar.curHp -= (int)move.dmgReport.damage;
            //now we have to force the other team to switch!
        }

        private void applyHeal(PokemonBase tar, PokemonBase self, MoveResults move)
        {
            if (move.dmgReport.heal == 0) { return; }
            if (move.dmgReport.heal + self.curHp > self.maxHP)
            {
                move.dmgReport.heal = self.maxHP - self.curHp;
            }
            self.curHp += (int)move.dmgReport.heal;
        }

        private void applyRecoil(PokemonBase tar, PokemonBase self, MoveResults move)
        {
            if (move.dmgReport.recoil == 0) { return; }
            if (self.curHp - move.dmgReport.recoil <= 0)
            {
                move.dmgReport.recoil = self.curHp;
            }
            self.curHp -= (int)move.dmgReport.recoil;
        }

        //this swaps the index??
        public void swapPokemon(TeamPokemon curTeam, int newIndex)
        {
            if (curTeam == blueTeam)
            {

            }
            else
            {

            }
        }
    }
}
