using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This class handles all of the enemy pokemon's calculations and variables associated with the pokemon. This is essentially
/// the master class for that pokemon in terms of its physical attributes. 
/// </summary>
public class EnemyPokemonHandler : MonoBehaviour
{
    PokemonEntity testPokemon;
    List<PokemonEntity> enemyTeam = new List<PokemonEntity>();
    public int curEnemyPokemonIndex;
    private const int TEAMLENGTH = 6;

    private int levelBonus { get; set; }

    public int PokemonID;
    public string PokemonName { get; private set; }

    public string attack1;
    public string attack2;
    public string attack3;
    public string attack4;

    private int baseHP { get; set; }
    private int baseAttack { get; set; }
    private int baseDefense { get; set; }
    private int baseSpecial_Attack { get; set; }
    private int baseSpecial_Defense { get; set; }
    private int baseSpeed { get; set; }
    private bool CanEvolve { get; set; }

    private int HP { get; set; }
    public int Level { get; private set; }
    public int Attack { get; private set; }
    public int attack_Stage { get; set; }

    public int Defense { get; private set; }
    public int defense_Stage { get; set; }
    public int Special_Attack { get; private set; }
    public int spAttack_Stage { get; set; }
    public int Special_Defense { get; private set; }
    public int spDefense_stage { get; set; }
    public int Speed { get; private set; }
    public int speed_stage { get; set; }

    public string Type1 { get; private set; }
    public string Type2 { get; private set; }

    public bool isChargingAttack { get; set; }
    public bool isUnderground { get; set; }
    public bool canAttack { get; set; }
    public bool canBeAttacked { get; set; }
    public bool isConfused { get; set; }
    public bool isSleeping { get; set; }
    public bool hasAttacked { get; set; }
    public bool isStunned { get; set; }
    public bool isFlinched { get; set; }
    public bool isBurned { get; set; }
    public bool isFrozen { get; set; }
    public bool isFlying { get; set; }
    public bool isParalized { get; set; }
    public float cachedDamage { get; set; }
    public int sleepDuration { get; set; }
    public int confusedDuration { get; set; }
    public bool hasSubstitute { get; set; }
    public int substitueHP { get; set; }
    public bool hasLightScreen { get; set; }
    public int lightScreenDuration { get; set; }
    public string cachedAttackName { get; set; }

    private PokemonLibrary pl;
    private GifRenderer gif;
    public GUIScript gui;
    private PokemonAttacks attackData;

    private int GifID;

    private int tempID { get; set; }
    public int curHp { get; set; }
    public int maxHP { get; private set; }


    // Use this for initialization
    void Start()
    {
        tempID = Random.Range(0, 151);
        attackData = GameObject.FindGameObjectWithTag("AttackData").GetComponent<PokemonAttacks>();
        pl = GameObject.FindGameObjectWithTag("Library").GetComponent<PokemonLibrary>();
        gif = this.GetComponent<GifRenderer>();
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < TEAMLENGTH; i++)
        {
            Level = 100;
            tempID = Random.Range(0, 151);
            FetchPokemonBaseStats(tempID);
            CreatePokemonStruct();
            //DebugPokemonStruct();
        }
        OnChangePokemon(0);

    }

    private void FetchPokemonBaseStats(int id)
    {
        PokemonName = pl.GetName(id);
        PokemonID = id + 1;

        gif.ChangeSprite(PokemonName, PokemonID);

        baseHP = pl.GetHP(id);
        baseAttack = pl.GetAttack(id);
        baseDefense = pl.GetDefense(id);
        baseSpecial_Attack = pl.GetSpecialAttack(id);
        baseSpecial_Defense = pl.GetSpecialDefense(id);
        baseSpeed = pl.GetSpeed(id);
        CanEvolve = pl.GetCanEvolve(id);
        Type1 = pl.GetType1(id);
        Type2 = pl.GetType2(id);
    }

    private void StatsBasedOffLevel()
    {

        //max hp = 2* base stat + 110
        //max other stats = 1.79 * stat + 5(levelBonus)
        //level bonus cannot exceed 5
        Level = (int)Random.Range(70f, 100f) + 1;
        Level = 100;
        levelBonus = Level / (int)(Random.Range(16f, 20f) + 1); //level bonus is between 17 and 20 to add some slight variation to the maximum base stats

        float hpLevelCalc = 1f + ((float)Level / 100);
        float levelCalc = .79f + ((float)Level / 100);

        float attackCalc = (float)baseAttack * levelCalc;
        Attack = (int)attackCalc + levelBonus;

        float defenseCalc = (float)baseDefense * levelCalc;
        Defense = (int)defenseCalc + levelBonus;

        float spaBonus = (float)baseSpecial_Attack * levelCalc;
        Special_Attack = (int)spaBonus + levelBonus;

        float spdBonus = (float)baseSpecial_Defense * levelCalc;
        Special_Defense = (int)spdBonus + levelBonus;

        float spBonus = (float)baseSpeed * levelCalc;
        Speed = (int)spBonus + levelBonus;

        float hpBonus = (float)baseHP * hpLevelCalc;
        float hpLevelBonus = 110f * (float)Level / 100f;
        HP = (int)hpBonus + (int)hpLevelBonus;
    }

    public void updateStatStage(string type, float multiplier)
    {
        float levelCalc = .79f + ((float)Level / 100);
        switch (type)
        {
            case "attack":
                float attackCalc = (float)baseAttack * levelCalc;
                Attack = (int)attackCalc + levelBonus;
                Attack = (int)(Attack * multiplier);
                break;
            case "defense":
                float defenseCalc = (float)baseDefense * levelCalc;
                Defense = (int)defenseCalc + levelBonus;
                Defense = (int)(Defense * multiplier);
                break;
            case "spAttack":
                float spaBonus = (float)baseSpecial_Attack * levelCalc;
                Special_Attack = (int)spaBonus + levelBonus;
                Special_Attack = (int)(Special_Attack * multiplier);
                break;
            case "spDefense":
                float spdBonus = (float)baseSpecial_Defense * levelCalc;
                Special_Defense = (int)spdBonus + levelBonus;
                Special_Defense = (int)(Special_Defense * multiplier);
                break;
            case "speed":
                float spBonus = (float)baseSpeed * levelCalc;
                Speed = (int)spBonus + levelBonus;
                Speed = (int)(Speed * multiplier);
                break;
            default:
                Debug.Log("no type " + type + " found");
                break;
        }
    }

    private void CreatePokemonStruct()
    {
        testPokemon = new PokemonEntity(PokemonName, PokemonID, Level, baseHP, baseAttack,
            baseDefense, baseSpecial_Attack, baseSpecial_Defense, baseSpeed, Type1, Type2,
            attackData.masterGetAttacks(PokemonID));
        enemyTeam.Add(testPokemon);
    }

    private void DebugPokemonStruct()
    {
        Debug.Log(string.Format("Name {0} ID {1} MaxHP {2} Attack {3} Defense {4} SP Attack {5} Level {6}",
            testPokemon.Name, testPokemon.ID, testPokemon.maxHP, testPokemon.Attack,
            testPokemon.Defense, testPokemon.Special_Attack, testPokemon.Level));
        Debug.Log(string.Format("Attack 1: {0} Attack 2: {1} Attack 3: {2} Attack 4: {3}",
            testPokemon.Attack1, testPokemon.Attack2, testPokemon.Attack3, testPokemon.Attack4));
    }

    /// <summary>
    /// Changes the pokemon stats and attacks to the one that is currently out
    /// </summary>
    /// <param name="index">index number in the team</param>
    private void OnChangePokemon(int index)
    {
        //update the stats first so that the ID and Name are right before calling
        //to change the sprite
        UpdateStats(enemyTeam[index]);
        GifID = PokemonID + 1;
        gif.ChangeSprite(PokemonName, GifID);
        gui.UpdateEnemyInfo();
    }

    /// <summary>
    /// 
    /// </summary>
    private void UpdateStats(PokemonEntity pk)
    {

        PokemonID = pk.ID;
        PokemonName = pk.Name;
        Level = pk.Level;

        curHp = pk.curHp;
        maxHP = pk.maxHP;

        Type1 = pk.Type1;
        Type2 = pk.Type2;

        attack1 = pk.Attack1;
        attack2 = pk.Attack2;
        attack3 = pk.Attack3;
        attack4 = pk.Attack4;

        //Ensure that we don't mess up the base stats here

        Attack = pk.Attack;
        Defense = pk.Defense;
        Special_Attack = pk.Special_Attack;
        Special_Defense = pk.Special_Defense;
        Speed = pk.Speed;

        attack_Stage = pk.attack_Stage;
        defense_Stage = pk.defense_Stage;
        spAttack_Stage = pk.spAttack_Stage;
        spDefense_stage = pk.spDefense_stage;
        speed_stage = pk.speed_stage;

        isChargingAttack = false;
        isUnderground = false;
        //canAttack = true;
        canBeAttacked = true;
        hasAttacked = false;

        isConfused = pk.isConfused;
        isSleeping = pk.isSleeping;
        isStunned = pk.isStunned;
        isFlinched = false;
        isBurned = pk.isBurned;
        isFlying = false;
        isParalized = pk.isParalized;

        sleepDuration = pk.sleepDuration;
        confusedDuration = pk.confusedDuration;
    }

    /// <summary>
    /// Just a test case for swapping pokemon and making sure that they change 
    /// the proper variables
    /// </summary>
    public void swapEnemyPokemon()
    {

        if (curEnemyPokemonIndex < enemyTeam.Count - 1)
        {
            curEnemyPokemonIndex++;
        }
        else
        {
            curEnemyPokemonIndex = 0;
        }

        OnChangePokemon(curEnemyPokemonIndex);
    }
}