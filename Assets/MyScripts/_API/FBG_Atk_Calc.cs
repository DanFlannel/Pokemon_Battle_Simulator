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
        private TurnController tc;

        private FBG_Pokemon targetPokemon;
        private FBG_Pokemon thisPokemon;

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
        /// This method takes the name of the attack and then passes it into other methods in the Attack_Switch_Case to get the effect
        /// of the attack on the enemy or player pokemon, if it is a status type of attack or one that deals damage or stuns...ect.
        /// </summary>
        public MoveResults calculateAttackEffect(FBG_Pokemon t, FBG_Pokemon s, string atkName)
        {
            MoveResults MR = new MoveResults();
            targetPokemon = t;
            thisPokemon = s;
            Debug.LogWarning(string.Format(" {0} is using {1} ", thisPokemon.Name, atkName));

            int atkIndex = getAttackListIndex(atkName);

            string atkCat = FBG_Atk_Data.attackList[atkIndex].cat;
            string atkType = FBG_Atk_Data.attackList[atkIndex].type;
            int accuracy = FBG_Atk_Data.attackList[atkIndex].accuracy;

            float dmg = calculateDamage(atkName, atkIndex);
            //this also sets our crit bool in the move results
            float dmgMod = modifier(atkName, atkType, MR);
            dmg = Mathf.Round(dmg * dmgMod);

            MR.hit = checkAccuracy_and_Hit(accuracy);

            //Debug.Log("Attack Name: " + attack_name);
            switch (atkCat)
            {
                case "Status":
                    //attack_Switch_Case.updateTurnController(isPlayer, attack_name, AttackType.status);
                    break;
                case "Physical":
                    //attack_Switch_Case.physicalAttacks(attack_name, predictedDamage, isPlayer);
                    break;
                case "Special":
                    //attack_Switch_Case.specialAttacks(attack_name, predictedDamage, isPlayer);
                    break;
            }
            return MR;
        }

        /// <summary>
        /// This method calculates the damage that each attack will do based off the serebii.net damage formula, this does not take into effect the different modifiers or attack calculations each specific move has
        /// <param name="atkName">takes in the name of the current attack being passed in</param>
        /// <returns>the final basic damage based on all modifiers and multipliers</returns>
        /// </summary>
        private float calculateDamage(string atkName, int atkIndex)
        {
            float dmg = 0;

            string attackCat = FBG_Atk_Data.attackList[atkIndex].cat;
            set_attack_and_def(attackCat);

            if (calcExitConditions(attackCat, atkName, atkIndex))
            {
                return dmg;
            }


            float level_mod = levelModifier();
            float att_div_defense = ((float)FBG_Atk_Data.attackList[atkIndex].power) / defense_mod;

            //Debug.Log("attack div defense: " + baseAttackPower(attack_index) + "/" + defense_mod + " = " + att_div_defense);
            

            //Damage Calculations here
            dmg = level_mod;
            //Debug.Log("Damage LEVEL MOD: " + "mod: " + level_mod + " Damage: " + final_damage);
            dmg *= attack_mod;
            //Debug.Log("Damage * ATTACK MOD: " + "mod: " + attack_mod + " Damage: " + final_damage);
            dmg *= att_div_defense;
            //Debug.Log("Damage * ATTACK/Defense: " + "mod: " + att_div_defense + " Damage: " + final_damage);
            dmg /= 50;
            //Debug.Log("Damage /50" + " Damage: " + final_damage);
            dmg += 2;
            //Debug.Log("Damage +2: " + " Damage: " + final_damage);
            //final_damage *= damage_mod;
            //Debug.Log("Damage * damage_Mod: " + "mod: " + damage_mod + " Damage: " + final_damage);
            //final_damage = Mathf.Round(final_damage);
            return dmg;
        }

        private bool calcExitConditions(string atkCat, string atkName, int atkIndex)
        {

            if (atkCat == FBG_consts.Status) //we do not have to calculate damage for status moves!
            {
                Debug.Log("Status move");
                return false;
            }
            if (FBG_Atk_Data.attackList[atkIndex].power == 0)  //there is no base attack power so we can just return 0, it is a sepcial attack that has its own calculations
            {
                Debug.Log("This attack calclates it's own damage: " + atkName);
                return false;
            }

            if (attack_mod == 0)
            {
                Debug.LogError("Attack modifier is 0");
                return false;
            }
            if (defense_mod == 0)
            {
                Debug.LogError("Defense modifier is 0");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Sets the multiplier Base Power * STAB * Type modifier * Critical * other * randomNum(.85,1)
        /// <param name="atkIndex">the index of the move in the attack list</param>
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <param name="atkName">the name of the move being passed in</param>
        /// <returns>the final value of all the modifiers</returns>
        /// </summary>
        private float modifier(string atkName, string attackType, MoveResults mr)
        {
            float modifier;
            float stab = 1f;
            if (isStab(attackType))
            {
                stab = 1.5f;
            }

            float critical = 1f;
            int critProb = critChance(atkName);
            //Debug.Log("Crit chance: 1 /" + critProb);
            bool crit = isCrit(critProb);
            mr.crit = crit;
            if (crit)
            {
                Debug.Log("Critical HIT!");
                critical = 1.5f;
            }
            float rnd = UnityEngine.Random.Range(.85f, 1f);
            float typeMultiplier = fetchDmgMultModifier(attackType);

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
        private float levelModifier()
        {
            //(2 * level / 5) + 2
            float level = targetPokemon.Level;
            float modifier = 2 * level;
            modifier /= 5;
            modifier += 2;

            return modifier;
        }

        /// <summary>
        ///  Sets the player and enemy attack and defense based on the attack category (physical, status, special)
        /// <param name="attack_index">the index of the move in the list of attacks</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <param name="attackCat">the category of the attack move, either special, status, or physical</param>
        /// </summary>
        private void set_attack_and_def(string attackCat)
        {
            if (attackCat == FBG_consts.Special)                  //we are calculating a special attack
            {
                attack_mod = thisPokemon.Special_Attack;
                defense_mod = targetPokemon.Special_Defense;
            }
            if (attackCat == FBG_consts.Physical)                  //we are calculating a physical attack
            {
                attack_mod = thisPokemon.Attack;
                defense_mod = targetPokemon.Defense;
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
            base_damage = (float)FBG_Atk_Data.attackList[index].power;
            //Debug.Log("Base Damage: " + base_damage);
            return base_damage;
        }

        /// <summary>
        ///  checks if the current attack is a STAB type attack or same type attack
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <returns>a boolean that is true if the move is a stab type move or not</returns>
        /// </summary>
        private bool isStab(string attackType)
        {
            if(attackType == thisPokemon.type1 || attackType == thisPokemon.type2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the index of the pokemon in the attack list so we can use this index later rather than having to get it multiple times
        /// <param name="name">the name of the move being passed in</param>
        /// <returns>the index of the move being passed in, within the attack list</returns>
        /// </summary>
        private int getAttackListIndex(string name)
        {
            //Debug.Log("called Attack List Index");
            for (int i = 0; i < FBG_Atk_Data.attackList.Count; i++)
            {
                if (name.ToLower() == FBG_Atk_Data.attackList[i].name.ToLower())
                {
                    //Debug.Log("Calculating Damage for " + name);
                    return i;
                }
            }
            Debug.Log("No Attack with name " + name + " found");
            return 0;
        }

        /// <summary>
        /// Uses the index to get the type multiplier from the master list in the damage multiplier class
        /// <param name="attackType">the type of the attack</param>
        /// <param name="index">the index of the attack move in the attack list</param>
        /// <returns>the modifier of the attack move based off its attack type</returns>
        /// </summary>
        private float fetchDmgMultModifier(string attackType)
        {
            float modifier = 0f;
            attackType = attackType.ToLower();
            switch (attackType)
            {
                case "normal":
                    modifier = targetPokemon.damageMultiplier.normal;
                    break;
                case "fighting":
                    modifier = targetPokemon.damageMultiplier.fighting;
                    break;
                case "flying":
                    modifier = targetPokemon.damageMultiplier.flying;
                    break;
                case "poison":
                    modifier = targetPokemon.damageMultiplier.poison;
                    break;
                case "ground":
                    modifier = targetPokemon.damageMultiplier.ground;
                    break;
                case "rock":
                    modifier = targetPokemon.damageMultiplier.rock;
                    break;
                case "bug":
                    modifier = targetPokemon.damageMultiplier.bug;
                    break;
                case "ghost":
                    modifier = targetPokemon.damageMultiplier.ghost;
                    break;
                case "steel":
                    modifier = targetPokemon.damageMultiplier.steel;
                    break;
                case "fire":
                    modifier = targetPokemon.damageMultiplier.fire;
                    break;
                case "water":
                    modifier = targetPokemon.damageMultiplier.water;
                    break;
                case "grass":
                    modifier = targetPokemon.damageMultiplier.grass;
                    break;
                case "electric":
                    modifier = targetPokemon.damageMultiplier.electric;
                    break;
                case "psychic":
                    modifier = targetPokemon.damageMultiplier.psychic;
                    break;
                case "ice":
                    modifier = targetPokemon.damageMultiplier.ice;
                    break;
                case "dragon":
                    modifier = targetPokemon.damageMultiplier.dragon;
                    break;
                case "dark":
                    modifier = targetPokemon.damageMultiplier.dark;
                    break;
                case "fairy":
                    modifier = targetPokemon.damageMultiplier.fairy;
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
        private bool checkAccuracy_and_Hit(int accuracy)
        {
            if (accuracy == 100 || accuracy == 0)
            {
                return true;
            }
            return FBG_Atk_Methods.Chance_100(accuracy);
        }

        /// <summary>
        /// Calculates the 1/16 chance every move has for getting a critical strike
        /// <param name="chance">the chance probability either (1/8) or (1/16)</param>
        /// <returns>true if the move crit, false if it did not</returns>
        ///</summary>
        private bool isCrit(int chance)
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

    public class MoveResults
    {
        public bool hit;
        public bool crit;
        public string affectedStage;
        public int stageDiff;
        public move_DmgReport dmg;
    }

}
