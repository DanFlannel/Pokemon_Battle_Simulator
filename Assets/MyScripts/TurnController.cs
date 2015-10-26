using UnityEngine;
using System.Collections;

public class TurnController : MonoBehaviour {

    [Header("Player")]
    public int PlayerDamage;
    public int PlayerHeal;
    public int PlayerRecoil;
    public bool PlayerCriticalStrike = false;
    public bool PlayerMissed = false;
    public string Player_attackName;
    public bool Player_AttacksFirst = false;
    public bool PlayerDataComplete = false;
    public bool Player_StatusMove = false;
    public bool Player_AppliedStatusEffect = false;


    [Header("Enemy")]
    public int EnemyDamage;
    public int EnemyHeal;
    public int EnemyRecoil;
    public bool EnemyCriticalStrike = false;
    public bool EnemyMissed = false;
    public string Enemy_attackName;
    public bool Enemy_AttacksFirst = false;
    public bool EnemyDataCompelte = false;
    public bool Enemy_StatusMove = false;
    public bool Enemy_AppliedStatusEffect = false;

    private PokemonCreatorBack playerStats;
    private PokemonCreatorFront enemyStats;


    // Use this for initialization
    void Start () {
        enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
        playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        checkSpeed();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void checkSpeed()
    {
        if(playerStats.Speed > enemyStats.Speed)
        {
            Player_AttacksFirst = true;
            Enemy_AttacksFirst = false;
        }
        else
        {
            Player_AttacksFirst = false;
            Enemy_AttacksFirst = true;
        }
    }

    public void checkStats()
    {

    }
}
