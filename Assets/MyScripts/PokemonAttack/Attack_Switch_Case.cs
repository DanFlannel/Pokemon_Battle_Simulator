using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Attack_Switch_Case : Attack_Special_Methods {

    void Start()
    {
        SpecialCasesInit();
    }

    public void statusAttacks(string name, bool isPlayer)
    {
        //Debug.Log("attack name: " + name);
        final_damage = 0;
        final_heal = 0;
        recoil = 0;
        string tempname = name.ToLower();
        int rnd;
        switch (tempname)
        {
            default:
                Debug.Log("No status move with name " + name + " found");
                break;
            //raises users defense by 2 stages
            case "acid armor":          
                changeStats(defense, 2, isPlayer);
                break;
            //raises users speed by 2 stages
            case "agility":             
                changeStats(speed, 2, isPlayer);
                break;
            //raises users spDefense by 2 stages
            case "amnesia":             
                changeStats(spDefense, 2, isPlayer);
                break;
            //raises users defense by 2 stages
            case "barrier":
                changeStats(defense, 2, isPlayer);
                break;
            //confuses opponenet
            case "confuse ray":
                rnd = UnityEngine.Random.Range(1, 4);
                isConfused(isPlayer, 10, rnd);                      
                break;
            //chages users type of its first move
            case "conversion":
                conversion(isPlayer, name);
                break;
            //raises uers defense by 1 stage
            case "defense curl":
                changeStats(defense, 1, isPlayer);
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
                changeStats(attack, -1, !isPlayer);
                break;
            case "growth":
                changeStats(spAttack, 1, isPlayer);
                changeStats(attack, 1, isPlayer);
                break;
            case "harden":
                changeStats(defense, 1, isPlayer);
                break;
            case "haze":
                updateStatChange(attack, 1, isPlayer);
                updateStatChange(defense, 1, isPlayer);
                updateStatChange(spAttack, 1, isPlayer);
                updateStatChange(spDefense, 1, isPlayer);

                updateStatChange(attack, 1, !isPlayer);
                updateStatChange(defense, 1, !isPlayer);
                updateStatChange(spAttack, 1, !isPlayer);
                updateStatChange(spDefense, 1, !isPlayer);
                break;
            case "hypnosis":
                rnd = UnityEngine.Random.Range(1, 3);
                                        //puts the user to sleep for rnd turns
                break;
            case "kinesis":
                                        //lower enemy accuracy by 1 stage
                break;
            case "leech seed":
                one_eigth_perm(isPlayer);
                leech_seed(isPlayer);
                break;
            case "leer":
                changeStats(defense, -1, !isPlayer);
                break;
            case "light screen":
                if (isPlayer)
                {
                    playerStats.hasLightScreen = true;
                    playerStats.lightScreenDuration = 5;
                }
                else
                {
                    enemyStats.hasLightScreen = true;
                    enemyStats.lightScreenDuration = 5;
                }
                break;
            case "lovely kiss":
                rnd = UnityEngine.Random.Range(1, 3);
                                        //puts the user to sleep for rnd turns
                break;
            case "meditate":
                changeStats(attack, 1, isPlayer);
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
                isPosioned(isPlayer, 100);
                break;
            case "poison powder":       
                isPosioned(isPlayer, 100);
                break;
            case "recover":
                if (isPlayer)
                {
                    final_heal = playerStats.maxHP / 2f;
                }
                else
                {
                    final_heal = enemyStats.maxHP / 2f;
                }
                break;
            case "reflect":             //halves the damage from physical attacks for 5 turns
                break;
            case "rest":                //user falls asleep for 2 turns but health is fully recovered
                if (isPlayer)
                {
                    final_heal = playerStats.maxHP;
                }
                else
                {
                    final_heal = enemyStats.maxHP;
                }
                break;
            case "roar":                //opponent switches pokemon out
                break;
            case "sand attack":         //lowers opponent accuracy by one stage
                break;
            case "screech":
                changeStats(defense, -2, !isPlayer);
                break;
            case "sharpen":
                changeStats(attack, 1, isPlayer);
                break;
            case "sing":                //puts the user to sleep for 1-3 turns
                rnd = UnityEngine.Random.Range(1, 3);
                break;
            case "smokescreen":         //lower accuracy by one stage
                break;
            case "soft boiled":
                if (isPlayer)
                {
                    final_heal = playerStats.maxHP/2f;
                }
                else
                {
                    final_heal = enemyStats.maxHP/2f;
                }
                break;
            case "splash":              //This does nothing
                final_damage = 0;
                break;
            case "spore":               //puts the opponent to sleep for 1-3 turns
                rnd = UnityEngine.Random.Range(1, 3);
                break;
            case "string shot":
                changeStats(speed, -2, !isPlayer);
                break;
            case "stun spore":
                isParalized(isPlayer, 100);
                break;
            case "substitute":
                substitute(isPlayer);
                break;
            case "supersonic":
                rnd = UnityEngine.Random.Range(1, 4);
                isConfused(isPlayer, 100, rnd);
                break;
            case "swords dance":
                changeStats(attack, 2, isPlayer);
                break;
            case "tail whip":
                changeStats(defense, -1, !isPlayer);
                break;
            case "teleport":
                //say something stupid here
                break;
            case "thunder wave":
                isParalized(isPlayer, 100);
                break;
            case "toxic":               //increasingly does more toxic damage at the end of each turn, starts at 1/16
                toxic(isPlayer); 
                break;
            case "transform":           //takes the attacks of the opponent
                break;
            case "whirlwind":           //blows the opponent away if they are a lower level
                break;
            case "withdraw":
                changeStats(defense, 1, isPlayer);
                break;
        }
        Debug.Log("Did a status move!");
        updateTurnController(isPlayer, name);
    }

    public void physicalAttacks(string name, float predictedDamage, bool isPlayer)
    {
        final_damage = 0;
        final_heal = 0;
        recoil = 0;
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
                predictedDamage = multiAttack(rnd, name);
                break;
            case "bide":                //waits 2 turns then deals back double.... :(
                break;
            case "bind":                //need to create a damage over time effect here for rndBind turns
                rnd = UnityEngine.Random.Range(4, 5);
                one_sixteenth_temp(isPlayer, rnd);
                break;
            case "bite":
                isFlinched(isPlayer, 30);
                break;
            case "body slam":
                isParalized(isPlayer, 30);
                break;
            case "bone club":
                isFlinched(isPlayer, 10);
                break;
            case "bonemerang":
                rnd = 2;
                predictedDamage = multiAttack(rnd, name);
                break;
            case "clamp":               //traps for 4-5 turns dealing 1/16th damage
                rnd = UnityEngine.Random.Range(4, 5);
                one_sixteenth_temp(isPlayer, rnd);
                break;
            case "comet punch":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd,name);
                break;
            case "constrict":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "counter":             //hits back with 2x power iff is hit with physical attack
                break;
            case "crabhammer":          //has a 1/8 crit ratio not a 1/16.... have to recalculate for this
                break;
            case "cut":                 //no additional effects
                break;
            case "dig":                 //redo based off of turn controller
                if (isPlayer)
                {
                    playerStats.isUnderground = true;
                    playerStats.cachedDamage = predictedDamage;
                }
                else
                {
                    enemyStats.isUnderground = true;
                    enemyStats.cachedDamage = predictedDamage;
                }
                predictedDamage = 0;
                break;
            case "dizzy punch":
                rnd = UnityEngine.Random.Range(1, 4);
                isConfused(isPlayer, 20, rnd);
                break;
            case "double kick":
                rnd = 2;
                predictedDamage = multiAttack(rnd,name);
                break;
            case "double slap":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd,name);
                break;
            case "double edge":
                recoil = predictedDamage / 3f;
                recoil = Mathf.Round(recoil);
                break;
            case "drill peck":          //no additional effects
                break;
            case "earthquake":
                predictedDamage = earthQuake(isPlayer, predictedDamage);
                break;
            case "egg bomb":            //no additional effects 
                break;
            case "explosion":           //causes user to faint
                if (isPlayer)
                    playerStats.curHp = 0;
                else
                    enemyStats.curHp = 0;
                break;
            case "fire punch":
                isBurned(isPlayer, 10);
                break;
            case "fissure":
                oneHitKO(isPlayer);
                break;
            case "fly":
                if (isPlayer)
                {
                    playerStats.isFlying = true;
                    playerStats.cachedDamage = predictedDamage;
                }
                else
                {
                    enemyStats.isFlying = true;
                    enemyStats.cachedDamage = predictedDamage;
                }
                predictedDamage = 0; 
                break;
            case "fury attack":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd,name);
                break;
            case "fury swipes":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd,name);
                break;
            case "guillotine":
                oneHitKO(isPlayer);
                break;
            case "headbutt":
                isFlinched(isPlayer, 30);
                break;
            case "high jump kick":      //if this misses it casues 1/2 of the damage it would have inflicted on the user
                break;
            case "horn attack":         //no additional effect
                break;
            case "horn drill":
                oneHitKO(isPlayer);
                break;
            case "hyper fang":
                isFlinched(isPlayer, 10);
                break;
            case "ice punch":
                isFrozen(isPlayer, 10);
                break;
            case "jump kick":           //lose 1/2 hp is the user misses just like high jump kick
                break;
            case "karate chop":         //high crit ratio 1/8 versus 1/16
                break;
            case "leech life":
                final_heal = Mathf.Round(predictedDamage / 2f);
                break;
            case "low kick":
                isFlinched(isPlayer, 30);
                break;
            case "mega kick":           //no additional effect
                break;
            case "mega punch":          //no additional effect
                break;
            case "pay day":             //small amount of money at the end of the battle??
                break;
            case "peck":                //no additional effect
                break;
            case "pin missile":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd, name);
                break;
            case "poison sting":        //chance to poison the target
                isPosioned(isPlayer, 30);
                break;
            case "pound":               //no additional effect
                break;
            case "quick attack":        //has +1 priority
                break;
            case "rage":                //while rage is active, user increases his/her attack by one stage each time the user is hit
                break;
            case "razor leaf":          //high crit ratio
                break;
            case "rock slide":
                isFlinched(isPlayer, 30);
                break;
            case "rock throw":          //no additional effect
                break;
            case "rolling kick":
                isFlinched(isPlayer, 30);
                break;
            case "scratch":             //no additional effect
                break;
            case "seismic toss":
                predictedDamage = levelBasedDamage(isPlayer);
                break;
            case "self destruct":       //user faints
                if (isPlayer)
                {
                    playerStats.curHp = 0;
                }
                else
                {
                    enemyStats.curHp = 0;
                }
                break;
            case "skull bash":          //charges first turn, raising defense, hits on the second turn
                break;
            case "sky attack":          //charges on first turn, hits on second, 30% flinch chance
                isFlinched(isPlayer, 30);
                break;
            case "slam":                //no additional effect
                break;
            case "slash":               //high crit ratio 1/8 not 1/16
                break;
            case "spike cannon":
                rnd = UnityEngine.Random.Range(2, 5);
                predictedDamage = multiAttack(rnd, name);
                break;
            case "stomp":               //if minimized *2 damage
                isFlinched(isPlayer, 30);
                break;
            case "strength":            //no additional effect
                break;
            case "struggle":            //hurts the user if all the pp are gone
                break;
            case "submission":
                recoil = Mathf.Round(final_damage / 4f);
                break;
            case "super fang":
                if (isPlayer)
                {
                    predictedDamage = enemyStats.curHp / 2f;
                }
                else
                {
                    predictedDamage = playerStats.curHp / 2f;
                }
                break;
            case "tackle":              //no additional effect
                break;
            case "take down":
                recoil = Mathf.Round(predictedDamage / 4f);
                break;
            case "thrash":              //attacks for 2-3 turns, but cannot switch out or use a different attack
                break;
            case "thunder punch":
                isParalized(isPlayer, 10);
                break;
            case "twineedle":           //20% chance to poison the target
                multiAttack(2, name);
                isPosioned(isPlayer, 20);
                break;
            case "vice grip":           //no additional effect
                break;
            case "vine whip":           //no additional effect
                break;
            case "waterfall":
                isFlinched(isPlayer, 20);
                break;
            case "wing attack":         //no additional effect, can hit non-adjacent pokemon in triple battles
                break;
            case "wrap":                //causes 1/16th damage for 4-5 turns
                rnd = UnityEngine.Random.Range(4, 5);
                one_sixteenth_temp(isPlayer, rnd);
                break;
        }
        final_damage = predictedDamage;

        Debug.Log("Did a Physical Attack!");
        Debug.Log("final heal = " + final_heal);
        Debug.Log("final damage = " + final_damage);


        updateTurnController(isPlayer, name);

    }

    public void specialAttacks(string name, float predictedDamage, bool isPlayer)
    {
        //Debug.Log("attack name: " + name);
        final_damage = 0;
        final_heal = 0;
        recoil = 0;
        string tempname = name.ToLower();
        int rnd;
        bool isHit = false;
        switch (tempname)
        {
            default:
                Debug.Log("No special attack with name " + name + " found");
                break;
            case "absorb":
                final_heal = predictedDamage / 2f;
                break;
            case "acid":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(spDefense, -1, !isPlayer);
                }
                break;
            case "aurora beam":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(attack, -1, !isPlayer);
                }
                break;
            case "blizzard":
                isFrozen(isPlayer, 10);
                break;
            case "bubble":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "bubble beam":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(speed, -1, !isPlayer);
                }
                break;
            case "confusion":
                rnd = UnityEngine.Random.Range(1, 4);
                isConfused(isPlayer, 10, rnd);
                break;
            case "dragon rage":
                predictedDamage = 40;
                break;
            case "dream eater":
                predictedDamage = dreamEater(isPlayer, predictedDamage);
                break;
            case "ember":
                isBurned(isPlayer, 10);
                break;
            case "fire blast":
                isBurned(isPlayer, 10);
                break;
            case "fire spin":           //Damages the target for 4-5 turns
                break;
            case "flamethrower":
                isBurned(isPlayer, 10);
                break;
            case "gust":
                if (isPlayer)
                {
                    if (enemyStats.isFlying)
                        predictedDamage *= 2f;
                }
                else
                {
                    if(playerStats.isFlying)
                        predictedDamage *= 2f;
                }
                break;
            case "hydro pump":          //no additional effect
                break;
            case "hyper beam":          //cannot move next turn
                if (isPlayer)
                {
                    playerStats.canAttack = false;
                }
                else
                    enemyStats.canAttack = false;
                break;
            case "ice beam":
                isFrozen(isPlayer, 10);
                break;
            case "mega drain":
                final_heal = Mathf.Round(predictedDamage / 2f);
                break;
            case "night shade":
                predictedDamage = levelBasedDamage(isPlayer);
                break;
            case "petal dance":         //attacks for 2-3 turns, cannot be switched out, then becomes confused
                break;
            case "psybeam":
                rnd = UnityEngine.Random.Range(1, 4);
                isConfused(isPlayer, 10, rnd);
                break;
            case "psychic":
                isHit = Chance_100(10);
                if (isHit)
                {
                    changeStats(spDefense, -1, !isPlayer);
                }
                break;
            case "psywave":
                float mod = UnityEngine.Random.Range(.5f, 1.5f);
                predictedDamage = levelBasedDamage(isPlayer) * mod;
                break;
            case "razor wind":          //charges the first turn then attacks the second
                final_damage = 0;
                break;
            case "sludge":              //30% chance to poison the target
                isPosioned(isPlayer, 30);
                break;
            case "smog":                //40% chance to poison the target
                isPosioned(isPlayer, 40);
                break;
            case "solar beam":          //charges on the fist turn, hits on the second
                break;
            case "sonic boom":
                predictedDamage = sonicBoom(isPlayer);
                break;
            case "surf":                //does double damage if the pokemon used dive(introduced in gen3)
                break;
            case "swift":               //ignores evasiveness and accuracy
                break;
            case "thunder":
                isParalized(isPlayer, 30);
                break;
            case "thunder shock":
                isParalized(isPlayer, 10);
                break;
            case "thunder bolt":
                isParalized(isPlayer, 10);
                break;
            case "tri attack":          //I changed this from 6.67% chance for each to 10%
                rnd = UnityEngine.Random.Range(4, 5);
                isParalized(isPlayer, 6.67f);
                isBurned(isPlayer, 6.67f);
                isFrozen(isPlayer, 6.67f);
                break;
            case "water gun":           //no additional effect
                break;
        }
        //Check for lightscreen to halve special attack damage
        final_damage = predictedDamage;

        Debug.Log("Did a Special Attack!");
        Debug.Log("final heal = " + final_heal);
        Debug.Log("final damage = " + final_damage);

        updateTurnController(isPlayer, name);
    }
}
