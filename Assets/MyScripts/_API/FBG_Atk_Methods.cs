﻿using UnityEngine;
using System.Collections;
namespace FatBobbyGaming
{
    public static class FBG_Atk_Methods
    {

        public static void noAdditionalEffect()
        {
            return;
        }

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

        /// <summary>
        /// Checks the type of the target pokemon against all of the strings passes in, checks both type1 and type2
        /// </summary>
        /// <param name="target">target pokemon</param>
        /// <param name="s"> types to check as strings</param>
        /// <returns></returns>
        private static bool checkTypes(FBG_Pokemon target, params string[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (target.type1 == s[i] || target.type2 == s[i])
                {
                    return true;
                }
            }
            return false;
        }

        //..

        public static void changeStats(string type, int stageMod, FBG_Pokemon target)
        {
            int newStage = getStatStage(type, target);
            newStage += stageMod;

            if (newStage > 6)
                newStage = 6;
            if (newStage < -6)
                newStage = -6;

            setStatStage(type, newStage, target);
            float multiplier = stageToMultiplier(newStage);

            target.updateStatStage(type, multiplier);
        }

        private static int getStatStage(string type, FBG_Pokemon target)
        {
            int statStage = 0;

            switch (type)
            {
                case "attack":
                    statStage = target.attack_Stage;
                    break;
                case "spAttack":
                    statStage = target.spAttack_Stage;
                    break;
                case "defense":
                    statStage = target.defense_Stage;
                    break;
                case "spDefense":
                    statStage = target.spDefense_stage;
                    break;
                case "speed":
                    statStage = target.speed_stage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }

            return statStage;
        }

        private static void setStatStage(string type, int newStage, FBG_Pokemon target)
        {

            switch (type)
            {
                case "attack":
                    target.attack_Stage = newStage;
                    break;
                case "spAttack":
                    target.spAttack_Stage = newStage;
                    break;
                case "defense":
                    target.defense_Stage = newStage;
                    break;
                case "spDefense":
                    target.spDefense_stage = newStage;
                    break;
                case "speed":
                    target.speed_stage = newStage;
                    break;
                default:
                    Debug.LogError("no type " + type + " found");
                    break;
            }

        }

        private static float stageToMultiplier(int stage)
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
                MoveResults mr = FBG_Atk_Calc.calculateAttackEffect(FBG_Atk_Switch.target, FBG_Atk_Switch.self, name);
                damage += mr.dmgReport.damage;
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
        public static void isBurned(FBG_Pokemon target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, FBG_consts.Fire)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.burned;
            }
        }

        /// <summary>
        /// Freezes a Pokemon if they arent an ice type and dont have any other type A status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of getting frozen</param>
        public static void isFrozen(FBG_Pokemon target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, FBG_consts.Ice)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.frozen;
            }
        }

        /// <summary>
        /// Paralizes a Pokemon if they have no other status effects and aren't an electric type
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of landing this effect</param>
        public static void isParalized(FBG_Pokemon target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, FBG_consts.Electric)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.paralized;
                changeStats(FBG_consts.speed, -6, target);
            }
        }

        /// <summary>
        /// Applies a poision to the pokemon if they aren't steel or posion type and have no other status effects
        /// </summary>
        /// <param name="isPlayer"></param>
        /// <param name="prob"></param>
        public static void isPosioned(FBG_Pokemon target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, FBG_consts.Steel, FBG_consts.Poison)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.poisioned;
            }
        }

        /// <summary>
        /// Applying toxic to a pokemon if they arent either steel or a poison type pokemon and have no other status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        public static void toxic(FBG_Pokemon target)
        {
            if (checkTypes(target, FBG_consts.Steel, FBG_consts.Poison)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.toxic;
            }
        }

        /// <summary>
        /// Puts the pokemon to sleep if they have no other status effect on them
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of it hitting</param>
        /// <param name="duration">duration pokemon is asleep for</param>
        public static void isSleep(FBG_Pokemon target, float prob, int duration)
        {
            if (!Chance_100(prob)) return;
            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.nonVolDuration = duration;
                target.status_A = nonVolitileStatusEffects.toxic;
            }
        }

        //.. Status B

        public static void isConfused(FBG_Pokemon target, float prob, int duration)
        {
            if (!Chance_100(prob)) return;

            if (target.status_B == volitileStatusEffects.none)
            {
                target.status_B = volitileStatusEffects.confused;
            }
        }

        //I need to add gender to the FBG_Pokemon before I can implement this
        public static void isInfatuated(FBG_Pokemon target, FBG_Pokemon self, float prob)
        {
            if (!Chance_100(prob)) return;

            if (target.status_B == volitileStatusEffects.none)
            {

                target.status_B = volitileStatusEffects.infatuated;
            }
        }

        //Other

        public static void isFlinched(FBG_Pokemon target, float prob)
        {
            if (!Chance_100(prob)) return;
            target.isFlinched = true;
        }

        //..

        public static void conversion(FBG_Pokemon self)
        {

            //tempList = genAttacks.get_playerAttackName();
            //string tempName = tempList[0];
            string name = self.atkMoves[0];
            int attack_index = FBG_Atk_Calc.getAttackListIndex(name);
            string attack_type = FBG_Atk_Data.attackList[attack_index].type;
            self.type1 = attack_type;
            string[] types = new string[2];
            types[0] = self.type1;
            types[1] = self.type2;
            self.damageMultiplier = FBG_DmgMult.createMultiplier(types);
        }

        public static float dreamEater(FBG_Pokemon target, float predictedDamage, MoveResults mr)
        {

            if (target.status_A == nonVolitileStatusEffects.sleep)
            {
                mr.dmgReport.heal = Mathf.Round(predictedDamage / 2f);
            }
            else
            {
                mr.dmgReport.damage = 0;
            }
            return predictedDamage;
        }

        public static float earthQuake(FBG_Pokemon target, float predictedDamage)
        {
            if (target.position == pokemonPosition.underground)
            {
                return Mathf.Round(predictedDamage * 2);
            }
            return predictedDamage;
        }

        public static float oneHitKO(FBG_Pokemon target, FBG_Pokemon self, MoveResults mr)
        {
            int acc = target.Level - self.Level + 30;
            if (Chance_100(acc))
            {
                return target.maxHP;
            }
            mr.hit = false;
            return 0;
        }

        public static float levelBasedDamage(FBG_Pokemon target)
        {
            float damage = target.Level;
            return damage;
        }

        public static float sonicBoom(FBG_Pokemon target)
        {
            if (checkTypes(target, FBG_consts.Ghost))
            {
                return 0;
            }
            else
            {
                return 20f;
            }
        }

        public static void substitute(FBG_Pokemon target, MoveResults mr)
        {
            mr.dmgReport.recoil = Mathf.Round(target.maxHP / 4f);
            target.hasSubstitute = true;
            //subsitute health = mr.dmg.recoil
        }

        public static void leech_seed(bool isPlayer)
        {

        }
    }
}
