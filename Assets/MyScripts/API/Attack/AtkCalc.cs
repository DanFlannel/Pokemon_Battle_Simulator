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
            MoveResults MR = new MoveResults(self, tar, atkName);
            targetPokemon = tar;
            thisPokemon = self;

            atkName = checkCachedAttack(atkName, self); ;

            Debug.LogWarning(string.Format(" {0} is using {1} ", self.Name, atkName));

            int atkIndex = getAttackListIndex(atkName);
            string atkCat = DexHolder.attackDex.getAttack(atkName).cat;
            string atkType = DexHolder.attackDex.getAttack(atkName).type;
            MR.crit = new CritCalculator(self, atkName).sucess;

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
        public static float GenBaseDamage(string atkName)
        {
            int atkIndex = getAttackListIndex(atkName);
            string atkCat = MoveSets.attackList[atkIndex].cat;
            string atkType = MoveSets.attackList[atkIndex].type;
            MoveResults MR = new MoveResults(thisPokemon, targetPokemon, atkName);

            return GenBaseDamage(atkName, atkCat, atkType, atkIndex, thisPokemon, MR);
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

            if (mr.crit)
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
    }
}
