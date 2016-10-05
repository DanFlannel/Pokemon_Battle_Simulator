using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PokedexJsonReader : MonoBehaviour
{

    public TextAsset jsonFile;
    private PokedexJson json;

    // Use this for initialization
    void Awake()
    {
        createPokeDex();
    }

    void createPokeDex()
    {
        json = JsonUtility.FromJson<PokedexJson>(jsonFile.text);
    }

    private class PokedexJson
    {
        PokemonJsonData[] pokemon;
    }

    private class PokemonJsonData
    {
        string name;
        int num;
        string species;
        string baseSpecies;
        string forme;
        string formeLetter;
        string[] types;
        string gender;
        genderRatioJson genderRatio;
        baseStatsJson baseStates;
        string[] abilities;
        float height;   //in meters
        float weight;   //in KG
        string color;
        string[] evos;
        string prevo;
        int evoLevel;
        string evoMove;
        string[] eggGroups;
        string[] otherFormes;
    }

    private class genderRatioJson
    {
        float M;
        float F;
    }

    private class baseStatsJson
    {
        int hp;
        int atk;
        int def;
        int spa;
        int spd;
        int spe;
    }
}
