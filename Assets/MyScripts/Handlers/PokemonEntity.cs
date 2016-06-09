using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

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

    //gotta get these generated.....
    public string Attack1 { get; private set; }
    public string Attack2 { get; private set; }
    public string Attack3 { get; private set; }
    public string Attack4 { get; private set; }

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
    //public bool hasSubstitute { get; set; }
    //public bool hasLightScreen { get; set; }

    public float cachedDamage { get; set; }

    public int sleepDuration { get; set; }
    public int confusedDuration { get; set; }


    public int curHp { get; set; }
    public int maxHP { get; private set; }

    public PokemonEntity(string m_Name, int m_ID, int m_Level, int m_baseHP, int m_baseAttack, int m_baseDefense,
        int m_baseSpAttack, int m_baseSpDef, int m_baseSpeed, string m_Type1, string m_Type2, 
        List<attackIndex> attackMoves)
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
        isBurned = isFrozen = isFlying = isParalized = false;

        cachedDamage = sleepDuration = confusedDuration = 0;

        canAttack = canBeAttacked = true;

        levelBonus = 0;



        //need to set these to something before updaing them
        Attack1 = Attack2 = Attack3 = Attack4 = "";
        maxHP = curHp = Attack = Defense = Special_Attack = Special_Defense = Speed = 0;
        generatePokemonStats(Level);
        generateAttacks(attackMoves);

    }

    /// <summary>
    /// Generates all the inital stats for the pokemon
    /// </summary>
    /// <param name="Level">Takes in the Pokemon's level</param>
    private void generatePokemonStats(int Level)
    {
        Debug.LogWarning("Called Generate Pokemon Stats Level: " + Level);
        //max hp = 2* base stat + 110
        //max other stats = 1.79 * stat + 5(levelBonus)
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

    private void generateAttacks(List<attackIndex> attackMoves)
    {
        List<int> randomNumbers = generateRandomList(attackMoves);
        SetAttacks(attackMoves, randomNumbers);
    }

    private List<int> generateRandomList(List<attackIndex> attackMoves)
    {
        int totalPossibleMoves = attackMoves.Count;
        List<int> rndNumberList = new List<int>();
        //this is supposed to be a const
        int MOVES = 4;

        Debug.Log("Range: " + totalPossibleMoves);
        //Debug.Log("List Cout: " + list.Count);
        int numToAdd = -1;
        //if the pokemon has more than 4 moves that it can learn, then we pick from those randomly
        if (totalPossibleMoves > MOVES)
        {
            for (int i = 0; i < MOVES; i++)
            {
                numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                while (rndNumberList.Contains(numToAdd))
                {
                    numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                }
                rndNumberList.Add(numToAdd);
            }
        }
        //this ensures that all possible moves are added for pokemon with less than or equal to 4 moves
        else
        {
            totalPossibleMoves -= 1;
            int totalMoves = 0;
            for (int i = 0; i < MOVES; i++)
            {
                if (i <= totalPossibleMoves)
                {
                    numToAdd = i;
                    totalMoves++;
                }
                else
                {
                    numToAdd = UnityEngine.Random.Range(0, totalMoves);
                }
                rndNumberList.Add(numToAdd);
            }
        }
        Debug.Log("Generated Attack List");
        return rndNumberList;
    }

    //We will see if we can do something about that hardcoding...
    private void SetAttacks(List<attackIndex> attackMoves, List<int> rndNums)
    {
        Attack1 = attackMoves[rndNums[0]].attack.name;
        Attack2 = attackMoves[rndNums[1]].attack.name;
        Attack3 = attackMoves[rndNums[2]].attack.name;
        Attack4 = attackMoves[rndNums[3]].attack.name;
    }
}
