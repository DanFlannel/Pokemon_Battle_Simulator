using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    [Header("Player")]
    public int PlayerHealth;
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
    public Slider playerHealthBar;



    [Header("Enemy")]
    public int EnemyHealth;
    public int EnemyDamage;
    public int EnemyHeal;
    public int EnemyRecoil;
    public bool EnemyCriticalStrike = false;
    public bool EnemyMissed = false;
    public string Enemy_attackName;
    public bool Enemy_AttacksFirst = false;
    public bool EnemyDataComplete = false;
    public bool Enemy_StatusMove = false;
    public bool Enemy_AppliedStatusEffect = false;
    public Slider enemyHealthBar;

    [Header("Post Damage Conditions")]
    public bool player_one_sixteenth = false;
    public int player_one_sixteenth_duration = -1;
    public bool player_one_eigth = false;
    public int player_one_eigth_duration = -1;
    public bool enemy_one_sixteen = false;
    public int enemy_one_sixteenth_duration = -1;
    public bool enemy_one_eigth = false;
    public int enemy_one_eigth_duration = -1;

    public bool player_leech_seed = false;
    public bool enemy_leech_seed = false;

    public bool player_toxic = false;
    public int player_toxic_mult = 0;

    public bool enemy_toxic = false;
    public int enemy_toxic_mult = 0;


    private PokemonCreatorBack playerStats;
    private PokemonCreatorFront enemyStats;


    // Use this for initialization
    void Start () {
        enemyStats = GameObject.FindGameObjectWithTag("PTR").GetComponent<PokemonCreatorFront>();
        playerStats = GameObject.FindGameObjectWithTag("PBL").GetComponent<PokemonCreatorBack>();
        EnemyHealth = enemyStats.curHp;
        PlayerHealth = playerStats.curHp;
        checkSpeed();
    }
	
	// Update is called once per frame
	void Update () {
	    if(PlayerDataComplete && EnemyDataComplete) //check these conditions so that we can run the attack calls
        {
            checkSpeed();   //checks the speed to see who attacks first
            if (Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)    //player attacks first so we do this
            {
                //apply damage to enemy if there is any and check to make sure the enemy is alive still
                damage_Player_to_Enemy();           

                //then heal the player
                healPlayer();

                //apply damage to player and check if the player is still alive
                if (enemyStats.curHp > 0)   //checks to see that the enemy is still alive before the enemy attacks
                {
                    damage_Enemy_to_Player();

                    //then heal the enemy
                    healEnemy();
                }


            }
            else if(!Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)                      //enemy attacks first so we do this
            {
                //apply damage to the player
                damage_Enemy_to_Player();

                //heal the enemy
                healEnemy();

                if (playerStats.curHp > 0)
                {
                    //apply damage to the enemy
                    damage_Player_to_Enemy();

                    //heal the player
                    healPlayer();
                }
            }
            PlayerDataComplete = false;
            EnemyDataComplete = false;
        }
	}

    public void checkSpeed()
    {
        if(playerStats.Speed >= enemyStats.Speed)
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

    public void ApplyDamage()
    {

    }

    public void ApplyEffects()
    {

    }

    private void healPlayer()
    {
        if (playerStats.curHp + PlayerHeal > playerStats.maxHP)
        {
            playerStats.curHp = playerStats.maxHP;
        }
        else
        {
            playerStats.curHp += PlayerHeal;
        }
    }

    private void damage_Player_to_Enemy()
    {
        float newValue;
        if (PlayerDamage > 0)
        {
            enemyStats.curHp -= PlayerDamage;
            Debug.Log(enemyStats.curHp + "/" + enemyStats.maxHP);
            newValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;   //have to move the enemy health bar to this new value in range 0 to 1
            Debug.Log("new value: " + newValue);
            enemyHealthBar.value = newValue;
        }
    }

    private void damage_Enemy_to_Player()
    {
        float newValue;
        if (EnemyDamage > 0)
        {
            playerStats.curHp -= EnemyDamage;
            Debug.Log(playerStats.curHp + "/" + playerStats.maxHP);
            newValue = (float)playerStats.curHp / (float)playerStats.maxHP;
            Debug.Log("new value: " + newValue);
            playerHealthBar.value = newValue;
        }

    }

    private void healEnemy()
    {
        if (enemyStats.curHp + EnemyHeal > enemyStats.maxHP)
        {
            enemyStats.curHp = enemyStats.maxHP;
        }
        else
        {
            enemyStats.curHp += EnemyHeal;
        }
    }

    private bool checkDead()
    {
        bool isDead = false;
        if (playerStats.curHp <= 0)
        {
            //the player is dead
            isDead =  true;
        }if(enemyStats.curHp <= 0) {
            //the ennemy is dead
            isDead = true;
        }
        if (isDead)
        {
            PlayerDataComplete = false;
            EnemyDataComplete = false;
        }
        return isDead;
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
