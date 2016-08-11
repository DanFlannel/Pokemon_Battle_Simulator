using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using CoroutineQueueHelper;
using System.Threading;
/// <summary>
/// This class handles how the turn is played out. 
/// It handles doing attack damage to both the enemy and player
/// It has a queue that handles all of the different Ienumerations
/// The IEnumerations are passed in to move the health bars and display
/// the appropriate battle text
/// </summary>
public class TurnController : CoroutineQueueHelper.CoroutineList
{
    public GameObject textPanel;
    public Text moveText;

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
    public AttackType Player_AttackType;
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
    public AttackType Enemy_AttackType;
    public bool Enemy_AttacksFirst = false;
    public bool EnemyDataComplete = false;
    public bool Enemy_StatusMove = false;
    public bool Enemy_AppliedStatusEffect = false;
    public Slider enemyHealthBar;

    [Header("Type A Conditions")]
    public nonVolitileStatusEffects playerNVStatus;
    public nonVolitileStatusEffects enemyNVStatus;

    public int playerNVDur;
    public int enemyNVDur;

    [Header("Type B Conditions")]
    public bool playerConfused;
    public int playerConfusedDur;

    public bool enemyconfused;
    public int enemyConfusedDur;

    [Header("Type C Conditions")]
    //....

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


    //---------------------------------//
    private bool hasInitalized = false;

    private int HelathBarChangeDuration = 1;

    private PlayerPokemonHandler playerStats;
    private EnemyPokemonHandler enemyStats;
    private GUIScript gui;
    private Attack_Switch_Case attack_Switch_Case;

    // Use this for initialization
    void Awake()
    {
        textPanel.SetActive(false);
        c_Queue = this.GetComponent<CoroutineList>();
        Init();
    }

    private void Init()
    {
        enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        attack_Switch_Case = GameObject.FindGameObjectWithTag("Attacks").GetComponent<Attack_Switch_Case>();
    }

    private void DelayInit()
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

            c_Queue.AddCoroutineToQueue(DisableTextPanel());
            c_Queue.StartQueue();
        }
    }

    /// <summary>
    /// Checks the speed and decide who attacks first
    /// </summary>
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

    //..

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

    //..

    private void player_DoMove()
    {
        if (checkPlayerNVStatus())
        {
            Debug.Log("Cant move because of status");
            return;
        }

        if (PlayerMissed)
        {
            attackText(true);
            string text = playerStats.PokemonName + " missed!";
            c_Queue.AddCoroutineToQueue(DisplayText(text));
            return;
        }

        if (Player_AttackType == AttackType.status)
        {
            Debug.Log("Player is doing status move");
            attackText(true);
            attack_Switch_Case.statusAttacks(Player_attackName, false);
            return;
        }

        Debug.Log("Player is doing move");
        attackText(true);
        if (PlayerDamage > 0)
        {
            enemyStats.curHp -= PlayerDamage;
            changeEnemyHealthBar();
        }
    }

    private void enemy_DoMove()
    {
        if (checkEnemyNVStatus())
        {
            Debug.Log("Cant move because of status");
            return;
        }

        if (EnemyMissed)
        {
            attackText(false);
            string text = enemyStats.PokemonName + " missed!";
            c_Queue.AddCoroutineToQueue(DisplayText(text));
            return;
        }

        if(Enemy_AttackType == AttackType.status)
        {
            attackText(false);
            attack_Switch_Case.statusAttacks(Enemy_attackName, false);
            return;
        }

        Debug.Log("Enemy is doing move");
        attackText(false);
        if (EnemyDamage > 0)
        {
            playerStats.curHp -= EnemyDamage;
            changePlayerHealthBar();

        }
    }

    //..

    /// <summary>
    /// These are the status effects the impead on the ability to move
    /// </summary>
    private bool checkEnemyNVStatus()
    {
        if (enemyNVStatus != nonVolitileStatusEffects.none)
        {
            if (enemyNVStatus == nonVolitileStatusEffects.paralized)
            {
                int rnd = UnityEngine.Random.Range(1, 4);
                if(rnd == 1)
                {
                    string text = enemyStats.PokemonName + " is Paralized!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }
            }
            if (enemyNVStatus == nonVolitileStatusEffects.sleep)
            {
                enemyNVDur--;
                string text = "";
                if(enemyNVDur == 0)
                {
                    text = enemyStats.PokemonName + " woke up!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                }
                else {
                    text = enemyStats.PokemonName + " is fast asleep!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }
                
            }
            if (enemyNVStatus == nonVolitileStatusEffects.frozen)
            {
                int rnd = UnityEngine.Random.Range(1, 10);
                if (rnd >= 2)
                {
                    string text = enemyStats.PokemonName + " is Frozen!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }
                else
                {
                    enemyNVStatus = nonVolitileStatusEffects.none;
                    string text = enemyStats.PokemonName + " thawed out!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    
                }
            }
        }
        return false;
    }

    private bool checkPlayerNVStatus()
    {
        if (playerNVStatus != nonVolitileStatusEffects.none)
        {
            if (playerNVStatus == nonVolitileStatusEffects.paralized)
            {
                int rnd = UnityEngine.Random.Range(1, 4);
                if (rnd == 1)
                {
                    string text = playerStats.PokemonName + " is Paralized!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }
            }
            if (playerNVStatus == nonVolitileStatusEffects.sleep)
            {
                playerNVDur--;
                string text = "";
                if (playerNVDur == 0)
                {
                    text = playerStats.PokemonName + " woke up!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                }
                else {
                    text = playerStats.PokemonName + " is fast asleep!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }

            }
            if (playerNVStatus == nonVolitileStatusEffects.frozen)
            {
                int rnd = UnityEngine.Random.Range(1, 10);
                if (rnd >= 2)
                {
                    string text = playerStats.PokemonName + " is Frozen!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));
                    return true;
                }
                else
                {
                    playerNVStatus = nonVolitileStatusEffects.none;
                    string text = playerStats.PokemonName + " thawed out!";
                    c_Queue.AddCoroutineToQueue(DisplayText(text));

                }
            }
        }
        return false;
    }

    //..

    private void damage_Enemy_Effects()
    {
        if (enemyNVStatus != nonVolitileStatusEffects.none)
        {
            if(enemyNVStatus == nonVolitileStatusEffects.burned || enemyNVStatus == nonVolitileStatusEffects.poisioned || enemyNVStatus == nonVolitileStatusEffects.toxic)
            {
                Debug.Log("Dealing non volitile status effect");
                float one_eight = enemyStats.maxHP / 8f;
                if(enemyNVStatus == nonVolitileStatusEffects.toxic)
                {
                    enemyNVDur++;
                    one_eight *= enemyNVDur;
                }
                enemyStats.curHp -= (int)one_eight;
                dmgStatusText(false);
                changeEnemyHealthBar();
            }
        }
    }

    private void damage_Player_Effects()
    {
        if (playerNVStatus != nonVolitileStatusEffects.none)
        {
            if (playerNVStatus == nonVolitileStatusEffects.burned || playerNVStatus == nonVolitileStatusEffects.poisioned || playerNVStatus == nonVolitileStatusEffects.toxic)
            {
                Debug.Log("Dealing non volitile status effect");
                float one_eight = playerStats.maxHP / 8f;
                if (playerNVStatus == nonVolitileStatusEffects.toxic)
                {
                    playerNVDur++;
                    one_eight *= playerNVDur;
                }
                playerStats.curHp -= (int)one_eight;
                dmgStatusText(true);
                changePlayerHealthBar();
            }
        }
    }

    //..

    private void changeEnemyHealthBar()
    {
        float newSliderValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;
        c_Queue.AddCoroutineToQueue((AnimateSliderOverTime(1, enemyHealthBar, newSliderValue)));
    }

    public void setEnemyHealthBar()
    {
        float newSliderValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;
        enemyHealthBar.value = newSliderValue;
    }

    private void changePlayerHealthBar()
    {
        float newSliderValue = (float)playerStats.curHp / (float)playerStats.maxHP;
        c_Queue.AddCoroutineToQueue((AnimateSliderOverTime(1, playerHealthBar, newSliderValue)));
    }

    public void setPlayerHealthBar()
    {
        float newSliderValue = (float)playerStats.curHp / (float)playerStats.maxHP;
        playerHealthBar.value = newSliderValue;
    }

    //..

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
            //the enemy is dead
            isDead = true;
        }
        if (isDead)
        {
            PlayerDataComplete = false;
            EnemyDataComplete = false;
        }
        return isDead;
    }

    //..

    private void PlayerAttacksFirst()
    {
        Debug.Log("Player is attacking first!");
        //apply damage to enemy if there is any and check to make sure the enemy is alive still
        player_DoMove();
        //then heal the player
        healPlayer();

        //then apply damage effects to the player
        damage_Player_Effects();

        //apply damage to player and check if the player is still alive
        if (enemyStats.curHp > 0)   //checks to see that the enemy is still alive before the enemy attacks
        {
                enemy_DoMove();

            gui.updatePlayerHealth();

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
        enemy_DoMove();
        gui.updatePlayerHealth();
        //heal the enemy
        healEnemy();
        //take end of turn damage effects
        damage_Player_Effects();

        if (playerStats.curHp > 0)
        {
            //apply damage to the enemy
            player_DoMove();
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

    //..

    private void attackText(bool isPlayer)
    {
        string pokemon = playerStats.PokemonName;
        string move = Player_attackName;


        if (!isPlayer)
        {
            pokemon = enemyStats.PokemonName;
            move = Enemy_attackName;
        }

        string text = pokemon + " used " + move + ".";
        c_Queue.AddCoroutineToQueue(DisplayText(text));

    }

    private void dmgStatusText(bool isPlayer)
    {
        nonVolitileStatusEffects curNVE = playerNVStatus;
        string pokemon = playerStats.PokemonName;
        if (!isPlayer)
        {
            pokemon = enemyStats.PokemonName;
            curNVE = enemyNVStatus;
        }

        string status = "";

        switch (curNVE)
        {
            case nonVolitileStatusEffects.burned:
                status = "burn";
                break;
            case nonVolitileStatusEffects.poisioned:
                status = "poison";
                break;
            case nonVolitileStatusEffects.toxic:
                status = "toxic";
                break;
        }

        string text = pokemon + " was hurt by " + status + ".";
        c_Queue.AddCoroutineToQueue(DisplayText(text));
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
        //Debug.Log("Animating Healthbar");
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            float curValue = slider.value;
            if (newValue < 0)
            {
                newValue = 0;
            }
            slider.value = Mathf.Lerp(curValue, newValue, lerpValue);
            yield return null;
        }
        //Debug.Log("HelathBarCompelte");
    }

    IEnumerator DisableTextPanel()
    {
        textPanel.SetActive(false);
        yield return null;
    }

    IEnumerator DisplayText(string text_to_display)
    {
        float CHAR_DELAY = .05f;
        float WAIT_TIMER = .5f;

        if (!textPanel.activeInHierarchy)
        {
            textPanel.SetActive(true);
        }
        moveText.text = "";

        Char[] chars = text_to_display.ToCharArray();
        int length = chars.Length;
        string temp, cur = "";

        float seconds = chars.Length * CHAR_DELAY;
        float animationTime = 0;
        float nextLetterTime = 0;

        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            cur = moveText.text;
            int charsUsed = cur.Length - 1;
            int nextChar = charsUsed + 1;
            if (nextLetterTime <= animationTime && nextChar < length)
            {
                nextLetterTime = CHAR_DELAY * charsUsed;
                temp = cur + chars[nextChar];
                cur = temp;

            }
            moveText.text = cur;
            yield return null;
        }

        animationTime = 0;
        while (animationTime < WAIT_TIMER)
        {
            animationTime += Time.deltaTime;
            yield return null;
        }
    }


}

public enum AttackType
{
    status,
    physical,
    special
};
