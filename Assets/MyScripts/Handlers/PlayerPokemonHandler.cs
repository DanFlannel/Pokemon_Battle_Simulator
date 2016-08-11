using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerPokemonHandler : MonoBehaviour
{
    PokemonEntity testPokemon;
    public List<PokemonEntity> playerTeam = new List<PokemonEntity>();
    private int curPlayerPokemonIndex;
    [HideInInspector]
    public readonly int TEAMLENGTH = 6;

    public int PokemonID;
    public string PokemonName;
    public string attack1;
    public string attack2;
    public string attack3;
    public string attack4;

    private int baseHP;
    private int baseAttack;
    private int baseDefense;
    private int baseSpecial_Attack;
    private int baseSpecial_Defense;
    private int baseSpeed;
    private int levelBonus;

    private bool CanEvolve; //not sure the use of this but its there
    public nonVolitileStatusEffects non_volitile_status;

    public int Level;

    public int Attack;
    public int attack_Stage = 0;
    public int Defense;
    public int defense_Stage = 0;
    public int Special_Attack;
    public int spAttack_Stage = 0;
    public int Special_Defense;
    public int spDefense_stage = 0;
    public int Speed;
    public int speed_stage = 0;

    public string Type1;
    public string Type2;

    public bool isChargingAttack = false;
    public bool isUnderground = false;
    public bool canAttack = true;
    public bool canBeAttacked = true;
    public bool isConfused = false;

    public bool hasAttacked = false;
    public bool isStunned = false;
    public bool isFlinched = false;
    public bool isFlying = false;

    public float cachedDamage = 0;

    public bool hasSubstitute = false;
    public bool hasLightScreen = false;
    public int lightScreenDuration = 0;
    public int substitueHP = 0;

    public int sleepDuration = 0;
    public int confusedDuration = 0;

    public string cachedAttackName;

    private PokemonLibrary pokeDex;
    private GifRenderer gif;
    private GUIScript gui;
    private PokemonAttacks attackData;
    private TurnController tc;

    private int tempID;
    public int curHp;
    public int maxHP;
    private int GifID;

    private bool InitPokemonData;

    // Use this for initialization
    void Start()
    {
        tempID = Random.Range(0, 151);
        attackData = GameObject.FindGameObjectWithTag("AttackData").GetComponent<PokemonAttacks>();
        pokeDex = GameObject.FindGameObjectWithTag("Library").GetComponent<PokemonLibrary>();
        gif = this.GetComponent<GifRenderer>();
        gui = GameObject.FindGameObjectWithTag("GUIScripts").GetComponent<GUIScript>();
        tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
        Init();
    }

    private void Init()
    {
        InitPokemonData = true;
        for (int i = 0; i < TEAMLENGTH; i++)
        {
            Level = 100;
            tempID = Random.Range(0, 151);
            FetchPokemonBaseStats(tempID);
            CreatePokemonStruct();
            //DebugPokemonStruct();
        }
        curPlayerPokemonIndex = 0;
        OnChangePokemon(curPlayerPokemonIndex);
        gui.updatePokemonNames(playerTeam);
        // Debug.Log("Scene has now loaded with Player: " + PokemonName);
    }

    private void FetchPokemonBaseStats(int id)
    {
        PokemonName = pokeDex.GetName(id);
        PokemonID = id;
        GifID = PokemonID + 1;

        //gif.ChangeSprite(PokemonName, GifID);

        baseHP = pokeDex.GetHP(id);
        baseAttack = pokeDex.GetAttack(id);
        baseDefense = pokeDex.GetDefense(id);
        baseSpecial_Attack = pokeDex.GetSpecialAttack(id);
        baseSpecial_Defense = pokeDex.GetSpecialDefense(id);
        baseSpeed = pokeDex.GetSpeed(id);
        CanEvolve = pokeDex.GetCanEvolve(id);
        Type1 = pokeDex.GetType1(id);
        Type2 = pokeDex.GetType2(id);
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
        //DebugPokemonStruct();
        playerTeam.Add(testPokemon);
    }

    private void DebugPokemonStruct()
    {
        Debug.Log(string.Format("Name {0} ID {1} MaxHP {2} Attack {3} Defense {4} SP Attack {5}",
            testPokemon.Name, testPokemon.ID, testPokemon.maxHP, testPokemon.Attack,
            testPokemon.Defense, testPokemon.Special_Attack));
        Debug.Log(string.Format("Attack 1: {0} Attack 2: {1} Attack 3: {2} Attack 4: {3}",
            testPokemon.Attack1, testPokemon.Attack2, testPokemon.Attack3, testPokemon.Attack4));
    }

    /// <summary>
    /// Changes the pokemon stats and attacks to the one that is currently out
    /// </summary>
    /// <param name="index">index number in the team</param>
    private void OnChangePokemon(int index)
    {
        if (!InitPokemonData)
        {
            //saves the current pokemon data if we have a team already
            SaveStats(playerTeam[curPlayerPokemonIndex]);
        }
        //update our current index after saving the data
        curPlayerPokemonIndex = index;
        //update the stats first so that the ID and Name are right before calling
        //to change the sprite
        UpdateStats(playerTeam[curPlayerPokemonIndex]);
        GifID = PokemonID + 1;
        gif.ChangeSprite(PokemonName, GifID);
        gui.UpdatePlayerInfo();
        tc.setPlayerHealthBar();

        InitPokemonData = false;
    }

    /// <summary>
    /// Updates the stats in this script to be the ones of the current Pokemon's
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

        Attack = pk.Attack;
        Defense = pk.Defense;
        Special_Attack = pk.Special_Attack;
        Special_Defense = pk.Special_Defense;
        Speed = pk.Speed;

        //Ensure that we don't mess up the base stats here
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
        isStunned = pk.isStunned;

        isFlinched = false;
        isFlying = false;

        sleepDuration = pk.sleepDuration;
        confusedDuration = pk.confusedDuration;
    }

    /// <summary>
    /// Saves the current stats to the pokemon so that when we 
    /// Switch back, the pokemon doesnt get reset
    /// </summary>
    /// <param name="pk">Pokemon To Save</param>
    private void SaveStats(PokemonEntity pk)
    {
        //PokemonID    PokemonName    Level    MaxHP    Type1    Type2
        //Attack1    Attack2    Attack3    Attack4    Attack    Defense
        //SpAttack    SpDefense    Speed    
        pk.curHp = curHp;

        //Unsure if we keep stage changes across pokemon changes
        //probably going to have to generate another method to handle special cases.
        pk.attack_Stage = attack_Stage;
        pk.defense_Stage = defense_Stage;
        pk.spAttack_Stage = spAttack_Stage;
        pk.spDefense_stage = spDefense_stage;
        pk.speed_stage = speed_stage;

        pk.sleepDuration = sleepDuration;

        pk.isConfused = false;
        pk.confusedDuration = 0;

        pk.status_A = non_volitile_status;
    }

    /// <summary>
    /// Just a test case for swapping pokemon and making sure that they change 
    /// the proper variables
    /// </summary>
    public void SwapPlayerPokemon(int swapIndex)
    {
        OnChangePokemon(swapIndex);
    }
}