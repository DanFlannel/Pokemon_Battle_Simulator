using UnityEngine;
using System.Collections;

namespace FatBobbyGaming
{
    public static class FBG_Atk_Switch
    {
        //TODO:
        //I need to set these before calling this method it will reduce a lot of code.
        //
        private static FBG_Pokemon target;
        private static FBG_Pokemon self;
        private static MoveResults moveRes;

        private static float damage;
        private static float heal;
        private static float recoil;
        private static string stageName;
        private static int stageDiff;

        public static void setPokemon(FBG_Pokemon tar, FBG_Pokemon s, MoveResults mr)
        {
            target = tar;
            self = s;
            moveRes = mr;

            damage = 0;
            heal = 0;
            recoil = 0;
            stageName = "";
            stageDiff = 0;
        }

        public static move_DmgReport statusAttacks(string name)
        {
            string tempname = name.ToLower();
            int rnd;
            switch (tempname)
            {
                
                default:
                    Debug.Log("No status move with name " + name + " found");
                    break;

                //raises users defense by 2 stages
                case "acid armor":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 2, self);
                    stageName = FBG_consts.defense;
                    stageDiff = 2;
                    break;

                //raises users speed by 2 stages
                case "agility":
                    FBG_Atk_Methods.changeStats(FBG_consts.speed, 2, self);
                    stageName = FBG_consts.speed;
                    stageDiff = 2;
                    break;

                //raises users spDefense by 2 stages
                case "amnesia":
                    FBG_Atk_Methods.changeStats(FBG_consts.spDefense, 2, self);
                    stageName = FBG_consts.spDefense;
                    stageDiff = 2;
                    break;

                //raises users defense by 2 stages
                case "barrier":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 2, self);
                    stageName = FBG_consts.defense;
                    stageDiff = 2;
                    break;

                //confuses opponenet
                case "confuse ray":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(target, 10, rnd);
                    break;

                //chages users type of its first move
                case "conversion":
                    FBG_Atk_Methods.conversion(self);
                    break;

                //raises uers defense by 1 stage
                case "defense curl":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, self);
                    stageName = FBG_consts.defense;
                    stageDiff = 1;
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
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, -1, target);
                    stageName = FBG_consts.attack;
                    stageDiff = -1;
                    break;

                case "growth":
                    FBG_Atk_Methods.changeStats(FBG_consts.spAttack, 1, self);
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, self);
                    stageName = FBG_consts.attack + " & " + FBG_consts.spAttack;
                    stageDiff = 1;
                    break;

                case "harden":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, self);
                    stageName = FBG_consts.defense;
                    stageDiff = 1;
                    break;

                case "haze":
                    self.resetStatStages();
                    target.resetStatStages();
                    stageName = "reset all stat changes";
                    stageDiff = 0;
                    break;

                case "hypnosis":
                    rnd = UnityEngine.Random.Range(1, 3);
                    //puts the user to sleep for rnd turns
                    break;

                case "kinesis":
                    //lower enemy accuracy by 1 stage
                    break;

                case "leech seed":
                    break;

                case "leer":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -1, target);
                    stageName = FBG_consts.defense;
                    stageDiff = -1;
                    break;

                case "light screen":
                    break;

                case "lovely kiss":
                    rnd = UnityEngine.Random.Range(1, 3);
                    //puts the user to sleep for rnd turns
                    break;

                case "meditate":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, self);
                    stageName = FBG_consts.attack;
                    stageDiff = 1;
                    break;

                //preforms any move in the game at random?
                case "metronome":
                    rnd = Random.Range(0, FBG_Atk_Data.attackList.Count);
                    string atkName = FBG_Atk_Data.attackList[rnd].name;
                    FBG_Atk_Calc.calculateAttackEffect(target, self, atkName);
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
                    FBG_Atk_Methods.isPosioned(target, 100);
                    break;

                case "poison powder":
                    FBG_Atk_Methods.isPosioned(target, 100);
                    break;

                case "recover":
                    heal = self.maxHP / 2f;
                    break;

                case "reflect":             //halves the damage from physical attacks for 5 turns
                    break;

                case "rest":                //user falls asleep for 2 turns but health is fully recovered
                        heal = self.maxHP;
                    break;

                case "roar":                //opponent switches pokemon out
                    break;

                case "sand attack":         //lowers opponent accuracy by one stage
                    break;

                case "screech":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -2, target);
                    stageName = FBG_consts.defense;
                    stageDiff = -2;
                    break;

                case "sharpen":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 1, self);
                    stageName = FBG_consts.attack;
                    stageDiff = 1;
                    break;

                case "sing":                //puts the user to sleep for 1-3 turns
                    rnd = UnityEngine.Random.Range(1, 3);
                    break;

                case "smokescreen":         //lower accuracy by one stage
                    break;

                case "soft boiled":
                    heal = self.maxHP / 2f;
                    break;

                case "splash":              //This does nothing
                    damage = 0;
                    break;

                case "spore":               //puts the opponent to sleep for 1-3 turns
                    rnd = UnityEngine.Random.Range(1, 3);
                    break;

                case "string shot":
                    FBG_Atk_Methods.changeStats(FBG_consts.speed, -2, target);
                    stageName = FBG_consts.speed;
                    stageDiff = -2;
                    break;

                case "stun spore":
                    FBG_Atk_Methods.isParalized(target, 100);
                    break;

                case "substitute":
                    FBG_Atk_Methods.substitute(self, moveRes);
                    break;

                case "supersonic":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(target, 100, rnd);
                    break;

                case "swords dance":
                    FBG_Atk_Methods.changeStats(FBG_consts.attack, 2, self);
                    stageName = FBG_consts.attack;
                    stageDiff = 2;
                    break;

                case "tail whip":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, -1, target);
                    stageName = FBG_consts.attack;
                    stageDiff = -1;
                    break;

                case "teleport":
                    //say something stupid here
                    break;

                case "thunder wave":
                    FBG_Atk_Methods.isParalized(target, 100);
                    break;

                case "toxic":               //increasingly does more toxic damage at the end of each turn, starts at 1/16
                    FBG_Atk_Methods.toxic(target);
                    break;

                case "transform":           //takes the attacks of the opponent
                    break;

                case "whirlwind":           //blows the opponent away if they are a lower level
                    break;

                case "withdraw":
                    FBG_Atk_Methods.changeStats(FBG_consts.defense, 1, self);
                    stageName = FBG_consts.defense;
                    stageDiff = 1;
                    break;

            }
            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4}", damage, heal, recoil, stageName, stageDiff));
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff);
            return report;
        }

        public static move_DmgReport physicalAttacks(string name, float baseDamage)
        {
            damage = baseDamage;
            string tempname = name.ToLower();
            bool isHit = false;
            int rnd;
            switch (tempname)
            {
                default:
                    Debug.LogError("No physical attack with name " + name.ToLower() + " found");
                    break;

                case "barrage":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "bide":                //waits 2 turns then deals back double.... :(
                    break;

                case "bind":                //need to create a damage over time effect here for rndBind turns
                    rnd = UnityEngine.Random.Range(4, 5);
                    break;

                case "bite":
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                case "body slam":
                    FBG_Atk_Methods.isParalized(target, 30);
                    break;

                case "bone club":
                    FBG_Atk_Methods.isFlinched(target, 10);
                    break;

                case "bonemerang":
                    rnd = 2;
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "clamp":               //traps for 4-5 turns dealing 1/16th damage
                    rnd = UnityEngine.Random.Range(4, 5);
                    break;

                case "comet punch":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "constrict":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, target);
                        stageName = FBG_consts.speed;
                        stageDiff = -1;
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
                    self.position = pokemonPosition.underground;
                    break;

                case "dizzy punch":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(target, 20, rnd);
                    break;

                case "double kick":
                    rnd = 2;
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "double slap":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "double edge":
                    recoil = Mathf.Round(damage / 3f);
                    break;

                case "drill peck":          //no additional effects
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "earthquake":
                    damage = FBG_Atk_Methods.earthQuake(target, damage);
                    break;

                case "egg bomb":            //no additional effects 
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                //***********************//
                case "explosion":           //causes user to faint
                    break;

                case "fire punch":
                    FBG_Atk_Methods.isBurned(target, 10);
                    break;

                case "fissure":
                    FBG_Atk_Methods.oneHitKO(target, self, moveRes);
                    break;

                //*****************************************//
                case "fly":
                    self.position = pokemonPosition.flying;
                    break;

                case "fury attack":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "fury swipes":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "guillotine":
                    FBG_Atk_Methods.oneHitKO(target, self, moveRes);
                    break;

                case "headbutt":
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                //************************************************//
                case "high jump kick":      //if this misses it casues 1/2 of the damage it would have inflicted on the user
                    break;

                case "horn attack":         //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "horn drill":
                    FBG_Atk_Methods.oneHitKO(target, self, moveRes);
                    break;

                case "hyper fang":
                    FBG_Atk_Methods.isFlinched(target, 10);
                    break;

                case "ice punch":
                    FBG_Atk_Methods.isFrozen(target, 10);
                    break;

                //***********************************************//
                case "jump kick":           //lose 1/2 hp is the user misses just like high jump kick
                    break;

                case "karate chop":         //high crit ratio 1/8 versus 1/16
                    break;

                case "leech life":
                    heal = Mathf.Round(damage / 2f);
                    break;

                case "low kick":
                    FBG_Atk_Methods.isFlinched(self, 30);
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
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "poison sting":        //chance to poison the target
                    FBG_Atk_Methods.isPosioned(target, 30);
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
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                case "rock throw":          //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "rolling kick":
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                case "scratch":             //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "seismic toss":
                    damage = FBG_Atk_Methods.levelBasedDamage(target);
                    break;
                //***************************************************//
                case "self destruct":       //user faints
                    break;

                case "skull bash":          //charges first turn, raising defense, hits on the second turn
                    break;

                case "sky attack":          //charges on first turn, hits on second, 30% flinch chance
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                case "slam":                //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "slash":               //high crit ratio 1/8 not 1/16
                    break;

                case "spike cannon":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = FBG_Atk_Methods.multiAttack(rnd, name);
                    break;

                case "stomp":               //if minimized *2 damage
                    FBG_Atk_Methods.isFlinched(target, 30);
                    break;

                case "strength":            //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "struggle":            //hurts the user if all the pp are gone
                    break;

                case "submission":
                    recoil = Mathf.Round(damage / 4f);
                    break;

                case "super fang":
                    damage = target.curHp / 2f;
                    break;

                case "tackle":              //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "take down":
                    recoil = Mathf.Round(damage / 4f);
                    break;

                case "thrash":              //attacks for 2-3 turns, but cannot switch out or use a different attack
                    break;

                case "thunder punch":
                    FBG_Atk_Methods.isParalized(target, 10);
                    break;

                case "twineedle":           //20% chance to poison the target
                    FBG_Atk_Methods.multiAttack(2, name);
                    FBG_Atk_Methods.isPosioned(target, 20);
                    break;

                case "vice grip":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "vine whip":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "waterfall":
                    FBG_Atk_Methods.isFlinched(target, 20);
                    break;

                case "wing attack":         //no additional effect, can hit non-adjacent pokemon in triple battles
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;
                //**************************************//
                case "wrap":                //causes 1/16th damage for 4-5 turns
                    rnd = UnityEngine.Random.Range(4, 5);
                    break;
            }

            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4}", damage, heal, recoil, stageName, stageDiff));

            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff);
            return report;
        }

        public static move_DmgReport specialAttacks(string name, float baseDamage)
        {
            damage = baseDamage;
            string tempname = name.ToLower();
            int rnd;
            bool isHit = false;
            switch (tempname)
            {
                default:
                    Debug.Log("No special attack with name " + name + " found");
                    break;

                case "absorb":
                    heal = damage / 2f;
                    break;

                case "acid":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.spDefense, -1, target);
                        stageName = FBG_consts.spDefense;
                        stageDiff = -1;
                    }
                    break;

                case "aurora beam":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.attack, -1, target);
                        stageName = FBG_consts.attack;
                        stageDiff = -1;
                    }
                    break;

                case "blizzard":
                    FBG_Atk_Methods.isFrozen(target, 10);
                    break;

                case "bubble":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, target);
                        stageName = FBG_consts.speed;
                        stageDiff = -1;
                    }
                    break;

                case "bubble beam":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.speed, -1, target);
                        stageName = FBG_consts.speed;
                        stageDiff = -1;
                    }
                    break;

                case "confusion":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(target, 10, rnd);
                    break;

                case "dragon rage":
                    damage = 40;
                    break;

                case "dream eater":
                    damage = FBG_Atk_Methods.dreamEater(target, damage, moveRes);
                    break;

                case "ember":
                    FBG_Atk_Methods.isBurned(target, 10);
                    break;

                case "fire blast":
                    FBG_Atk_Methods.isBurned(target, 10);
                    break;

                case "fire spin":           //Damages the target for 4-5 turns
                    break;

                case "flamethrower":
                    FBG_Atk_Methods.isBurned(target, 10);
                    break;

                case "gust":
                    if(target.position == pokemonPosition.flying)
                    {
                        damage *= 2f;
                    }
                    break;

                case "hydro pump":          //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

                case "hyper beam":          //cannot move next turn
                    
                    break;

                case "ice beam":
                    FBG_Atk_Methods.isFrozen(target, 10);
                    break;

                case "mega drain":
                    heal = Mathf.Round(damage / 2f);
                    break;

                case "night shade":
                    damage = FBG_Atk_Methods.levelBasedDamage(target);
                    break;

                case "petal dance":         //attacks for 2-3 turns, cannot be switched out, then becomes confused
                    break;

                case "psybeam":
                    rnd = UnityEngine.Random.Range(1, 4);
                    FBG_Atk_Methods.isConfused(target, 10, rnd);
                    break;

                case "psychic":
                    isHit = FBG_Atk_Methods.Chance_100(10);
                    if (isHit)
                    {
                        FBG_Atk_Methods.changeStats(FBG_consts.spDefense, -1, target);
                        stageName = FBG_consts.spDefense;
                        stageDiff = -1;
                    }
                    break;

                case "psywave":
                    float mod = UnityEngine.Random.Range(.5f, 1.5f);
                    damage = FBG_Atk_Methods.levelBasedDamage(target) * mod;
                    break;

                case "razor wind":          //charges the first turn then attacks the second
                    damage = 0;
                    break;

                case "sludge":              //30% chance to poison the target
                    FBG_Atk_Methods.isPosioned(target, 30);
                    break;

                case "smog":                //40% chance to poison the target
                    FBG_Atk_Methods.isPosioned(target, 40);
                    break;

                case "solar beam":          //charges on the fist turn, hits on the second
                    break;

                case "sonic boom":
                    damage = FBG_Atk_Methods.sonicBoom(target);
                    break;

                case "surf":                //does double damage if the pokemon used dive(introduced in gen3)
                    break;

                case "swift":               //ignores evasiveness and accuracy
                    break;

                case "thunder":
                    FBG_Atk_Methods.isParalized(target, 30);
                    break;

                case "thunder shock":
                    FBG_Atk_Methods.isParalized(target, 10);
                    break;

                case "thunder bolt":
                    FBG_Atk_Methods.isParalized(target, 10);
                    break;

                case "tri attack":          //6.67% chance for each
                    rnd = UnityEngine.Random.Range(4, 5);
                    FBG_Atk_Methods.isParalized(target, 6.67f);
                    FBG_Atk_Methods.isBurned(target, 6.67f);
                    FBG_Atk_Methods.isFrozen(target, 6.67f);
                    break;

                case "water gun":           //no additional effect
                    FBG_Atk_Methods.noAdditionalEffect();
                    break;

            }
            //Check for lightscreen to halve special attack damage

            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4}", damage, heal, recoil, stageName, stageDiff));

            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff);
            return report;
        }
    }
}
