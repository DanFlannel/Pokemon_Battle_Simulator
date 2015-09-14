using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour {

	private string bottomName;
	private string topName;
	public Text bottomNameText;
	public Text topNameText;

	private int bottomLevel;
	private int topLevel;
	public Text bottomLevelText;
	public Text topLevelText;

	public Text bottomHealth;
	private int curHealth;
	private int maxHealth;

	private PokemonCreatorBack pcb;
	private PokemonCreatorFront pcf;
	private GenerateAttacks attackGen;

	public Text move1;
	public Image type1;
	public Image type2;
	public Sprite[] types;

	public Text playerAttack1;
	public Text playerAttack2;
	public Text playerAttack3;
	public Text playerAttack4;
	private bool generatedAttacks = false;


	// Use this for initialization
	void Start () {
		pcb = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
		pcf = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		attackGen = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
		generatePokemonStats();
		attackNames();
	}	
	
	// Update is called once per frame
	void Update () { 

		if(attackGen.attackDatabaseCompiled && !generatedAttacks){
			attackNames();
		}
	}

	

	private void names(){
		bottomName = pcb.PokemonName;
		topName = pcf.PokemonName;

		bottomNameText.text = bottomName;
		topNameText.text = topName;
	}

	private void level(){
		bottomLevel = pcb.Level;
		topLevel = pcf.Level;

		bottomLevelText.text = "Lv"+bottomLevel;
		topLevelText.text = "Lv"+topLevel;
	}

	private void health(){
		maxHealth = pcb.maxHP;
		curHealth = pcb.curHp;

		bottomHealth.text = curHealth + " / " + maxHealth;
	}

	private void generatePokemonStats(){
		names();
		level();
		health();
	}

	private void attackNames(){
		playerAttack1.text = attackGen.playerAttackName[0];
		playerAttack2.text = attackGen.playerAttackName[1];
		playerAttack3.text = attackGen.playerAttackName[2];
		playerAttack4.text = attackGen.playerAttackName[3];
		generatedAttacks = true;
	}


}
