using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Data;

namespace FBG.Attack
{
    public class BaseMoves {

        public bool ignoreReflect { get; set; }
        public bool ignoreLightScreen { get; set; }
        public MoveResults moveRes { get; set; }

        public  void noAdditionalEffect()
        {
            return;
        }

        /// <summary>
        /// Does a random guess for a probability in 100
        /// </summary>
        /// <param name="prob"></param>
        /// <returns></returns>
        public  bool Chance_100(float prob)
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
        private  bool checkTypes(PokemonBase target, params string[] s)
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

        public  void changeStats(string type, int stageMod, PokemonBase target)
        {
            if (target.team.hasMist && stageMod < 0)
            {
                return;
            }

            int newStage = getStatStage(type, target);
            newStage += stageMod;

            if (newStage > 6)
                newStage = 6;
            if (newStage < -6)
                newStage = -6;

            setStatStage(type, newStage, target);

            if (type == Consts.accuracy || type == Consts.evasion)
            {
                return;
            }

            float multiplier = stageToMultiplier_BaseStat(newStage);
            target.updateStatStage(type, multiplier);
        }

        //done for acc and evasion
        private  int getStatStage(string type, PokemonBase target)
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
                case "accuracy":
                    statStage = target.acc_stage;
                    break;
                case "evasion":
                    statStage = target.evasive_stage;
                    break;
                default:
                    Debug.Log("no type " + type + " found");
                    break;
            }

            return statStage;
        }

        private  void setStatStage(string type, int newStage, PokemonBase target)
        {
            switch (type)
            {
                case Consts.attack:
                    target.attack_Stage = newStage;
                    break;

                case Consts.spAttack:
                    target.spAttack_Stage = newStage;
                    break;

                case Consts.defense:
                    target.defense_Stage = newStage;
                    break;

                case Consts.spDefense:
                    target.spDefense_stage = newStage;
                    break;

                case Consts.speed:
                    target.speed_stage = newStage;
                    break;

                case Consts.accuracy:
                    target.acc_stage = newStage;
                    break;

                case Consts.evasion:
                    target.evasive_stage = newStage;
                    break;

                default:
                    Debug.LogError("no type " + type + " found");
                    break;
            }

        }

        private  float stageToMultiplier_BaseStat(int stage)
        {
            float multiplier = 1f;
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
        public float multiAttack(int rnd, string name)
        {
            //Debug.Log("Final name check:" + name);
            float damage = 0;
            for (int i = 0; i < rnd; i++)
            {
                damage += AtkCalc.GenBaseDamage(AtkCalc.thisPokemon, name);
            }
            Debug.Log(string.Format("Hit {0} times", rnd));
            return damage;
        }

        //Non-volatile status effects

        /// <summary>
        /// Burns a pokemon if their Type A status is none and they aren't a fire type
        /// </summary>
        /// <param name="isPlayer">who is attacking</param>
        /// <param name="prob">the probability of being hit</param>
        /// <param name="duration">duration of the effect</param>
        public  void isBurned(PokemonBase target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, Consts.Fire)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.burned;
                target.nvCurDur = 0;
                Debug.Log(string.Format("{0} is now burned", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.burned.ToString();
            }
        }

        /// <summary>
        /// Freezes a Pokemon if they arent an ice type and dont have any other type A status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of getting frozen</param>
        public  void isFrozen(PokemonBase target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, Consts.Ice)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.frozen;
                Debug.Log(string.Format("{0} is now frozen", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.frozen.ToString();
            }
        }

        /// <summary>
        /// Paralizes a Pokemon if they have no other status effects and aren't an electric type
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of landing this effect</param>
        public  void isParalized(PokemonBase target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, Consts.Electric)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.paralized;
                changeStats(Consts.speed, -6, target);
                Debug.Log(string.Format("{0} is now paralized", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.paralized.ToString();
            }
        }

        /// <summary>
        /// Applies a poision to the pokemon if they aren't steel or posion type and have no other status effects
        /// </summary>
        /// <param name="isPlayer"></param>
        /// <param name="prob"></param>
        public  void isPosioned(PokemonBase target, float prob)
        {
            if (!Chance_100(prob)) return;
            if (checkTypes(target, Consts.Steel, Consts.Poison)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.poisioned;
                target.nvCurDur = 0;
                Debug.Log(string.Format("{0} is now poisoned", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.poisioned.ToString();
            }
        }

        /// <summary>
        /// Applying toxic to a pokemon if they arent either steel or a poison type pokemon and have no other status effects
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        public  void toxic(PokemonBase target)
        {
            if (checkTypes(target, Consts.Steel, Consts.Poison)) return;

            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.status_A = nonVolitileStatusEffects.toxic;
                target.nvCurDur = 0;
                Debug.Log(string.Format("{0} is now badly posioned", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.toxic.ToString();
            }
        }

        /// <summary>
        /// Puts the pokemon to sleep if they have no other status effect on them
        /// </summary>
        /// <param name="isPlayer">is the player attacking</param>
        /// <param name="prob">probability of it hitting</param>
        /// <param name="duration">duration pokemon is asleep for</param>
        public  void isSleep(PokemonBase target, float prob, int duration)
        {
            if (!Chance_100(prob)) return;
            if (target.status_A == nonVolitileStatusEffects.none)
            {
                target.nvDur = duration;
                target.status_A = nonVolitileStatusEffects.sleep;
                Debug.Log(string.Format("{0} is now asleep", target.Name));
                moveRes.statusEffect = nonVolitileStatusEffects.sleep.ToString();
            }
        }

        //.. Status B

        public  void isConfused(PokemonBase target, float prob, int duration)
        {
            if (!Chance_100(prob)) return;

            if (target.status_B == volitileStatusEffects.none)
            {
                target.status_B = volitileStatusEffects.confused;
                Debug.Log(string.Format("{0} is now confused", target.Name));
                moveRes.statusEffect = volitileStatusEffects.confused.ToString();
            }
        }

        //I need to add gender to the PokemonBase before I can implement this
        public  void isInfatuated(PokemonBase target, PokemonBase self, float prob)
        {
            if (!Chance_100(prob)) return;

            if (target.status_B == volitileStatusEffects.none)
            {
                target.status_B = volitileStatusEffects.infatuated;
                Debug.Log(string.Format("{0} is now infatuated", target.Name));
                moveRes.statusEffect = volitileStatusEffects.infatuated.ToString();
            }
        }

        //Other

        public  void isFlinched(PokemonBase target, float prob)
        {
            if (!Chance_100(prob)) return;
            target.isFlinched = true;
        }

        public  float ChargingMove(PokemonBase self, string atkName, float dmg)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.cachedDamage = dmg;
                self.atkStatus = attackStatus.charging;
                self.nextAttack = atkName;
                dmg = 0;

            }
            else if (self.atkStatus == attackStatus.charging)
            {
                self.atkStatus = attackStatus.normal;
                dmg = self.cachedDamage;
                self.cachedDamage = 0;
            }
            return dmg;
        }

        public  float ReChargeMove(PokemonBase self, string atkName, float dmg)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.atkStatus = attackStatus.recharging;
                self.nextAttack = atkName;
            }
            else if (self.atkStatus == attackStatus.recharging)
            {
                self.atkStatus = attackStatus.normal;
                dmg = 0;
            }
            return dmg;
        }

        //..

        public  void conversion(PokemonBase self)
        {

            //tempList = genAttacks.get_playerAttackName();
            //string tempName = tempList[0];
            string name = self.atkMoves[0];
            int attack_index = AtkCalc.getAttackListIndex(name);
            string attack_type = MoveSets.attackList[attack_index].type;
            self.type1 = attack_type;
            string[] types = new string[2];
            types[0] = self.type1;
            types[1] = self.type2;
            self.damageMultiplier = DamageMultipliers.createMultiplier(types);
        }

        public  float dreamEater(PokemonBase target, float predictedDamage, MoveResults mr)
        {
            if (target.status_A == nonVolitileStatusEffects.sleep)
            {
                mr.dmgReport.heal = Mathf.Round(predictedDamage / 2f);
            }
            else
            {
                predictedDamage = 0;
            }
            return predictedDamage;
        }

        public float oneHitKO(PokemonBase target, PokemonBase self, MoveResults mr)
        {
            ignoreLightScreen = true;
            ignoreReflect = true;

            int acc = target.Level - self.Level + 30;
            if (Chance_100(acc))
            {
                return target.maxHP;
            }
            mr.hit = false;
            return 0;
        }

        public float levelBasedDamage(PokemonBase target)
        {
            ignoreLightScreen = true;
            ignoreReflect = true;
            return target.Level;
        }

        public float sonicBoom(PokemonBase target)
        {
            ignoreLightScreen = true;

            if (checkTypes(target, Consts.Ghost))
            {
                return 0;
            }
            else
            {
                return 20f;
            }
        }

        public  void substitute(PokemonBase target, MoveResults mr)
        {
            mr.dmgReport.recoil = Mathf.Round(target.maxHP / 4f);
            target.hasSubstitute = true;
            //subsitute health = mr.dmg.recoil
        }

        public  void leech_seed()
        {
            
        }

        public  bool hasEffector(PokemonBase target, string eName)
        {
            for (int i = 0; i < target.effectors.Count; i++)
            {
                if (target.effectors[i].name == eName)
                {
                    return true;
                }
            }
            return false;
        }

        public bool rndSwap(PokemonBase target)
        {
            if (canBeSwapped(target))
            {
                List<PokemonBase> pkmn = new List<PokemonBase>();
                for(int i = 0; i < target.team.pokemon.Count; i++)
                {
                    if(target.team.pokemon[i].curHp > 0 && target.team.pokemon[i] != target.team.curPokemon)
                    {
                        pkmn.Add(target.team.pokemon[i]);
                    }
                }
                int rnd = Random.Range(0, pkmn.Count);
                target.team.swap(rnd);
                return true;
            }
            return false;
        }

        private bool canBeSwapped(PokemonBase target)
        {
            for(int i = 0; i < target.team.pokemon.Count; i++)
            {
                if (target.team.pokemon[i].curHp > 0 && target.team.pokemon[i] != target.team.curPokemon)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
