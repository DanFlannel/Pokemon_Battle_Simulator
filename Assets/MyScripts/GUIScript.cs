﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class GUIScript : MonoBehaviour {

	public Text playerPokemonName;
	public Text EnemyPokemonName;

	public Text playerPokemonLevel;
	public Text enemyPokemonLevel;

	public Text playerHealth;
	private int curHealth;
	private int maxHealth;

	private PlayerPokemonHandler playerStats;
	private EnemyPokemonHandler enemyStats;
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
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
		enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
		attackGen = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
    }	
	
	//** need to remove this
	void Update () { 

        //** get rid of this 
		if(attackGen.attackDatabaseCompiled && !generatedAttacks){
            UpdatePlayerInfo();
            UpdateEnemyInfo();
            Console.WriteLine("PK : GUIScript : Initalized");
        }
	}

    /// <summary>
    /// Gets the Pokemon's health and displays it on the GUI
    /// </summary>
	public void updateHealth(){
        curHealth = playerStats.curHp;
        maxHealth = playerStats.maxHP;

        if (curHealth < 0)
        {
            curHealth = 0;
        }

		playerHealth.text = curHealth + " / " + maxHealth;
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

    public void UpdatePlayerInfo()
    {
        attackNames();
        playerPokemonLevel.text = playerStats.Level.ToString();
        playerPokemonName.text = playerStats.PokemonName.ToString();
        updateHealth();

    }

    public void UpdateEnemyInfo()
    {
        enemyPokemonLevel.text = enemyStats.Level.ToString();
        EnemyPokemonName.text = enemyStats.PokemonName.ToString();
    }


}
