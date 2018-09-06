using System.Collections.Generic;

using UnityEngine;

using Data;
using Base;

namespace Battle
{
    [System.Serializable]
    public class battleHistory
    {
        public PokemonBase self;
        public PokemonBase target;
        public string attackName;
        public string atkCategory;
        public string atkType;
        public MoveResults MR;
        public string halted;

        public battleHistory(PokemonBase self, MoveResults res, PokemonBase target)
        {
            this.self = self;
            attackName = res.atkName;
            MR = res;

            attacks move = DexHolder.attackDex.getAttack(res.atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }

        public battleHistory(PokemonBase pkmn, string moveName, string haltingcondition)
        {
            self = pkmn;
            attackName = moveName;
            halted = haltingcondition;
        }
    }

    public static class battleHistExtensions
    {
        public static battleHistory getLastEnemyAttack(this List<battleHistory> list, PokemonBase pkmn)
        {
            Debug.Log(string.Format("{0} is searching for last enemy attack", pkmn.Name));
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].self.Name != pkmn.Name && list[i].self.team != pkmn.team)
                {
                    Debug.Log(string.Format("found prev enemy attack by: {0} move: {1} damage: {2}", list[i].self.Name, list[i].attackName, list[i].MR.dmgReport.damage));
                    return list[i];
                }
            }
            Debug.Log("No valid previous attacks found");
            return null;
        }
    }
}