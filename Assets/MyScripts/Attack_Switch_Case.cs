﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack_Switch_Case : MonoBehaviour {

    public float final_damage;
    public float final_heal;

    public bool isPlayerStunned;
    public bool isEnemyStunned;

    private PokemonCreatorBack playerStats;
    private PokemonCreatorFront enemyStats;
    private AttackDamageCalc attackCalc;
    private GenerateAttacks genAttacks;
    private PokemonAttacks attacks;

    private string defense = "defense";
    private string attack = "attack";
    private string spAttack = "spAttack";
    private string spDefense = "spDefense";
    private string speed = "speed";


    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
        playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        attackCalc = GameObject.FindGameObjectWithTag("Attacks").GetComponent<AttackDamageCalc>();
        genAttacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
        attacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
    }

    public void statusAttacks(string name, bool isPlayer)
    {
        string tempname = name.ToLower();
        int rnd;
        switch (tempname)
        {
            default:
                Debug.Log("No attack with name " + name + " found");
                break;
            case "acid armor":
                changeStats(defense, 2, isPlayer);
                break;
            case "agility":
                changeStats(speed, 2, isPlayer);
                break;
            case "amnesia":
                changeStats(spDefense, 2, isPlayer);
                break;
            case "barrier":
                changeStats(defense, 2, isPlayer);
                break;
            case "confuse ray":
                rnd = Random.Range(1, 4);
                //then we cause the pokemon to be confused
                break;
            case "conversion":
                conversion(isPlayer, name);                
                break;
            case "defense curl":
                changeStats(defense, 1, isPlayer);
                break;
            case "disable":
                //disables the enemies last move
                break;
            
        }
    }

    public void physicalAttacks(string name, float predictedDamage, bool isPlayer)
    {
        name = name.ToLower();
        bool stunHit = false;
        int rnd;
        switch (name)
        {
            default:
                Debug.Log("No attack with name " + name + " found");
                break;
            case "barrage":
                rnd = Random.Range(2, 5);
                predictedDamage = stackAttacks(predictedDamage, rnd);
                break;
            case "bide":
                //waits 2 turns then deals back double.... :(
                break;
            case "bind":
                //need to create a damage over time effect here for rndBind turns
                rnd = Random.Range(4, 5);
                if (isPlayer)
                {
                    predictedDamage = enemyStats.maxHP / 16f;
                }
                else
                {
                    predictedDamage = playerStats.maxHP / 16f;
                }
                predictedDamage = Mathf.Round(predictedDamage);
                break;
            case "bite":
                stunHit = stunProbability(3);
                break;
            case "body slam":
                stunHit = stunProbability(3);
                break;
            case "bone club":
                //if the enemy hasnt attacked yet
                stunHit = stunProbability(1);
                break;
            case "bonemerang":
                rnd = 2;
                predictedDamage = stackAttacks(predictedDamage, rnd);
                break;
            case "clamp":
                rnd = Random.Range(4, 5);
                if (isPlayer)
                {
                    predictedDamage = enemyStats.maxHP / 16f;
                }
                else
                {
                    predictedDamage = playerStats.maxHP / 16f;
                }
                predictedDamage = Mathf.Round(predictedDamage);
                break;
            case "comet punch":
                rnd = Random.Range(2, 5);
                predictedDamage = stackAttacks(predictedDamage, rnd);
                break;
            case "constrict":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "crabhammer":
                //has a 1/8 crit ratio not a 1/16.... have to recalculate for this
                break;
            case "cut": //attack without modifiers
                break;
            case "dig":
                //user goes underground for 1 turn then deals the damage
                break;
            case "dizzy punch":
                stunHit = stunProbability(2);
                if (stunHit)
                {
                    if (isPlayer)
                        enemyStats.isConfused = true;
                    if (!isPlayer)
                        playerStats.isConfused = true;
                }
                break;
        }
    }

    public void specialAttacks(string name, float predictedDamage, bool isPlayer)
    {
        name = name.ToLower();
        final_heal = 0;
        bool stunHit = false;
        switch (name)
        {
            default:
                Debug.Log("No attack with name " + name + " found");
                break;
            case "absorb":
                final_heal = predictedDamage / 2f;
                break;
            case "acid":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(spDefense, -1, !isPlayer);
                }
                break;
            case "aurora beam":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(attack, -1, !isPlayer);
                }
                break;
            case "blizzard":
                stunHit = stunProbability(1);
                break;
            case "bubble":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "bubble beam":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "confusion":
                stunHit = stunProbability(1);
                //confuse the target if true
                break;
            
        }
        Debug.Log("Effect hit = " + stunHit);
    }


    /// <summary>
    /// Takes in the probability of getting a stun for a move out of ten, then makes a list of that many unique random numbers
    /// if the guess is any of those unique random numbers the method returns true, otherwise the attack did not stun the 
    /// enemy pokemon
    /// </summary>
    private bool stunProbability(int prob)
    {
        bool stunHit = false;
        List<int> probability = new List<int>();
        for (int i = 0; i < prob; i++)
        {
            int chance = Mathf.RoundToInt(Random.Range(1f, 10f));
            while (probability.Contains(chance))
            {
                chance = Mathf.RoundToInt(Random.Range(1f, 10f));
            }
            probability.Add(chance);
        }

        int guess = Mathf.RoundToInt(Random.Range(1f, 10f));        //Gets our guess, a random integer between 1 and 10

        for (int i = 0; i < probability.Count; i++)
        {
            if (guess == probability[i])
            {
                stunHit = true;
            }
        }

        return stunHit;
    }

    private void changeStats(string type, int stageMod, bool isPlayer)
    {
        int newStage = getStatStage(type, isPlayer);
        newStage += stageMod;

        if (newStage > 6)
            newStage = 6;
        if (newStage < -6)
            newStage = -6;

        setStatStage(type, newStage, isPlayer);
        float multiplier = stageToMultiplier(newStage);
        updateStatChange(type, multiplier, isPlayer);

    }

    private int getStatStage(string type, bool isPlayer)
    {
        int statStage = 0;
        if (isPlayer)
        {
            switch (type)
            {
                case "attack":
                    statStage = playerStats.attack_Stage;
                    break;
                case "spAttack":
                    statStage = playerStats.spAttack_Stage;
                    break;
                case "defense":
                    statStage = playerStats.defense_Stage;
                    break;
                case "spDefense":
                    statStage = playerStats.spDefense_stage;
                    break;
                case "speed":
                    statStage = playerStats.speed_stage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case "attack":
                    statStage = enemyStats.attack_Stage;
                    break;
                case "spAttack":
                    statStage = enemyStats.spAttack_Stage;
                    break;
                case "defense":
                    statStage = enemyStats.defense_Stage;
                    break;
                case "spDefense":
                    statStage = enemyStats.spDefense_stage;
                    break;
                case "speed":
                    statStage = enemyStats.speed_stage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }
        }
        return statStage;
    }

    private void setStatStage(string type, int newStage, bool isPlayer)
    {
        if (isPlayer)
        {
            switch (type)
            {
                case "attack":
                    playerStats.attack_Stage = newStage;
                    break;
                case "spAttack":
                    playerStats.spAttack_Stage = newStage;
                    break;
                case "defense":
                    playerStats.defense_Stage = newStage;
                    break;
                case "spDefense":
                    playerStats.spDefense_stage = newStage;
                    break;
                case "speed":
                    playerStats.speed_stage = newStage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }
        }
        else
        {
            switch (type)
            {
                case "attack":
                    enemyStats.attack_Stage = newStage;
                    break;
                case "spAttack":
                    enemyStats.spAttack_Stage = newStage;
                    break;
                case "defense":
                    enemyStats.defense_Stage = newStage;
                    break;
                case "spDefense":
                    enemyStats.spDefense_stage = newStage;
                    break;
                case "speed":
                    enemyStats.speed_stage = newStage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }
        }
    }

    private void updateStatChange(string type, float multiplier, bool isPlayer)
    {
        if (isPlayer)
        {
            playerStats.updateStatStage(type, multiplier);
        }
        else
        {
            enemyStats.updateStatStage(type, multiplier);
        }
    }

    private float stageToMultiplier(int stage)
    {

        float multiplier = 1;
        switch (stage)
        {
            case -6:
                multiplier = .25f;
                break;
            case -5:
                multiplier = .285f;
                break;
            case -4:
                multiplier = .33f;
                break;
            case -3:
                multiplier = .4f;
                break;
            case -2:
                multiplier = .5f;
                break;
            case -1:
                multiplier = .66f;
                break;
            case 0:
                multiplier = 1;
                break;
            case 1:
                multiplier = 1.5f;
                break;
            case 2:
                multiplier = 2f;
                break;
            case 3:
                multiplier = 2.5f;
                break;
            case 4:
                multiplier = 3f;
                break;
            case 5:
                multiplier = 3.5f;
                break;
            case 6:
                multiplier = 4f;
                break;
        }
        return multiplier;
    }

    /// <summary>
    /// this method takes in the amount of times the attack gets calculated so that the total damage accounts for 
    /// each attack as its own sperate attack rather than multiplying by the number
    /// </summary>
    private float stackAttacks(float predictedDamage, int rnd)
    {
        predictedDamage = 0;
        for(int i = 0; i < rnd; i++)
        {
            predictedDamage += attackCalc.calculateDamage();
        }

        return predictedDamage;
    }

    private void conversion(bool isPlayer, string name)
    {
        if (isPlayer)
        {
            string tempName = genAttacks.playerAttackName[0];
            int attack_index = attackCalc.getAttackListIndex(name);
            string attack_type = attacks.attackList[attack_index].type;
            playerStats.Type1 = attack_type;
        }
        else
        {
            string tempName = genAttacks.playerAttackName[0];
            int attack_index = attackCalc.getAttackListIndex(name);
            string attack_type = attacks.attackList[attack_index].type;
            playerStats.Type1 = attack_type;
        }
    }

}
