using System.Linq;

using UnityEngine;

using JSON;
using Attack;

namespace Data
{
    public static class DexMethods
    {
        /// <summary>
        /// Got this from http://stackoverflow.com/questions/7411438/remove-characters-from-c-sharp-string
        /// Use this to remove all non alphabetical characters, spaces from a given string
        /// </summary>
        /// <param name="str">string to be stripped</param>
        /// <returns>a string with only lower case characters</returns>
        public static string stripString(string str)
        {
            str = new string((from c in str where char.IsWhiteSpace(c) || char.IsLetterOrDigit(c) select c).ToArray());
            str = str.Replace(" ", string.Empty);
            return str.ToLower();
        }

        public static AttackJsonData getAttackJsonData(this AttackData a, string atkName)
        {
            for (int i = 0; i < a.attacks.Length; i++)
            {
                atkName = stripString(atkName);
                string id = stripString(a.attacks[i].id);
                if (atkName == id)
                {
                    return a.attacks[i];
                }
            }
            Debug.Log(string.Format("Attack {0} not found in {1} attacks", atkName, a.attacks.Length));
            return null;
        }

        public static AttackJsonData getAttackJsonData(this AttackData a, int id)
        {
            if (id <= a.attacks.Length)
            {
                return a.attacks[id];
            }
            Debug.Log(string.Format("Attack id: {0} not found in {1} attacks", id, a.attacks.Length));
            return null;
        }

        public static attacks getAttack(this AttackData a, string atkName)
        {
            for (int i = 0; i < MoveSets.attackList.Count; i++)
            {
                if (MoveSets.attackList[i].name.ToLower() == atkName.ToLower())
                {
                    return MoveSets.attackList[i];
                }
            }
            Debug.Log(string.Format("Attack {0} not found in {1} attacks", atkName, MoveSets.attackList.Count));
            return new attacks();
        }

        public static attacks getAttack(this AttackData a, int id)
        {
            if (id <= MoveSets.attackList.Count)
            {
                return MoveSets.attackList[id];
            }
            Debug.Log(string.Format("Attack id: {0} not found in {1} attacks", id, MoveSets.attackList.Count));
            return new attacks();
        }

        public static int GetCirtRatio(this AttackData a, string atkName)
        {
            int ratio = 0;
            for (int i = 0; i < a.attacks.Length; i++)
            {
                atkName = stripString(atkName);
                string id = stripString(a.attacks[i].id);
                if (atkName == id)
                {
                    ratio = a.attacks[i].critRatio;
                }
            }
            if (ratio > 0)
            {
                //subtracting 1 because all ratios start at 2 in the attack json
                ratio--;
            }
            return ratio;
        }

        public static bool checkFlag(this AttackData a, string atkName, string flag)
        {
            AttackJsonData atk = a.getAttackJsonData(atkName);

            for (int i = 0; i < atk.flags.Length; i++)
            {
                if (flag.ToLower() == atk.flags[i].ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static corePokemonData getStats(this PokedexData p, int i)
        {
            PokemonJsonData poke = p.pokemon[i];
            corePokemonData data = new corePokemonData(poke);
            return data;
        }

        public static corePokemonData getStats(this PokedexData p, string name)
        {
            PokemonJsonData poke = null;
            for (int i = 0; i < p.pokemon.Length; i++)
            {
                if (p.pokemon[i].name.ToLower() == name.ToLower())
                {
                    poke = p.pokemon[i];
                }
            }
            if (poke != null)
            {
                corePokemonData data = new corePokemonData(poke);
                return data;
            }
            return null;
        }

        public static PokemonJsonData getData(this PokedexData p, int i)
        {
            return p.pokemon[i];
        }

        public static PokemonJsonData getData(this PokedexData p, string name)
        {
            for (int i = 0; i < p.pokemon.Length; i++)
            {
                if (p.pokemon[i].name.ToLower() == name.ToLower())
                {
                    return p.pokemon[i];
                }
            }
            return null;
        }
    }
}