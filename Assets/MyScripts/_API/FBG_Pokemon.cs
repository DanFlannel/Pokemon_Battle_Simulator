using UnityEngine;
using System.Collections.Generic;

namespace FatBobbyGaming
{
    public class FBG_Pokemon
    {

        //*****************************************************//
        //these are the only variables that can be passed in
        //in order to generate the rest of the stats for the pokemon

        public nonVolitileStatusEffects status_A;
        public pokemonPosition position;
        public attackStatus atkStatus;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }

        //made these private to protect them
        private int baseHP { get; set; }
        private int baseAttack { get; set; }
        private int baseDefense { get; set; }
        private int baseSpecial_Attack { get; set; }
        private int baseSpecial_Defense { get; set; }
        private int baseSpeed { get; set; }


        //*******************************************************//

        //using properties to protect how these variables are set
        public string type1 { get; private set; }
        public string type2 { get; private set; }
        public dmgMult damageMultiplier { get; private set; }

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

        public string Attack1 { get; private set; }
        public string Attack2 { get; private set; }
        public string Attack3 { get; private set; }
        public string Attack4 { get; private set; }

        public bool isChargingAttack { get; set; }
        public bool isUnderground { get; set; }
        public bool canAttack { get; set; }
        public bool canBeAttacked { get; set; }

        public bool hasAttacked { get; set; }
        public bool isFlying { get; set; }

        public float cachedDamage { get; set; }

        public int curHp { get; set; }
        public int maxHP { get; private set; }

        private List<int> randomNumbers = new List<int>();

        public FBG_Pokemon(int m_Level, corePokemonData data, List<attackIndex> attackMoves)
        {
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
            position = pokemonPosition.normal;
            atkStatus = attackStatus.Normal;

            //need to set these to something before updaing them
            setStages();
            setBools();
            generatePokemonStats(Level);
            randomNumbers = generateRandomList(attackMoves.Count);
            SetAttacks(attackMoves, randomNumbers);

        }

        private void setBools()
        {
            isChargingAttack = false;
            hasAttacked = false;
            isFlying = false;
        }

        private void setStages()
        {
            attack_Stage = 0;
            defense_Stage = 0;
            spAttack_Stage = 0;
            spDefense_stage = 0;
            speed_stage = 0;
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
            int levelBonus = Level / (int)Random.Range(16f, 20f); //level bonus is between 17 and 20 to add some slight variation to the maximum base stats

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

        /// <summary>
        /// This takes in a list and generates a random list of unique integers based off that liast
        /// </summary>
        /// <param name="attackMoves"></param>
        /// <returns></returns>
        private List<int> generateRandomList(int totalPossibleMoves)
        {
            List<int> rndNumberList = new List<int>();
            //this is supposed to be a const
            int MOVES = 4;

            //Debug.Log("Range: " + totalPossibleMoves);
            //Debug.Log("List Cout: " + list.Count);
            int numToAdd = -1;
            //if the pokemon has more than 4 moves that it can learn, then we pick from those randomly
            if (totalPossibleMoves > MOVES)
            {
                for (int i = 0; i < MOVES; i++)
                {
                    numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                    while (rndNumberList.Contains(numToAdd))
                    {
                        numToAdd = UnityEngine.Random.Range(0, totalPossibleMoves);
                    }
                    rndNumberList.Add(numToAdd);
                }
            }
            //this ensures that all possible moves are added for pokemon with less than or equal to 4 moves
            else
            {
                Debug.LogWarning(string.Format("{0} total moves {1}", Name, totalPossibleMoves));

                int totalMoves = 0;
                for (int i = 0; i < MOVES; i++)
                {
                    if (totalMoves < totalPossibleMoves)
                    {
                        numToAdd = i;
                        totalMoves++;
                    }
                    else
                    {
                        numToAdd = UnityEngine.Random.Range(0, totalMoves);
                    }
                    rndNumberList.Add(numToAdd);
                }
            }
            //Debug.Log(string.Format("Name: {0} Total: {1} indexes: {2} {3} {4} {5}",
            //    Name, totalPossibleMoves,
            //    rndNumberList[0], rndNumberList[1], rndNumberList[2], rndNumberList[3]));
            return rndNumberList;
        }

        //We will see if we can do something about that hardcoding...
        private void SetAttacks(List<attackIndex> attackMoves, List<int> rndNums)
        {
            Attack1 = attackMoves[rndNums[0]].attack.name;
            Attack2 = attackMoves[rndNums[1]].attack.name;
            Attack3 = attackMoves[rndNums[2]].attack.name;
            Attack4 = attackMoves[rndNums[3]].attack.name;
        }
    }
}