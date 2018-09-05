using System.Collections;
using System.Collections.Generic;
using FBG.Attack;
using FBG.Data;
using FBG.JSON;
using UnityEngine;

namespace FBG.Base
{
    public class PokemonBase
    {
        public TeamPokemon team;

        public List<IEffector> effectors = new List<IEffector>();

        public nonVolitileStatusEffects status_A { get; set; }
        public int nvCurDur { get; set; }
        public int nvDur { get; set; }

        public volitileStatusEffects status_B { get; set; }

        public pokemonPosition position { get; set; }
        public attackStatus atkStatus { get; set; }

        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }

        private int baseHP { get; set; }
        private int baseAttack { get; set; }
        private int baseDefense { get; set; }
        private int baseSpecial_Attack { get; set; }
        private int baseSpecial_Defense { get; set; }
        private int baseSpeed { get; set; }
        private int levelBonus { get; set; }

        public string type1 { get; set; }
        public string type2 { get; set; }
        public dmgMult damageMultiplier { get; set; }

        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int Special_Attack { get; private set; }
        public int Special_Defense { get; private set; }
        public int Speed { get; private set; }

        public int attack_Stage { get; set; }
        public int defense_Stage { get; set; }
        public int spAttack_Stage { get; set; }
        public int spDefense_stage { get; set; }
        public int speed_stage { get; set; }
        public int acc_stage { get; set; }
        public int evasive_stage { get; set; }

        public int critRatio_stage { get; set; }

        public List<string> atkMoves = new List<string>();
        public List<int> maxPP = new List<int>();
        public List<int> curPP = new List<int>();

        public float cachedDamage { get; set; }
        public string nextAttack { get; set; }

        public int curHp { get; set; }
        public int maxHP { get; private set; }

        private List<int> randomNumbers = new List<int>();

        public bool isFlinched { get; set; }
        public bool hasSubstitute;
        public float substituteHealth;

        public PokemonBase(int m_Level, corePokemonData data, List<attackIndex> attackMoves, ref TeamPokemon t)
        {
            team = t;

            Name = data.name;
            ID = data.id;
            Level = m_Level;

            baseHP = data.baseStats.hp;
            baseAttack = data.baseStats.atk;
            baseDefense = data.baseStats.def;
            baseSpecial_Attack = data.baseStats.spa;
            baseSpecial_Defense = data.baseStats.spd;
            baseSpeed = data.baseStats.spe;

            type1 = data.type1;
            type2 = data.type2;

            damageMultiplier = data.damageMultiplier;

            cachedDamage = 0;

            status_A = nonVolitileStatusEffects.none;
            nvDur = 0;

            position = pokemonPosition.normal;
            atkStatus = attackStatus.normal;

            nextAttack = "";

            //need to set these to something before updaing them
            setStages();
            generatePokemonStats(Level);
            randomNumbers = Utilities.generateRandomList(Name, attackMoves.Count, 4);
            //Debug.Log(string.Format("Pokemon: {0} random numbers: {1} {2} {3} {4}", Name, randomNumbers[0], randomNumbers[1], randomNumbers[2], randomNumbers[3]));
            SetAttacks(attackMoves, randomNumbers);
            //Debug.Log(attackMoves.Count);
        }

        private void setStages()
        {
            attack_Stage = 0;
            defense_Stage = 0;
            spAttack_Stage = 0;
            spDefense_stage = 0;
            speed_stage = 0;
            critRatio_stage = 0;
        }

        /// <summary>
        /// Generates all the inital stats for the pokemon
        /// </summary>
        /// <param name="Level">Takes in the Pokemon's level</param>
        private void generatePokemonStats(int Level)
        {
            //Debug.LogWarning("Called Generate Pokemon Stats Level: " + Level);
            //max hp = 2* base stat + 110
            //max other stats = 1.79 * stat + 5(levelBonus)
            //level bonus cannot exceed 5
            levelBonus = Level / (int)UnityEngine.Random.Range(16f, 20f); //level bonus is between 17 and 20 to add some slight variation to the maximum base stats

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
            maxHP = (int)hpBonus + (int)hpLevelBonus;

            curHp = maxHP;
            //Debug.Log("Stats generated");
        }

        private void SetAttacks(List<attackIndex> attackMoves, List<int> rndNums)
        {
            for (int i = 0; i < rndNums.Count; i++)
            {
                atkMoves.Add(attackMoves[rndNums[i]].attack.name);
                maxPP.Add(attackMoves[rndNums[i]].attack.pp);
                curPP.Add(attackMoves[rndNums[i]].attack.pp);
            }
        }

        public void changeCritStage(int add)
        {
            critRatio_stage += add;
            if (critRatio_stage > 6)
            {
                critRatio_stage = 6;
            }
            if (critRatio_stage < 0)
            {
                critRatio_stage = 0;
            }
        }

        public void resetStatStages()
        {
            updateStatStage(Consts.attack, 1);
            attack_Stage = 0;
            updateStatStage(Consts.defense, 1);
            defense_Stage = 0;
            updateStatStage(Consts.spAttack, 1);
            spAttack_Stage = 0;
            updateStatStage(Consts.spDefense, 1);
            spDefense_stage = 0;
            acc_stage = 0;
            evasive_stage = 0;
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
                    //Debug.Log("no type " + type + " found");
                    break;
            }
        }
    }
}
