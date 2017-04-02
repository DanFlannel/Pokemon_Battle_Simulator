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

        public List<PokemonBase> redTeam = new List<PokemonBase>();
        public List<PokemonBase> blueTeam = new List<PokemonBase>();

        public int teamSize = 1;
        public int redIndex;
        public int blueIndex;

        private TeamPokemon redTeamStatus = new TeamPokemon();
        private TeamPokemon blueTeamStatus = new TeamPokemon();

        public MoveResults redResult;
        public MoveResults blueResult;
        public static List<battleHistory> moveHistory = new List<battleHistory>();

        public BattleGUI battleGUI;

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

            redTeamStatus.assignEnemyTeam(ref blueTeamStatus);
            blueTeamStatus.assignEnemyTeam(ref redTeamStatus);

            //get the enviornment ready
            BattleEnviornment.init();

            //this creates our teams
            createTeams();
            //debugRedTeam();

            battleGUI = this.GetComponent<BattleGUI>();
            battleGUI.checkButtonNames(redTeam, redIndex);

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        void Update()
        {
            capPokemonIndex();
            battleGUI.checkButtonNames(redTeam, redIndex);
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

        public void redTeamAttack(int index)
        {
            //this is so if the blue team's move affects any of our stat stages, we calulate our attack with that already taken into account
            if (redTeam[redIndex].Speed < blueTeam[blueIndex].Speed && !isBlueMoveCalculated)
            {
                blueTeamAttack();
            }

            string atkName = redTeam[redIndex].atkMoves[index];

            if (atkName == "")
            {
                return;
            }
            //print(atkName);

            PokemonBase self = redTeam[redIndex];
            PokemonBase tar = blueTeam[blueIndex];
            redResult = AtkCalc.calculateAttackEffect(tar, self, atkName);

            //if we go first we add the move to the history of moves
            if (redTeam[redIndex].Speed > blueTeam[blueIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, redResult.name, redResult);
                moveHistory.Add(hist);
            }
            else
            {
                //if we go seconds and didnt die, we also add the move to the history of moves
                if (redTeam[redIndex].curHp - blueResult.dmgReport.damage > 0)
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
            if (redTeam[redIndex].Speed > blueTeam[blueIndex].Speed && !isRedMoveCalculated)
            {
                //redTeamAttack();
            }

            int len = blueTeam[blueIndex].atkMoves.Count;
            int rnd = Random.Range(0, len);
            string atkName = blueTeam[blueIndex].atkMoves[rnd];

            while (atkName == "")
            {
                rnd = Random.Range(0, len);
                atkName = blueTeam[blueIndex].atkMoves[rnd];
            }
            PokemonBase self = blueTeam[blueIndex];
            PokemonBase tar = redTeam[redIndex];
            blueResult = AtkCalc.calculateAttackEffect(tar, self, atkName);

            //if this pokemon went first we add that move to our history of moves
            if (blueTeam[blueIndex].Speed > redTeam[redIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, blueResult.name, blueResult);
                moveHistory.Add(hist);
            }
            else
            {
                //if we didnt go first we will add this move to the history if we don't die before we use it
                if (blueTeam[blueIndex].curHp - redResult.dmgReport.damage > 0)
                {
                    battleHistory hist = new battleHistory(self.Name, blueResult.name, blueResult);
                    moveHistory.Add(hist);
                }
            }
            isBlueMoveCalculated = true;
        }

        private void createTeams()
        {
            corePokemonData data;
            int id;
            int level = 75;
            List<attackIndex> attacks = new List<attackIndex>();
            PokemonBase pokemon;

            //unfortunately we have to generate it twice.
            for (int i = 0; i < teamSize; i++)
            {
                id = Random.Range(0, 151);
                //id = 0;
                //level = 100;
                data = DexHolder.pokeDex.getStats(id);
                attacks = MoveSets.masterGetAttacks(id);
                //print(string.Format("{0}", attacks.Count));
                pokemon = new PokemonBase(level, data, attacks, ref redTeamStatus);

                redTeam.Add(pokemon);

                id = Random.Range(0, 151);
                //id = 1;
                data = DexHolder.pokeDex.getStats(id);
                attacks = MoveSets.masterGetAttacks(id);
                pokemon = new PokemonBase(level, data, attacks, ref blueTeamStatus);

                blueTeam.Add(pokemon);
            }
        }

        private void doTurn()
        {
            if (isRedMoveCalculated && isBlueMoveCalculated)
            {
                isRedMoveCalculated = false;
                isBlueMoveCalculated = false;

                if (redTeam[redIndex].Speed >= blueTeam[blueIndex].Speed)
                {
                    doMove(blueTeam[blueIndex], redTeam[redIndex], redResult);
                    doMove(redTeam[redIndex], blueTeam[blueIndex], blueResult);
                }
                else
                {
                    doMove(redTeam[redIndex], blueTeam[blueIndex], blueResult);
                    doMove(blueTeam[blueIndex], redTeam[redIndex], redResult);
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
        public void swapPokemon(PokemonBase cur, PokemonBase desired)
        {
            if (cur.team == blueTeamStatus)
            {

            }
            else
            {

            }
        }
    }
}
