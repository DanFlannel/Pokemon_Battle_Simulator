using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

namespace FatBobbyGaming
{
    public class FBG_Atk_Calc : MonoBehaviour
    {

        #region Declared Variables
        private PlayerPokemonHandler playerStats;
        private EnemyPokemonHandler enemyStats;
        private PokemonAttacks attacks;
        private PokemonDamageMultipliers damage_mult;
        private Attack_Switch_Case attack_Switch_Case;
        private TurnController tc;

        [Header("Player")]
        public string
            playerAttack1, playerAttack2, playerAttack3, playerAttack4;

        [Header("Enemy")]
        public string
            enemyAttack1, enemyAttack2, enemyAttack3, enemyAttack4;

        public string
            enemyType1, enemyType2;
        public string
            playerType1, playerType2;

        private dmgMult playerDamageMultiplier;
        private dmgMult enemyDamageMultiplier;

        //private GenerateAttacks genAttacks;

        private float attack_mod;
        private float defense_mod;

        private bool isPlayer;
        private string attack_name;
        #endregion

        // Use this for initialization
        void Awake()
        {
            Init();
        }

        private void Init()
        {
            Console.WriteLine("PK : Attack Damage Calculator: Initalizing");
            enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
            if (enemyStats == null)
            {
                Debug.LogError("No Enemy Stats");
            }
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
            if (playerStats == null)
            {
                Debug.Log("No Player Stats");
            }

            attacks = GameObject.FindGameObjectWithTag("AttackData").GetComponent<PokemonAttacks>();
            //genAttacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
            attack_Switch_Case = GameObject.FindGameObjectWithTag("Attacks").GetComponent<Attack_Switch_Case>();
            damage_mult = GameObject.FindGameObjectWithTag("dmg_mult").GetComponent<PokemonDamageMultipliers>();
            tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();
            Console.WriteLine("PK : Attack Damage Calculator: Initalized");
        }

        /// <summary>
        /// Gets the pokemon Types for the Type modifier in the damage calculation
        /// </summary>
        private void getPokemonTypes()
        {
            enemyType1 = playerStats.Type1;
            enemyType2 = playerStats.Type2;

            playerType1 = enemyStats.Type1;
            playerType2 = enemyStats.Type2;
        }

        /// <summary>
        /// Button method, makes sure that if the button was clicked, the isPlayer boolean is true
        /// <param name="p">a boolean to set the isPlayer boolean</param>
        /// </summary>
        public void isPlayer_Button(bool p)
        {
            isPlayer = p;
        }

        /// <summary>
        /// Button method, tells this script which attack has been called based off an integer between 1 and 4
        /// <param name="index">the index of the current attack in the list of attacks</param>
        /// </summary>
        public void get_attack_name(int index)
        {
            if (isPlayer)
            {
                if (index == 1)
                {
                    attack_name = playerStats.attack1;
                }
                if (index == 2)
                {
                    attack_name = playerStats.attack2;
                }
                if (index == 3)
                {
                    attack_name = playerStats.attack3;
                }
                if (index == 4)
                {
                    attack_name = playerStats.attack4;
                }
            }
            else
            {
                index = (int)UnityEngine.Random.Range(1, 5);
                if (index == 1)
                {
                    attack_name = enemyStats.attack1;
                }
                if (index == 2)
                {
                    attack_name = enemyStats.attack2;
                }
                if (index == 3)
                {
                    attack_name = enemyStats.attack3;
                }
                if (index == 4)
                {
                    attack_name = enemyStats.attack4;
                }
            }
        }

        /// <summary>
        /// This method calculates the damage that each attack will do based off the serebii.net damage formula, this does not take into effect the different modifiers or attack calculations each specific move has
        /// <param name="name">takes in the name of the current attack being passed in</param>
        /// <returns>the final basic damage based on all modifiers and multipliers</returns>
        /// </summary>
        public float calculateDamage(string name)
        {
            if (!isPlayer)
            {
                Debug.LogWarning("Enemy Attack Name: " + name);
            }
            else
            {
                Debug.LogWarning("Player Attack Name: " + name);
            }

            float final_damage = 0;
            //Setup for the methods that will get different aspects of the damage calculation
            int attack_index = getAttackListIndex(name);
            //Debug.Log("attack index: " + attack_index);
            string attackType = attacks.attackList[attack_index].type;
            string attackCat = attacks.attackList[attack_index].cat;

            if (attackCat == "Status")   //we do not have to calculate damage for status moves!
            {
                return 0;
            }
            if (baseAttackPower(attack_index) == 0)  //there is no base attack power so we can just return 0, it is a sepcial attack that has its own calculations
            {
                Debug.Log("This attack calclates it's own damage: " + name);
                return 0;
            }

            //Setup for the damage calculations
            set_attack_and_def(attack_index, isPlayer, attackCat);
            if (attack_mod == 0)
            {
                Debug.LogError("Attack modifier is 0");
            }
            if (defense_mod == 0)
            {
                Debug.LogError("Defense modifier is 0");
            }

            float level_mod = levelModifier(isPlayer);
            float att_div_defense = baseAttackPower(attack_index) / defense_mod;

            //Debug.Log("attack div defense: " + baseAttackPower(attack_index) + "/" + defense_mod + " = " + att_div_defense);
            float damage_mod = modifier(attack_index, attackType, isPlayer, name);

            //Damage Calculations here
            final_damage = level_mod;
            //Debug.Log("Damage LEVEL MOD: " + "mod: " + level_mod + " Damage: " + final_damage);
            final_damage *= attack_mod;
            //Debug.Log("Damage * ATTACK MOD: " + "mod: " + attack_mod + " Damage: " + final_damage);
            final_damage *= att_div_defense;
            //Debug.Log("Damage * ATTACK/Defense: " + "mod: " + att_div_defense + " Damage: " + final_damage);
            final_damage /= 50;
            //Debug.Log("Damage /50" + " Damage: " + final_damage);
            final_damage += 2;
            //Debug.Log("Damage +2: " + " Damage: " + final_damage);
            final_damage *= damage_mod;
            //Debug.Log("Damage * damage_Mod: " + "mod: " + damage_mod + " Damage: " + final_damage);
            final_damage = Mathf.Round(final_damage);
            return final_damage;
        }

        /// <summary>
        /// Sets the multiplier Base Power * STAB * Type modifier * Critical * other * randomNum(.85,1)
        /// <param name="index">the index of the move in the attack list</param>
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <param name="name">the name of the move being passed in</param>
        /// <returns>the final value of all the modifiers</returns>
        /// </summary>
        private float modifier(int index, string attackType, bool isPlayer, string name)
        {
            float modifier;
            float stab;
            if (isStab(attackType, isPlayer))
            {
                stab = 1.5f;
            }
            else
            {
                stab = 1f;
            }

            float critical = 1f;
            int critProb = critChance(name);
            //Debug.Log("Crit chance: 1 /" + critProb);
            bool crit = isCrit(critProb);
            if (crit)
            {
                Debug.Log("Critical HIT!");
                critical = 1.5f;
            }
            float rnd = UnityEngine.Random.Range(.85f, 1f);
            float typeMultiplier = getTypeMultiplier(attackType, isPlayer);

            if (typeMultiplier == 0)
            {
                Debug.Log("The pokemon is immune");
                return 0;
            }
            else if (typeMultiplier < 1)
            {
                Debug.Log("The move was not very effective");
            }
            else if (typeMultiplier > 1)
            {
                Debug.LogWarning("The move is super effective");
            }

            modifier = stab * typeMultiplier * critical * rnd;
            //Debug.Log("modifier = Stab: " + stab + " type multiplier: " + typeMultiplier + " critical: " + critical + " randomnum: " + rnd);
            //Debug.Log("modifier: " + modifier + " = Stab: " + stab + " type multiplier: " + typeMultiplier + " critical: " + critical + " randomnum: " + rnd);
            return modifier;
        }

        /// <summary>
        /// Sets the level multiplier (2 * level / 5) + 2
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <returns>a float value of the level modifier</returns>
        /// </summary>
        private float levelModifier(bool isPlayer)
        {
            //(2 * level / 5) + 2
            float level;
            float modifier;
            if (isPlayer)
            {
                level = playerStats.Level;
            }
            else
            {
                level = enemyStats.Level;
            }

            modifier = 2 * level;
            modifier /= 5;
            modifier += 2;

            //Debug.Log("Level modifier: " + modifier);
            return modifier;
        }

        /// <summary>
        ///  Sets the player and enemy attack and defense based on the attack category (physical, status, special)
        /// <param name="attack_index">the index of the move in the list of attacks</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <param name="attackCat">the category of the attack move, either special, status, or physical</param>
        /// </summary>
        private void set_attack_and_def(int attack_index, bool isPlayer, string attackCat)
        {
            if (attackCat == attacks.Special)                  //we are calculating a special attack
            {
                if (isPlayer)                                   //the player is using a special attack
                {
                    attack_mod = playerStats.Special_Attack;
                    defense_mod = enemyStats.Special_Defense;
                }
                else                                            //the enemy is using a special attack
                {
                    attack_mod = enemyStats.Special_Attack;
                    defense_mod = playerStats.Special_Defense;
                }
                //Debug.Log("SpAttack: " + attack_mod);
                //Debug.Log("SpDefense: " + defense_mod);
            }
            if (attackCat == attacks.Physical)                  //we are calculating a physical attack
            {
                if (isPlayer)                                   //the player is using a physical attack
                {
                    attack_mod = playerStats.Attack;
                    defense_mod = enemyStats.Defense;
                }
                else                                            //the enemy is using a physical attack
                {
                    attack_mod = enemyStats.Attack;
                    defense_mod = playerStats.Defense;
                }
                //Debug.Log("Attack: " + attack_mod);
                //Debug.Log("Defense: " + defense_mod);
            }
        }

        /// <summary>
        /// Takes the index or location of the pokemon in the attack list so we can fetch the base attack power for that attack
        /// <param name="index">the index of the move in the list of attacks</param>
        /// <returns> the base damage of the move</returns>
        /// </summary>
        private float baseAttackPower(int index)
        {
            float base_damage = 0;
            base_damage = (float)attacks.attackList[index].power;
            //Debug.Log("Base Damage: " + base_damage);
            return base_damage;
        }

        /// <summary>
        ///  checks if the current attack is a STAB type attack or same type attack
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <returns>a boolean that is true if the move is a stab type move or not</returns>
        /// </summary>
        private bool isStab(string attackType, bool isPlayer)
        {
            getPokemonTypes();
            if (isPlayer)
            {
                if (attackType == playerType1 || attackType == playerType2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (attackType == enemyType1 || attackType == enemyType2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Gets the index of the pokemon in the attack list so we can use this index later rather than having to get it multiple times
        /// <param name="name">the name of the move being passed in</param>
        /// <returns>the index of the move being passed in, within the attack list</returns>
        /// </summary>
        public int getAttackListIndex(string name)
        {
            //Debug.Log("called Attack List Index");
            for (int i = 0; i < attacks.attackList.Count; i++)
            {
                if (name.ToLower() == attacks.attackList[i].name.ToLower())
                {
                    //Debug.Log("Calculating Damage for " + name);
                    return i;
                }
            }
            Debug.Log("No Attack with name " + name + " found");
            return 0;
        }

        /// <summary>
        ///  Returns the type modifier for the attack based off of the pokemon's damange multiplier for that specific type of attack 
        /// <param name="attackType">the attack type or the mvoe being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <returns>the multiplier as a float</returns>
        /// </summary>
        private float getTypeMultiplier(string attackType, bool isPlayer)
        {
            float modifier = 0f;

            if (isPlayer)
            {
                int index = enemyStats.PokemonID;
                if (enemyStats.PokemonName == damage_mult.master_list[index].name)
                {
                    modifier = fetchAttackTypeIndex(attackType, index);
                }
                else
                {
                    Debug.LogError("name did not match when searching for multiplier");
                    Debug.LogError("searched for " + enemyStats.PokemonName);
                    Debug.LogError("found " + damage_mult.master_list[index].name);
                }
            }
            else
            {
                int index = playerStats.PokemonID;
                if (playerStats.PokemonName == damage_mult.master_list[index].name)
                {
                    modifier = fetchAttackTypeIndex(attackType, index);
                }
                else
                {
                    Debug.LogError("name did not match when searching for multiplier");
                    Debug.LogError("searched for " + playerStats.PokemonName);
                    Debug.LogError("found " + damage_mult.master_list[index].name);
                }
            }
            //Debug.Log("Type modifier for attacs is: " + modifier);
            return modifier;
        }

        /// <summary>
        /// Uses the index to get the type multiplier from the master list in the damage multiplier class
        /// <param name="attackType">the type of the attack</param>
        /// <param name="index">the index of the attack move in the attack list</param>
        /// <returns>the modifier of the attack move based off its attack type</returns>
        /// </summary>
        private float fetchAttackTypeIndex(string attackType, int index)
        {
            float modifier = 0f;
            attackType = attackType.ToLower();
            switch (attackType)
            {
                case "normal":
                    modifier = damage_mult.master_list[index].damage.normal;
                    break;
                case "fighting":
                    modifier = damage_mult.master_list[index].damage.fighting;
                    break;
                case "flying":
                    modifier = damage_mult.master_list[index].damage.flying;
                    break;
                case "poison":
                    modifier = damage_mult.master_list[index].damage.poison;
                    break;
                case "ground":
                    modifier = damage_mult.master_list[index].damage.ground;
                    break;
                case "rock":
                    modifier = damage_mult.master_list[index].damage.rock;
                    break;
                case "bug":
                    modifier = damage_mult.master_list[index].damage.bug;
                    break;
                case "ghost":
                    modifier = damage_mult.master_list[index].damage.ghost;
                    break;
                case "steel":
                    modifier = damage_mult.master_list[index].damage.steel;
                    break;
                case "fire":
                    modifier = damage_mult.master_list[index].damage.fire;
                    break;
                case "water":
                    modifier = damage_mult.master_list[index].damage.water;
                    break;
                case "grass":
                    modifier = damage_mult.master_list[index].damage.grass;
                    break;
                case "electric":
                    modifier = damage_mult.master_list[index].damage.electric;
                    break;
                case "psychic":
                    modifier = damage_mult.master_list[index].damage.psychic;
                    break;
                case "ice":
                    modifier = damage_mult.master_list[index].damage.ice;
                    break;
                case "dragon":
                    modifier = damage_mult.master_list[index].damage.dragon;
                    break;
                case "dark":
                    modifier = damage_mult.master_list[index].damage.dark;
                    break;
                case "fairy":
                    modifier = damage_mult.master_list[index].damage.fairy;
                    break;
            }
            return modifier;
        }

        /// <summary>
        /// This method takes in the acuracy of the pokemon (always divisible by 5) and calculates if it hits or not and returns a boolean
        /// value based on if it hits
        /// <param name="accuracy">the accuracy of the move being passed in</param>
        /// <returns>true if the move hit, false if it missed</returns>
        /// </summary>
        public bool checkAccuracy_and_Hit(int accuracy)
        {
            bool hit = true;

            if (accuracy == 100 || accuracy == 0)
            {
                hit = true;
                //Debug.Log("100% acc: " + accuracy + " : " + hit);
            }
            else
            {
                hit = attack_Switch_Case.Chance_100(accuracy);
                //Debug.Log("not 100%: " + accuracy + " : " + hit);
            }

            if (!hit)
            {
                Debug.LogWarning("The move missed!");
                if (isPlayer)
                {
                    tc.PlayerMissed = true;
                    tc.PlayerDamage = 0;
                    tc.PlayerHeal = 0;
                    tc.PlayerRecoil = 0;
                    tc.PlayerDataComplete = true;

                }
                else
                {
                    tc.EnemyMissed = true;
                    tc.EnemyDamage = 0;
                    tc.EnemyRecoil = 0;
                    tc.EnemyHeal = 0;
                    tc.EnemyDataComplete = true;
                }
            }
            else
            {
                if (isPlayer)
                {
                    tc.PlayerMissed = false;
                }
                else
                {
                    tc.EnemyMissed = false;
                }
            }
            return hit;
        }

        /// <summary>
        /// Calculates the 1/16 chance every move has for getting a critical strike
        /// <param name="chance">the chance probability either (1/8) or (1/16)</param>
        /// <returns>true if the move crit, false if it did not</returns>
        ///</summary>
        public bool isCrit(int chance)
        {
            bool crit = false;

            int guess = UnityEngine.Random.Range(1, chance);
            int guess2 = UnityEngine.Random.Range(1, chance);

            if (guess == guess2)
            {
                crit = true;
            }
            if (crit)
            {
                if (isPlayer)
                {
                    tc.PlayerCriticalStrike = true;
                }
                else
                {
                    tc.EnemyCriticalStrike = true;
                }
            }
            else
            {
                if (isPlayer)
                {
                    tc.PlayerCriticalStrike = false;
                }
                else
                {
                    tc.EnemyCriticalStrike = false;
                }
            }

            return crit;
        }

        /// <summary>
        /// Handles the cases where the move has a high probability of getting a critical hit (1/8) versus (1/16)
        /// </summary>
        /// <param name="name"> the name of the attack</param>
        /// <returns>the crit chance of the move either (1/8) or (1/16)</returns>
        private int critChance(string name)
        {
            int chance;
            switch (name.ToLower())
            {
                default:
                    chance = 16;
                    break;
                case "crabhamer":
                    chance = 8;
                    break;
                case "karate chop":
                    chance = 8;
                    break;
                case "razor leaf":
                    chance = 8;
                    break;
                case "slash":
                    chance = 8;
                    break;
            }
            return chance;
        }
    }

}
