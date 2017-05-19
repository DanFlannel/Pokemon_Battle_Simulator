using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FBG.Data;
using FBG.Base;

namespace FBG.Attack
{
    public class SpecialAtkHandler : BaseMoves, IAttackHandler
    {
        public PokemonBase target { get; set; }
        public PokemonBase self { get; set; }

        public SpecialAtkHandler(PokemonBase tar, PokemonBase s, MoveResults mr)
        {
            setPokemon(tar, s, mr);
        }

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

        public move_DmgReport result(string name, float baseDamage)
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
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.spDefense, -1, target);
                        stageName = Consts.spDefense;
                        stageDiff = -1;
                        stagePokemon = target.Name;
                    }
                    break;

                case "aurora beam":
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.attack, -1, target);
                        stageName = Consts.attack;
                        stageDiff = -1;
                        stagePokemon = target.Name;
                    }
                    break;

                case "blizzard":
                    isFrozen(target, 10);
                    break;

                case "bubble":
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.speed, -1, target);
                        stageName = Consts.speed;
                        stageDiff = -1;
                        stagePokemon = target.Name;
                    }
                    break;

                case "bubble beam":
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.speed, -1, target);
                        stageName = Consts.speed;
                        stageDiff = -1;
                        stagePokemon = target.Name;
                    }
                    break;

                case "confusion":
                    rnd = UnityEngine.Random.Range(1, 4);
                    isConfused(target, 10, rnd);
                    break;

                case "dragon rage":
                    ignoreLightScreen = true;
                    damage = 40;
                    break;

                case "dream eater":
                    damage = dreamEater(target, damage, moveRes);
                    break;

                case "ember":
                    isBurned(target, 10);
                    break;

                case "fire blast":
                    isBurned(target, 10);
                    break;

                case "fire spin":           //Damages the target for 4-5 turns
                    rnd = Random.Range(4, 5);
                    float fSpingDmg = target.maxHP / 16f;
                    target.team.addBind(rnd, fSpingDmg);
                    break;

                case "flamethrower":
                    isBurned(target, 10);
                    break;

                case "gust":
                    if (target.position == pokemonPosition.flying)
                    {
                        damage *= 2f;
                    }
                    break;

                case "hydro pump":          //no additional effect
                    noAdditionalEffect();
                    break;

                case "hyper beam":          //cannot move next turn
                    damage = ReChargeMove(self, name, baseDamage);
                    break;

                case "ice beam":
                    isFrozen(target, 10);
                    break;

                case "mega drain":
                    heal = Mathf.Round(damage / 2f);
                    break;

                case "night shade":
                    damage = levelBasedDamage(target);
                    break;

                case "petal dance":         //attacks for 2-3 turns, cannot be switched out, then becomes confused
                    int dur = Random.Range(2, 3);
                    repeatAttack_Confused petalDance = new repeatAttack_Confused(tempname, dur, self);
                    if (!hasEffector(self, tempname))
                    {
                        self.effectors.Add(petalDance);
                    }
                    break;

                case "psybeam":
                    rnd = UnityEngine.Random.Range(1, 4);
                    isConfused(target, 10, rnd);
                    break;

                case "psychic":
                    isHit = Chance_100(10);
                    if (isHit)
                    {
                        changeStats(Consts.spDefense, -1, target);
                        stageName = Consts.spDefense;
                        stageDiff = -1;
                        stagePokemon = target.Name;
                    }
                    break;

                case "psywave":
                    float mod = UnityEngine.Random.Range(.5f, 1.5f);
                    damage = levelBasedDamage(target) * mod;
                    break;

                case "razor wind":          //charges the first turn then attacks the second
                    damage = ChargingMove(self, name, baseDamage);
                    break;

                case "sludge":              //30% chance to poison the target
                    isPosioned(target, 30);
                    break;

                case "smog":                //40% chance to poison the target
                    isPosioned(target, 40);
                    break;

                case "solar beam":          //charges on the fist turn, hits on the second
                    damage = ChargingMove(self, name, baseDamage);
                    break;

                case "sonic boom":
                    damage = sonicBoom(target);
                    break;

                case "surf":                //does double damage if the pokemon used dive(introduced in gen3)
                    noAdditionalEffect();
                    break;

                case "swift":               //ignores evasiveness and accuracy
                    noAdditionalEffect();
                    break;

                case "thunder":
                    isParalized(target, 30);
                    break;

                case "thunder shock":
                    isParalized(target, 10);
                    break;

                case "thunderbolt":
                    isParalized(target, 10);
                    break;

                case "tri attack":          //6.67% chance for each
                    rnd = UnityEngine.Random.Range(4, 5);
                    isParalized(target, 6.67f);
                    isBurned(target, 6.67f);
                    isFrozen(target, 6.67f);
                    break;

                case "water gun":           //no additional effect
                    noAdditionalEffect();
                    break;

            }
            //Check for lightscreen to halve special attack damage

            Debug.Log(string.Format(" dmg {0} heal {1} recoil {2} stageName {3} stageDiff {4} hit {5}", damage, heal, recoil, stageName, stageDiff, moveRes.hit));

            if (target.team.hasLightScreen && !ignoreLightScreen)
            {
                Debug.Log("halving damage because of light screen");
                damage /= 2f;
            }

            ignoreLightScreen = false;
            ignoreReflect = false;
            move_DmgReport report = new move_DmgReport(damage, heal, recoil, stageName, stageDiff, stagePokemon);
            return report;
        }

    }
}
