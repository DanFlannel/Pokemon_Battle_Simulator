using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenerateAttacks : MonoBehaviour {

	private PokemonCreatorFront pcf;
	private PokemonCreatorBack pcb;
	private PokemonAttacks attackData;
	private int frontMoveList = 15;
	private int backMoveList = 15;
	private int moves = 4;
	private int maxNum = 15;
	public bool attackDatabaseCompiled = false;

	private List<masterList> masterList = new List<masterList>();

	private List<int> randomFrontList = new List<int>();
	private string playerPokemonName1;
	private List<attackIndex> playerAttackList1 = new List<attackIndex>();
	public List<string> playerAttackName = new List<string>();

	private List<int> randomBackList = new List<int>();
	private string enemyPokemonName1;
	private List<attackIndex> enemyAttackList1 = new List<attackIndex>();
	public List<string> enemyAttackName = new List<string>();

	// Use this for initialization
	void Start () {
		pcf = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
		pcb = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack> ();
		attackData = GameObject.FindGameObjectWithTag("Attacks").GetComponent<PokemonAttacks>();

		PlayerPokemonGen();
		EnemyPlayerPokemonGen();
	}

	// Update is called once per frame
	void Update () {
		checkInitalGen();
	}

	private void checkInitalGen(){
		if(pcb.PokemonName != playerPokemonName1){
			Debug.Log("name1:" + pcb.PokemonName + "name2:" + playerPokemonName1);
			genAttacksPlayer();
		}
	}

	private void debugList(){
		for(int i = 0; i < moves; i++){
			Debug.Log("Front" + randomFrontList[i]);
			Debug.Log("Back" + randomBackList[i]);
		}
	}

	private void PlayerPokemonGen(){
		generateRandomList(randomBackList, backMoveList);
	}

	private void EnemyPlayerPokemonGen(){
		generateRandomList(randomFrontList,frontMoveList);
	}

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
		playerPokemonName1 = pcb.PokemonName;
		if(goForward == false){
			//Debug.Log("Nope");
		}else{
			int id = pcb.PokemonID -1;
			//Debug.Log(id);
			playerAttackList1 = attackData.masterGetAttacks(id);
			//Debug.Log(attackData.masterGetName(id));
			checkList(playerAttackList1);
			returnPlayerAttacks();
		}
	}

	private void checkList(List<attackIndex> list){
		for(int i = 0; i < list.Count; i++){
			//Debug.Log(list[i].attack.name);
		}
	}

	private void returnPlayerAttacks(){
		for (int i = 0; i < moves; i++){
			//Debug.Log("attack" + i + ": " + playerAttackList1[randomBackList[i]].attack.name);
			playerAttackName.Add(playerAttackList1[randomBackList[i]].attack.name);
			attackDatabaseCompiled = true;
		}
	}

	private void genAttackEnemy(){

	}
}