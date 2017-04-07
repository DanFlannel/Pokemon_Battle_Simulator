using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Data;
using FBG.Battle;

namespace FBG.Attack
{
    public class PhysicalAtkHandler : PhysicalAtkMethods, IAttackHandler
    {
        public PokemonBase target { get; set; }
        public PokemonBase self { get; set; }
        public MoveResults moveRes { get; set; }

        public float damage { get; set; }
        public float heal { get; set; }
        public float recoil { get; set; }
        public string stageName { get; set; }
        public int stageDiff { get; set; }

        public void setPokemon(PokemonBase tar, PokemonBase s, MoveResults mr)
        {
            target = tar;
            self = s;
            moveRes = mr;

            damage = 0;
            heal = 0;
            recoil = 0;
            stageName = "";
            stageDiff = 0;

            s.nextAttack = "";

            ignoreReflect = ignoreLightScreen = false;
        }

        public PhysicalAtkHandler(PokemonBase tar, PokemonBase s, MoveResults mr)
        {
            setPokemon(tar, s, mr);
        }

        public move_DmgReport result(string name, float baseDamage)
        {
            damage = baseDamage;
            switch (name.ToLower())
            {
                default:
                    Debug.LogError("No physical attack with name " + name.ToLower() + " found");
                    break;

                case "barrage":
                    damage = barrage(name);
                    break;

                //waits 2 turns then deals back double
                case "bide":                
                    damage = bide(self, damage);
                    break;

                case "bind":
                    bind(target);
                    break;

                case "bite":
                    bite(target);
                    break;

                case "body slam":
                    bodySlam(target);
                    break;

                case "bone club":
                    boneClub(target);
                    break;

                case "bonemerang":
                    damage = bonemerang(name);
                    break;

                case "clamp":               //traps for 4-5 turns dealing 1/16th damage
                    clamp(target);
                    break;

                case "comet punch":
                    damage = cometPunch(name);
                    break;

                case "constrict":
                    if (constrict(target))
                    {
                        stageName = Consts.speed;
                        stageDiff = -1;
                    }
                    break;

                case "counter":             //hits back with 2x power iff is hit with physical attack
                    damage = counter(self, target, damage);
                    break;

                //has a 1/8 crit ratio not a 1/16
                case "crabhammer":
                    crabHammer();
                    break;

                case "cut":                 //no additional effects
                    cut();
                    break;

                case "dig":                 //redo based off of turn controller
                    damage = dig(self, damage);
                    break;

                case "dizzy punch":
                    dizzyPunch(target);
                    break;

                case "double kick":
                    damage = doubleKick(name);
                    break;

                case "double slap":
                    damage = doubleSlap(name);
                    break;

                case "double edge":
                    recoil = Mathf.Round(damage / 3f);
                    doubleEdge();
                    break;

                case "drill peck":          //no additional effects
                    drillPeck();
                    break;

                case "earthquake":
                    damage = earthQuake(target, damage);
                    break;

                case "egg bomb":            //no additional effects 
                    eggBomb();
                    break;

                case "explosion":           //causes user to faint
                    recoil = self.maxHP;
                    explosion();
                    break;

                case "fire punch":
                    firePunch(target);
                    break;

                case "fissure":
                    damage = fissure(target, self, moveRes);
                    break;

                case "fly":
                    damage = fly(self, damage);
                    break;

                case "fury attack":
                    damage = furyAttack(name);
                    break;

                case "fury swipes":
                    damage = furySwipes(name);
                    break;

                case "guillotine":
                    damage = guillotine(target, self, moveRes);
                    break;

                case "headbutt":
                    headbutt(target);
                    break;

                case "high jump kick":      //if this misses it casues 1/2 of the damage it would have inflicted on the user
                    if (!moveRes.hit)
                    {
                        recoil = damage / 2f;
                    }
                    highJumpKick();
                    break;

                case "horn attack":         //no additional effect
                    hornAttack();
                    break;

                case "horn drill":
                    damage = hornDrill(target, self, moveRes);
                    break;

                case "hyper fang":
                    hyperFang(target);
                    break;

                case "ice punch":
                    icePunch(target);
                    break;

                case "jump kick":           //lose 1/2 hp is the user misses just like high jump kick
                    if (!moveRes.hit)
                    {
                        recoil = damage / 8f;
                    }
                    jumpKick();
                    break;

                case "karate chop":         //high crit ratio 1/8 versus 1/16
                    karateChop();
                    break;

                case "leech life":
                    heal = Mathf.Round(damage / 2f);
                    leechLife();
                    break;

                case "low kick":
                    lowKick(self);
                    break;

                case "mega kick":           //no additional effect
                    megaKick();
                    break;

                case "mega punch":          //no additional effect
                    megaPunch();
                    break;

                case "pay day":             //small amount of money at the end of the battle??
                    payDay();
                    break;

                case "peck":                //no additional effect
                    peck();
                    break;

                case "pin missile":
                    damage = pinMissile(name);
                    break;

                case "poison sting":        //chance to poison the target
                    poisonSting(target);
                    break;

                case "pound":               //no additional effect
                    pound();
                    break;

                case "quick attack":        //has +1 priority
                    quickAttack();
                    break;

                case "rage":
                    rage(self, name);
                    break;

                case "razor leaf":          //high crit ratio
                    razorLeaf();
                    break;

                case "rock slide":
                    rockSlide(target);
                    break;

                case "rock throw":          //no additional effect
                    rockThrow();
                    break;

                case "rolling kick":
                    rollingKick(target);
                    break;

                case "scratch":             //no additional effect
                    scratch();
                    break;

                case "seismic toss":
                    damage = seismicToss(target);
                    break;

                case "self destruct":       //user faints
                    recoil = self.maxHP;
                    break;

                case "skull bash":          //charges first turn, raising defense, hits on the second turn
                    damage = skullBash(self, baseDamage);
                    if (self.atkStatus == attackStatus.normal)
                    {
                        stageName = Consts.defense;
                        stageDiff = 1;
                    }
                    break;

                case "sky attack":          //charges on first turn, hits on second, 30% flinch chance
                    damage = skyAttack(self, target, baseDamage);
                    break;

                case "slam":                //no additional effect
                    slam();
                    break;

                case "slash":               //high crit ratio 1/8 not 1/16
                    slash();
                    break;

                case "spike cannon":
                    damage = spikeCannon(name);
                    break;

                case "stomp":               //if minimized *2 damage
                    damage = stomp(target, damage);
                    break;

                case "strength":            //no additional effect
                    strength();
                    break;

                case "struggle":            //hurts the user if all the pp are gone
                    recoil = self.maxHP / 4f;
                    struggle();
                    break;

                case "submission":
                    recoil = Mathf.Round(damage / 4f);
                    submission();
                    break;

                case "super fang":
                    damage = superFang(target);
                    break;

                case "tackle":              //no additional effect
                    tackle();
                    break;

                case "take down":
                    recoil = Mathf.Round(damage / 4f);
                    takeDown();
                    break;

                case "thrash":              //attacks for 2-3 turns, but cannot switch out or use a different attack
                    thrash(self, name);
                    break;

                case "thunder punch":
                    thunderPunch(target);
                    break;

                case "twineedle":           //20% chance to poison the target
                    damage = twinNeedle(target, name);
                    break;

                case "vice grip":           //no additional effect
                    viceGrip();
                    break;

                case "vine whip":           //no additional effect
                    vineWhip();
                    break;

                case "waterfall":
                    waterFall(target);
                    break;

                case "wing attack":         //no additional effect, can hit non-adjacent pokemon in triple battles
                    wingAttack();
                    break;

                case "wrap":                //causes 1/16th damage for 4-5 turns, traps
                    wrap(target);
                    break;
            }

            if (target.team.hasReflect && !ignoreReflect)
            {
                damage /= 2f;
            }

            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4}", damage, heal, recoil, stageName, stageDiff));

            ignoreLightScreen = false;
            ignoreReflect = false;
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff);
            return report;
        }

    }
}
