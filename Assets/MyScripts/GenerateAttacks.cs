using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// This class generated the attacks for the player, as shown in th GUI. It fetches the attack data for the pokemon 
/// in the PokemonAttacks script and then picks 4 attacks from the list of attacks the specified pokemon can use and
/// sends the data to the GUI so the user can see what attacks he/she can has at his/her disposal.
/// </summary>
public class GenerateAttacks : MonoBehaviour {

	private EnemyPokemonHandler enemyStats;
	private PlayerPokemonHandler playerStats;
	private PokemonAttacks attackData;
    private AttackDamageCalc adc;
	private const int MOVES = 4;

    public bool attackDatabaseCompiled = false;
    private bool attacksGenerated;

    [Header("Player")]
    [SerializeField]
    private List<int> rndPlayerMovesIndex = new List<int>();
    private string playerPokemonName1;
	private List<attackIndex> playerAttackList1 = new List<attackIndex>();
    private List<string> playerAttackName = new List<string>();

    [Header("Enemy")]
    [SerializeField]
    private List<int> rndEnemyMovesIndex = new List<int>();
	private string enemyPokemonName1;
	private List<attackIndex> enemyAttackList1 = new List<attackIndex>();
    private List<string> enemyAttackName = new List<string>();

	// Use this for initialization
	void Start () {
        Init();
    }

    private void Init()
    {
        //Console.WriteLine("PK : Generate Attacks: Initalizing");
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
        attackData = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        adc = GameObject.FindGameObjectWithTag("Attacks").GetComponent<AttackDamageCalc>();
        //Console.WriteLine("PK : Generate Attacks: Initalized");
        //Debug.Log("name1:" + pcb.PokemonName + "name2:" + playerPokemonName1);
        attacksGenerated = false;
    }


	// Update is called once per frame
	void Update () {
        //checkInitalGen();
        if (attackData.completedDatabaseInitalization && !attacksGenerated)
        {
            Debug.Log("Name 1:" + playerStats.PokemonName + "   Name 2:" + playerPokemonName1);
            playerPokemonName1 = playerStats.PokemonName;
            PlayerPokemonGen();
            EnemyPokemonGen();
            genPlayerAttacks();
            genEnemyAttacks();
            attacksGenerated = true;
        }
    }

    /// <summary>
    /// Checks to make sure that the inital Gen of the pokemon attack list went through, if not it calls the
    /// generation of the attacks again.
    /// </summary>
	private void checkInitalGen(){
        
        if (playerStats.PokemonName != playerPokemonName1){
            PlayerPokemonGen();
            EnemyPokemonGen();
			Debug.Log("name1:" + playerStats.PokemonName + "name2:" + playerPokemonName1);
			genPlayerAttacks();
            genEnemyAttacks();
		}
	}

    /// <summary>
    /// A simple debug function that debugs all the attacks each pokemon can use
    /// </summary>
	private void debugList(){
		for(int i = 0; i < MOVES; i++){
			Debug.Log("Front" + rndEnemyMovesIndex[i]);
			Debug.Log("Back" + rndPlayerMovesIndex[i]);
		}
	}

    /// <summary>
    /// Generates the random list of player attacks
    /// </summary>
	private void PlayerPokemonGen(){
        int id = playerStats.getPokemonID() -1;
		playerAttackList1 = attackData.masterGetAttacks(id);
        Debug.Log("Number of different player attacks: " + playerAttackList1.Count);
	    generateRandomList(rndPlayerMovesIndex, playerAttackList1.Count);
	}

    /// <summary>
    /// Generates the random list of enemy attacks
    /// </summary>
	private void EnemyPokemonGen(){
        //Debug.Log(pcf.PokemonID);
        int id = enemyStats.PokemonID - 1;
        enemyAttackList1 = attackData.masterGetAttacks(id);
        generateRandomList(rndEnemyMovesIndex,enemyAttackList1.Count);
	}

    /// <summary>
    /// Generates the random list of attacks based on the input of the list variable and checks for pokemon with less than 4 moves
    /// </summary>
	private void generateRandomList(List<int> list, int totalPossibleMoves){
        list.Clear();
        Debug.Log("Range: " + totalPossibleMoves);
        Debug.Log("List Cout: " + list.Count);
        int numToAdd = -1;
        //if the pokemon has more than 4 moves that it can learn, then we pick from those randomly
        if (totalPossibleMoves > MOVES)
        {
            for (int i = 0; i < MOVES; i++)
            {
                numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                while (list.Contains(numToAdd))
                {
                    numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                }
                list.Add(numToAdd);
            }
        }
        //this ensures that all possible moves are added for pokemon with less than or equal to 4 moves
        else
        {
            totalPossibleMoves -= 1;
            Debug.LogWarning("Pokemon: " + playerStats.PokemonName + " or " + enemyStats.PokemonName + " has less than four attacks!");
            int totalMoves = 0;
            for(int i = 0; i < MOVES; i++)
            {
                
                if (i <= totalPossibleMoves)
                {
                    numToAdd = i;
                    totalMoves++;
                }
                else
                {
                    numToAdd = UnityEngine.Random.Range(0, totalMoves);
                }
                list.Add(numToAdd);
            }
        }
        Debug.Log("Generated List");
	}

    /// <summary>
    /// generates the Player attack names
    /// </summary>
    private void genPlayerAttacks(){
		bool goForward = attackData.completedDatabaseInitalization;
		playerPokemonName1 = playerStats.PokemonName;
		if(goForward == false){
			Debug.Log("Nope");
		}else{
            Debug.Log(attackData.masterGetName(playerStats.getPokemonID()));
            returnPlayerAttacks();
            adc.playerAttack1 = playerAttackName[0];
            adc.playerAttack2 = playerAttackName[1];
            adc.playerAttack3 = playerAttackName[2];
            adc.playerAttack4 = playerAttackName[3];
        }
    }

    /// <summary>
    /// Simple check of the generated attack list.
    /// </summary>
	private void checkList(List<attackIndex> list){
		for(int i = 0; i < list.Count; i++){
			//Debug.Log(list[i].attack.name);
		}
	}

    /// <summary>
    /// the moves is always 4, 
    /// </summary>
	private void returnPlayerAttacks(){
		for (int i = 0; i < MOVES; i++){
            if (i < rndPlayerMovesIndex.Count)
            {
                playerAttackName.Add(playerAttackList1[rndPlayerMovesIndex[i]].attack.name);
            }
            else
            {
                playerAttackName.Add(playerAttackList1[rndPlayerMovesIndex[0]].attack.name);
            }
		}
        attackDatabaseCompiled = true;
    }

    /// <summary>
    /// Adds an attack to the enemyAttack string list based off if that pokemon can use it
    /// </summary>
    private void returnEnemyAttacks()
    {
        for(int i = 0; i < MOVES; i++)
        {
            if (i < rndEnemyMovesIndex.Count)
            {
                enemyAttackName.Add(enemyAttackList1[rndEnemyMovesIndex[i]].attack.name);
            }
            else
            {
                enemyAttackName.Add(enemyAttackList1[rndEnemyMovesIndex[0]].attack.name);
            }
        }
    }

    /// <summary>
    /// generates the Enemy attack names
    /// </summary>
    private void genEnemyAttacks()
    {
        bool goForward = attackData.completedDatabaseInitalization;
        if (!goForward)
        {
            Debug.Log("cant generate enemy attacks yet");
        }
        else
        {
            returnEnemyAttacks();
        }
        adc.enemyAttack1 = enemyAttackName[0];
        adc.enemyAttack2 = enemyAttackName[1];
        adc.enemyAttack3 = enemyAttackName[2];
        adc.enemyAttack4 = enemyAttackName[3];
    }

    /// <summary>
    /// Gets the playerAttackName string list
    /// </summary>
    /// <returns>The playerAttackName string list variable</returns>
    public List<string> get_playerAttackName()
    {
        return playerAttackName;
    }

    /// <summary>
    /// Gets the enemyAttackName string list
    /// </summary>
    /// <returns>The enemyAttackName string list variable</returns>
    public List<string> get_enemyAttackName()
    {
        return enemyAttackName;
    }

}