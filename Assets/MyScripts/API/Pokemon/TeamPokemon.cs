using FBG.Attack;
using FBG.Data;
using FBG.JSON;
using System.Collections;
using System.Collections.Generic;


namespace FBG.Base
{
    public class TeamPokemon : TeamEffects
    {
        private TeamPokemon instance;
        public TeamPokemon enemyTeam;
        public List<int> aliveIndexs = new List<int>();
        public List<PokemonBase> pokemon = new List<PokemonBase>();
        public int teamSize;
        public int curIndex;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }

        public TeamPokemon(int size)
        {
            teamSize = size;
            instance = this;
            Init();
        }

        public void Init()
        {
            SetupTeamEffects();
        }

        public void assignEnemyTeam(ref TeamPokemon team)
        {
            enemyTeam = team;
        }

        public void OnSwap()
        {
            if (enemyTeam.hasLeechSeed)
            {
                enemyTeam.hasLeechSeed = false;
            }
        }

        public void generateRandomTeam()
        {
            corePokemonData data;
            int id;
            int level = 75;
            List<attackIndex> attacks = new List<attackIndex>();
            PokemonBase tmpPokemon;

            //unfortunately we have to generate it twice.
            for (int i = 0; i < teamSize; i++)
            {
                id = UnityEngine.Random.Range(0, 151);
                //id = 0;
                //level = 100;
                data = DexHolder.pokeDex.getStats(id);
                attacks = MoveSets.masterGetAttacks(id);
                //print(string.Format("{0}", attacks.Count));
                tmpPokemon = new PokemonBase(level, data, attacks, ref instance);
                pokemon.Add(tmpPokemon);
            }
        }
    }
}
