using UnityEngine;
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
    public int ID, Level;

    public string Name;

    public int
        baseHP, baseAttack, baseDefense, baseSpecial_Attack,
        baseSpecial_Defense, baseSpeed;
    //******************************************************//
    public int curHp, maxHP, levelBonus;

    public int
        Attack, attackStage, Defense, defenseStage,
        Special_Attack, spAttack_stage, Special_Defense,
        spDefense_stage, Speed, speed_stage;

    public string
        Type1, Type2;

    public bool
        isCharingAttack, isUnderground, canAttack, canBeAttacked,
        isConfused, isSleepting, hasAttacked, isStunned, isFlinched,
        isBurned, isFrozen, isFlying, isParalized, hasSubstitute,
        hasLightScreen;

    public float cachedDamage;

    public int
            sleepDuration, confusedDuration;

    /// <summary>
    /// our class initalizer that generates its final stats based on its base stats and level
    /// </summary>
    /// <param name="m_Name">Name of the pokemon to be generated</param>
    /// <param name="m_ID">ID or Pokedex index of the pokemon</param>
    /// <param name="m_Level">Level of the pokemon</param>
    /// <param name="m_baseHP">Base HP of the pokemon</param>
    /// <param name="m_baseAttack">Base attack of the pokemon</param>
    /// <param name="m_baseDefense">Base defense of the pokemon</param>
    /// <param name="m_baseSpAttack">Base special defense of the pokemon</param>
    /// <param name="m_baseSpDef">Base special defense of the pokemon</param>
    /// <param name="m_baseSpeed">Base speed of the pokemon</param>
    /// <param name="m_Type1">Type 1 of the pokemon</param>
    /// <param name="m_Type2">Type 2 of the pokemon</param>
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

        attackStage = defenseStage = spAttack_stage = spDefense_stage = speed_stage = 0;

        isCharingAttack = isUnderground = canBeAttacked =
        isConfused = isSleepting = hasAttacked = isStunned = isFlinched =
        isBurned = isFrozen = isFlying = isParalized = hasSubstitute =
        hasLightScreen = false;

        cachedDamage = sleepDuration = confusedDuration = 0;

        canAttack = canBeAttacked = true;

        levelBonus = 0;
        maxHP = curHp = Attack = Defense = Special_Attack = Special_Defense = Speed = 0;

        GeneratePokemonStats(Level);
    }

    /// <summary>
    /// Generates all the inital stats for the pokemon based off the base stats and level
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
