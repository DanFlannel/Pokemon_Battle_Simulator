using FBG.Attack;
using FBG.Data;

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

            attacks move = DexHolder.attackDex.getAttack(atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }

        public battleHistory(string attackerName, MoveResults res)
        {
            pokemonName = attackerName;
            attackName = res.name;
            MR = res;

            attacks move = DexHolder.attackDex.getAttack(res.name);
            atkType = move.type;
            atkCategory = move.cat;
        }
    }
}