using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

/// <summary>
/// This class will handle all of the fixed GUI changes for the pokemon
/// This class does not handle the sliders/health bars
/// </summary>
public class GUIScript : MonoBehaviour
{

    public Transform swapPanelUI;
    public Button[] swapButtons;

    public Text playerPokemonName;
    public Text enemyPokemonName;

    public Text playerPokemonLevel;
    public Text enemyPokemonLevel;

    public Text playerHealth;
    private int curHealth;
    private int maxHealth;

    private PlayerPokemonHandler playerStats;
    private EnemyPokemonHandler enemyStats;

    public Image type1;
    public Image type2;
    public Sprite[] types;

    public Text playerAttack1;
    public Text playerAttack2;
    public Text playerAttack3;
    public Text playerAttack4;
    private bool generatedAttacks = false;
    private bool swapPanelOpen;

    // Use this for initialization
    void Awake()
    {
        swapPanelOpen = true;
        Console.WriteLine("PK : GUIScript : Initalizing");
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
        togglePanel();
    }

    /// <summary>
    /// Gets the Pokemon's health and displays it on the GUI
    /// </summary>
	public void updatePlayerHealth()
    {
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
	private void attackNames()
    {
        playerAttack1.text = playerStats.attack1;
        playerAttack2.text = playerStats.attack2;
        playerAttack3.text = playerStats.attack3;
        playerAttack4.text = playerStats.attack4;
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
        int level = playerStats.Level;
        playerPokemonLevel.text = "Lv" + level.ToString();
        playerPokemonName.text = playerStats.PokemonName.ToString();
        updatePlayerHealth();

    }

    public void UpdateEnemyInfo()
    {
        enemyPokemonName.text = enemyStats.PokemonName.ToString();
        int level = enemyStats.Level;
        enemyPokemonLevel.text = "Lv" + level.ToString();
    }

    public void togglePanel()
    {

        swapPanelOpen = !swapPanelOpen;
        Debug.LogWarning(string.Format("Called to open change pokemon panel {0}", swapPanelOpen));
        swapPanelUI.gameObject.SetActive(swapPanelOpen);
        foreach (Transform child in swapPanelUI)
        {
            child.gameObject.SetActive(swapPanelOpen);
        }

        if (swapPanelOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void updatePokemonNames(List<PokemonEntity> pokemonTeam)
    {
        for (int i = 0; i < 6; i++)
        {
            Text buttonText = swapButtons[i].GetComponentInChildren<Text>();
            string pokemonName = pokemonTeam[i].Name;
            buttonText.text = pokemonName;
            //Debug.Log("Pokemon Name: " + pokemonName);
        }
    }
}
