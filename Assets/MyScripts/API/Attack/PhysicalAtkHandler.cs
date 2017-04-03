using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Base;
using FBG.Data;
using FBG.Battle;

namespace FBG.Attack
{
    public class PhysicalAtkHandler : BaseMoves, IAttackHandler
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
                    damage = multiAttack(rnd, name);
                    break;

                case "bide":                //waits 2 turns then deals back double.... :(
                    if (self.atkStatus == attackStatus.normal)
                    {
                        self.cachedDamage += (baseDamage * 2f);
                        self.atkStatus = attackStatus.charging_2;
                        self.nextAttack = "bide";
                    }
                    else if (self.atkStatus == attackStatus.charging_2)
                    {
                        self.atkStatus = attackStatus.charging;
                        self.cachedDamage += (baseDamage * 2f);
                        self.nextAttack = "bide";

                    }
                    else if (self.atkStatus == attackStatus.charging)
                    {
                        ignoreReflect = true;
                        self.atkStatus = attackStatus.normal;
                        damage = self.cachedDamage;
                        self.cachedDamage = 0;
                    }

                    break;

                case "bind":                //need to create a damage over time effect here for rndBind turns
                    rnd = Random.Range(4, 5);
                    float bindDmg = target.maxHP / 16f;
                    target.team.addBind(rnd, bindDmg);
                    break;

                case "bite":
                    isFlinched(target, 30);
                    break;

                case "body slam":
                    isParalized(target, 30);
                    break;

                case "bone club":
                    isFlinched(target, 10);
                    break;

                case "bonemerang":
                    rnd = 2;
                    damage = multiAttack(rnd, name);
                    break;

                case "clamp":               //traps for 4-5 turns dealing 1/16th damage
                    rnd = Random.Range(4, 5);
                    float clampDmg = target.maxHP / 16f;
                    target.team.addBind(rnd, clampDmg);
                    break;

                case "comet punch":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "constrict":
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.speed, -1, target);
                        stageName = Consts.speed;
                        stageDiff = -1;
                    }
                    break;

                case "counter":             //hits back with 2x power iff is hit with physical attack
                    ignoreReflect = true;
                    //check that it attacks second
                    if (self.Speed < target.Speed)
                    {
                        int index = BattleSimulator.Instance.moveHistory.Count;
                        if (BattleSimulator.Instance.moveHistory[index].atkCategory == Consts.Physical)
                        {
                            damage *= 2;
                        }
                    }
                    break;

                case "crabhammer":          //has a 1/8 crit ratio not a 1/16.... have to recalculate for this
                    break;

                case "cut":                 //no additional effects
                    noAdditionalEffect();
                    break;

                case "dig":                 //redo based off of turn controller

                    if (self.atkStatus == attackStatus.normal)
                    {
                        self.position = pokemonPosition.underground;
                        self.atkStatus = attackStatus.charging;
                        self.cachedDamage = baseDamage;
                        self.nextAttack = "dig";

                    }
                    else if (self.atkStatus == attackStatus.charging)
                    {
                        damage = self.cachedDamage;
                        self.position = pokemonPosition.normal;
                        self.atkStatus = attackStatus.normal;
                        self.cachedDamage = 0;
                    }
                    break;

                case "dizzy punch":
                    rnd = UnityEngine.Random.Range(1, 4);
                    isConfused(target, 20, rnd);
                    break;

                case "double kick":
                    rnd = 2;
                    damage = multiAttack(rnd, name);
                    break;

                case "double slap":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "double edge":
                    recoil = Mathf.Round(damage / 3f);
                    break;

                case "drill peck":          //no additional effects
                    noAdditionalEffect();
                    break;

                case "earthquake":
                    damage = earthQuake(target, damage);
                    break;

                case "egg bomb":            //no additional effects 
                    noAdditionalEffect();
                    break;

                case "explosion":           //causes user to faint
                    recoil = self.maxHP;
                    break;

                case "fire punch":
                    isBurned(target, 10);
                    break;

                case "fissure":
                    oneHitKO(target, self, moveRes);
                    break;

                case "fly":
                    if (self.atkStatus == attackStatus.normal)
                    {
                        self.position = pokemonPosition.flying;
                        self.atkStatus = attackStatus.charging;
                        self.cachedDamage = baseDamage;
                        self.nextAttack = "fly";

                    }
                    else if (self.atkStatus == attackStatus.charging)
                    {
                        damage = self.cachedDamage;
                        self.position = pokemonPosition.normal;
                        self.atkStatus = attackStatus.normal;
                        self.cachedDamage = 0;
                    }
                    break;

                case "fury attack":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "fury swipes":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "guillotine":
                    oneHitKO(target, self, moveRes);
                    break;

                case "headbutt":
                    isFlinched(target, 30);
                    break;

                case "high jump kick":      //if this misses it casues 1/2 of the damage it would have inflicted on the user
                    if (!moveRes.hit)
                    {
                        recoil = damage / 2f;
                    }
                    break;

                case "horn attack":         //no additional effect
                    noAdditionalEffect();
                    break;

                case "horn drill":
                    oneHitKO(target, self, moveRes);
                    break;

                case "hyper fang":
                    isFlinched(target, 10);
                    break;

                case "ice punch":
                    isFrozen(target, 10);
                    break;

                case "jump kick":           //lose 1/2 hp is the user misses just like high jump kick
                    if (!moveRes.hit)
                    {
                        recoil = damage / 8f;
                    }
                    break;

                case "karate chop":         //high crit ratio 1/8 versus 1/16
                    break;

                case "leech life":
                    heal = Mathf.Round(damage / 2f);
                    break;

                case "low kick":
                    isFlinched(self, 30);
                    break;

                case "mega kick":           //no additional effect
                    noAdditionalEffect();
                    break;

                case "mega punch":          //no additional effect
                    noAdditionalEffect();
                    break;

                case "pay day":             //small amount of money at the end of the battle??
                    noAdditionalEffect();
                    break;

                case "peck":                //no additional effect
                    noAdditionalEffect();
                    break;

                case "pin missile":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "poison sting":        //chance to poison the target
                    isPosioned(target, 30);
                    break;

                case "pound":               //no additional effect
                    noAdditionalEffect();
                    break;

                case "quick attack":        //has +1 priority
                    noAdditionalEffect();
                    break;

                case "rage":
                    rage r = new rage(tempname, 1, self);
                    for (int i = 0; i < self.effectors.Count; i++)
                    {
                        if (self.effectors[i].name == tempname)
                        {
                            self.effectors[i].duration = 1;
                        }
                    }
                    self.effectors.Add(r);
                    break;

                case "razor leaf":          //high crit ratio
                    break;

                case "rock slide":
                    isFlinched(target, 30);
                    break;

                case "rock throw":          //no additional effect
                    noAdditionalEffect();
                    break;

                case "rolling kick":
                    isFlinched(target, 30);
                    break;

                case "scratch":             //no additional effect
                    noAdditionalEffect();
                    break;

                case "seismic toss":
                    damage = levelBasedDamage(target);
                    break;

                case "self destruct":       //user faints
                    recoil = self.maxHP;
                    break;

                case "skull bash":          //charges first turn, raising defense, hits on the second turn
                    if (self.atkStatus == attackStatus.normal)
                    {
                        self.atkStatus = attackStatus.charging;
                        self.cachedDamage = baseDamage;
                        self.nextAttack = "skull bash";
                        changeStats(Consts.defense, 1, self);
                        stageName = Consts.defense;
                        stageDiff = 1;

                    }
                    else if (self.atkStatus == attackStatus.charging)
                    {
                        self.atkStatus = attackStatus.normal;
                        damage = self.cachedDamage;
                        self.cachedDamage = 0;
                    }
                    break;

                case "sky attack":          //charges on first turn, hits on second, 30% flinch chance
                    if (self.atkStatus == attackStatus.normal)
                    {
                        self.atkStatus = attackStatus.charging;
                        self.cachedDamage = baseDamage;
                        self.nextAttack = "sky attack";
                    }
                    if (self.atkStatus == attackStatus.charging)
                    {
                        self.atkStatus = attackStatus.normal;
                        damage = self.cachedDamage;
                        isFlinched(target, 30);
                        self.cachedDamage = 0;
                    }
                    break;

                case "slam":                //no additional effect
                    noAdditionalEffect();
                    break;

                case "slash":               //high crit ratio 1/8 not 1/16
                    break;

                case "spike cannon":
                    rnd = UnityEngine.Random.Range(2, 5);
                    damage = multiAttack(rnd, name);
                    break;

                case "stomp":               //if minimized *2 damage
                    if (target.position == pokemonPosition.minimized)
                    {
                        damage *= 2f;
                    }
                    isFlinched(target, 30);
                    break;

                case "strength":            //no additional effect
                    noAdditionalEffect();
                    break;

                case "struggle":            //hurts the user if all the pp are gone
                    recoil = self.maxHP / 4f;
                    break;

                case "submission":
                    recoil = Mathf.Round(damage / 4f);
                    break;

                case "super fang":
                    ignoreReflect = true;
                    damage = target.curHp / 2f;
                    break;

                case "tackle":              //no additional effect
                    noAdditionalEffect();
                    break;

                case "take down":
                    recoil = Mathf.Round(damage / 4f);
                    break;

                case "thrash":              //attacks for 2-3 turns, but cannot switch out or use a different attack
                    int dur = Random.Range(2, 3);
                    repeatAttack_Confused thrash = new repeatAttack_Confused(tempname, dur, self);
                    if (!hasEffector(self, tempname))
                    {
                        self.effectors.Add(thrash);
                    }
                    break;

                case "thunder punch":
                    isParalized(target, 10);
                    break;

                case "twineedle":           //20% chance to poison the target
                    multiAttack(2, name);
                    isPosioned(target, 20);
                    break;

                case "vice grip":           //no additional effect
                    noAdditionalEffect();
                    break;

                case "vine whip":           //no additional effect
                    noAdditionalEffect();
                    break;

                case "waterfall":
                    isFlinched(target, 20);
                    break;

                case "wing attack":         //no additional effect, can hit non-adjacent pokemon in triple battles
                    noAdditionalEffect();
                    break;
                //**************************************//
                case "wrap":                //causes 1/16th damage for 4-5 turns
                    rnd = UnityEngine.Random.Range(4, 5);
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
