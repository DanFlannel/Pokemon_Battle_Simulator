﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerPokemonHandler : MonoBehaviour
{
    PokemonEntity testPokemon;
    List<PokemonEntity> playerTeam = new List<PokemonEntity>();
    private int curPlayerPokemonIndex;
    private const int TEAMLENGTH = 6;

    public int PokemonID;
    public string PokemonName;
    public string attack1;
    public string attack2;
    public string attack3;
    public string attack4;

    private int baseHP;
    private int baseAttack;
    private int baseDefense;
    private int baseSpecial_Attack;
    private int baseSpecial_Defense;
    private int baseSpeed;
    private int levelBonus;

    private bool CanEvolve; //not sure the use of this but its there

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
    private GUIScript gui;
    private PokemonAttacks attackData;

    private int tempID;
    public int curHp;
    public int maxHP;
    private int GifID;

    private bool hasloaded = false;


    // Use this for initialization
    void Start()
    {
        tempID = Random.Range(0, 151);
        attackData = GameObject.FindGameObjectWithTag("AttackData").GetComponent<PokemonAttacks>();
        pl = GameObject.FindGameObjectWithTag("Library").GetComponent<PokemonLibrary>();
        gif = this.GetComponent<GifRenderer>();
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        for (int i = 0; i < TEAMLENGTH; i++)
        {
            Level = 100;
            tempID = Random.Range(0, 151);
            FetchPokemonStats(tempID);
            CreatePokemonStruct();
            DebugPokemonStruct();
        }
        OnChangePokemon(0);
        // Debug.Log("Scene has now loaded with Player: " + PokemonName);
    }

    private void FetchPokemonStats(int id)
    {
        PokemonName = pl.GetName(id);
        PokemonID = id;
        GifID = PokemonID + 1;

        //gif.ChangeSprite(PokemonName, GifID);

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
            baseDefense, baseSpecial_Attack, baseSpecial_Defense, baseSpeed, Type1, Type2,
            attackData.masterGetAttacks(PokemonID));
        playerTeam.Add(testPokemon);
    }

    private void DebugPokemonStruct()
    {
        Debug.Log(string.Format("Name {0} ID {1} MaxHP {2} Attack {3} Defense {4} SP Attack {5}",
            testPokemon.Name, testPokemon.ID, testPokemon.maxHP, testPokemon.Attack,
            testPokemon.Defense, testPokemon.Special_Attack));
        Debug.Log(string.Format("Attack 1: {0} Attack 2: {1} Attack 3: {2} Attack 4: {3}",
            testPokemon.Attack1, testPokemon.Attack2, testPokemon.Attack3, testPokemon.Attack4));
    }

    /// <summary>
    /// Changes the pokemon stats and attacks to the one that is currently out
    /// </summary>
    /// <param name="index">index number in the team</param>
    private void OnChangePokemon(int index)
    {
        //update the stats first so that the ID and Name are right before calling
        //to change the sprite
        UpdateStats(playerTeam[index]);
        GifID = PokemonID + 1;
        gif.ChangeSprite(PokemonName, GifID);
        gui.UpdatePlayerInfo();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateStats(PokemonEntity pk)
    {
        PokemonID = pk.ID;
        PokemonName = pk.Name;
        Level = pk.Level;

        curHp = pk.curHp;
        maxHP = pk.maxHP;

        Type1 = pk.Type1;
        Type2 = pk.Type2;

        attack1 = pk.Attack1;
        attack2 = pk.Attack2;
        attack3 = pk.Attack3;
        attack4 = pk.Attack4;

        //Ensure that we don't mess up the base stats here

        Attack = pk.Attack;
        Defense = pk.Defense;
        Special_Attack = pk.Special_Attack;
        Special_Defense = pk.Special_Defense;
        Speed = pk.Speed;

        attack_Stage = pk.attack_Stage;
        defense_Stage = pk.defense_Stage;
        spAttack_Stage = pk.spAttack_Stage;
        spDefense_stage = pk.spDefense_stage;
        speed_stage = pk.speed_stage;

        isChargingAttack = false;
        isUnderground = false;
        //canAttack = true;
        canBeAttacked = true;
        hasAttacked = false;

        isConfused = pk.isConfused;
        isSleeping = pk.isSleeping;
        isStunned = pk.isStunned;
        isFlinched = false;
        isBurned = pk.isBurned;
        isFlying = false;
        isParalized = pk.isParalized;

        sleepDuration = pk.sleepDuration;
        confusedDuration = pk.confusedDuration;
    }

    /// <summary>
    /// Just a test case for swapping pokemon and making sure that they change 
    /// the proper variables
    /// </summary>
    public void SwapPlayerPokemon()
    {

        if (curPlayerPokemonIndex < playerTeam.Count -1)
        {
            curPlayerPokemonIndex++;
        }
        else
        {
            curPlayerPokemonIndex = 0;
        }

        OnChangePokemon(curPlayerPokemonIndex);
    }

}