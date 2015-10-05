using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Attack_Switch_Case : MonoBehaviour {

    public float final_damage;
    public float final_heal;

    public bool isPlayerStunned;
    public bool isEnemyStunned;

    private PokemonCreatorBack playerStats;
    private PokemonCreatorFront enemyStats;

    private string defense = "defense";
    private string attack = "attack";
    private string spAttack = "spAttack";
    private string spDefense = "spDefense";


    void Start()
    {
        enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
        playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
    }

    public void statusAttacks(string name, bool isPlayer)
    {
        name = name.ToLower();
    }

    public void physicalAttacks(string name, float predictedDamage, bool isPlayer)
    {
        name = name.ToLower();
    }

    public void specialAttacks(string name, float predictedDamage, bool isPlayer)
    {
        name = name.ToLower();
        final_heal = 0;
        bool stunHit = false;
        switch (name)
        {
            case "absorb":
                final_heal = predictedDamage / 2f;
                break;
            case "acid":
                stunHit = stunProbability(1, isPlayer);
                if (stunHit)
                {
                    changeStats(spDefense, 1, !isPlayer);
                }
                break;
        }
    }


    /// <summary>
    /// Takes in the probability of getting a stun for a move out of ten, then makes a list of that many unique random numbers
    /// if the guess is any of those unique random numbers the method returns true, otherwise the attack did not stun the 
    /// enemy pokemon
    /// </summary>
    private bool stunProbability(int prob, bool isPlayer)
    {
        isPlayerStunned = false;
        isEnemyStunned = false;
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

}
