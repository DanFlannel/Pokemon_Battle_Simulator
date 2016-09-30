using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class PokedexJsonReader : MonoBehaviour
{

    public TextAsset jsonFile;
    private object json;
    private

    // Use this for initialization
    void Start()
    {
        readJson();
    }

    void readJson()
    {
        json = JsonUtility.FromJson<JsonHolder>(jsonFile.text);
    }

    private class JsonHolder
    {
        JsonItem[] pokemon;
    }

    private class JsonItem
    {
        string name;
        JsonStats stats;
    }

    private class JsonStats
    {
        int num;
        string species;
        string baseSpecies;
        JsonStatsSub1 types;
        float[] genderRatio;
        JsonStatsSub2 subStats;
        string[] abilities;
        float height;
        float weight;
        string color;
        string[] evos;
        string[] eggGroups;

    }

    private class JsonStatsSub1
    {
        string type1;
        string type2;
    }

    private class JsonStatsSub2
    {
        int hp;
        int atk;
        int def;
        int spa;
        int spd;
        int spe;
    }
}
