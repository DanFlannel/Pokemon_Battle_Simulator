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
    private AttackDamageCalc adc;
	private int moves = 4;

    public bool attackDatabaseCompiled = false;

    [Header("Player")]
    [SerializeField]
    private List<int> randomPlayerIndex = new List<int>();
    private string playerPokemonName1;
	private List<attackIndex> playerAttackList1 = new List<attackIndex>();
    private List<string> playerAttackName = new List<string>();

    [Header("Enemy")]
    [SerializeField]
    private List<int> randomEnemyIndex = new List<int>();
	private string enemyPokemonName1;
	private List<attackIndex> enemyAttackList1 = new List<attackIndex>();
    private List<string> enemyAttackName = new List<string>();

	// Use this for initialization
	void Start () {
		pcf = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		pcb = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack> ();
		attackData = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();
        adc = GameObject.FindGameObjectWithTag("Attacks").GetComponent<AttackDamageCalc>();

        //PlayerPokemonGen();
        //EnemyPlayerPokemonGen();
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
		if(pcb.PokemonName != playerPokemonName1){
			Debug.Log("name1:" + pcb.PokemonName + "name2:" + playerPokemonName1);
			genPlayerAttacks();
            genEnemyAttacks();
		}
	}

    /// <summary>
    /// A simple debug function that debugs all the attacks each pokemon can use
    /// </summary>
	private void debugList(){
		for(int i = 0; i < moves; i++){
			Debug.Log("Front" + randomEnemyIndex[i]);
			Debug.Log("Back" + randomPlayerIndex[i]);
		}
	}

    /// <summary>
    /// Generates the random list of player attacks
    /// </summary>
	private void PlayerPokemonGen(){
        int id = pcb.getPokemonID() -1;
		playerAttackList1 = attackData.masterGetAttacks(id);
	    generateRandomList(randomPlayerIndex, playerAttackList1.Count);
	}

    /// <summary>
    /// Generates the random list of enemy attacks
    /// </summary>
	private void EnemyPlayerPokemonGen(){
        //Debug.Log(pcf.PokemonID);
        int id = pcf.PokemonID - 1;
        enemyAttackList1 = attackData.masterGetAttacks(id);
        generateRandomList(randomEnemyIndex,enemyAttackList1.Count);
	}

    /// <summary>
    /// Generates the random list of attacks based on the input of the list variable
    /// </summary>
	private void generateRandomList(List<int> list, int range){
		for(int i = 0; i < range; i++){
			int numToAdd = Random.Range(0,range);
			while(list.Contains(numToAdd)){
				numToAdd = Random.Range(0,range);
			}
			list.Add(numToAdd);
		}
	}

    /// <summary>
    /// generates the Player attack names
    /// </summary>
    private void genPlayerAttacks(){
		bool goForward = attackData.completedDatabaseInitalization;
		playerPokemonName1 = pcb.PokemonName;
		if(goForward == false){
			//Debug.Log("Nope");
		}else{
            //Debug.Log(attackData.masterGetName(id));
            returnPlayerAttacks();
		}
        adc.playerAttack1 = playerAttackName[0];
        adc.playerAttack2 = playerAttackName[1];
        adc.playerAttack3 = playerAttackName[2];
        adc.playerAttack4= playerAttackName[3];
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
		for (int i = 0; i < moves; i++){
            if (i < randomPlayerIndex.Count)
            {
                playerAttackName.Add(playerAttackList1[randomPlayerIndex[i]].attack.name);
            }
            else
            {
                playerAttackName.Add(playerAttackList1[randomPlayerIndex[0]].attack.name);
            }
		}
        attackDatabaseCompiled = true;
    }

    /// <summary>
    /// Adds an attack to the enemyAttack string list based off if that pokemon can use it
    /// </summary>
    private void returnEnemyAttacks()
    {
        for(int i = 0; i < moves; i++)
        {
            if (i < randomEnemyIndex.Count)
            {
                enemyAttackName.Add(enemyAttackList1[randomEnemyIndex[i]].attack.name);
            }
            else
            {
                enemyAttackName.Add(enemyAttackList1[randomEnemyIndex[0]].attack.name);
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