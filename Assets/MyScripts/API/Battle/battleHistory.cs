using FBG.Attack;

namespace FBG.Battle
{
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

            attacks move = AtkData.searchAttackList(atkName);
            atkType = move.type;
            atkCategory = move.cat;
        }
    }
}