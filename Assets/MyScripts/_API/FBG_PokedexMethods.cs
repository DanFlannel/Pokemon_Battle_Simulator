using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FatBobbyGaming
{

    public static class FBG_PokedexMethods
    {

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
            for(int i = 0; i < p.pokemon.Length; i++)
            {
                if(p.pokemon[i].name.ToLower() == name.ToLower())
                {
                    flag = true;
                    poke = p.pokemon[i];
                }
            }
            if(flag == false)
            {
                return null;
            }
            corePokemonData data = new corePokemonData(poke);
            return data;
        }
    }
}
