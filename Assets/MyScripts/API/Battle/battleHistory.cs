using FBG.Attack;

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
            attackName = mr.name;
            MR = mr;

            attacks move = MoveSets.searchAttackList(atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }

        public battleHistory(string attackerName, MoveResults res)
        {
            pokemonName = attackerName;
            attackerName = res.name;
            MR = res;

            attacks move = MoveSets.searchAttackList(res.name);
            atkType = move.type;
            atkCategory = move.cat;
        }
    }
}