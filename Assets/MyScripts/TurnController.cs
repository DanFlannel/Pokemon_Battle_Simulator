using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnController : MonoBehaviour {

    private float timerLength = 1f;
    public float timeRemaining = 1f;

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

    [Header("Special Effects")]
    public bool player_leech_seed = false;
    public bool enemy_leech_seed = false;

    public bool player_toxic = false;
    public int player_toxic_mult = 0;

    public bool enemy_toxic = false;
    public int enemy_toxic_mult = 0;

    [Header("Sleep")]
    public bool player_isAsleep = false;
    public int player_sleepDur = 0;

    public bool enemy_isAsleep = false;
    public int enemy_sleepDur = 0;
    private bool hasInitalized = false;


    private PokemonCreatorBack playerStats;
    private PokemonCreatorFront enemyStats;
    private GUIScript gui;


    // Use this for initialization
    void Start () {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<PokemonCreatorFront>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PokemonCreatorBack>();
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        if (playerStats == null) Debug.Log("Cannot find PokemonCreatorBack");
    }

    private void Init()
    {
        if (!hasInitalized)
        {
            EnemyHealth = enemyStats.curHp;
            PlayerHealth = playerStats.curHp;
            checkSpeed();
            hasInitalized = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Init();

	    if(PlayerDataComplete && EnemyDataComplete) //check these conditions so that we can run the attack calls
        {
            checkSpeed();   //checks the speed to see who attacks first
            if (Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)    //player attacks first so we do this
            {
                //apply damage to enemy if there is any and check to make sure the enemy is alive still
                damage_Player_to_Enemy();           

                //then heal the player
                healPlayer();

                //then apply damage effects to the player
                damage_Player_Effects();

                timeRemaining = timerLength;

                //apply damage to player and check if the player is still alive
                if (enemyStats.curHp > 0)   //checks to see that the enemy is still alive before the enemy attacks
                {
                    damage_Enemy_to_Player();
                    gui.updateHealth();

                    //then heal the enemy
                    healEnemy();

                    //then damage effects on the enemy
                    damage_Enemy_Effects();
                }
                else
                {
                    //the enemy died with tthis turn's attack
                }

                if(playerStats.curHp <= 0)
                {
                    //the player died with this turn's attack
                }


            }
            else if(!Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)                      //enemy attacks first so we do this
            {
                timeRemaining = timerLength;
                StartCoroutine(Wait(1));
                //apply damage to the player
                damage_Enemy_to_Player();
                gui.updateHealth();

                //heal the enemy
                healEnemy();

                //take damage effects
                damage_Player_Effects();

                timeRemaining = timerLength;
                StartCoroutine(Wait(1));

                if (playerStats.curHp > 0)
                {
                    //apply damage to the enemy
                    damage_Player_to_Enemy();

                    //heal the player
                    healPlayer();

                    //enemy effects
                    damage_Enemy_Effects();
                }
                else
                {
                    //the player died with this turn's attack
                }

                if(enemyStats.curHp <= 0)
                {
                    //the enemy died with this turn's attack
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

    private void healPlayer()
    {
        if(PlayerHeal <= 0)
        {
            return;
        }

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
        if (PlayerDamage > 0)
        {
            enemyStats.curHp -= PlayerDamage;
            //Debug.Log(enemyStats.curHp + "/" + enemyStats.maxHP);
            changeEnemyHealthBar();
        }
    }

    private void damage_Enemy_to_Player()
    {
        if (EnemyDamage > 0)
        {
            playerStats.curHp -= EnemyDamage;
            //Debug.Log(playerStats.curHp + "/" + playerStats.maxHP);
            changePlayerHealthBar();

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

    private void damage_Enemy_Effects()
    {
        if(!enemy_one_eigth && !enemy_one_sixteen)
        {
            return;
        }
        if (enemy_one_eigth)    //there is an effect that takes away 1/8th of the enemies health
        {
            //base case this is permanant
            if(enemy_one_eigth_duration == -1)
            {
            }
            else if(enemy_one_eigth_duration > 0)
            {
                enemy_one_eigth_duration--; //keep going down each turn based off inital damage
            }
            else if(enemy_one_eigth_duration == 0)  //the effect will end after this turn is over
            {
                enemy_one_eigth = false;
            }
            float damage = (float)enemyStats.maxHP * (float)(1f / 8f);  //gets the exact value
            enemyStats.curHp -= Mathf.RoundToInt(damage);   //round the damage
            changeEnemyHealthBar();       
        }
        if (enemy_one_sixteen)  //there is an effect that is taking away 1/16th of the enemy's health
        {
            //base case this is permanant
            if (enemy_one_sixteenth_duration == -1)
            {
            }
            //this effect is lasting
            else if (enemy_one_sixteenth_duration > 0)
            {
                enemy_one_sixteenth_duration--;
            }
            //this effect is ending
            else if (enemy_one_sixteenth_duration == 0)
            {
                enemy_one_sixteen = false;
            }
            float damage = (float)enemyStats.maxHP * (float)(1f / 16f);  //gets the exact value
            enemyStats.curHp -= Mathf.RoundToInt(damage);   //round the damage and apply it
            changeEnemyHealthBar(); //apply the damage to the health bar
        }
    }

    private void damage_Player_Effects()
    {
        if (!player_one_eigth && !player_one_sixteenth)
        {
            return;
        }
        if (player_one_eigth)    //there is an effect that takes away 1/8th of the enemies health
        {
            //base case this is permanant
            if (player_one_eigth_duration == -1)
            {
            }
            else if (player_one_eigth_duration > 0)
            {
                player_one_eigth_duration--; //keep going down each turn based off inital damage
            }
            else if (player_one_eigth_duration == 0)  //the effect will end after this turn is over
            {
                player_one_eigth = false;
            }
            float damage = (float)playerStats.maxHP * (float)(1f / 8f);  //gets the exact value
            playerStats.curHp -= Mathf.RoundToInt(damage);   //round the damage
            changePlayerHealthBar();
        }
        if (player_one_sixteenth)  //there is an effect that is taking away 1/16th of the enemy's health
        {
            //base case this is permanant
            if (player_one_sixteenth_duration == -1)
            {
            }
            //this effect is lasting
            else if (player_one_sixteenth_duration > 0)
            {
                player_one_sixteenth_duration--;
            }
            //this effect is ending
            else if (player_one_sixteenth_duration == 0)
            {
                player_one_sixteenth = false;
            }
            float damage = (float)playerStats.maxHP * (float)(1f / 16f);  //gets the exact value
            playerStats.curHp -= Mathf.RoundToInt(damage);   //round the damage and apply it
            changePlayerHealthBar(); //apply the damage to the health bar
        }
    }

    private void changeEnemyHealthBar()
    {
        float newValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;
        enemyHealthBar.value = newValue;
    }       //changes the health bar of the enemy

    private void changePlayerHealthBar()
    {
        float newValue = (float)playerStats.curHp / (float)playerStats.maxHP;
        playerHealthBar.value = newValue;
    }      //changes the health bar of the player

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

    private void timerControl()
    {
        timeRemaining -= Time.deltaTime;
    }

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
