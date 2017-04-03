using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.JSON;
using System.Linq;

namespace FBG.Data
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

        public static AttackJsonData Get(this AttackData a, string atkName)
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
            if(ratio > 0)
            {
                //subtracting 1 because all ratios start at 2 in the attack json
                ratio--;
            }
            return ratio;
        }

        public static bool checkFlag(this AttackData a, string atkName, string flag)
        {
            AttackJsonData atk = a.Get(atkName);

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
            bool flag = false;
            for (int i = 0; i < p.pokemon.Length; i++)
            {
                if (p.pokemon[i].name.ToLower() == name.ToLower())
                {
                    flag = true;
                    poke = p.pokemon[i];
                }
            }
            if (flag == false)
            {
                return null;
            }
            corePokemonData data = new corePokemonData(poke);
            return data;
        }
    }
}
