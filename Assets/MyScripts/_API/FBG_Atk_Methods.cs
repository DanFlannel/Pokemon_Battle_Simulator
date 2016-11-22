﻿using UnityEngine;
using System.Collections;
namespace FatBobbyGaming
{
    public static class FBG_Atk_Methods
    {

        //create a struct out of these 3 things and return it!
        public static float final_damage;
        public static float final_heal;
        public static float recoil;

        //I also need to get rid of these
        public static PlayerPokemonHandler playerStats;
        public static EnemyPokemonHandler enemyStats;
        public static AttackDamageCalc attackCalc;
        public static PokemonAttacks attacks;
        public static TurnController tc;

        public static void SpecialCasesInit()
        {
            //Console.WriteLine("PK : Attack Switch Case: Initalizing");

            enemyStats = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyPokemonHandler>();
            playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPokemonHandler>();
            attackCalc = GameObject.FindGameObjectWithTag("Attacks").GetComponent<AttackDamageCalc>();
            //genAttacks = GameObject.FindGameObjectWithTag("Attacks").GetComponent<GenerateAttacks>();
            attacks = GameObject.FindGameObjectWithTag("AttackData").GetComponent<PokemonAttacks>();
            tc = GameObject.FindGameObjectWithTag("TurnController").GetComponent<TurnController>();

            //Console.WriteLine("PK : Attack Switch Case: Initalized");
        }

        //..

        /// <summary>
        /// Does a random guess for a probability in 100
        /// </summary>
        /// <param name="prob"></param>
        /// <returns></returns>
        public static bool Chance_100(float prob)
        {
            bool chance = false;

            float guess = Random.Range(0, 100);
            //Debug.Log(guess + " : " + prob);
            if (guess < prob)
            {
                chance = true;
            }

            return chance;
        }

        //..

        public static void changeStats(string type, int stageMod, bool isPlayer)
        {
            int newStage = getStatStage(type, isPlayer);
            newStage += stageMod;

            if (newStage > 6)
                newStage = 6;
            if (newStage < -6)
                newStage = -6;

            setStatStage(type, newStage, isPlayer);
            float multiplier = stageToMultiplier(newStage);
            updateStatChange(type, multiplier, isPlayer);

        }

        public static int getStatStage(string type, bool isPlayer)
        {
            int statStage = 0;
            if (isPlayer)
            {
                switch (type)
                {
                    case "attack":
                        statStage = playerStats.attack_Stage;
                        break;
                    case "spAttack":
                        statStage = playerStats.spAttack_Stage;
                        break;
                    case "defense":
                        statStage = playerStats.defense_Stage;
                        break;
                    case "spDefense":
                        statStage = playerStats.spDefense_stage;
                        break;
                    case "speed":
                        statStage = playerStats.speed_stage;
                        break;
                    default:
                        Debug.Log("no type " + type + " found");
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "attack":
                        statStage = enemyStats.attack_Stage;
                        break;
                    case "spAttack":
                        statStage = enemyStats.spAttack_Stage;
                        break;
                    case "defense":
                        statStage = enemyStats.defense_Stage;
                        break;
                    case "spDefense":
                        statStage = enemyStats.spDefense_stage;
                        break;
                    case "speed":
                        statStage = enemyStats.speed_stage;
                        break;
                    default:
                        Debug.Log("no type " + type + " found");
                        break;
                }
            }
            return statStage;
        }

        public static void setStatStage(string type, int newStage, bool isPlayer)
        {
            if (isPlayer)
            {
                switch (type)
                {
                    case "attack":
                        playerStats.attack_Stage = newStage;
                        break;
                    case "spAttack":
                        playerStats.spAttack_Stage = newStage;
                        break;
                    case "defense":
                        playerStats.defense_Stage = newStage;
                        break;
                    case "spDefense":
                        playerStats.spDefense_stage = newStage;
                        break;
                    case "speed":
                        playerStats.speed_stage = newStage;
                        break;
                    default:
                        Debug.LogError("no type " + type + " found");
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "attack":
                        enemyStats.attack_Stage = newStage;
                        break;
                    case "spAttack":
                        enemyStats.spAttack_Stage = newStage;
                        break;
                    case "defense":
                        enemyStats.defense_Stage = newStage;
                        break;
                    case "spDefense":
                        enemyStats.spDefense_stage = newStage;
                        break;
                    case "speed":
                        enemyStats.speed_stage = newStage;
                        break;
                    default:
                        Debug.LogError("no type " + type + " found");
                        break;
                }
            }
        }

        public static void updateStatChange(string type, float multiplier, bool isPlayer)
        {
            if (isPlayer)
            {
                playerStats.updateStatStage(type, multiplier);
            }
            else
            {
                enemyStats.updateStatStage(type, multiplier);
            }
        }

        public static float stageToMultiplier(int stage)
        {

            float multiplier = 1;
            switch (stage)
            {
                case -6:
                    multiplier = .25f;
                    break;
                case -5:
                    multiplier = .285f;
                    break;
                case -4:
                    multiplier = .33f;
                    break;
                case -3:
                    multiplier = .4f;
                    break;
                case -2:
                    multiplier = .5f;
                    break;
                case -1:
                    multiplier = .66f;
                    break;
                case 0:
                    multiplier = 1;
                    break;
                case 1:
                    multiplier = 1.5f;
                    break;
                case 2:
                    multiplier = 2f;
                    break;
                case 3:
                    multiplier = 2.5f;
                    break;
                case 4:
                    multiplier = 3f;
                    break;
                case 5:
                    multiplier = 3.5f;
                    break;
                case 6:
                    multiplier = 4f;
                    break;
            }
            return multiplier;
        }

        /// <summary>
        /// this method takes in the amount of times the attack gets calculated so that the total damage accounts for 
        /// each attack as its own sperate attack rather than multiplying by the number
        /// </summary>
        public static float multiAttack(int rnd, string name)
        {
            //Debug.Log("Final name check:" + name);
            float damage = 0;
            for (int i = 0; i < rnd; i++)
            {
                damage += attackCalc.calculateDamage(name);
            }
            Debug.Log("Final Damage: " + damage);
            return damage;
        }

        //Non-volatile status effects

        /// <summary>
        /// Burns a pokemon if their Type A status is none and they aren't a fire type
        /// </summary>
        /// <param name="isPlayer">who is attacking</param>
        /// <param name="prob">the probability of being hit</param>
        /// <param name="duration">duration of the effect</param>
        public static void isBurned(bool isPlayer, float prob)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                Debug.Log("Implimenting Burn");
                if (isPlayer && enemyStats.Type1 != attacks.Fire && enemyStats.Type2 != attacks.Fire)
                {
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Enemy Burned");
                        enemyStats.non_volitile_status = nonVolitileStatusEffects.burned;
                    }
                }
                else if (playerStats.Type1 != attacks.Fire && playerStats.Type2 != attacks.Fire)
                {

                    if (playerStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("player Burned");
                        playerStats.non_volitile_status = nonVolitileStatusEffects.burned;
                    }
                }
            }
        }

        /// <summary>
        /// Freezes a Pokemon if they arent an ice type and dont have any other type A status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of getting frozen</param>
        public static void isFrozen(bool isPlayer, float prob)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                Debug.Log("Implimenting Freeze");
                if (isPlayer && enemyStats.Type1 != attacks.Ice && enemyStats.Type2 != attacks.Ice)
                {
                    enemyStats.isFrozen = true;
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Enemy Frozen");
                        enemyStats.non_volitile_status = nonVolitileStatusEffects.frozen;
                    }
                }
                else if (playerStats.Type1 != attacks.Ice && playerStats.Type2 != attacks.Ice)
                {
                    if (playerStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Player Frozen");
                        playerStats.non_volitile_status = nonVolitileStatusEffects.frozen;
                    }
                }
            }
        }

        /// <summary>
        /// Paralizes a Pokemon if they have no other status effects and aren't an electric type
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of landing this effect</param>
        public static void isParalized(bool isPlayer, float prob)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                Debug.Log("Implimenting Paralysis");
                if (isPlayer && enemyStats.Type1 != attacks.Electric && enemyStats.Type2 != attacks.Electric)
                {
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Enemy Paralized");
                        enemyStats.non_volitile_status = nonVolitileStatusEffects.paralized;
                        changeStats(FBG_consts.speed, -6, !isPlayer);
                    }
                    enemyStats.isParalized = true;
                }
                else if (playerStats.Type1 != attacks.Electric && playerStats.Type2 != attacks.Electric)
                {
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Player Paralized");
                        playerStats.non_volitile_status = nonVolitileStatusEffects.paralized;
                        changeStats(FBG_consts.speed, -6, isPlayer);
                    }
                }
            }
        }

        /// <summary>
        /// Applies a poision to the pokemon if they aren't steel or posion type and have no other status effects
        /// </summary>
        /// <param name="isPlayer"></param>
        /// <param name="prob"></param>
        public static void isPosioned(bool isPlayer, float prob)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                if (isPlayer && enemyStats.Type1 != attacks.Steel && enemyStats.Type1 != attacks.Poison && enemyStats.Type2 != attacks.Steel && enemyStats.Type2 != attacks.Poison)
                {
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Enemy is Poisoned");
                        enemyStats.non_volitile_status = nonVolitileStatusEffects.poisioned;
                    }
                }
                else if (playerStats.Type1 != attacks.Steel && playerStats.Type1 != attacks.Poison && playerStats.Type2 != attacks.Steel && playerStats.Type2 != attacks.Poison)
                {
                    if (playerStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Player is Poisoned");
                        playerStats.non_volitile_status = nonVolitileStatusEffects.poisioned;
                    }
                }
            }
        }

        /// <summary>
        /// Applying toxic to a pokemon if they arent either steel or a poison type pokemon and have no other status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        public static void toxic(bool isPlayer)
        {
            if (isPlayer && enemyStats.Type1 != attacks.Steel && enemyStats.Type1 != attacks.Poison && enemyStats.Type2 != attacks.Steel && enemyStats.Type2 != attacks.Poison)
            {
                if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                {
                    Debug.Log("Enemy now has Toxic");
                    enemyStats.non_volitile_status = nonVolitileStatusEffects.toxic;
                }
            }
            else if (playerStats.Type1 != attacks.Steel && playerStats.Type1 != attacks.Poison && playerStats.Type2 != attacks.Steel && playerStats.Type2 != attacks.Poison)
            {
                if (playerStats.non_volitile_status == nonVolitileStatusEffects.none)
                {
                    Debug.Log("Player now has Toxic");
                    playerStats.non_volitile_status = nonVolitileStatusEffects.toxic;
                }
            }
        }

        /// <summary>
        /// Puts the pokemon to sleep if they have no other status effect on them
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of it hitting</param>
        /// <param name="duration">duration pokemon is asleep for</param>
        public static void isSleep(bool isPlayer, float prob, int duration)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                Debug.Log("Implimenting Sleep");
                if (isPlayer)
                {
                    if (enemyStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Enemy Asleep");
                        tc.enemyNVDur = duration;
                        enemyStats.non_volitile_status = nonVolitileStatusEffects.sleep;
                    }
                }
                else
                {
                    if (playerStats.non_volitile_status == nonVolitileStatusEffects.none)
                    {
                        Debug.Log("Player Asleep");
                        tc.playerNVDur = duration;
                        playerStats.non_volitile_status = nonVolitileStatusEffects.sleep;
                    }
                }
            }
        }

        //..

        public static void isConfused(bool isPlayer, float prob, int duration)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                if (isPlayer)
                {
                    Debug.Log("Target Pokemon is now Confused");
                    enemyStats.isConfused = true;
                    enemyStats.confusedDuration = duration;
                }
                else
                {
                    Debug.Log("Target Pokemon is now Confused");
                    playerStats.isConfused = true;
                    playerStats.confusedDuration = duration;
                }
            }
        }

        public static void isFlinched(bool isPlayer, float prob)
        {
            bool isHit = Chance_100(prob);
            if (isHit)
            {
                if (isPlayer)
                {
                    if (!enemyStats.hasAttacked)
                    {
                        Debug.Log("Target Pokemon is now Flinched");
                        enemyStats.isFlinched = true;
                    }
                }
                else
                {
                    if (!playerStats.hasAttacked)
                    {
                        Debug.Log("Target Pokemon is now Flinched");
                        playerStats.isFlinched = true;
                    }
                }
            }
        }

        //..

        public static void conversion(bool isPlayer, string name)
        {
            //List<string> tempList = new List<string>();
            if (isPlayer)
            {
                //tempList = genAttacks.get_playerAttackName();
                //string tempName = tempList[0];
                int attack_index = attackCalc.getAttackListIndex(name);
                string attack_type = attacks.attackList[attack_index].type;
                playerStats.Type1 = attack_type;
            }
            else
            {
                //tempList = genAttacks.get_enemyAttackName();
                //string tempName = tempList[0];
                int attack_index = attackCalc.getAttackListIndex(name);
                string attack_type = attacks.attackList[attack_index].type;
                playerStats.Type1 = attack_type;
            }
        }

        public static float dreamEater(bool isPlayer, float predictedDamage)
        {

            if (isPlayer)
            {
                if (tc.enemyNVStatus == nonVolitileStatusEffects.sleep)
                {
                    final_heal = Mathf.Round(predictedDamage / 2f);
                }
                else
                {
                    predictedDamage = 0;
                }
            }
            else
            {
                if (tc.playerNVStatus == nonVolitileStatusEffects.sleep)
                {
                    final_heal = Mathf.Round(predictedDamage / 2f);
                }
                else
                {
                    predictedDamage = 0;
                }
            }

            return predictedDamage;
        }

        public static float earthQuake(bool isPlayer, float predictedDamage)
        {

            if (isPlayer)
            {
                if (enemyStats.isUnderground)
                {
                    predictedDamage *= 2f;
                }
            }
            else
            {
                if (playerStats.isUnderground)
                {
                    predictedDamage *= 2f;
                }
            }

            return predictedDamage;
        }

        public static void oneHitKO(bool isPlayer)
        {
            int acc;
            if (isPlayer)
                acc = playerStats.Level - enemyStats.Level + 30;
            else
                acc = enemyStats.Level - playerStats.Level + 30;
            if (attackCalc.checkAccuracy_and_Hit(acc))
            {
                if (isPlayer)
                    enemyStats.curHp = 0;
                else
                    playerStats.curHp = 0;
            }
        }

        public static float levelBasedDamage(bool isPlayer)
        {
            float damage = 0;
            if (isPlayer)
            {
                damage = enemyStats.Level;
            }
            else
            {
                damage = playerStats.Level;
            }
            return damage;
        }

        public static float sonicBoom(bool isPlayer)
        {
            float damage = 0;
            if (isPlayer)
            {
                if (enemyStats.Type1 == "Ghost" || enemyStats.Type2 == "Ghost")
                {
                    damage = 0;
                }
                else
                {
                    damage = 20;
                }
            }
            else
            {
                if (playerStats.Type1 == "Ghost" || playerStats.Type2 == "Ghost")
                {
                    damage = 0;
                }
                else
                {
                    damage = 20;
                }
            }
            return damage;
        }

        public static void substitute(bool isPlayer)
        {
            if (isPlayer)
            {
                if (playerStats.curHp > (playerStats.maxHP / 4f))
                {
                    playerStats.hasSubstitute = true;
                    recoil = playerStats.maxHP / 4f;
                }
            }
            else
            {
                if (enemyStats.curHp > (enemyStats.maxHP / 4f))
                {
                    enemyStats.hasSubstitute = true;
                    recoil = playerStats.maxHP / 4f;
                }
            }
        }

        //THESE NEED TO BE ELEMINATED
        public static void one_sixteenth_perm(bool isPlayer)
        {
            if (isPlayer)
            {
                tc.enemy_one_sixteen = true;
            }
            else
            {
                tc.player_one_sixteenth = true;
            }
        }

        public static void one_sixteenth_temp(bool isPlayer, int duration)
        {
            if (isPlayer)
            {
                tc.enemy_one_sixteen = true;
                tc.enemy_one_sixteenth_duration = duration;
            }
            else
            {
                tc.player_one_sixteenth = true;
                tc.player_one_sixteenth_duration = duration;
            }
        }

        public static void one_eigth_perm(bool isPlayer)
        {
            if (isPlayer)
            {
                tc.enemy_one_eigth = true;
            }
            else
            {
                tc.player_one_eigth = true;
            }
        }

        public static void one_eigth_temp(bool isPlayer, int duration)
        {
            if (isPlayer)
            {
                tc.enemy_one_eigth = true;
                tc.enemy_one_eigth_duration = duration;
            }
            else
            {
                tc.player_one_eigth = true;
                tc.player_one_eigth_duration = duration;
            }
        }

        public static void noAdditionalEffect()
        {
            return;
        }

        //..

        public static void leech_seed(bool isPlayer)
        {
            if (isPlayer)
            {
                tc.enemy_leech_seed = true;
            }
            else
            {
                tc.player_leech_seed = true;
            }
        }

        /// <summary>
        /// Updates the controls
        /// </summary>
        /// <param name="isPlayer">player or enemy</param>
        /// <param name="name">Attack name</param>
        public static void updateTurnController(bool isPlayer, string name, AttackType type)
        {
            if (isPlayer)
            {
                tc.PlayerDamage = (int)final_damage;
                tc.PlayerHeal = (int)final_heal;
                tc.PlayerRecoil = (int)recoil;
                tc.PlayerDataComplete = true;
                tc.Player_attackName = name;
                tc.Player_AttackType = type;
                tc.playerNVStatus = playerStats.non_volitile_status;
            }
            else
            {
                tc.EnemyDamage = (int)final_damage;
                tc.EnemyHeal = (int)final_heal;
                tc.EnemyRecoil = (int)recoil;
                tc.EnemyDataComplete = true;
                tc.Enemy_attackName = name;
                tc.Enemy_AttackType = type;
                tc.enemyNVStatus = enemyStats.non_volitile_status;
            }
        }

    }
}