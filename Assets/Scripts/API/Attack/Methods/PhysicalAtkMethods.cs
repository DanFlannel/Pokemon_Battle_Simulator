﻿using UnityEngine;

using Base;
using Data;
using Battle;

namespace Attack
{
    public class PhysicalAtkMethods : BaseMoves
    {
        //testing attacks
        public void kill()
        {
            damage = 1000;
        }

        public void killAll()
        {
            damage = 1000;
            recoil = 1000;
        }

        public float barrage(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        //need to fix still this takes into account the
        public void bide(PokemonBase self, float baseDamage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.atkStatus = attackStatus.charging_2;
                self.nextAttack = "bide";
                damage = 0;
            }
            else if (self.atkStatus == attackStatus.charging_2)
            {
                battleHistory hist = BattleSimulator.Instance.moveHistory.getLastEnemyAttack(self);
                if (hist != null)
                {
                    self.cachedDamage += hist.MR.dmgReport.damage;
                }
                self.atkStatus = attackStatus.charging;
                self.nextAttack = "bide";
                damage = 0;
            }
            else if (self.atkStatus == attackStatus.charging)
            {
                ignoreReflect = true;
                battleHistory hist = BattleSimulator.Instance.moveHistory.getLastEnemyAttack(self);
                if (hist != null)
                {
                    self.cachedDamage += hist.MR.dmgReport.damage;
                }
                self.atkStatus = attackStatus.normal;
                damage = self.cachedDamage * 2f;
                self.cachedDamage = 0;
                Debug.Log(string.Format("Bide is doing: {0} damage", damage));
            }
            else
            {
                Debug.Log("Bid Error");
                damage = 0;
            }
        }

        public void bind(PokemonBase target)
        {
            int rnd = Random.Range(4, 5);
            float bindDmg = target.maxHP / 16f;
            target.team.addBind(rnd, bindDmg, "bind");
        }

        public void bite(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void bodySlam(PokemonBase target)
        {
            isParalized(target, 30);
        }

        public void boneClub(PokemonBase target)
        {
            isFlinched(target, 10);
        }

        public float bonemerang(string name)
        {
            return multiAttack(2, name);
        }

        public void clamp(PokemonBase target)
        {
            int rnd = Random.Range(4, 5);
            float clampDmg = target.maxHP / 16f;
            target.team.addBind(rnd, clampDmg, "clamp");
        }

        public float cometPunch(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public bool constrict(PokemonBase target)
        {
            bool isHit = Chance_100(10);
            if (isHit)
            {
                changeStats(Consts.speed, -1, target);
            }
            return isHit;
        }

        public float counter(PokemonBase self, PokemonBase target, MoveResults mr, float damage)
        {
            ignoreReflect = true;
            battleHistory hist = BattleSimulator.Instance.moveHistory.getLastEnemyAttack(self);
            if (hist == null)
            {
                mr.failed = true;
                return 0;
            }
            if (hist.atkCategory == Consts.Physical)
            {
                //Debug.Log("counter was sucessful!");
                return hist.MR.dmgReport.damage * 2f;
            }
            else
            {
                //Debug.Log("Counter was used against a non physical move so it failed.");
                mr.failed = true;
                return 0;
            }
        }

        public void crabHammer()
        {
        }

        public void cut()
        {
        }

        public float dig(PokemonBase self, float damage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.position = pokemonPosition.underground;
                self.atkStatus = attackStatus.charging;
                self.cachedDamage = damage;
                self.nextAttack = "dig";
                return 0;
            }
            else if (self.atkStatus == attackStatus.charging)
            {
                damage = self.cachedDamage;
                self.position = pokemonPosition.normal;
                self.atkStatus = attackStatus.normal;
                self.cachedDamage = 0;
                return damage;
            }
            else
            {
                Debug.Log("Dig move error");
                return 0;
            }
        }

        public void dizzyPunch(PokemonBase target)
        {
            int rnd = UnityEngine.Random.Range(1, 4);
            isConfused(target, 20, rnd);
        }

        public float doubleKick(string name)
        {
            return multiAttack(2, name);
        }

        public float doubleSlap(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public void doubleEdge()
        {
            recoil = Mathf.Round(damage / 3f);
        }

        public void drillPeck()
        {
        }

        public void earthQuake(PokemonBase target, float baseDamage)
        {
            moveRes.hit.ignore(pokemonPosition.underground);
            damage = baseDamage;
            if (target.position == pokemonPosition.underground)
            {
                damage *= 2f;
            }
        }

        public void eggBomb()
        {
        }

        public void explosion()
        {
        }

        public void firePunch(PokemonBase target)
        {
            isBurned(target, 10);
        }

        public float fissure(PokemonBase target, PokemonBase self, MoveResults moveRes)
        {
            moveRes.hit.ignore(pokemonPosition.underground);
            return oneHitKO(target, self, moveRes);
        }

        public float fly(PokemonBase self, float baseDamage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.position = pokemonPosition.flying;
                self.atkStatus = attackStatus.charging;
                self.cachedDamage = baseDamage;
                self.nextAttack = "fly";
                return 0;
            }
            else if (self.atkStatus == attackStatus.charging)
            {
                float damage = self.cachedDamage;
                self.position = pokemonPosition.normal;
                self.atkStatus = attackStatus.normal;
                self.cachedDamage = 0;
                return damage;
            }
            else
            {
                Debug.Log("Fly Error");
                return 0;
            }
        }

        public float furyAttack(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public float furySwipes(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public float guillotine(PokemonBase target, PokemonBase self, MoveResults res)
        {
            return oneHitKO(target, self, res);
        }

        public void headbutt(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void highJumpKick(PokemonBase target)
        {
            if (!moveRes.hit.sucess)
            {
                recoil = target.maxHP / 2f;
            }
        }

        public void hornAttack()
        {
        }

        public float hornDrill(PokemonBase target, PokemonBase self, MoveResults res)
        {
            return oneHitKO(target, self, res);
        }

        public void hyperFang(PokemonBase target)
        {
            isFlinched(target, 10);
        }

        public void icePunch(PokemonBase target)
        {
            isFrozen(target, 10);
        }

        public void jumpKick(PokemonBase target)
        {
            if (!moveRes.hit.sucess)
            {
                recoil = target.maxHP / 2f;
            }
        }

        public void karateChop()
        {
        }

        public void leechLife()
        {
        }

        public void lowKick(PokemonBase self)
        {
            isFlinched(self, 30);
        }

        public void megaKick()
        {
        }

        public void megaPunch()
        {
        }

        public void payDay()
        {
        }

        public void peck()
        {
        }

        public float pinMissile(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public void poisonSting(PokemonBase target)
        {
            isPosioned(target, 30);
        }

        public void pound()
        {
        }

        public void quickAttack()
        {
        }

        public void rage(PokemonBase self, string name)
        {
            rage r = new rage(name, 1, self);
            for (int i = 0; i < self.effectors.Count; i++)
            {
                if (self.effectors[i].name == name)
                {
                    self.effectors[i].duration = 1;
                }
            }
            self.effectors.Add(r);
        }

        public void razorLeaf()
        {
        }

        public void rockSlide(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void rockThrow()
        {
        }

        public void rollingKick(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void scratch()
        {
        }

        public float seismicToss(PokemonBase target)
        {
            return levelBasedDamage(target);
        }

        public float skullBash(PokemonBase self, float damage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.atkStatus = attackStatus.charging;
                self.cachedDamage = damage;
                self.nextAttack = "skull bash";
                changeStats(Consts.defense, 1, self);
                stageName = Consts.defense;
                stageDiff = 1;
                return 0;
            }
            else if (self.atkStatus == attackStatus.charging)
            {
                self.atkStatus = attackStatus.normal;
                damage = self.cachedDamage;
                self.cachedDamage = 0;
                return damage;
            }
            else
            {
                Debug.Log("Skill bash error");
                return 0;
            }
        }

        public float skyAttack(PokemonBase self, PokemonBase target, float damage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.atkStatus = attackStatus.charging;
                self.cachedDamage = damage;
                self.nextAttack = "sky attack";
                return 0;
            }
            if (self.atkStatus == attackStatus.charging)
            {
                self.atkStatus = attackStatus.normal;
                damage = self.cachedDamage;
                isFlinched(target, 30);
                self.cachedDamage = 0;
                return damage;
            }
            else
            {
                Debug.Log("sky attack error");
                return 0;
            }
        }

        public void slam()
        {
        }

        public void slash()
        {
        }

        public float spikeCannon(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public float stomp(PokemonBase target, float damage)
        {
            if (target.position == pokemonPosition.minimized)
            {
                damage *= 2f;
            }
            isFlinched(target, 30);
            return damage;
        }

        public void strength()
        {
        }

        public void struggle(PokemonBase target)
        {
            recoil = target.maxHP / 4f;
        }

        public void submission()
        {
            recoil = Mathf.Round(damage / 4f);
        }

        public float superFang(PokemonBase target)
        {
            ignoreReflect = true;
            return target.curHp / 2f;
        }

        public void tackle()
        {
        }

        public void takeDown()
        {
            recoil = Mathf.Round(damage / 4f);
        }

        public void thrash(PokemonBase self, string name)
        {
            int dur = Random.Range(2, 3);
            repeatAttack_Confused thrash = new repeatAttack_Confused(name, dur, self);
            if (!hasEffector(self, name))
            {
                self.effectors.Add(thrash);
            }
        }

        public void thunderPunch(PokemonBase target)
        {
            isParalized(target, 10);
        }

        public float twinNeedle(PokemonBase target, string name)
        {
            isPosioned(target, 20);
            return multiAttack(2, name);
        }

        public void viceGrip()
        {
        }

        public void vineWhip()
        {
        }

        public void waterFall(PokemonBase target)
        {
            isFlinched(target, 20);
        }

        public void wingAttack()
        {
        }

        public void wrap(PokemonBase target)
        {
            int rnd = Random.Range(4, 5);
            int dmg = (int)(target.curHp / 16f);
            target.team.addBind(rnd, dmg, "wrap");
        }
    }
}