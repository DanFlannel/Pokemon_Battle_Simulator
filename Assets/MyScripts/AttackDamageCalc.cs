using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class is created to take the damage done by each attack when a button is pressed. It is meant to work on every single attack.
/// </summary>
public class AttackDamageCalc : MonoBehaviour {

	private PokemonCreatorBack playerStats;
	private PokemonCreatorFront enemyStats;
    private PokemonAttacks attacks;
    private PokemonDamageMultipliers damage_mult;

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

    public Text playerAttack1Text;
    public Text playerAttack2Text;
    public Text playerAttack3Text;
    public Text playerAttack4Text;

    // Use this for initialization
    void Start () {
		enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        attacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        genAttacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
        damage_mult = GameObject.FindGameObjectWithTag("dmg_mult").GetComponent<PokemonDamageMultipliers>();
	}
	
	// Update is called once per frame
	void Update () {
	}

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

	private void getPokemonTypes(){
        enemyType1 = playerStats.Type1;
        enemyType2 = playerStats.Type2;

        playerType1 = enemyStats.Type1;
        playerType2 = enemyStats.Type2;
	}

    public void isPlayer_Button(bool p)
    {
        isPlayer = p;
    }

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

	//This script should calculate the damage for every attack along with the probability of a pokemon getting a status
	//Buff or debuff
	public void calculateDamage(){
        string name = attack_name;
        Debug.Log("Attack Damage for " + name + " to be calculated");

        int attack_index = getAttackListIndex(name);
        string attackType = attacks.attackList[attack_index].type;
        string attackCat = attacks.attackList[attack_index].cat;
        set_attack_and_def(attack_index, isPlayer);

        float final_damage;
        float level_mod = levelModifier(isPlayer);
        //Debug.Log("level mod: " + level_mod);

        float att_div_defense = baseAttackPower(attack_index) / defense_mod;
        //Debug.Log("attack/defense: " + att_div_defense);

        final_damage = level_mod;
        final_damage *= attack_mod;
        final_damage *= att_div_defense;
        final_damage /= 50;
        final_damage += 2;

        float damage_mod = modifier(attack_index, attackType, isPlayer);
        //Debug.Log("final modifier = " + damage_mod);

        final_damage *= damage_mod;
        final_damage = Mathf.Round(final_damage);
        Debug.Log("prediceted damage = " + final_damage);
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

        float typeMultiplier = getTypeMultiplier(attackType, isPlayer);
        float critical = 1f;
        //Critical

        float rnd = Random.Range(.85f, 1f);

        float base_attack = baseAttackPower(index);
        
        modifier = stab * typeMultiplier * critical * rnd;
        Debug.Log("modifier = Stab: " + stab + " type multiplier: " + typeMultiplier + " critical: " + critical + " randomnum: " + rnd);
        return modifier;

    }


    /// <summary>
    /// Sets the level multiplier (2 * level + 10)/250 
    /// </summary>
    private float levelModifier(bool isPlayer)
    {
        //(2 * level + 10 / 250
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
        modifier += 2;;
        return modifier;
    }

    /// <summary>
    ///  Sets the player and enemy attack and defense based on the attack category (physical, status, special)
    /// </summary>
    private void set_attack_and_def(int attack_index, bool isPlayer)//
    {
        float modifier;
        string attackCat = attacks.attackList[attack_index].cat;
        Debug.Log("attack type for attack/defense: " + attackCat);
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
    private int getAttackListIndex(string name)
    {
        for(int i = 0; i < attacks.attackList.Count; i++)
        {
            if (name == attacks.attackList[i].name)
                return i;
        }
        Debug.Log("No Attack with name " + name + " found");
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
        //Debug.Log("Pokemon dmg mult modifier for normal " + damage_mult.master_list[index].damage.normal);
        //Debug.Log("dmg mult attack type" + attackType);
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


}
