﻿using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Creating a structure to use for our pokemon so that we can use
/// a list to create multiple pokemon
/// I didn't use a class because I am more comfotable with lists
/// Also I am using a class to handle the current pokemon
/// </summary>
public struct PokemonEntity
{
    //*****************************************************//
    //these are the only variables that can be passed in
    //in order to generate the rest of the stats for the pokemon


    public int ID { get; private set; }
    public string Name { get; private set; }
    public int Level { get; private set; }

    //made these private to protect them
    private int baseHP { get; set; }
    private int baseAttack { get; set; }
    private int baseDefense { get; set; }
    private int baseSpecial_Attack { get; set; }
    private int baseSpecial_Defense { get; set; }
    private int baseSpeed { get; set; }

    public string Type1 { get; private set; }
    public string Type2 { get; private set; }
    //*******************************************************//

    //private bool CanEvolve { get; set; }
    private int levelBonus { get; set; }

    //using properties to protect how these variables are set
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Special_Attack { get; private set; }
    public int Special_Defense { get; private set; }
    public int Speed { get; private set; }

    public int attack_Stage { get; set; }
    public int defense_Stage { get; set; }
    public int spAttack_Stage { get; set; }
    public int spDefense_stage { get; set; }
    public int speed_stage { get; set; }

    public bool isChargingAttack { get; set; }
    public bool isUnderground { get; set; }
    public bool canAttack { get; set; }
    public bool canBeAttacked { get; set; }
    public bool isConfused { get; set; }
    public bool isSleeping { get; set; }
    public bool hasAttacked { get; set; }
    public bool isStunned { get; set; }
    public bool isFlinched { get; set; }
    public bool isBurned { get; set; }
    public bool isFrozen { get; set; }
    public bool isFlying { get; set; }
    public bool isParalized { get; set; }
    public bool hasSubstitute { get; set; }
    public bool hasLightScreen { get; set; }

    public float cachedDamage { get; set; }

    public int sleepDuration { get; set; }
    public int confusedDuration { get; set; }


    public int curHp { get; set; }
    public int maxHP { get; private set; }

    public PokemonEntity(string m_Name, int m_ID, int m_Level, int m_baseHP, int m_baseAttack, int m_baseDefense,
        int m_baseSpAttack, int m_baseSpDef, int m_baseSpeed, string m_Type1, string m_Type2)
    {
        Name = m_Name;
        ID = m_ID;
        Level = m_Level;

        baseHP = m_baseHP;
        baseAttack = m_baseAttack;
        baseDefense = m_baseDefense;
        baseSpecial_Attack = m_baseSpAttack;
        baseSpecial_Defense = m_baseSpDef;
        baseSpeed = m_baseSpeed;

        Type1 = m_Type1;
        Type2 = m_Type2;

        attack_Stage = defense_Stage = spAttack_Stage = spDefense_stage = speed_stage = 0;

        isChargingAttack = isUnderground = canBeAttacked =
        isConfused = isSleeping = hasAttacked = isStunned = isFlinched =
        isBurned = isFrozen = isFlying = isParalized = hasSubstitute =
        hasLightScreen = false;

        cachedDamage = sleepDuration = confusedDuration = 0;

        canAttack = canBeAttacked = true;

        levelBonus = 0;
        maxHP = curHp = Attack = Defense = Special_Attack = Special_Defense = Speed = 0;

        //GeneratePokemonStats(Level);
    }

    /// <summary>
    /// Generates all the inital stats for the pokemon
    /// </summary>
    /// <param name="Level">Takes in the Pokemon's level</param>
    private void GeneratePokemonStats(int Level)
    {

        //max hp = 2* base stat + 110
        //max other stats = 1.79* stat + 5(levelBonus)
        //level bonus cannot exceed 5
        levelBonus = Level / (int)UnityEngine.Random.Range(16f, 20f); //level bonus is between 17 and 20 to add some slight variation to the maximum base stats

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
}
