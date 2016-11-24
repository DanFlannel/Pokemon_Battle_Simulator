using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;

namespace FatBobbyGaming
{
    public static class  FBG_JsonReader
    {
        public static PokedexData createPokeDex()
        {
            TextAsset jsonFile = loadTextFile();
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            PokedexData p = JsonUtility.FromJson<PokedexData>(jsonFile.text);
            createDamageMultipliers(p);
            //debugJson(p);
            //sw.Stop();
            //print("time to create json in ms: " + sw.ElapsedMilliseconds);
            return p;
        }

        public static corePokemonData pokemonStats(PokedexData p, int id)
        {
            PokemonJsonData pokemon = p.pokemon[id];
            
            corePokemonData data = new corePokemonData(pokemon);
            return data;
        }

        private static void createDamageMultipliers(PokedexData Pokedex)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < Pokedex.pokemon.Length - 1; i++)
            {
                Pokedex.pokemon[i].damageMultiplier = FBG_DmgMult.createMultiplier(Pokedex.pokemon[i].types);
            }
            sw.Stop();
            //print("time to create dmg multipliers in ms: " + sw.ElapsedMilliseconds);
        }

        private static void debugJson(PokedexData Pokedex)
        {
            //int index = 0;
            //print(json.pokemon[0].name);
            for (int i = 0; i < Pokedex.pokemon.Length; i++)
            {
                UnityEngine.Debug.Log(string.Format("{3} {0} {1} {2}", Pokedex.pokemon[i].name, Pokedex.pokemon[i].num, Pokedex.pokemon[i].baseStats.atk, i));
            }
            //print(index);
        }

        private static TextAsset loadTextFile()
        {
            object o;
            TextAsset t;
            string path = "JSON/PokeDexJSON";
            o = Resources.Load(path);
            if(o == null)
            {
                UnityEngine.Debug.Log("JSON NOT FOUND");
            }
            t = o as TextAsset;
            return t;
        }
    }

    public class corePokemonData
    {
        public string name;
        public int id;
        public baseStatsJson baseStats;
        public dmgMult damageMultiplier;
        public string type1;
        public string type2;

        public corePokemonData(PokemonJsonData p)
        {
            name = p.name;
            id = p.num;
            baseStats = p.baseStats;
            damageMultiplier = p.damageMultiplier;
            type1 = p.types[0];
            if(p.types.Length  > 1)
            {
                type2 = p.types[1];
            }else
            {
                type2 = null;
            }
        }

    }

    [Serializable]
    public class PokedexData
    {
        public PokemonJsonData[] pokemon;
    }

    [Serializable]
    public class PokemonJsonData
    {
        public string name;
        public int num;
        public string species;
        public string baseSpecies;
        public string forme;
        public string formeLetter;
        public string[] types;
        public string gender;
        public genderRatioJson genderRatio;
        public baseStatsJson baseStats;
        public string[] abilities;
        public float height;   //in meters
        public float weight;   //in KG
        public string color;
        public string[] evos;
        public string prevo;
        public int evoLevel;
        public string evoMove;
        public string[] eggGroups;
        public string[] otherFormes;
        public dmgMult damageMultiplier;
    }

    [Serializable]
    public class genderRatioJson
    {
        public float M;
        public float F;
    }

    [Serializable]
    public class baseStatsJson
    {
        public int hp;
        public int atk;
        public int def;
        public int spa;
        public int spd;
        public int spe;
    }
}

