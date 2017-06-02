using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Data;

namespace FBG.Attack
{
    public static class AtkCalc
    {

        public static PokemonBase targetPokemon;
        public static PokemonBase thisPokemon;

        private static float attack_mod;
        private static float defense_mod;

        /// <summary>
        /// This method takes the name of the attack and then passes it into other methods in the Attack_Switch_Case to get the effect
        /// of the attack on the enemy or player pokemon, if it is a status type of attack or one that deals damage or stuns...ect.
        /// </summary>
        public static MoveResults calculateAttack(PokemonBase tar, PokemonBase self, string atkName)
        {
            MoveResults MR = new MoveResults(atkName);
            targetPokemon = tar;
            thisPokemon = self;

            atkName = checkCachedAttack(atkName, self); ;

            Debug.LogWarning(string.Format(" {0} is using {1} ", self.Name, atkName));

            int atkIndex = getAttackListIndex(atkName);
            string atkCat = MoveSets.attackList[atkIndex].cat;
            string atkType = MoveSets.attackList[atkIndex].type;
            int accuracy = MoveSets.attackList[atkIndex].accuracy;

            //need to rethink this before damage report!
            MR.hit = checkAccuracy_and_Hit(self, tar, atkName, accuracy, atkCat);
            MR.hit = checkSemiInvulnerable(MR);

            float baseDamage = GenBaseDamage(atkName, atkCat, atkType, atkIndex, self, MR);

            MR.dmgReport = GenDmgReport(atkName, atkCat, baseDamage, tar, self, MR);


            return MR;
        }

        private static string checkCachedAttack(string atkName, PokemonBase self)
        {
            if (self.nextAttack != "")
            {
                return self.nextAttack;
            }
            return atkName;
        }

        private static float GenBaseDamage(string atkName, string atkCat, string atkType, int atkIndex, PokemonBase self, MoveResults MR)
        {
            float baseDamage = calculateDamage(atkName, atkCat, atkIndex);
            //Debug.Log("Base Damage: " + baseDamage);
            //this also sets our crit bool in the move results
            float dmgMod = modifier(self, atkName, atkType, MR);
            baseDamage = Mathf.Round(baseDamage * dmgMod);
            //Debug.Log("Damage: " + baseDamage);
            return baseDamage;
        }

        /// <summary>
        /// Used for attacks that strike multiple times.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="atkName"></param>
        /// <returns></returns>
        public static float GenBaseDamage(PokemonBase self, string atkName)
        {
            int atkIndex = getAttackListIndex(atkName);
            string atkCat = MoveSets.attackList[atkIndex].cat;
            string atkType = MoveSets.attackList[atkIndex].type;
            MoveResults MR = new MoveResults(atkName);

            return GenBaseDamage(atkName, atkCat, atkType, atkIndex, self, MR);
        }

        private static move_DmgReport GenDmgReport(string atkName, string atkCat, float baseDamage, PokemonBase tar, PokemonBase self, MoveResults MR)
        {
            move_DmgReport report = new move_DmgReport();
            switch (atkCat)
            {

                case Consts.Status:
                    Debug.Log("Status Move");
                    StatusAtkHandler statAtk = new StatusAtkHandler(tar, self, ref MR);
                    report = statAtk.result(atkName);
                    break;
                case Consts.Physical:
                    Debug.Log("Physical Move");
                    PhysicalAtkHandler physAtk = new PhysicalAtkHandler(tar, self, ref MR);
                    report = physAtk.result(atkName, baseDamage);
                    break;
                case Consts.Special:
                    Debug.Log("Special Move");
                    SpecialAtkHandler spAtk = new SpecialAtkHandler(tar, self, ref MR);
                    report = spAtk.result(atkName, baseDamage);
                    break;
            }
            return report;
        }

        /// <summary>
        /// Gets the index of the pokemon in the attack list so we can use this index later rather than having to get it multiple times
        /// <param name="name">the name of the move being passed in</param>
        /// <returns>the index of the move being passed in, within the attack list</returns>
        /// </summary>
        public static int getAttackListIndex(string name)
        {
            //Debug.Log("called Attack List Index");
            for (int i = 0; i < MoveSets.attackList.Count; i++)
            {
                if (name.ToLower() == MoveSets.attackList[i].name.ToLower())
                {
                    //Debug.Log("Calculating Damage for " + name);
                    return i;
                }
            }
            Debug.Log("No Attack with name " + name + " found");
            return 0;
        }

        /// <summary>
        /// This method calculates the damage that each attack will do based off the serebii.net damage formula, this does not take into effect the different modifiers or attack calculations each specific move has
        /// <param name="atkName">takes in the name of the current attack being passed in</param>
        /// <returns>the final basic damage based on all modifiers and multipliers</returns>
        /// </summary>
        private static float calculateDamage(string atkName, string atkCat, int atkIndex)
        {
            set_attack_and_def(atkCat);
            if (calcExitConditions(atkCat, atkName, atkIndex))
            {
                //Debug.LogWarning("damage exit conditions met");
                return 0;
            }

            float level_mod = levelModifier();
            float att_div_defense = ((float)MoveSets.attackList[atkIndex].power) / defense_mod;

            //Debug.Log("attack div defense: " + baseAttackPower(attack_index) + "/" + defense_mod + " = " + att_div_defense);

            float dmg = 0;
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
            //final_damage = Mathf.Round(final_damage);
            return dmg;
        }

        /// <summary>
        /// A set of variables for which we check if we even have to calculate damage for
        /// </summary>
        /// <param name="atkCat"></param>
        /// <param name="atkName"></param>
        /// <param name="atkIndex"></param>
        /// <returns></returns>
        private static bool calcExitConditions(string atkCat, string atkName, int atkIndex)
        {
            if (atkCat == Consts.Status) //we do not have to calculate damage for status moves!
            {
                //Debug.Log("Status move");
                return true;
            }
            if (MoveSets.attackList[atkIndex].power == 0)  //there is no base attack power so we can just return 0, it is a sepcial attack that has its own calculations
            {
                //Debug.Log("This attack calclates it's own damage: " + atkName);
                return true;
            }

            if (attack_mod == 0)
            {
                Debug.LogError("Attack modifier is 0");
                return true;
            }
            if (defense_mod == 0)
            {
                Debug.LogError("Defense modifier is 0");
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the multiplier Base Power * STAB * Type modifier * Critical * other * randomNum(.85,1)
        /// <param name="atkIndex">the index of the move in the attack list</param>
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <param name="atkName">the name of the move being passed in</param>
        /// <returns>the final value of all the modifiers</returns>
        /// </summary>
        private static float modifier(PokemonBase self, string atkName, string attackType, MoveResults mr)
        {
            float modifier;
            float stab = 1f;
            if (isStab(attackType))
            {
                stab = 1.5f;
            }

            float critical = 1f;
            int critProb = critChance(self, atkName);
            //Debug.Log("Crit chance: 1 /" + critProb);
            bool crit = isCrit(critProb);
            mr.crit = crit;
            if (crit)
            {
                Debug.Log("Critical HIT!");
                critical = 1.5f;
            }
            float rnd = UnityEngine.Random.Range(.85f, 1f);
            //float typeMultiplier = fetchDmgMultModifier(attackType);
            float typeMultiplier = DamageMultipliers.getEffectiveness(targetPokemon.damageMultiplier, attackType);

            if (typeMultiplier == 0)
            {
                Debug.Log("The pokemon is immune to " + attackType);
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
        private static float levelModifier()
        {
            //(2 * level / 5) + 2
            float level = targetPokemon.Level;
            float modifier = 2 * level;
            modifier /= 5;
            modifier += 2;

            return modifier;
        }

        private static float levelModifier(PokemonBase target)
        {
            float level = target.Level;
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
        private static void set_attack_and_def(string attackCat)
        {
            if (attackCat == Consts.Special)                  //we are calculating a special attack
            {
                attack_mod = thisPokemon.Special_Attack;
                defense_mod = targetPokemon.Special_Defense;
            }
            if (attackCat == Consts.Physical)                  //we are calculating a physical attack
            {
                attack_mod = thisPokemon.Attack;
                defense_mod = targetPokemon.Defense;
            }
            //Debug.Log(string.Format("atk: {0} def: {1}", attack_mod, defense_mod));
        }

        private static void set_attack_and_def(string atkCat, PokemonBase self, PokemonBase target)
        {
            if (atkCat == Consts.Special)                  //we are calculating a special attack
            {
                attack_mod = self.Special_Attack;
                defense_mod = target.Special_Defense;
            }
            if (atkCat == Consts.Physical)                  //we are calculating a physical attack
            {
                attack_mod = self.Attack;
                defense_mod = target.Defense;
            }
        }

        /// <summary>
        /// Takes the index or location of the pokemon in the attack list so we can fetch the base attack power for that attack
        /// <param name="index">the index of the move in the list of attacks</param>
        /// <returns> the base damage of the move</returns>
        /// </summary>
        private static float baseAttackPower(int index)
        {
            float base_damage = 0;
            base_damage = (float)MoveSets.attackList[index].power;
            //Debug.Log("Base Damage: " + base_damage);
            return base_damage;
        }

        /// <summary>
        ///  checks if the current attack is a STAB type attack or same type attack
        /// <param name="attackType">the attack type of the move being passed in</param>
        /// <param name="isPlayer">a boolean to see if the player is using the move or the enemy</param>
        /// <returns>a boolean that is true if the move is a stab type move or not</returns>
        /// </summary>
        private static bool isStab(string attackType)
        {
            if (attackType == thisPokemon.type1 || attackType == thisPokemon.type2)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Uses the index to get the type multiplier from the master list in the damage multiplier class
        /// <param name="attackType">the type of the attack</param>
        /// <param name="index">the index of the attack move in the attack list</param>
        /// <returns>the modifier of the attack move based off its attack type</returns>
        /// </summary>
        private static float fetchDmgMultModifier(string attackType)
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
        /// This method takes in the acuracy of the pokemon and calculates if it hits or not and returns a boolean
        /// value based on if it hits
        /// <param name="accuracy">the accuracy of the move being passed in</param>
        /// <returns>true if the move hit, false if it missed</returns>
        /// </summary>
        private static bool checkAccuracy_and_Hit(PokemonBase self, PokemonBase tar, string atkName, int accuracy, string cat)
        {
            if (cat == Consts.Status)
            {
                //Debug.Log("Status move, no accuracy check");
                return true;
            }

            if (accuracy == 0)
            {
                Debug.Log("Has 0 accuracy: " + atkName);
                return true;
            }

            float accStage = self.acc_stage;
            float accMod = accStage + 3f;
            if (accStage >= 1)
            {
                accMod *= 100;
                accMod /= 3f;
            }
            else
            {
                accMod = 300f / accMod;

            }

            float evadeStage = tar.evasive_stage;
            float evadeMod = evadeStage + 3;

            if (evadeStage >= 1)
            {
                evadeMod = 300f / evadeMod;
            }
            else
            {
                evadeMod *= 100;
                evadeMod /= 3f;
            }

            float prob = (accuracy) * (accMod / evadeMod);
            if (ignoreAcc_Evade(atkName))
            {
                prob = accuracy;
            }

            //Debug.Log(string.Format("move acc {0} self acc {1} target evasion {2} total probability {3} * {4} = {5}", accuracy, accMod, evadeMod, (accuracy), (accMod / evadeMod), prob));

            if (prob >= 100)
            {
                return true;
            }
            return Utilities.probability(prob, 100f);
        }

        /// <summary>
        /// Calculates the 1/16 chance every move has for getting a critical strike
        /// <param name="chance">the chance probability either (1/8) or (1/16)</param>
        /// <returns>true if the move crit, false if it did not</returns>
        ///</summary>
        private static bool isCrit(int chance)
        {
            float divider = 1f / (float)chance;
            divider *= 100f;
            //Debug.Log("crit chance: " + divider);
            return Utilities.probability(divider, 100f);
        }

        /// <summary>
        /// Handles the crit ratio of the pokemon and of the attack move
        /// </summary>
        /// <param name="atkName"> the name of the attack</param>
        /// <returns>the crit chance of the move either (1/8) or (1/16)</returns>
        private static int critChance(PokemonBase self, string atkName)
        {
            int stage = self.critRatio_stage;
            if (DexHolder.attackDex == null)
            {
                Debug.Log("Null attack dex?");
            }
            int atkratio = DexHolder.attackDex.GetCirtRatio(atkName);
            int total = stage + atkratio;
            int final;

            if (atkratio != 0 || stage != 0)
            {
                Debug.Log(string.Format("stage: {0} attackRatio: {1} final: {2}", stage, atkratio, total));
            }

            switch (total)
            {
                default:
                    final = 16;
                    break;

                case 0:
                    final = 16;
                    break;

                case 1:
                    final = 8;
                    break;

                case 2:
                    final = 2;
                    break;

                case 3:
                    final = 1;
                    break;

                case 4:
                    final = 1;
                    break;

                case 5:
                    final = 1;
                    break;

                case 6:
                    final = 1;
                    break;

            }
            return final;
        }

        /// <summary>
        /// This checks if we ignore accuracy and evasion, simply using a string array that we define
        /// </summary>
        /// <param name="atkName"></param>
        /// <returns></returns>
        private static bool ignoreAcc_Evade(string atkName)
        {
            string name = atkName.ToLower();

            string[] ignoreMoves = { "swift", "fissure", "guillotine", "horn drill" };
            for (int i = 0; i < ignoreMoves.Length; i++)
            {
                if (name == ignoreMoves[i])
                {
                    Debug.Log("This move ignores accuracy and evasion");
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// This checks for the semi-invulnerable condition (flying, undergouround ect...) and if the move ignores it.
        /// </summary>
        /// <param name="mr">move results</param>
        /// <returns>a bool determining if the pokemon move hit</returns>
        private static bool checkSemiInvulnerable(MoveResults mr)
        {
            bool hitCheck = mr.hit;
            if (targetPokemon.position != pokemonPosition.normal)
            {
                hitCheck = mr.hit && mr.ignoreSemiInvulerable;
            }
            return hitCheck;
        }
    }
}
