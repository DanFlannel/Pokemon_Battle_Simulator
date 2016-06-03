using UnityEngine;
using System.Collections;

/// <summary>
/// This class handles all of the enemy pokemon's calculations and variables associated with the pokemon. This is essentially
/// the master class for that pokemon in terms of its physical attributes. 
/// </summary>
namespace EnemyPokemon
{
    public class EnemyPokemonHandler : MonoBehaviour
    {

        private int levelBonus { get; set; }

        public int PokemonID { get; private set; }
        public string PokemonName { get; private set; }

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
        public float cachedDamage { get; set; }
        public int sleepDuration { get; set; }
        public int confusedDuration { get; set; }
        public bool hasSubstitute { get; set; }
        public int substitueHP { get; set; }
        public bool hasLightScreen { get; set; }
        public int lightScreenDuration { get; set; }
        public string cachedAttackName { get; set; }

        private PokemonLibrary pl;
        public GifRenderer gif;

        private int temp { get; set; }
        public int curHp { get; set; }
        public int maxHP { get; private set; }


        // Use this for initialization
        void Start()
        {
            temp = Random.Range(0, 151);
            pl = GameObject.FindGameObjectWithTag("Library").GetComponent<PokemonLibrary>();
            gif = this.GetComponent<GifRenderer>();
            if(gif == null)
            {
                Debug.LogError("GifRenderer not found on Enemy Pokemon");
            }
            Init();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Init()
        {
            attack_Stage = defense_Stage = speed_stage = spAttack_Stage = spDefense_stage = 0;

            isChargingAttack = false;
            isUnderground = false;
            canAttack = true;
            canBeAttacked = true;
            isConfused = false;
            isSleeping = false;
            hasAttacked = false;
            isStunned = false;
            isFlinched = false;
            isBurned = false;
            isFrozen = false;
            isFlinched = false;
            hasSubstitute = false;
            hasLightScreen = false;

            cachedDamage = 0;
            sleepDuration = 0;
            substitueHP = 0;
            lightScreenDuration = 0;
            confusedDuration = 0;


            GetPokemonBaseData(temp);
            StatsBasedOffLevel();
            maxHP = HP;
            curHp = maxHP;
            //Debug.Log("Scene has now loaded with enemy: " + PokemonName);

        }

        private void GetPokemonBaseData(int id)
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




    }
}