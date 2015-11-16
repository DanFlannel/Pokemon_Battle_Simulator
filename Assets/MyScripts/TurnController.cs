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
	    if(PlayerDataComplete && EnemyDataComplete)
        {
            float newValue;
            if (Player_AttacksFirst)    //player attacks first so we do this
            {
                if (PlayerDamage > 0)
                {
                    enemyStats.curHp -= PlayerDamage;
                    Debug.Log(enemyStats.curHp + "/" + enemyStats.maxHP);
                }
                newValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;   //have to move the enemy health bar to this new value in range 0 to 1
                Debug.Log("new value: " + newValue);
                enemyHealthBar.value = newValue;

                if (EnemyDamage > 0)
                {
                    playerStats.curHp -= EnemyDamage;
                    Debug.Log(playerStats.curHp + "/" + playerStats.maxHP);
                }
                newValue = (float)playerStats.curHp / (float)playerStats.maxHP;
                Debug.Log("new value: " + newValue);
                playerHealthBar.value = newValue;
            }
            else                        //enemy attacks first so we do this
            {
                if (EnemyDamage > 0)
                {
                    playerStats.curHp -= EnemyDamage;
                    Debug.Log(playerStats.curHp + "/" + playerStats.maxHP);
                }
                newValue = (float)playerStats.curHp / (float)playerStats.maxHP;
                Debug.Log("new value: " + newValue);
                playerHealthBar.value = newValue;

                if (PlayerDamage > 0)
                {
                    enemyStats.curHp -= PlayerDamage;
                    Debug.Log(enemyStats.curHp + "/" + enemyStats.maxHP);
                }
                newValue = (float)enemyStats.curHp / (float)enemyStats.maxHP;   //have to move the enemy health bar to this new value in range 0 to 1
                Debug.Log("new value: " + newValue);
                enemyHealthBar.value = newValue;
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

    IEnumerator Wait(float duration)
    {
        yield return new WaitForSeconds(duration);
    }
}
