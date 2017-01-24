using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;

namespace FatBobbyGaming
{
    //Load time is between 60-120 miliseconds
    public class FBG_BattleSimulator : MonoBehaviour
    {
        public List<FBG_Pokemon> redTeam = new List<FBG_Pokemon>();
        public List<FBG_Pokemon> blueTeam = new List<FBG_Pokemon>();

        public int teamSize = 1;
        public int redIndex;
        public int blueIndex;

        private FBG_PokemonTeam redTeamStatus = new FBG_PokemonTeam();
        public FBG_PokemonTeam blueTeamStatus = new FBG_PokemonTeam();

        public MoveResults redResult;
        public MoveResults blueResult;
        public static List<battleHistory> moveHistory = new List<battleHistory>();

        public FBG_BattleGUI battleGUI;

        private bool isRedMoveCalculated;
        private bool isBlueMoveCalculated;

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
            FBG_BattleEnviornment.init();

            //this creates our teams
            createTeams();
            //debugRedTeam();

            battleGUI = this.GetComponent<FBG_BattleGUI>();
            battleGUI.checkButtonNames(redTeam, redIndex);

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        void Update()
        {
            battleGUI.checkButtonNames(redTeam, redIndex);
            doTurn();
        }

        public void redTeamAttack(int index)
        {
            //this is so if the blue team's move affects any of our stat stages, we calulate our attack with that already taken into account
            if(redTeam[redIndex].Speed < blueTeam[blueIndex].Speed && !isBlueMoveCalculated)
            {
                blueTeamAttack();
            }

            string atkName = redTeam[redIndex].atkMoves[index];

            if (atkName == "")
            {
                return;
            }
            //print(atkName);

            FBG_Pokemon self = redTeam[redIndex];
            FBG_Pokemon tar = blueTeam[blueIndex];
            redResult = FBG_Atk_Calc.calculateAttackEffect(tar,self, atkName);

            //if we go first we add the move to the history of moves
            if (redTeam[redIndex].Speed > blueTeam[blueIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, redResult.name, redResult);
                moveHistory.Add(hist);
            }else
            {
                //if we go seconds and didnt die, we also add the move to the history of moves
                if(redTeam[redIndex].curHp - blueResult.dmgReport.damage > 0)
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
            FBG_Pokemon self = blueTeam[blueIndex];
            FBG_Pokemon tar = redTeam[redIndex];
            blueResult = FBG_Atk_Calc.calculateAttackEffect(tar, self, atkName);

            //if this pokemon went first we add that move to our history of moves
            if(blueTeam[blueIndex].Speed > redTeam[redIndex].Speed)
            {
                battleHistory hist = new battleHistory(self.Name, blueResult.name, blueResult);
                moveHistory.Add(hist);
            }else
            {
                //if we didnt go first we will add this move to the history if we don't die before we use it
                if(blueTeam[blueIndex].curHp - redResult.dmgReport.damage > 0)
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
            FBG_Pokemon pokemon;

            //unfortunately we have to generate it twice.
            for(int i = 0; i < teamSize; i++)
            {
                id = Random.Range(0, 151);
                //id = 0;
                //level = 100;
                data = FBG_DexHandler.pokeDex.getStats(id);
                attacks = FBG_Atk_Data.masterGetAttacks(id);
                //print(string.Format("{0}", attacks.Count));
                pokemon = new FBG_Pokemon(level, data, attacks, ref redTeamStatus);

                redTeam.Add(pokemon);

                id = Random.Range(0, 151);
                //id = 1;
                data = FBG_DexHandler.pokeDex.getStats(id);
                attacks = FBG_Atk_Data.masterGetAttacks(id);
                pokemon = new FBG_Pokemon(level, data, attacks, ref blueTeamStatus);

                blueTeam.Add(pokemon);
            }
        }

        private void debugRedTeam()
        {
            for(int i = 0; i < redTeam.Count; i++)
            {
                FBG_Pokemon tmp = redTeam[i];
                if (redTeam[i] == null) return;
                print(string.Format("Red Team {0} Name: {1} Level {7} atk: {2} def: {3} spa: {4} spd: {5} spe: {6}", i, tmp.Name, tmp.Attack, tmp.Defense, tmp.Special_Attack, tmp.Special_Defense, tmp.Speed, tmp.Level));
            }
        }

        private void doTurn()
        {
            if(isRedMoveCalculated && isBlueMoveCalculated)
            {
                isRedMoveCalculated = false;
                isBlueMoveCalculated = false;

                if(redTeam[redIndex].Speed >= blueTeam[blueIndex].Speed)
                {
                    doMove(blueTeam[blueIndex], redTeam[redIndex], redResult);
                    doMove(redTeam[redIndex], blueTeam[blueIndex], blueResult);
                }else
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
        private void doMove(FBG_Pokemon tar, FBG_Pokemon self, MoveResults move)
        {
            //if the current pokemon is dead, then we do not apply damage, heal, or recoil
            if (self.curHp <= 0) return;
            if(self.status_A != nonVolitileStatusEffects.none)
            {
                nonVolitleMove nv = FBG_Utils.isMoveHaltedByNV(self);
                if (nv.isAffected)
                {
                    //well we don't do the move.... but we gotta say something in the text
                    return;
                }
            }

            //Damage
            if(move.dmgReport.damage != 0)
            {
                if(tar.curHp - move.dmgReport.damage <= 0)
                {
                    move.dmgReport.damage = tar.curHp;
                }
                tar.curHp -= (int)move.dmgReport.damage;
                //now we have to force the other team to switch!
            }

            //Heal
            if(move.dmgReport.heal > 0)
            {
                if(move.dmgReport.heal + self.curHp > self.maxHP)
                {
                    move.dmgReport.heal = self.maxHP - self.curHp;
                }
                self.curHp += (int)move.dmgReport.heal;
            }

            //Recoil
            if (move.dmgReport.recoil > 0)
            {
                if (self.curHp - move.dmgReport.recoil <= 0)
                {
                    move.dmgReport.recoil = self.curHp;
                }
                self.curHp -= (int)move.dmgReport.recoil;
                //we have to force ourselves to switch
            }
        }

        //this swaps the index??
        public void swapPokemon(FBG_Pokemon cur, FBG_Pokemon desired)
        {
            if(cur.team == blueTeamStatus)
            {

            }else
            {

            }
        }
    }

    [System.Serializable]
    public class battleHistory
    {
        public string pokemonName;
        public string attackName;
        public string atkCategory;
        public string atkType;
        public MoveResults MR;

        public battleHistory( string name, string atkName, MoveResults mr)
        {
            pokemonName = name;
            attackName = atkName;
            MR = mr;

            attacks move = FBG_Atk_Data.searchAttackList(atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }
    }
}
