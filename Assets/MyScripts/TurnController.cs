using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using CoroutineQueueHelper;
/// <summary>
/// This class handles how the turn is played out. 
/// It handles doing attack damage to both the enemy and player
/// It has a queue that handles all of the different Ienumerations
/// The IEnumerations are passed in to move the health bars and display
/// the appropriate battle text
/// </summary>
public class TurnController : CoroutineQueueHelper.CoroutineList
{
    public CoroutineList c_Queue;
    public bool EndOfTurn;

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

    private int HelathBarChangeDuration = 1;

    private PlayerPokemonHandler playerStats;
    private EnemyPokemonHandler enemyStats;
    private GUIScript gui;




    // Use this for initialization
    void Start()
    {
        c_Queue = this.GetComponent<CoroutineList>();
        Init();
    }

    private void Init()
    {
        Console.WriteLine("PK: Turn Controller: Initalizing");
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
        if (enemyStats == null)
        {
            Console.WriteLine("PK: Turn Controller: Enemy Stats Null");
        }
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
        if (playerStats == null)
        {
            Console.WriteLine("PK: Turn Controller: Player Stats Null");
        }
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        if (gui == null)
        {
            Console.WriteLine("PK: Turn Controller: gui Null");
        }

        if (playerStats == null)
        {
            Debug.Log("Cannot find PokemonCreatorBack");
        }
    }

    private void DelayInit()
    {
        if (!hasInitalized)
        {
            EnemyHealth = enemyStats.curHp;
            PlayerHealth = playerStats.curHp;
            checkSpeed();
            hasInitalized = true;
            Console.WriteLine("PK: Turn Controller Initalized");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DelayInit();

        EndOfTurn = c_Queue.isQueueRunning();
        //If it is a new turn and the player and the enemy have both selected their respective moves
        if (!EndOfTurn && PlayerDataComplete && EnemyDataComplete)
        {
            checkSpeed();   //checks the speed to see who attacks first
            if (Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)
            {
                PlayerAttacksFirst();
            }
            else if (!Player_AttacksFirst && playerStats.curHp > 0 && enemyStats.curHp > 0)
            {
                EnemyAttacksFrist();
            }
            PlayerDataComplete = false;
            EnemyDataComplete = false;
            c_Queue.StartQueue();
        }
    }

    public void checkSpeed()
    {
        if (playerStats.Speed >= enemyStats.Speed)
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
        if (PlayerHeal <= 0)
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
        changePlayerHealthBar();
    }

    private void damage_Player_to_Enemy()
    {
        Debug.Log("Player is doing move");
        if (PlayerDamage > 0)
        {
            enemyStats.curHp -= PlayerDamage;
            //Debug.Log(enemyStats.curHp + "/" + enemyStats.maxHP);
            changeEnemyHealthBar();
        }
    }

    private void damage_Enemy_to_Player()
    {
        Debug.Log("Enemy is doing move");
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
        changeEnemyHealthBar();
    }

    private void damage_Enemy_Effects()
    {
        if (!enemy_one_eigth && !enemy_one_sixteen)
        {
            return;
        }
        Debug.Log("Enemy is taking after turn damage!");
        if (enemy_one_eigth)    //there is an effect that takes away 1/8th of the enemies health
        {
            //base case this is permanant
            if (enemy_one_eigth_duration == -1)
            {
            }
            else if (enemy_one_eigth_duration > 0)
            {
                enemy_one_eigth_duration--; //keep going down each turn based off inital damage
            }
            else if (enemy_one_eigth_duration == 0)  //the effect will end after this turn is over
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
        Debug.Log("Player is taking after turn damage!");
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
        float newSliderValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;
        c_Queue.AddCoroutineToQueue((AnimateSliderOverTime(1, enemyHealthBar, newSliderValue)));
    }

    private void changePlayerHealthBar()
    { 
        //need to put in some logic that changes the health bar over time
        float newSliderValue = (float)playerStats.curHp / (float)playerStats.maxHP;
       c_Queue.AddCoroutineToQueue((AnimateSliderOverTime(1, playerHealthBar, newSliderValue)));
    }

    private bool checkDead()
    {
        bool isDead = false;
        if (playerStats.curHp <= 0)
        {
            //the player is dead
            isDead = true;
        }
        if (enemyStats.curHp <= 0)
        {
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

    private void PlayerAttacksFirst()
    {
        Debug.Log("Player is attacking first!");
        //apply damage to enemy if there is any and check to make sure the enemy is alive still
        damage_Player_to_Enemy();

        //then heal the player
        healPlayer();

        //then apply damage effects to the player
        damage_Player_Effects();

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

        if (playerStats.curHp <= 0)
        {
            //the player died with this turn's attack
        }


    }

    private void EnemyAttacksFrist()
    {
        //apply damage to the player
        damage_Enemy_to_Player();
        gui.updateHealth();
        //heal the enemy
        healEnemy();
        //take end of turn damage effects
        damage_Player_Effects();

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

        if (enemyStats.curHp <= 0)
        {
            //the enemy died with this turn's attack
        }

    }

    /// <summary>
    /// This enumerator lerps unity's sliders, in this case whatever health bar is passed in
    /// </summary>
    /// <param name="seconds">The amount of time to lerp the health bar</param>
    /// <param name="slider">The health bar to lerp</param>
    /// <param name="newValue">The new value of the slider to be changed to</param>
    /// <returns></returns>
    IEnumerator AnimateSliderOverTime(float seconds, Slider slider, float newValue)
    {
        Debug.Log("Animating Healthbar");
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            float curValue = slider.value;
            if(newValue < 0)
            {
                newValue = 0;
            }
            slider.value = Mathf.Lerp(curValue,newValue, lerpValue);
            yield return null;
        }
    }
}
