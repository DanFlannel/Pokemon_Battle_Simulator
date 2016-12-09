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
        public static List<battleHistory> moveHistory = new List<battleHistory>();
        public int teamSize = 1;
        public int redIndex;
        public int blueIndex;

        private FBG_PokemonTeam redTeamStatus = new FBG_PokemonTeam();
        public FBG_PokemonTeam blueTeamStatus = new FBG_PokemonTeam();


        public MoveResults redResult;
        [HideInInspector]
        public PokedexData pokeDex;

        public FBG_BattleGUI battleGUI;

        // Use this for initialization
        void Awake()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            redTeamStatus.assignEnemyTeam(ref blueTeamStatus);
            blueTeamStatus.assignEnemyTeam(ref redTeamStatus);

            //get the enviornment ready
            FBG_BattleEnviornment.init();

            //this initalizes our pokedex
            pokeDex = FBG_JsonReader.createPokeDex();

            //this creates our teams
            createTeams();
            //debugRedTeam();

            battleGUI = this.GetComponent<FBG_BattleGUI>();
            checkButtonNames();

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        void Update()
        {
            checkButtonNames();
        }

        public void redTeamAttack(int index)
        {
            string atkName = redTeam[redIndex].atkMoves[index];
            //print(atkName);
            redResult = FBG_Atk_Calc.calculateAttackEffect(blueTeam[blueIndex], redTeam[redIndex], atkName);
            string pName = redTeam[redIndex].Name;

            battleHistory hist = new battleHistory(pName, atkName, redResult);
            moveHistory.Add(hist);

        }

        //cheap way of doing mimic and transform
        public void checkButtonNames()
        {
            for(int i = 0; i < battleGUI.btns.Length; i++)
            {
                if(redTeam[redIndex].atkMoves[i] != battleGUI.btns[i].GetComponentInChildren<Text>().text)
                {
                    battleGUI.setButtonNames(redTeam[redIndex].atkMoves);
                }
            }
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
                data = FBG_JsonReader.pokemonStats(pokeDex, id);
                attacks = FBG_Atk_Data.masterGetAttacks(id);
                //print(string.Format("{0}", attacks.Count));
                pokemon = new FBG_Pokemon(level, data, attacks, ref redTeamStatus);

                redTeam.Add(pokemon);

                id = Random.Range(0, 151);
                //id = 1;
                data = FBG_JsonReader.pokemonStats(pokeDex, id);
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

        private bool checkPlayerNVStatus(FBG_Pokemon self)
        {
            if (self.status_A != nonVolitileStatusEffects.none)
            {
                if (self.status_A == nonVolitileStatusEffects.paralized)
                {
                    int rnd = UnityEngine.Random.Range(1, 4);
                    if (rnd == 1)
                    {
                        string text = self.Name + " is Paralized!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }
                }
                if (self.status_A == nonVolitileStatusEffects.sleep)
                {
                    self.nonVolDuration--;
                    string text = "";
                    if (self.nonVolDuration == 0)
                    {
                        text = self.Name + " woke up!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                    }
                    else
                    {
                        text = self.Name + " is fast asleep!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }

                }
                if (self.status_A == nonVolitileStatusEffects.frozen)
                {
                    int rnd = UnityEngine.Random.Range(1, 10);
                    if (rnd >= 2)
                    {
                        string text = self.Name + " is Frozen!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));
                        return true;
                    }
                    else
                    {
                        self.status_A = nonVolitileStatusEffects.none;
                        string text = self.Name + " thawed out!";
                        //c_Queue.AddCoroutineToQueue(DisplayText(text));

                    }
                }
            }
            return false;
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
