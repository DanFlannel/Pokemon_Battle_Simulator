using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public static class FBG_Atk_Switch
    {
        //TODO:
        //I need to set these before calling this method it will reduce a lot of code
        public static PokemonEntity target;
        public static PokemonEntity self;

        public static void statusAttacks(string name, bool isPlayer)
        {
            //Debug.Log("attack name: " + name);
            FBG_Atk_Methods.final_damage = 0;
            FBG_Atk_Methods.final_heal = 0;
            FBG_Atk_Methods.recoil = 0;
            string tempname = name.ToLower();
            int rnd;
            switch (tempname)
            {
                
                default:
                    Debug.Log("No status move with name " + name + " found");
                    break;
                //raises users defense by 2 stages
                case "acid armor":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 2, isPlayer);
                    break;
                //raises users speed by 2 stages
                case "agility":
                    FBG_Atk_Methods.changeStats(FBG_consts.speed, 2, isPlayer);
                    break;
                //raises users spDefense by 2 stages
                case "amnesia":
                    FBG_Atk_Methods.changeStats(FBG_consts.spDefense, 2, isPlayer);
                    break;
                //raises users defense by 2 stages
                case "barrier":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 2, isPlayer);
                    break;
                //confuses opponenet
                case "confuse ray":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(isPlayer, 10, rnd);
                    break;
                //chages users type of its first move
                case "conversion":
                    FBG_Atk_Methods.conversion(isPlayer, name);
                    break;
                //raises uers defense by 1 stage
                case "defense curl":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, isPlayer);
                    break;
                //disables enemies last move for a few turns
                case "disable":

                    break;
                //raises user evasive stage by one
                case "double team":

                    break;
                //lowers opponents accuracy by 1 stage
                case "flash":

                    break;
                //increases crit ratio...
                case "focus energy":

                    break;
                case "growl":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, -1, !isPlayer);
                    break;
                case "growth":
                    FBG_Atk_Methods.changeStats(FBG_consts.spAttack, 1, isPlayer);
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, isPlayer);
                    break;
                case "harden":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, isPlayer);
                    break;
                case "haze":
                    FBG_Atk_Methods.updateStatChange(FBG_consts.attack, 1, isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.defense, 1, isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.spAttack, 1, isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.spDefense, 1, isPlayer);

                    FBG_Atk_Methods.updateStatChange(FBG_consts.attack, 1, !isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.defense, 1, !isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.spAttack, 1, !isPlayer);
                    FBG_Atk_Methods.updateStatChange(FBG_consts.spDefense, 1, !isPlayer);
                    break;
                case "hypnosis":
                    rnd = UnityEngine.Random.Range(1, 3);
                    //puts the user to sleep for rnd turns
                    break;
                case "kinesis":
                    //lower enemy accuracy by 1 stage
                    break;
                case "leech seed":
                    FBG_Atk_Methods.one_eigth_perm(isPlayer);
                    FBG_Atk_Methods.leech_seed(isPlayer);
                    break;
                case "leer":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -1, !isPlayer);
                    break;
                case "light screen":
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.playerStats.hasLightScreen = true;
                        FBG_Atk_Methods.playerStats.lightScreenDuration = 5;
                    }
                    else
                    {
                        FBG_Atk_Methods.enemyStats.hasLightScreen = true;
                        FBG_Atk_Methods.enemyStats.lightScreenDuration = 5;
                    }
                    break;
                case "lovely kiss":
                    rnd = UnityEngine.Random.Range(1, 3);
                    //puts the user to sleep for rnd turns
                    break;
                case "meditate":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, isPlayer);
                    break;
                //preforms any move in the game at random?
                case "metronome":
                    break;
                //copies the opponents last move and replaces mimic with that
                case "mimic":
                    break;
                //raise evasion by 1 stage STOMP and STEAMROLLER do double damage against a minimized opponent
                case "minimize":
                    break;
                case "mirror move":         //preforms the opponents last move....
                    break;
                //no stat changes for 5 turns
                case "mist":
                    break;
                case "poison gas":
                    FBG_Atk_Methods.isPosioned(isPlayer, 100);
                    break;
                case "poison powder":
                    FBG_Atk_Methods.isPosioned(isPlayer, 100);
                    break;
                case "recover":
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.playerStats.maxHP / 2f;
                    }
                    else
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.enemyStats.maxHP / 2f;
                    }
                    break;
                case "reflect":             //halves the damage from physical attacks for 5 turns
                    break;
                case "rest":                //user falls asleep for 2 turns but health is fully recovered
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.playerStats.maxHP;
                    }
                    else
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.enemyStats.maxHP;
                    }
                    break;
                case "roar":                //opponent switches pokemon out
                    break;
                case "sand attack":         //lowers opponent accuracy by one stage
                    break;
                case "screech":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -2, !isPlayer);
                    break;
                case "sharpen":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, isPlayer);
                    break;
                case "sing":                //puts the user to sleep for 1-3 turns
                    rnd = UnityEngine.Random.Range(1, 3);
                    break;
                case "smokescreen":         //lower accuracy by one stage
                    break;
                case "soft boiled":
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.playerStats.maxHP / 2f;
                    }
                    else
                    {
                        FBG_Atk_Methods.final_heal = FBG_Atk_Methods.enemyStats.maxHP / 2f;
                    }
                    break;
                case "splash":              //This does nothing
                    FBG_Atk_Methods.final_damage = 0;
                    break;
                case "spore":               //puts the opponent to sleep for 1-3 turns
                    rnd = UnityEngine.Random.Range(1, 3);
                    break;
                case "string shot":
                    FBG_Atk_Methods.changeStats(FBG_consts.speed, -2, !isPlayer);
                    break;
                case "stun spore":
                    FBG_Atk_Methods.isParalized(isPlayer, 100);
                    break;
                case "substitute":
                    FBG_Atk_Methods.substitute(isPlayer);
                    break;
                case "supersonic":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(isPlayer, 100, rnd);
                    break;
                case "swords dance":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 2, isPlayer);
                    break;
                case "tail whip":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -1, !isPlayer);
                    break;
                case "teleport":
                    //say something stupid here
                    break;
                case "thunder wave":
                    FBG_Atk_Methods.isParalized(isPlayer, 100);
                    break;
                case "toxic":               //increasingly does more toxic damage at the end of each turn, starts at 1/16
                    FBG_Atk_Methods.toxic(isPlayer);
                    break;
                case "transform":           //takes the attacks of the opponent
                    break;
                case "whirlwind":           //blows the opponent away if they are a lower level
                    break;
                case "withdraw":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, isPlayer);
                    break;
            }
            //Debug.Log("Did a status move!");
            //updateTurnController(isPlayer, name);
        }

        public static void physicalAttacks(string name, float predictedDamage, bool isPlayer)
        {
            FBG_Atk_Methods.final_damage = 0;
            FBG_Atk_Methods.final_heal = 0;
            FBG_Atk_Methods.recoil = 0;
            string tempname = name.ToLower();
            bool isHit = false;
            int rnd;
            switch (tempname)
            {
                default:
                    Debug.Log("No physical attack with name " + name.ToLower() + " found");
                    break;
                case "barrage":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "bide":                //waits 2 turns then deals back double.... :(
                    break;
                case "bind":                //need to create a damage over time effect here for rndBind turns
                    rnd = UnityEngine.Random.Range(4, 5);
                    FBG_Atk_Methods.one_sixteenth_temp(isPlayer, rnd);
                    break;
                case "bite":
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "body slam":
                    FBG_Atk_Methods.isParalized(isPlayer, 30);
                    break;
                case "bone club":
                    FBG_Atk_Methods.isFlinched(isPlayer, 10);
                    break;
                case "bonemerang":
                    rnd = 2;
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "clamp":               //traps for 4-5 turns dealing 1/16th damage
                    rnd = UnityEngine.Random.Range(4, 5);
                    FBG_Atk_Methods.one_sixteenth_temp(isPlayer, rnd);
                    break;
                case "comet punch":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "constrict":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, !isPlayer);
                    }
                    break;
                case "counter":             //hits back with 2x power iff is hit with physical attack
                    break;
                case "crabhammer":          //has a 1/8 crit ratio not a 1/16.... have to recalculate for this
                    break;
                case "cut":                 //no additional effects
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "dig":                 //redo based off of turn controller
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.playerStats.isUnderground = true;
                        FBG_Atk_Methods.playerStats.cachedDamage = predictedDamage;
                    }
                    else
                    {
                        FBG_Atk_Methods.enemyStats.isUnderground = true;
                        FBG_Atk_Methods.enemyStats.cachedDamage = predictedDamage;
                    }
                    predictedDamage = 0;
                    break;
                case "dizzy punch":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(isPlayer, 20, rnd);
                    break;
                case "double kick":
                    rnd = 2;
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "double slap":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "double edge":
                    FBG_Atk_Methods.recoil = predictedDamage / 3f;
                    FBG_Atk_Methods.recoil = Mathf.Round(FBG_Atk_Methods.recoil);
                    break;
                case "drill peck":          //no additional effects
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "earthquake":
                    predictedDamage = FBG_Atk_Methods.earthQuake(isPlayer, predictedDamage);
                    break;
                case "egg bomb":            //no additional effects 
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "explosion":           //causes user to faint
                    if (isPlayer)
                        FBG_Atk_Methods.playerStats.curHp = 0;
                    else
                        FBG_Atk_Methods.enemyStats.curHp = 0;
                    break;
                case "fire punch":
                    FBG_Atk_Methods.isBurned(isPlayer, 10);
                    break;
                case "fissure":
                    FBG_Atk_Methods.oneHitKO(isPlayer);
                    break;
                case "fly":
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.playerStats.isFlying = true;
                        FBG_Atk_Methods.playerStats.cachedDamage = predictedDamage;
                    }
                    else
                    {
                        FBG_Atk_Methods.enemyStats.isFlying = true;
                        FBG_Atk_Methods.enemyStats.cachedDamage = predictedDamage;
                    }
                    predictedDamage = 0;
                    break;
                case "fury attack":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "fury swipes":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "guillotine":
                    FBG_Atk_Methods.oneHitKO(isPlayer);
                    break;
                case "headbutt":
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "high jump kick":      //if this misses it casues 1/2 of the damage it would have inflicted on the user
                    break;
                case "horn attack":         //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "horn drill":
                    FBG_Atk_Methods.oneHitKO(isPlayer);
                    break;
                case "hyper fang":
                    FBG_Atk_Methods.isFlinched(isPlayer, 10);
                    break;
                case "ice punch":
                    FBG_Atk_Methods.isFrozen(isPlayer, 10);
                    break;
                case "jump kick":           //lose 1/2 hp is the user misses just like high jump kick
                    break;
                case "karate chop":         //high crit ratio 1/8 versus 1/16
                    break;
                case "leech life":
                    FBG_Atk_Methods.final_heal = Mathf.Round(predictedDamage / 2f);
                    break;
                case "low kick":
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "mega kick":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "mega punch":          //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "pay day":             //small amount of money at the end of the battle??
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "peck":                //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "pin missile":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "poison sting":        //chance to poison the target
                    FBG_Atk_Methods.isPosioned(isPlayer, 30);
                    break;
                case "pound":               //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "quick attack":        //has +1 priority
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "rage":                //while rage is active, user increases his/her attack by one stage each time the user is hit
                    break;
                case "razor leaf":          //high crit ratio
                    break;
                case "rock slide":
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "rock throw":          //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "rolling kick":
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "scratch":             //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "seismic toss":
                    predictedDamage = FBG_Atk_Methods.levelBasedDamage(isPlayer);
                    break;
                case "self destruct":       //user faints
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.playerStats.curHp = 0;
                    }
                    else
                    {
                        FBG_Atk_Methods.enemyStats.curHp = 0;
                    }
                    break;
                case "skull bash":          //charges first turn, raising defense, hits on the second turn
                    break;
                case "sky attack":          //charges on first turn, hits on second, 30% flinch chance
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "slam":                //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "slash":               //high crit ratio 1/8 not 1/16
                    break;
                case "spike cannon":
                    rnd = UnityEngine.Random.Range(2, 5);
                    predictedDamage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;
                case "stomp":               //if minimized *2 damage
                    FBG_Atk_Methods.isFlinched(isPlayer, 30);
                    break;
                case "strength":            //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "struggle":            //hurts the user if all the pp are gone
                    break;
                case "submission":
                    FBG_Atk_Methods.recoil = Mathf.Round(FBG_Atk_Methods.final_damage / 4f);
                    break;
                case "super fang":
                    if (isPlayer)
                    {
                        predictedDamage = FBG_Atk_Methods.enemyStats.curHp / 2f;
                    }
                    else
                    {
                        predictedDamage = FBG_Atk_Methods.playerStats.curHp / 2f;
                    }
                    break;
                case "tackle":              //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "take down":
                    FBG_Atk_Methods.recoil = Mathf.Round(predictedDamage / 4f);
                    break;
                case "thrash":              //attacks for 2-3 turns, but cannot switch out or use a different attack
                    break;
                case "thunder punch":
                    FBG_Atk_Methods.isParalized(isPlayer, 10);
                    break;
                case "twineedle":           //20% chance to poison the target
                    FBG_Atk_Methods.multiAttack(2, name);
                    FBG_Atk_Methods.isPosioned(isPlayer, 20);
                    break;
                case "vice grip":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "vine whip":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "waterfall":
                    FBG_Atk_Methods.isFlinched(isPlayer, 20);
                    break;
                case "wing attack":         //no additional effect, can hit non-adjacent pokemon in triple battles
                    break;
                case "wrap":                //causes 1/16th damage for 4-5 turns
                    rnd = UnityEngine.Random.Range(4, 5);
                    FBG_Atk_Methods.one_sixteenth_temp(isPlayer, rnd);
                    break;
            }
            FBG_Atk_Methods.final_damage = predictedDamage;
            //Debug.Log("Did a Physical Attack!");
            //Debug.Log("final heal = " + final_heal);
            //Debug.Log("final damage = " + final_damage);
            FBG_Atk_Methods.updateTurnController(isPlayer, name, AttackType.physical);
        }

        public static void specialAttacks(string name, float predictedDamage, bool isPlayer)
        {
            //Debug.Log("attack name: " + name);
            FBG_Atk_Methods.final_damage = 0;
            FBG_Atk_Methods.final_heal = 0;
            FBG_Atk_Methods.recoil = 0;
            string tempname = name.ToLower();
            int rnd;
            bool isHit = false;
            switch (tempname)
            {
                default:
                    Debug.Log("No special attack with name " + name + " found");
                    break;
                case "absorb":
                    FBG_Atk_Methods.final_heal = predictedDamage / 2f;
                    break;
                case "acid":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.spDefense, -1, !isPlayer);
                    }
                    break;
                case "aurora beam":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.attack, -1, !isPlayer);
                    }
                    break;
                case "blizzard":
                    FBG_Atk_Methods.isFrozen(isPlayer, 10);
                    break;
                case "bubble":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, !isPlayer);
                    }
                    break;
                case "bubble beam":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, !isPlayer);
                    }
                    break;
                case "confusion":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(isPlayer, 10, rnd);
                    break;
                case "dragon rage":
                    predictedDamage = 40;
                    break;
                case "dream eater":
                    predictedDamage = FBG_Atk_Methods.dreamEater(isPlayer, predictedDamage);
                    break;
                case "ember":
                    FBG_Atk_Methods.isBurned(isPlayer, 10);
                    break;
                case "fire blast":
                    FBG_Atk_Methods.isBurned(isPlayer, 10);
                    break;
                case "fire spin":           //Damages the target for 4-5 turns
                    break;
                case "flamethrower":
                    FBG_Atk_Methods.isBurned(isPlayer, 10);
                    break;
                case "gust":
                    if (isPlayer)
                    {
                        if (FBG_Atk_Methods.enemyStats.isFlying)
                            predictedDamage *= 2f;
                    }
                    else
                    {
                        if (FBG_Atk_Methods.playerStats.isFlying)
                            predictedDamage *= 2f;
                    }
                    break;
                case "hydro pump":          //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                case "hyper beam":          //cannot move next turn
                    if (isPlayer)
                    {
                        FBG_Atk_Methods.playerStats.canAttack = false;
                    }
                    else
                    {
                        FBG_Atk_Methods.enemyStats.canAttack = false;
                    }
                    break;
                case "ice beam":
                    FBG_Atk_Methods.isFrozen(isPlayer, 10);
                    break;
                case "mega drain":
                    FBG_Atk_Methods.final_heal = Mathf.Round(predictedDamage / 2f);
                    break;
                case "night shade":
                    predictedDamage = FBG_Atk_Methods.levelBasedDamage(isPlayer);
                    break;
                case "petal dance":         //attacks for 2-3 turns, cannot be switched out, then becomes confused
                    break;
                case "psybeam":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(isPlayer, 10, rnd);
                    break;
                case "psychic":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.spDefense, -1, !isPlayer);
                    }
                    break;
                case "psywave":
                    float mod = UnityEngine.Random.Range(.5f, 1.5f);
                    predictedDamage = FBG_Atk_Methods.levelBasedDamage(isPlayer) * mod;
                    break;
                case "razor wind":          //charges the first turn then attacks the second
                    FBG_Atk_Methods.final_damage = 0;
                    break;
                case "sludge":              //30% chance to poison the target
                    FBG_Atk_Methods.isPosioned(isPlayer, 30);
                    break;
                case "smog":                //40% chance to poison the target
                    FBG_Atk_Methods.isPosioned(isPlayer, 40);
                    break;
                case "solar beam":          //charges on the fist turn, hits on the second
                    break;
                case "sonic boom":
                    predictedDamage = FBG_Atk_Methods.sonicBoom(isPlayer);
                    break;
                case "surf":                //does double damage if the pokemon used dive(introduced in gen3)
                    break;
                case "swift":               //ignores evasiveness and accuracy
                    break;
                case "thunder":
                    FBG_Atk_Methods.isParalized(isPlayer, 30);
                    break;
                case "thunder shock":
                    FBG_Atk_Methods.isParalized(isPlayer, 10);
                    break;
                case "thunder bolt":
                    FBG_Atk_Methods.isParalized(isPlayer, 10);
                    break;
                case "tri attack":          //I changed this from 6.67% chance for each to 10%
                    rnd = UnityEngine.Random.Range(4, 5);
                    FBG_Atk_Methods.isParalized(isPlayer, 6.67f);
                    FBG_Atk_Methods.isBurned(isPlayer, 6.67f);
                    FBG_Atk_Methods.isFrozen(isPlayer, 6.67f);
                    break;
                case "water gun":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
            }
            //Check for lightscreen to halve special attack damage
            FBG_Atk_Methods.final_damage = predictedDamage;

            Debug.Log("Did a Special Attack!");
            Debug.Log("final heal = " + FBG_Atk_Methods.final_heal);
            Debug.Log("final damage = " + FBG_Atk_Methods.final_damage);

            FBG_Atk_Methods.updateTurnController(isPlayer, name, AttackType.special);
        }
    }
}
