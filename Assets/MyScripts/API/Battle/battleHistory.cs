﻿using FBG.Attack;

namespace FBG.Battle
{
    [System.Serializable]
    public class battleHistory
    {
        public string pokemonName;
        public string attackName;
        public string atkCategory;
        public string atkType;
        public MoveResults MR;

        public battleHistory(string name, string atkName, MoveResults mr)
        {
            pokemonName = name;
            attackName = atkName;
            MR = mr;

            attacks move = MoveSets.searchAttackList(atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }
    }
}