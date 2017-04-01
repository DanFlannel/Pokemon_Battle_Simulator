using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBG.Base
{
    public class TeamPokemon : TeamEffects
    {
        public TeamPokemon enemyTeam;
        public List<int> aliveIndexs = new List<int>();

        public TeamPokemon()
        {
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

    }
}
