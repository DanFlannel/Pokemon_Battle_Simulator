using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

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

	private PlayerPokemonHandler pcb;
	private EnemyPokemonHandler pcf;
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
        Console.WriteLine("PK : GUIScript : Initalizing");
        pcb = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
		pcf = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
		attackGen = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
    }	
	
	// Update is called once per frame
	void Update () { 

		if(attackGen.attackDatabaseCompiled && !generatedAttacks){
            generatePokemonStats();
            Console.WriteLine("PK : GUIScript : Initalized");
        }
	}

    /// <summary>
    /// Gets the Pokemon's name and displays it on the GUI
    /// </summary>
	private void names(){
		bottomName = pcb.PokemonName;
		topName = pcf.PokemonName;

		bottomNameText.text = bottomName;
		topNameText.text = topName;
	}

    /// <summary>
    /// Gets the Pokemon's level and displays it on the GUI
    /// </summary>
	private void level(){
		bottomLevel = pcb.Level;
		topLevel = pcf.Level;
        //Debug.logger.Log("enemy level: " + topLevel);

		bottomLevelText.text = "Lv"+bottomLevel.ToString();
		topLevelText.text = "Lv"+topLevel.ToString();
	}

    /// <summary>
    /// Gets the Pokemon's health and displays it on the GUI
    /// </summary>
	public void updateHealth(){
		maxHealth = pcb.maxHP;
		curHealth = pcb.curHp;
        if (curHealth < 0)
            curHealth = 0;
		bottomHealth.text = curHealth + " / " + maxHealth;
	}

    /// <summary>
    /// Function to generate all the attack stats
    /// </summary>
	private void generatePokemonStats(){
		names();
		level();
		updateHealth();
        attackNames();
    }

    /// <summary>
    /// Gets the attack names from the randomly generated attack list
    /// </summary>
	private void attackNames(){
        List<string> playerAttackName = attackGen.get_playerAttackName();

		playerAttack1.text = playerAttackName[0];
		playerAttack2.text = playerAttackName[1];
		playerAttack3.text = playerAttackName[2];
		playerAttack4.text = playerAttackName[3];
		generatedAttacks = true;
	}

    /// <summary>
    /// Returns the application to the main menu screen
    /// </summary>
    public void main_menu()
    {
        SceneManager.LoadScene(0);
    }


}
