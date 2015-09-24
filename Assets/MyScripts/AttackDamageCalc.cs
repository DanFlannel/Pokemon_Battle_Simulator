using UnityEngine;
using System.Collections;

/// <summary>
/// This class is created to take the damage done by each attack when a button is pressed. It is meant to work on every single attack.
/// </summary>
public class AttackDamageCalc : MonoBehaviour {

	private PokemonCreatorBack pcb;
	private PokemonCreatorFront pcf;
    private PokemonAttacks attacks;
    private PokemonDamageMultipliers pdm;

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
		pcf = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		pcb = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        attacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        pdm = GameObject.FindGameObjectWithTag("dmg_mult").GetComponent<PokemonDamageMultipliers>();
	}
	
	// Update is called once per frame
	void Update () {
		getAttack(); //To be optimized later
	}

	private void getAttack(){
		frontAttack1 = pcf.Attack_1_Name;
		frontAttack2 = pcf.Attack_2_Name;
		frontAttack3 = pcf.Attack_3_Name;
		frontAttack4 = pcf.Attack_4_Name;

		backAttack1 = pcb.Attack_1_Name;
		backAttack2 = pcb.Attack_2_Name;
		backAttack3 = pcb.Attack_3_Name;
		backAttack4 = pcb.Attack_4_Name;
	}

	private void getLevel(){
		topLevel = pcf.Level;
		bottomLevel = pcb.Level;
	}

	private void getAttack(string type){
		if(type == "Normal"){
			topAttack = pcf.Attack;
			bottomAttack = pcb.Attack;
		}else{
			topAttack = pcf.Special_Attack;
			bottomAttack = pcb.Special_Attack;
		}
	}

	private void getDefense(string type){
		if(type == "Normal"){
			topDefense = pcf.Defense;
			bottomDefense = pcb.Defense;
		}else{
			topDefense = pcf.Special_Defense;
			bottomDefense = pcb.Special_Defense;
		}
	}

	private void getPokemonTypes(){
        topType1 = pcb.Type1;
        topType2 = pcb.Type2;

        bottomType1 = pcf.Type1;
        bottomType2 = pcf.Type2;
	}

	private int rndNum(int n){
		return Random.Range(0,n);
	}

	private int damage(){
		return 0;
	}

	//This script should calculate the damage for every attack along with the probability of a pokemon getting a status
	//Buff or debuff
	private void calculateDamage(string name, bool isPlayer){

		//Damage = (( 2 * level + 10)/250)
		//Damage *= (attack/defense)
		//Damage *= Base + 2
		//Modifier = STAB * Type * Critical * other * Random.Range(.85f,1f);

		int heal = 0;
        int index;

		float levelMod = (2f*topLevel + 10f);
		levelMod /= 250f;

        index = getAttackListIndex(name);

        switch (name){
		    case "Absorb":
                    
                    string attackType = attacks.attackList[index].type;
                    bool stab = isStab(attackType, isPlayer);
				    break;
		}
	}

    private void modifier()
    {
        //Modifier = STAB * Type * Critical * other * Random.Range(.85f,1f);
    }

    private bool isStab(string aType, bool isP) 
    {
        if (isP) {
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

    private int getTypeMultiplier(string name)
    {
        return 0;
    }

}
