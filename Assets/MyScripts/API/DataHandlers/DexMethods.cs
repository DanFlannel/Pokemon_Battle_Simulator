using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.JSON;

namespace FBG.Data
{
    public static class DexMethods
    {
        public static AttackJsonData Get(this AttackData a, string atkName)
        {
            for (int i = 0; i < a.attacks.Length; i++)
            {
                if (atkName.ToLower() == a.attacks[i].name.ToLower())
                {
                    return a.attacks[i];
                }
            }
            return null;
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
