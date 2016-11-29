using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;



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

        public MoveResults redResult;
        [HideInInspector]
        public PokedexData pokeDex;



        // Use this for initialization
        void Awake()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //this initalizes our pokedex

            pokeDex = FBG_JsonReader.createPokeDex();
            //Debug.Log(string.Format("{0} {1} ", pokeDex.pokemon[0].name, pokeDex.pokemon[0].baseStates.atk));
            //this initalizes the attack library

            //this creates our teams
            createTeams();
            //debugRedTeam();

            this.GetComponent<FBG_BattleGUI>().setButtonNames(redTeam[redIndex].atkMoves);

            sw.Stop();
            print(string.Format("Time to load {0}ms", sw.ElapsedMilliseconds));
        }

        public void redTeamAttack(int index)
        {
            string atkName = redTeam[redIndex].atkMoves[index];
            redResult = FBG_Atk_Calc.calculateAttackEffect(blueTeam[blueIndex], redTeam[redIndex], atkName);
            string pName = redTeam[redIndex].Name;

            battleHistory hist = new battleHistory(pName, atkName, redResult);
            moveHistory.Add(hist);

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
                pokemon = new FBG_Pokemon(level, data, attacks);

                redTeam.Add(pokemon);

                id = Random.Range(0, 151);
                //id = 1;
                data = FBG_JsonReader.pokemonStats(pokeDex, id);
                attacks = FBG_Atk_Data.masterGetAttacks(id);
                pokemon = new FBG_Pokemon(level, data, attacks);

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
    }

    [System.Serializable]
    public class battleHistory
    {
        public string pokemonName;
        public string attackName;
        public MoveResults MR;

        public battleHistory( string name, string atkName, MoveResults mr)
        {
            pokemonName = name;
            attackName = atkName;
            MR = mr;
        }
    }
}
