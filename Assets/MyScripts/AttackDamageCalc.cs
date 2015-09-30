using UnityEngine;
using System.Collections;

/// <summary>
/// This class is created to take the damage done by each attack when a button is pressed. It is meant to work on every single attack.
/// </summary>
public class AttackDamageCalc : MonoBehaviour {

	private PokemonCreatorBack playerStats;
	private PokemonCreatorFront enemyStats;
    private PokemonAttacks attacks;
    private PokemonDamageMultipliers damage_mult;

    private string status = "status";

	private string frontAttack1;
	private string frontAttack2;
	private string frontAttack3;
	private string frontAttack4;

	private string backAttack1;
	private string backAttack2;
	private string backAttack3;
	private string backAttack4;

	private string topType1;
    private string topType2;
    private string bottomType1;
    private string bottomType2;

	private int topLevel;
	private int bottomLevel;

	private int topAttack;
	private int bottomAttack;

	private int topDefense;
	private int bottomDefense;

	private dmgMult playerDamageMultiplier;
	private dmgMult enemyDamageMultiplier;

	// Use this for initialization
	void Start () {
		enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        attacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        damage_mult = GameObject.FindGameObjectWithTag("dmg_mult").GetComponent<PokemonDamageMultipliers>();
	}
	
	// Update is called once per frame
	void Update () {
		getAttack(); //To be optimized later
	}

	private void getAttack(){
		frontAttack1 = enemyStats.Attack_1_Name;
		frontAttack2 = enemyStats.Attack_2_Name;
		frontAttack3 = enemyStats.Attack_3_Name;
		frontAttack4 = enemyStats.Attack_4_Name;

		backAttack1 = playerStats.Attack_1_Name;
		backAttack2 = playerStats.Attack_2_Name;
		backAttack3 = playerStats.Attack_3_Name;
		backAttack4 = playerStats.Attack_4_Name;
	}

	private void getLevel(){
		topLevel = enemyStats.Level;
		bottomLevel = playerStats.Level;
	}

	private void getAttack(string type){
		if(type == "Normal"){
			topAttack = enemyStats.Attack;
			bottomAttack = playerStats.Attack;
		}else{
			topAttack = enemyStats.Special_Attack;
			bottomAttack = playerStats.Special_Attack;
		}
	}

	private void getDefense(string type){
		if(type == "Normal"){
			topDefense = enemyStats.Defense;
			bottomDefense = playerStats.Defense;
		}else{
			topDefense = enemyStats.Special_Defense;
			bottomDefense = playerStats.Special_Defense;
		}
	}

	private void getPokemonTypes(){
        topType1 = playerStats.Type1;
        topType2 = playerStats.Type2;

        bottomType1 = enemyStats.Type1;
        bottomType2 = enemyStats.Type2;
	}

	private int rndNum(int n){
		return Random.Range(0,n);
	}

	private int damage(){
		return 0;
	}

	//This script should calculate the damage for every attack along with the probability of a pokemon getting a status
	//Buff or debuff
	public void calculateDamage(string name, bool isPlayer){

		
		//Modifier = STAB * Type * Critical * other * Random.Range(.85f,1f);
        int attack_index = getAttackListIndex(name);
        string attackType = attacks.attackList[attack_index].type;

        float damage;

        float level_mod = levelModifier(isPlayer);                  //Damage = (( 2 * level + 10)/250)
        float att_def = attack_div_defense(attackType, isPlayer);   //Damage *= (attack/defense)
        float base_power = baseAttackPower(attack_index);           //Damage *= Base + 2

        damage = level_mod * att_def;
        damage *= base_power;            
        damage += 2;

        float damage_mod = modifier(attack_index, attackType, isPlayer);

        damage *= damage_mod;

        switch (name){
		    case "Absorb":
                    bool stab = isStab(attackType, isPlayer);
				    break;
		}
	}

    private float modifier(int index, string attackType, bool isPlayer)
    {
        //Modifier = STAB * Type * Critical * other * Random.Range(.85f,1f);
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

        float typeMultiplier = getTypeMultiplier(index, attackType, isPlayer);
        float critical = 1f;
        //Critical

        float rnd = Random.Range(.85f, 1f);

        modifier = stab * typeMultiplier * critical * rnd;
        return modifier;

    }

    private float levelModifier(bool isPlayer)
    {
        //(2 * level + 10)/250
        float level;
        float modifier;
        if (isPlayer)
        {
            level = bottomLevel;
        }
        else
        {
            level = topLevel;
        }

        modifier = 2 * level + 10;
        modifier /= 250f;

        return modifier;

    }

    private float attack_div_defense(string attackType, bool isPlayer)
    {
        float modifier;


        if (attackType != attacks.status)
        {
            float attack_mod = 0;
            float defense_mod = 0;

            if (attackType == attacks.special)                  //we are calculating a special attack
            {
                if (isPlayer)                                   //the player is using a special attack
                {
                    attack_mod = playerStats.Special_Attack;
                    defense_mod = enemyStats.Special_Defense;
                }
                else                                            //the enemy is using a special attack
                {
                    attack_mod = playerStats.Attack;
                    defense_mod = enemyStats.Defense;
                }
            }
            if(attackType == attacks.physical)                  //we are calculating a physical attack
            {
                if (isPlayer)                                   //the player is using a physical attack
                {
                    attack_mod = playerStats.Special_Attack;
                    defense_mod = enemyStats.Special_Defense;

                }
                else                                            //the enemy is using a physical attack
                {
                    attack_mod = enemyStats.Special_Attack;
                    defense_mod = playerStats.Special_Defense;
                }
            }
            modifier = attack_mod / defense_mod;
        }
        else                                                    //it is a status move
        {
            modifier = 0;
        }

        return modifier;
    }

    private float baseAttackPower(int index)
    {
        float base_damage = 0;
        base_damage = (float)attacks.attackList[index].power;
        return base_damage;
    }

    private bool isStab(string aType, bool isPlayer) 
    {
        if (isPlayer) {
            if (aType == bottomType1 || aType == bottomType2)
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
            if(aType == topType1 || aType == topType2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    private int getAttackListIndex(string n)
    {
        for(int i = 0; i < attacks.attackList.Count; i++)
        {
            if (n == attacks.attackList[i].name)
                return i;
        }
        Debug.Log("No Attack with name " + n + " found");
        return 0;
    }

    private float getTypeMultiplier(int index, string attackType, bool isPlayer)
    {
        float modifier = 0f;
        if (isPlayer)
        {
            if(enemyStats.name == damage_mult.master_list[index].name)
            {
                modifier = fetchAttackTypeIndex(attackType, index);
            }
            else
            {
                Debug.Log("name did not match when searching for multiplier");
            }
        }
        else
        {
            if(playerStats.name == damage_mult.master_list[index].name)
            {
                modifier = fetchAttackTypeIndex(attackType, index);
            }
            else
            {
                Debug.Log("name did not match when searching for multiplier");
            }
        }
        Debug.Log("Type modifier for attacs is: " + modifier);
        return modifier;
    }

    /// <summary>
    /// uses the index to get the type multiplier from the master list in the damage multiplier class
    /// </summary>
    private float fetchAttackTypeIndex(string attackType, int index)
    {
        float modifier = 0f; 
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
