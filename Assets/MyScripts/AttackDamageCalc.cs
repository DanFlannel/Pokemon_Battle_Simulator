using UnityEngine;
using System.Collections;

public class AttackDamageCalc : MonoBehaviour {

	private PokemonCreatorBack pcb;
	private PokemonCreatorFront pcf;

	private string frontAttack1;
	private string frontAttack2;
	private string frontAttack3;
	private string frontAttack4;

	private string backAttack1;
	private string backAttack2;
	private string backAttack3;
	private string backAttack4;

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
	}
	
	// Update is called once per frame
	void Update () {
		GetAttack(); //To be optimized later
	}

	private void GetAttack(){
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

	}

	private int rndNum(int n){
		return Random.Range(0,n);
	}

	private int damage(){
		return 0;
	}

	//This script should calculate the damage for every attack along with the probability of a pokemon getting a status
	//Buff or debuff
	private void calculateDamage(string name){

		//Damage = (( 2 * level + 10)/250)
		//Damage *= (attack/defense)
		//Damage *= Base + 2
		//Modifier = STAB * Type * Critical * other * Random.Range(.85f,1f);

		int heal = 0;

		float levelMod = (2f*topLevel + 10f);
		levelMod /= 250f;

		switch(name){
		case "Absorb": 
				getAttack("Normal");
				break;
		}
	}

}
