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
            //sw.Stop();
            //print("time to create json in ms: " + sw.ElapsedMilliseconds);
            return p;
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
            int index = 0;
            //print(json.pokemon[0].name);
            for (int i = 0; i < Pokedex.pokemon.Length; i++)
            {
                index++;
            }
            //print(index);
        }

        private static TextAsset loadTextFile()
        {
            object o;
            TextAsset t;
            string path = "JSON/PokeDexJSON.json";
            o = Resources.Load(path);
            t = o as TextAsset;
            return t;
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
            public baseStatsJson baseStates;
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
}
