﻿using UnityEngine;
using System.Collections;

public class PlayerPokemonHandler : MonoBehaviour
{
    PokemonEntity testPokemon;
    private int levelBonus;

    public int PokemonID;
    public string PokemonName;

    private int baseHP;
    private int baseAttack;
    private int baseDefense;
    private int baseSpecial_Attack;
    private int baseSpecial_Defense;
    private int baseSpeed;
    private bool CanEvolve;

    public int Level;

    public int Attack;
    public int attack_Stage = 0;
    public int Defense;
    public int defense_Stage = 0;
    public int Special_Attack;
    public int spAttack_Stage = 0;
    public int Special_Defense;
    public int spDefense_stage = 0;
    public int Speed;
    public int speed_stage = 0;

    public string Type1;
    public string Type2;

    public bool isChargingAttack = false;
    public bool isUnderground = false;
    public bool canAttack = true;
    public bool canBeAttacked = true;
    public bool isConfused = false;
    public bool isSleeping = false;
    public bool hasAttacked = false;
    public bool isStunned = false;
    public bool isFlinched = false;
    public bool isBurned = false;
    public bool isFrozen = false;
    public bool isFlying = false;
    public bool isParalized = false;

    public float cachedDamage = 0;

    public bool hasSubstitute = false;
    public bool hasLightScreen = false;
    public int lightScreenDuration = 0;
    public int substitueHP = 0;

    public int sleepDuration = 0;
    public int confusedDuration = 0;

    public string cachedAttackName;

    private PokemonLibrary pl;
    private GifRenderer gif;

    private int tempID;
    public int curHp;
    public int maxHP;
    private int GifID;

    private bool hasloaded = false;


    // Use this for initialization
    void Start()
    {
        tempID = Random.Range(0, 151);
        pl = GameObject.FindGameObjectWithTag("Library").GetComponent<PokemonLibrary>();
        gif = this.GetComponent<GifRenderer>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        Level = 100;
        LoadPokemon(tempID, Level);
        CreatePokemonStruct();
        DebugPokemonStruct();
       // Debug.Log("Scene has now loaded with Player: " + PokemonName);
    }

    private void LoadPokemon(int id, int level)
    {
        FetchPokemonStats(id);
        GeneratePokemonStats(level);
    }

    private void FetchPokemonStats(int id)
    {
        PokemonName = pl.GetName(id);
        PokemonID = id;
        GifID = PokemonID + 1;

        gif.ChangeSprite(PokemonName, GifID);

        baseHP = pl.GetHP(id);
        baseAttack = pl.GetAttack(id);
        baseDefense = pl.GetDefense(id);
        baseSpecial_Attack = pl.GetSpecialAttack(id);
        baseSpecial_Defense = pl.GetSpecialDefense(id);
        baseSpeed = pl.GetSpeed(id);
        CanEvolve = pl.GetCanEvolve(id);
        Type1 = pl.GetType1(id);
        Type2 = pl.GetType2(id);
    }

    private void GeneratePokemonStats(int Level)
    {

        //max hp = 2* base stat + 110
        //max other stats = 1.79* stat + 5(levelBonus)
        //level bonus cannot exceed 5
        levelBonus = Level / (int)(Random.Range(16f, 20f) + 1); //level bonus is between 17 and 20 to add some slight variation to the maximum base stats

        float hpLevelCalc = 1f + ((float)Level / 100);
        float levelCalc = .79f + ((float)Level / 100);

        float attackCalc = (float)baseAttack * levelCalc;
        Attack = (int)attackCalc + levelBonus;

        float defenseCalc = (float)baseDefense * levelCalc;
        Defense = (int)defenseCalc + levelBonus;

        float spaBonus = (float)baseSpecial_Attack * levelCalc;
        Special_Attack = (int)spaBonus + levelBonus;

        float spdBonus = (float)baseSpecial_Defense * levelCalc;
        Special_Defense = (int)spdBonus + levelBonus;

        float spBonus = (float)baseSpeed * levelCalc;
        Speed = (int)spBonus + levelBonus;

        float hpBonus = (float)baseHP * hpLevelCalc;
        float hpLevelBonus = 110f * (float)Level / 100f;
        maxHP = (int)hpBonus + (int)hpLevelBonus;
        curHp = maxHP;
    }

    public void updateStatStage(string type, float multiplier)
    {
        float levelCalc = .79f + ((float)Level / 100);
        switch (type)
        {
            case "attack":
                float attackCalc = (float)baseAttack * levelCalc;
                Attack = (int)attackCalc + levelBonus;
                Attack = (int)(Attack * multiplier);
                break;
            case "defense":
                float defenseCalc = (float)baseDefense * levelCalc;
                Defense = (int)defenseCalc + levelBonus;
                Defense = (int)(Defense * multiplier);
                break;
            case "spAttack":
                float spaBonus = (float)baseSpecial_Attack * levelCalc;
                Special_Attack = (int)spaBonus + levelBonus;
                Special_Attack = (int)(Special_Attack * multiplier);
                break;
            case "spDefense":
                float spdBonus = (float)baseSpecial_Defense * levelCalc;
                Special_Defense = (int)spdBonus + levelBonus;
                Special_Defense = (int)(Special_Defense * multiplier);
                break;
            case "speed":
                float spBonus = (float)baseSpeed * levelCalc;
                Speed = (int)spBonus + levelBonus;
                Speed = (int)(Speed * multiplier);
                break;
            default:
                Debug.Log("no type " + type + " found");
                break;
        }
    }

    private void CreatePokemonStruct()
    {
        testPokemon = new PokemonEntity(PokemonName, PokemonID, Level, baseHP, baseAttack,
            baseDefense, baseSpecial_Attack, baseSpecial_Defense, baseSpeed, Type1, Type2);
    }

    private void DebugPokemonStruct()
    {
        Debug.Log(string.Format("Name {0} ID {1} MaxHP {2} Attack {3} Defense {4} SP Attack {5}", 
            testPokemon.Name, testPokemon.ID, testPokemon.maxHP, testPokemon.Attack,
            testPokemon.Defense, testPokemon.Special_Attack));
    }

}