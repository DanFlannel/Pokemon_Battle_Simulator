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

        // Use this for initialization
        private void Start()
        {
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
        }

        public void redTeamAttack(int index)
        {
            string atkName = redTeam[redIndex].atkMoves[index];

            if (atkName == "")
            {
                return;
            }
            //print(atkName);

            FBG_Pokemon self = redTeam[redIndex];
            FBG_Pokemon tar = blueTeam[blueIndex];
            redResult = FBG_Atk_Calc.calculateAttackEffect(tar,self, atkName);

            //I have to redo how the history is done
            battleHistory hist = new battleHistory(self.Name, atkName, redResult);
            moveHistory.Add(hist);

        }

        //this is the random AI attack
        private void blueTeamAttack()
        {
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

            battleHistory hist = new battleHistory(self.Name, atkName, blueResult);
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

        //this swaps the index??
        public void swapPokemon(FBG_Pokemon tar, FBG_Pokemon desired)
        {
            if(tar.team == blueTeamStatus)
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
