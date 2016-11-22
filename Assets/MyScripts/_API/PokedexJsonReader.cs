using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

public class PokedexJsonReader : MonoBehaviour
{

    public TextAsset jsonFile;
    private PokedexData json;
    private PokemonDamageMultipliers pdm;

    // Use this for initialization
    void Awake()
    {
        createPokeDex();
        pdm = this.GetComponent<PokemonDamageMultipliers>();
        createDamageMultipliers();
    }

    void Start()
    {
        debugJson();
    }

    private void createPokeDex()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        json = JsonUtility.FromJson<PokedexData>(jsonFile.text);
        sw.Stop();
        print("time to create json in ms: " + sw.ElapsedMilliseconds);
    }

    private void createDamageMultipliers()
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        for (int i = 0; i < json.pokemon.Length -1; i++)
        {
            json.pokemon[i].damageMultiplier = pdm.createMultiplier(json.pokemon[i].types);
        }
        sw.Stop();
        print("time to create dmg multipliers in ms: " + sw.ElapsedMilliseconds);
    }

    private void debugJson()
    {
        int index = 0;
        print(json.pokemon[0].name);
        for(int i = 0; i < json.pokemon.Length; i++)
        {
            index++;
        }
        print(index);
    }

    [Serializable]
    private class PokedexData
    {
        public PokemonJsonData[] pokemon;
    }

    [Serializable]
    private class PokemonJsonData
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
    private class genderRatioJson
    {
        public float M;
        public float F;
    }

    [Serializable]
    private class baseStatsJson
    {
        public int hp;
        public int atk;
        public int def;
        public int spa;
        public int spd;
        public int spe;
    }
}
