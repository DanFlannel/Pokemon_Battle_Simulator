using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// This class is created to take the damage done by each attack when a button is pressed. It is meant to work on every single attack.
/// </summary>
public class AttackDamageCalc : MonoBehaviour {

    #region Declared Variables
    private PokemonCreatorBack playerStats;
	private PokemonCreatorFront enemyStats;
    private PokemonAttacks attacks;
    private PokemonDamageMultipliers damage_mult;
    private Attack_Switch_Case attack_Switch_Case;

	private string enemyAttack1;
	private string enemyAttack2;
	private string enemyAttack3;
	private string enemyAttack4;

	private string playerAttack1;
	private string playerAttack2;
	private string playerAttack3;
	private string playerAttack4;

	private string enemyType1;
    private string enemyType2;
    private string playerType1;
    private string playerType2;

	private dmgMult playerDamageMultiplier;
	private dmgMult enemyDamageMultiplier;
    private GenerateAttacks genAttacks;

    private float attack_mod;
    private float defense_mod;

    private bool isPlayer;
    private string attack_name;
    #endregion

    // Use this for initialization
    void Start () {
		enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        attacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        genAttacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
        attack_Switch_Case = GameObject.FindGameObjectWithTag("Attacks").GetComponent<Attack_Switch_Case>();
        damage_mult = GameObject.FindGameObjectWithTag("dmg_mult").GetComponent<PokemonDamageMultipliers>();
	}
	
	// Update is called once per frame
	void Update () {

	}

    /// <summary>
    /// Gets the generated pokemon attacks
    /// </summary>
	private void getPokemonAttacks(){
        /*if(enemyAttack1 != enemyStats.Attack_1_Name)
		    enemyAttack1 = enemyStats.Attack_1_Name;
		if(enemyAttack2 != enemyStats.Attack_2_Name)
            enemyAttack2 = enemyStats.Attack_2_Name;
        if(enemyAttack3 != enemyStats.Attack_3_Name)
            enemyAttack3 = enemyStats.Attack_3_Name;
        if(enemyAttack4 != enemyStats.Attack_4_Name)
            enemyAttack3 = enemyStats.Attack_3_Name;*/

        if (genAttacks.attackDatabaseCompiled)
        {
            playerAttack1 = genAttacks.playerAttackName[0];
            playerAttack2 = genAttacks.playerAttackName[1];
            playerAttack3 = genAttacks.playerAttackName[2];
            playerAttack4 = genAttacks.playerAttackName[3];
        }
    }

    /// <summary>
    /// Gets the pokemon Types for the Type modifier in the damage calculation
    /// </summary>
	private void getPokemonTypes(){
        enemyType1 = playerStats.Type1;
        enemyType2 = playerStats.Type2;

        playerType1 = enemyStats.Type1;
        playerType2 = enemyStats.Type2;
	}

    /// <summary>
    /// Button method, makes sure that if the button was clicked, the isPlayer boolean is true
    /// </summary>
    public void isPlayer_Button(bool p)
    {
        isPlayer = p;
    }

    /// <summary>
    /// Button method, tells this script which attack has been called based off an integer between 1 and 4
    /// </summary>
    public void get_attack_name(int index)
    {
        getPokemonAttacks();
        if (isPlayer) {
            if (index == 1)
            {
                attack_name = playerAttack1;
            }
            if(index == 2)
            {
                attack_name = playerAttack2;
            }
            if(index == 3)
            {
                attack_name = playerAttack3;
            }
            if( index == 4)
            {
                attack_name = playerAttack4;
            }
        }
        else
        {
            if (index == 1)
            {
                attack_name = enemyAttack1;
            }
            if (index == 2)
            {
                attack_name = enemyAttack2;
            }
            if (index == 3)
            {
                attack_name = enemyAttack3;
            }
            if (index == 4)
            {
                attack_name = enemyAttack4;
            }
        }
    }

	/// <summary>
    /// This method takes the name of the attack and then passes it into other methods in the Attack_Switch_Case to get the effect
    /// of the attack on the enemy or player pokemon, if it is a status type of attack or one that deals damage or stuns...ect.
    /// </summary>
	public void calculateAttackEffect(){
        int attack_index = getAttackListIndex(attack_name);
        //Debug.Log("attack index: " + attack_index);
        string attackCat = attacks.attackList[attack_index].cat;

        float predictedDamage = calculateDamage();
        int accuracy = attacks.attackList[attack_index].accuracy;
        Debug.Log("Predicted Damage: " + predictedDamage);

        bool hit = moveHitProbability(accuracy);
        if (!hit)
        {
            Debug.Log("The move missed!");
            return;
        }
        switch (attackCat)
        {
            case "Status":
                attack_Switch_Case.statusAttacks(attack_name, isPlayer);
                break;
            case "Physical":
                attack_Switch_Case.physicalAttacks(attack_name, predictedDamage, isPlayer);
                break;
            case "Special":
                attack_Switch_Case.specialAttacks(attack_name, predictedDamage, isPlayer);
                break;

        }
	}

    /// <summary>
    /// This method calculates the damage that each attack will do based off the serebii.net damage formula
    /// </summary>
    public float calculateDamage()
    {
        float final_damage = 0;
        //Setup for the methods that will get different aspects of the damage calculation
        int attack_index = getAttackListIndex(attack_name);
        //Debug.Log("attack index: " + attack_index);
        string attackType = attacks.attackList[attack_index].type;
        string attackCat = attacks.attackList[attack_index].cat;

        //Setup for the damage calculations
        set_attack_and_def(attack_index, isPlayer, attackCat);
        float level_mod = levelModifier(isPlayer);
        float att_div_defense = baseAttackPower(attack_index) / defense_mod;
        float damage_mod = modifier(attack_index, attackType, isPlayer);

        //Damage Calculations here
        final_damage = level_mod;
        final_damage *= attack_mod;
        final_damage *= att_div_defense;
        final_damage /= 50;
        final_damage += 2;
        final_damage *= damage_mod;
        final_damage = Mathf.Round(final_damage);
        return final_damage;
    }

    /// <summary>
    /// Sets the multiplier Base Power * STAB * Type modifier * Critical * other * randomNum(.85,1)
    /// </summary>
    private float modifier(int index, string attackType, bool isPlayer)
    {
        float modifier;
        float stab;
        if(isStab(attackType, isPlayer))
        {
            stab = 1.5f;
        }
        else
        {
            stab = 1f;
        }

        float critical = 1f;
        bool crit = isCrit();
        if (crit)
        {
            Debug.Log("Critical HIT!");
            critical = 1.5f;
        }
        float rnd = Random.Range(.85f, 1f);
        float typeMultiplier = getTypeMultiplier(attackType, isPlayer);

        modifier = stab * typeMultiplier * critical * rnd;
        Debug.Log("modifier = Stab: " + stab + " type multiplier: " + typeMultiplier + " critical: " + critical + " randomnum: " + rnd);
        return modifier;
    }

    /// <summary>
    /// Sets the level multiplier (2 * level / 5) + 2
    /// </summary>
    private float levelModifier(bool isPlayer)
    {
        //(2 * level / 5) + 2
        float level;
        float modifier;
        if (isPlayer)
        {
            level = playerStats.Level;
        }
        else
        {
            level = enemyStats.Level;
        }

        modifier = 2 * level;
        modifier /= 5;
        modifier += 2;

        Debug.Log("Level modifier: " + modifier);
        return modifier;
    }

    /// <summary>
    ///  Sets the player and enemy attack and defense based on the attack category (physical, status, special)
    /// </summary>
    private void set_attack_and_def(int attack_index, bool isPlayer, string attackCat)//
    {
        float modifier;
        //Debug.Log("attack type for attack/defense: " + attackCat);
        if (attackCat != attacks.status)
        {
            if (attackCat == attacks.special)                  //we are calculating a special attack
            {
                if (isPlayer)                                   //the player is using a special attack
                {
                    attack_mod = playerStats.Special_Attack;
                    defense_mod = enemyStats.Special_Defense;
                }
                else                                            //the enemy is using a special attack
                {
                    attack_mod = enemyStats.Special_Attack;
                    defense_mod = playerStats.Special_Defense;
                }
            }
            if(attackCat == attacks.physical)                  //we are calculating a physical attack
            {
                if (isPlayer)                                   //the player is using a physical attack
                {
                    attack_mod = playerStats.Attack;
                    defense_mod = enemyStats.Defense;
                }
                else                                            //the enemy is using a physical attack
                {
                    attack_mod = enemyStats.Attack;
                    defense_mod = playerStats.Defense;
                }
            }
            modifier = attack_mod / defense_mod;
        }
        else                                                    //it is a status move
        {
            modifier = 0;
        }
        //Debug.Log("Attack: " + attack_mod);
        //Debug.Log("Defense: " + defense_mod);
        //Debug.Log("Attack/Defense = " + modifier);
    }

    /// <summary>
    /// Takes the index or location of the pokemon in the attack list so we can fetch the base attack power for that attack
    /// </summary>
    private float baseAttackPower(int index)
    {
        float base_damage = 0;
        base_damage = (float)attacks.attackList[index].power;
        Debug.Log("Base Damage: " + base_damage);
        return base_damage;
    }

    /// <summary>
    ///  checks if the current attack is a STAB type attack or same type attack
    /// </summary>
    private bool isStab(string aType, bool isPlayer) 
    {
        getPokemonTypes();
        if (isPlayer) {
            if (aType == playerType1 || aType == playerType2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(aType == enemyType1 || aType == enemyType2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    /// <summary>
    /// Gets the index of the pokemon in the attack list so we can use this index later rather than having to get it multiple times
    /// </summary>
    public int getAttackListIndex(string name)
    {
        //Debug.Log("called Attack List Index");
        for (int i = 0; i < attacks.attackList.Count; i++)
        {
            if (name.ToLower() == attacks.attackList[i].name.ToLower())
            {
                //Debug.Log("Calculating Damage for " + name);
                return i;
            }
        }
        Debug.Log("No Attack with name " + name.ToLower() + " found");
        return 0;
    }

    /// <summary>
    ///  Returns the type modifier for the attack based off of the pokemon's damange multiplier for that specific type of attack 
    /// </summary>
    private float getTypeMultiplier(string attackType, bool isPlayer)
    {
        float modifier = 0f;
        
        if (isPlayer)
        {
            int index = enemyStats.PokemonID - 1;
            if (enemyStats.PokemonName == damage_mult.master_list[index].name)
            {
                modifier = fetchAttackTypeIndex(attackType, index);
            }
            else
            {
                Debug.Log("name did not match when searching for multiplier");
                Debug.Log("searched for " + enemyStats.PokemonName);
                Debug.Log("found " + damage_mult.master_list[index].name);
            }
        }
        else
        {
            int index = playerStats.PokemonID - 1;
            if (playerStats.PokemonName == damage_mult.master_list[index].name)
            {
                modifier = fetchAttackTypeIndex(attackType, index);
            }
            else
            {
                Debug.Log("name did not match when searching for multiplier");
                Debug.Log("searched for " + enemyStats.PokemonName);
                Debug.Log("found " + damage_mult.master_list[index].name);
            }
        }
        //Debug.Log("Type modifier for attacs is: " + modifier);
        return modifier;
    }

    /// <summary>
    /// uses the index to get the type multiplier from the master list in the damage multiplier class
    /// </summary>
    private float fetchAttackTypeIndex(string attackType, int index)
    {
        float modifier = 0f;
        attackType = attackType.ToLower();
        switch (attackType)
        {
            case "normal":
                modifier = damage_mult.master_list[index].damage.normal;
                break;
            case "fighting":
                modifier = damage_mult.master_list[index].damage.fighting;
                break;
            case "flying":
                modifier = damage_mult.master_list[index].damage.flying;
                break;
            case "poison":
                modifier = damage_mult.master_list[index].damage.poison;
                break;
            case "ground":
                modifier = damage_mult.master_list[index].damage.ground;
                break;
            case "rock":
                modifier = damage_mult.master_list[index].damage.rock;
                break;
            case "bug":
                modifier = damage_mult.master_list[index].damage.bug;
                break;
            case "ghost":
                modifier = damage_mult.master_list[index].damage.ghost;
                break;
            case "steel":
                modifier = damage_mult.master_list[index].damage.steel;
                break;
            case "fire":
                modifier = damage_mult.master_list[index].damage.fire;
                break;
            case "water":
                modifier = damage_mult.master_list[index].damage.water;
                break;
            case "grass":
                modifier = damage_mult.master_list[index].damage.grass;
                break;
            case "electric":
                modifier = damage_mult.master_list[index].damage.electric;
                break;
            case "psychic":
                modifier = damage_mult.master_list[index].damage.psychic;
                break;
            case "ice":
                modifier = damage_mult.master_list[index].damage.ice;
                break;
            case "dragon":
                modifier = damage_mult.master_list[index].damage.dragon;
                break;
            case "dark":
                modifier = damage_mult.master_list[index].damage.dark;
                break;
            case "fairy":
                modifier = damage_mult.master_list[index].damage.fairy;
                break;
        }
        return modifier;
    }

    /// <summary>
    /// Impliments the damage done by the attack and updates the text and health bars
    /// </summary>
    private void implimentDamage(float damage, bool isPlayer)
    {
        int final_damage = Mathf.RoundToInt(damage);
        if (!isPlayer)
        {
            enemyStats.curHp -= final_damage;
            if(enemyStats.curHp < 0)
            {
                enemyStats.curHp = 0;
            }
        }
        else
        {
            playerStats.curHp -= final_damage;
            if (playerStats.curHp < 0)
            {
                playerStats.curHp = 0;
            }
        }
    }

    /// <summary>
    /// This method takes in the acuracy of the pokemon (always divisible by 5) and calculates if it hits or not and returns a boolean
    /// value based on if it hits
    /// </summary>
    public bool moveHitProbability(int accuracy)
    {
        bool hit = true;
        int hitProb = accuracy / 5;
        int missProb = 20 - hitProb;
        //Debug.Log("accuracy = " + hitProb);
        Debug.Log("miss probability = " + missProb + "/20");
        List<int> missNums = new List<int>();
    
        for(int i = 0; i < missProb; i++)
        {
            int chance = Mathf.RoundToInt(Random.Range(1, 20));
            while (missNums.Contains(chance))
            {
                chance = Mathf.RoundToInt(Random.Range(1, 20));
            }
            missNums.Add(chance);
        }

        int guess = Mathf.RoundToInt(Random.Range(1, 20));
        for(int i = 0; i < missProb; i++)
        {
            if(missNums[i] == guess)
            {
                hit = false;
                return hit;
            }
        }

        return hit;
    }

    /// <summary>
    /// Calculates the 1/16 chance every move has for getting a critical strike
    /// </summary>
    private bool isCrit()
    {
        bool crit = false;

        int guess = Random.Range(1, 16);
        int guess2 = Random.Range(1, 16);

        if(guess == guess2)
        {
            crit = true;
        }

        return crit;
    }
}
