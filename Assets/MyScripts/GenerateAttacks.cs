using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class generated the attacks for the player, as shown in th GUI. It fetches the attack data for the pokemon 
/// in the PokemonAttacks script and then picks 4 attacks from the list of attacks the specified pokemon can use and
/// sends the data to the GUI so the user can see what attacks he/she can has at his/her disposal.
/// </summary>

public class GenerateAttacks : MonoBehaviour {

	private PokemonCreatorFront pcf;
	private PokemonCreatorBack pcb;
	private PokemonAttacks attackData;
	private int frontMoveList = 15;
	private int lengthOfList = 15;
	private int moves = 4;
	private int maxNum = 15;
	public bool attackDatabaseCompiled = false;

	private List<masterList> masterList = new List<masterList>();

    [Header("Player")]
	public List<int> playerIndexList = new List<int>();
	private string playerPokemonName;
    public int playerTotalMoves;
	private List<attackIndex> playerAttackList = new List<attackIndex>();
	public List<string> playerAttackName = new List<string>();

    [Header("Enemy")]
	public List<int> enemyIndexList = new List<int>();
	private string enemyPokemonName;
    public int enemyTotalMoves;
	private List<attackIndex> enemyAttackList = new List<attackIndex>();
	public List<string> enemyAttackName = new List<string>();

	// Use this for initialization
	void Start () {
		pcf = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		pcb = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack> ();
		attackData = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();

        generateRandomList(enemyIndexList, moves);
    }


	// Update is called once per frame
	void Update () {
		checkInitalGen();
	}

    /// <summary>
    /// Checks to make sure that the inital Gen of the pokemon attack list went through, if not it calls the
    /// generation of the attacks again.
    /// </summary>
	private void checkInitalGen(){
		if(pcb.PokemonName != playerPokemonName){
			//Debug.Log("name1:" + pcb.PokemonName + "name2:" + playerPokemonName1);
			genAttacksPlayer();		
            //Debug.Log("name1:" + pcb.PokemonName + "name2:" + playerPokemonName1);
            //generateEnemyAttacks();
        }

    }

    /// <summary>
    /// A simple debug function that debugs all the attacks each pokemon can use
    /// </summary>
	private void debugList(){
		for(int i = 0; i < moves; i++){
			Debug.Log("Front" + playerIndexList[i]);
			Debug.Log("Back" + enemyIndexList[i]);
		}
	}

    /// <summary>
    /// Generates the random list of attacks based on the input of the list variable
    /// </summary>
	private void generateRandomList(List<int> list, int range){
		for(int i = 0; i < maxNum; i++){
			int numToAdd = Random.Range(0,range);
			while(list.Contains(numToAdd)){
				numToAdd = Random.Range(0,range);
			}
			list.Add(numToAdd);
		}
	}

	private void genAttacksPlayer(){
		bool goForward = attackData.completedDatabaseInitalization;
		playerPokemonName = pcb.PokemonName;
		if(goForward == false){
			//Debug.Log("Nope");
		}else{
			int id = pcb.PokemonID -1;
			playerAttackList = attackData.masterGetAttacks(id);
            playerTotalMoves = playerAttackList.Count;

            //Debug.Log("length of player attack lis: " + lengthOfList);
            //Debug.Log("Pokemon ID" + pcb.PokemonID);

            generateRandomList(playerIndexList, playerTotalMoves);
            //Debug.Log(attackData.masterGetName(id));
            returnAttacks(playerIndexList);
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
	private void returnAttacks(List<int> list){
		for (int i = 0; i < moves; i++){
			//Debug.Log("attack" + i + ": " + playerAttackList1[randomBackList[i]].attack.name);
			playerAttackName.Add(playerAttackList[list[i]].attack.name);
		}
        attackDatabaseCompiled = true;
    }

    //TODO Generate Enemy attacks.
    private void generateEnemyAttacks()
    {
        bool goForward = attackData.completedDatabaseInitalization;
        playerPokemonName = pcf.PokemonName;
        if (goForward == false)
        { }
        else
        {
            int id = pcb.PokemonID - 1;
            enemyAttackList = attackData.masterGetAttacks(id);
            enemyTotalMoves = enemyAttackList.Count;
            //generateRandomList(enemyIndexList, enemyTotalMoves);
            //returnAttacks(enemyIndexList);
        }
    }

}