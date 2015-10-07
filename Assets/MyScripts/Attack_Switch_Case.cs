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
        Debug.Log("attack name: " + name);
        string tempname = name.ToLower();
        int rnd;
        switch (tempname)
        {
            default:
                Debug.Log("No status move with name " + name + " found");
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
            case "double team":
                //raises user evasive stage by one
                break;
            case "flash":
                //Do something
                break;
            case "focus energy":
                //increases crit ratio
                break;
            case "growl":
                changeStats(attack, -1, !isPlayer);
                break;
            case "growth":
                changeStats(spAttack, 1, isPlayer);
                changeStats(attack, 1, isPlayer);
                break;
            case "harden":
                changeStats(defense, 1, isPlayer);
                break;
            case "haze":
                updateStatChange(attack, 1, isPlayer);
                updateStatChange(defense, 1, isPlayer);
                updateStatChange(spAttack, 1, isPlayer);
                updateStatChange(spDefense, 1, isPlayer);

                updateStatChange(attack, 1, !isPlayer);
                updateStatChange(defense, 1, !isPlayer);
                updateStatChange(spAttack, 1, !isPlayer);
                updateStatChange(spDefense, 1, !isPlayer);
                break;
            case "hypnosis":
                rnd = Random.Range(1, 3);
                //puts the user to sleep for rnd turns
                break;
            case "kinesis":
                //lower enemy accuracy by 1 stage
                break;
            case "leech seed":
                //drain 1/8hp per turn at the end of the turn and restore it to the user
                break;
            case "leer":
                changeStats(defense, -1, !isPlayer);
                break;
            case "light screen":
                if (isPlayer)
                {
                    playerStats.hasLightScreen = true;
                    playerStats.lightScreenDuration = 5;
                }
                else
                {
                    enemyStats.hasLightScreen = true;
                    enemyStats.lightScreenDuration = 5;
                }
                break;
            case "lovely kiss":
                rnd = Random.Range(1, 3);
                //puts the user to sleep for rnd turns
                break;
            case "meditate":
                changeStats(attack, 1, isPlayer);
                break;

        }
        Debug.Log("Did a status move!");
    }

    public void physicalAttacks(string name, float predictedDamage, bool isPlayer)
    {
        Debug.Log("attack name: " + name);
        final_damage = 0;
        final_heal = 0;
        string tempname = name.ToLower();
        bool stunHit = false;
        int rnd;
        float recoil;
        switch (tempname)
        {
            default:
                Debug.Log("No physical attack with name " + name.ToLower() + " found");
                break;
            case "barrage":
                rnd = Random.Range(2, 5);
                predictedDamage = stackAttacks(predictedDamage, rnd, name);
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
                isFlinched(isPlayer, 3);
                break;
            case "body slam":
                isParalized(isPlayer, 3);
                break;
            case "bone club":
                isFlinched(isPlayer, 1);
                break;
            case "bonemerang":
                rnd = 2;
                predictedDamage = stackAttacks(predictedDamage, rnd, name);
                break;
            case "clamp":   //traps for 4-5 turns dealing 1/16th damage
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
                predictedDamage = stackAttacks(predictedDamage, rnd,name);
                break;
            case "constrict":
                stunHit = stunProbability(1);
                if (stunHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "counter":
                //hits back with 2x power iff is hit with physical attack
                break;
            case "crabhammer":
                //has a 1/8 crit ratio not a 1/16.... have to recalculate for this
                break;
            case "cut": //no additional effects
                break;
            case "dig":
                if (isPlayer)
                {
                    playerStats.isUnderground = true;
                    playerStats.cachedDamage = predictedDamage;
                }
                else
                {
                    enemyStats.isUnderground = true;
                    enemyStats.cachedDamage = predictedDamage;
                }
                predictedDamage = 0;
                break;
            case "dizzy punch":
                rnd = Random.Range(1, 4);
                isConfused(isPlayer, 2, rnd);
                break;
            case "double kick":
                rnd = 2;
                stackAttacks(predictedDamage, rnd,name);
                break;
            case "double slap":
                rnd = Random.Range(2, 5);
                stackAttacks(predictedDamage, rnd,name);
                break;
            case "double edge":
                recoil = predictedDamage / 3f;
                recoil = Mathf.Round(recoil);
                break;
            case "drill peck":  //no additional effects
                break;
            case "earthquake":
                predictedDamage = earthQuake(isPlayer, predictedDamage);
                break;
            case "egg bomb":   //no additional effects 
                break;
            case "explosion":       //causes user to faint
                if (isPlayer)
                    playerStats.curHp = 0;
                else
                    enemyStats.curHp = 0;
                break;
            case "fire punch":
                isBurned(isPlayer, 1);
                break;
            case "fissure":
                oneHitKO(isPlayer);
                break;
            case "fly":
                if (isPlayer)
                {
                    playerStats.isFlying = true;
                    playerStats.cachedDamage = predictedDamage;
                }
                else
                {
                    enemyStats.isFlying = true;
                    enemyStats.cachedDamage = predictedDamage;
                }
                predictedDamage = 0; 
                break;
            case "fury attack":
                rnd = Random.Range(2, 5);
                stackAttacks(predictedDamage, rnd,name);
                break;
            case "fury swipes":
                rnd = Random.Range(2, 5);
                stackAttacks(predictedDamage, rnd,name);
                break;
            case "guillotine":
                oneHitKO(isPlayer);
                break;
            case "headbutt":
                isFlinched(isPlayer, 3);
                break;
            case "high jump kick":
                //if this misses it casues 1/2 of the damage it would have inflicted on the user
                break;
            case "horn attack": //no additional effect
                break;
            case "horn drill":
                oneHitKO(isPlayer);
                break;
            case "hyper fang":
                isFlinched(isPlayer, 1);
                break;
            case "ice punch":
                isFrozen(isPlayer, 1);
                break;
            case "jump kick":
                //lose 1/2 hp is the user misses just like high jump kick
                break;
            case "karate chop":
                //high crit ratio 1/8 versus 1/16
                break;
            case "leech life":
                final_heal = Mathf.Round(predictedDamage / 2f);
                break;
            case "low kick":
                isFlinched(isPlayer, 3);
                break;

        }
        final_damage = predictedDamage;
        Debug.Log("final heal = " + final_heal);
        Debug.Log("final damage = " + final_damage);
        Debug.Log("Effect hit = " + stunHit);
    }

    public void specialAttacks(string name, float predictedDamage, bool isPlayer)
    {
        Debug.Log("attack name: " + name);
        final_damage = 0;
        final_heal = 0;
        string tempname = name.ToLower();
        int rnd;
        bool stunHit = false;
        switch (tempname)
        {
            default:
                Debug.Log("No special attack with name " + name + " found");
                break;
            case "absorb":
                final_heal = predictedDamage / 2f;
                break;
            case "acid":
                stunHit = stunProbability(1);
                if (stunHit)
                    changeStats(spDefense, -1, !isPlayer);
                break;
            case "aurora beam":
                stunHit = stunProbability(1);
                if (stunHit)
                    changeStats(attack, -1, !isPlayer);
                break;
            case "blizzard":
                isFrozen(isPlayer, 1);
                break;
            case "bubble":
                stunHit = stunProbability(1);
                if (stunHit)
                    changeStats(speed, -1, !isPlayer);
                break;
            case "bubble beam":
                stunHit = stunProbability(1);
                if (stunHit)
                    changeStats(speed, -1, !isPlayer);
                break;
            case "confusion":
                rnd = Random.Range(1, 4);
                isConfused(isPlayer, 1, rnd);
                break;
            case "dragon rage":
                predictedDamage = 40;
                break;
            case "dream eater":
                predictedDamage = dreamEater(isPlayer, predictedDamage);
                break;
            case "ember":
                isBurned(isPlayer, 1);
                break;
            case "fire blast":
                isBurned(isPlayer, 1);
                break;
            case "fire spin":
                rnd = Random.Range(4, 5);
                //burn for 1/16 for 4-5 turns
                break;
            case "flamethrower":
                isBurned(isPlayer, 1);
                break;
            case "gust":
                if (isPlayer)
                {
                    if (enemyStats.isFlying)
                        predictedDamage *= 2f;
                }
                else
                {
                    if(playerStats.isFlying)
                        predictedDamage *= 2f;
                }
                break;
            case "hydro pump":  //no additional effect
                break;
            case "hyper beam":  //cannot move next turn
                if (isPlayer)
                    playerStats.canAttack = false;
                else
                    enemyStats.canAttack = false;
                break;
            case "ice beam":
                isFrozen(isPlayer, 1);
                break;
            case "mega drain":
                final_heal = Mathf.Round(predictedDamage / 2f);
                break;
        }
        //Check for lightscreen to halve special attack damage
        final_damage = predictedDamage;
        Debug.Log("final heal = " + final_heal);
        Debug.Log("final damage = " + final_damage);
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
    private float stackAttacks(float predictedDamage, int rnd, string name)
    {
        Debug.Log("Final name check:" + name);
        predictedDamage = 0;
        for (int i = 0; i < rnd; i++)
        {
            predictedDamage += attackCalc.calculateDamage(name);
        }

        return predictedDamage;
    }

    private void isBurned(bool isPlayer, int prob)
    {
        bool stunHit = stunProbability(prob);
        if (stunHit)
        {
            if (isPlayer)
                enemyStats.isBurned = true;
            else
                playerStats.isBurned = true;
        }
    }

    private void isFrozen(bool isPlayer, int prob)
    {
        bool stunHit = stunProbability(prob);
        if (stunHit)
        {
            if (isPlayer)
            {
                if(enemyStats.Type1.ToLower() != "ice" && enemyStats.Type2.ToLower() != "ice")
                    enemyStats.isFrozen = true;
            }
            else
            {
                if (playerStats.Type1.ToLower() != "ice" && playerStats.Type2.ToLower() != "ice")
                    playerStats.isFrozen = true;
            }
        }
    }

    private void isConfused(bool isPlayer, int prob, int duration)
    {
        bool stunHit = stunProbability(prob);
        if (stunHit)
        {
            if (isPlayer)
            {
                enemyStats.isConfused = true;
                enemyStats.confusedDuration = duration;
            }
            else
            {
                playerStats.isConfused = true;
                playerStats.confusedDuration = duration;
            }
        }
    }

    private void isFlinched(bool isPlayer, int prob)
    {
        bool stunHit = stunProbability(prob);
        if (stunHit)
        {
            if (isPlayer)
            {
                if (!enemyStats.hasAttacked)
                {
                    enemyStats.isFlinched = true;
                }
            }
            else
            {
                if (!playerStats.hasAttacked)
                {
                    playerStats.isFlinched = true;
                }
            }
        }
    }

    private void isParalized(bool isPlayer, int prob) {
        bool stunHit = stunProbability(prob);
        if (stunHit)
        {
            if (isPlayer)
            {
                if (enemyStats.Type1.ToLower() != "electric" && enemyStats.Type2.ToLower() != "electric" && !enemyStats.hasSubstitute)
                {
                    enemyStats.isFrozen = true;
                    changeStats(speed, -6, !isPlayer);
                }
            }
            else
            {
                if (playerStats.Type1.ToLower() != "electric" && playerStats.Type2.ToLower() != "electric" && !playerStats.hasSubstitute)
                {
                    playerStats.isFrozen = true;
                    changeStats(speed, -6, isPlayer);
                }
            }
        }
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

    private float dreamEater(bool isPlayer, float predictedDamage)
    {

        if (isPlayer)
        {
            if (enemyStats.isSleeping)
            {
                final_heal = Mathf.Round(predictedDamage / 2f);
            }
            else
            {
                predictedDamage = 0;
            }
        }
        else
        {
            if (playerStats.isSleeping)
            {
                final_heal = Mathf.Round(predictedDamage / 2f);
            }
            else
            {
                predictedDamage = 0;
            }
        }

        return predictedDamage;
    }

    private float earthQuake(bool isPlayer, float predictedDamage){

        if (isPlayer)
        {
            if (enemyStats.isUnderground)
            {
                predictedDamage *=2f;
            }
        }
        else
        {
            if (playerStats.isUnderground)
            {
                predictedDamage *= 2f;
            }
        }

        return predictedDamage;
    }

    private void oneHitKO(bool isPlayer)
    {
        int acc;
        if (isPlayer)
            acc = playerStats.Level - enemyStats.Level + 30;
        else
            acc = enemyStats.Level - playerStats.Level + 30;
        if (attackCalc.moveHitProbability(acc))
        {
            if (isPlayer)
                enemyStats.curHp = 0;
            else
                playerStats.curHp = 0;
        }
    }

}