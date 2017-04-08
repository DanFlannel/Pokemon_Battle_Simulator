using UnityEngine;
using FBG.Base;
using FBG.Data;
using FBG.Battle;

namespace FBG.Attack {
    public class PhysicalAtkMethods :  BaseMoves{

        public float barrage(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public float bide(PokemonBase self, float baseDamage)
        {
            if (self.atkStatus == attackStatus.normal)
            {
                self.cachedDamage += (baseDamage * 2f);
                self.atkStatus = attackStatus.charging_2;
                self.nextAttack = "bide";
                return 0;
            }
            else if (self.atkStatus == attackStatus.charging_2)
            {
                self.atkStatus = attackStatus.charging;
                self.cachedDamage += (baseDamage * 2f);
                self.nextAttack = "bide";
                return 0;

            }
            else if (self.atkStatus == attackStatus.charging)
            {
                ignoreReflect = true;
                self.atkStatus = attackStatus.normal;
                float damage = self.cachedDamage;
                self.cachedDamage = 0;
                return damage;
            }
            else
            {
                Debug.Log("Bid Error");
                return 0;
            }
        }

        public void bind(PokemonBase target)
        {
            int rnd = Random.Range(4, 5);
            float bindDmg = target.maxHP / 16f;
            target.team.addBind(rnd, bindDmg);
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
            target.team.addBind(rnd, clampDmg);
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

        public float counter(PokemonBase self, PokemonBase target, float damage)
        {
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
            return damage;
        }

        public void crabHammer() { }

        public void cut() { }

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

        public void doubleEdge() { }

        public void drillPeck() { }

        public float earthQuake(PokemonBase target, float damage)
        {
            if (target.position == pokemonPosition.underground)
            {
                return Mathf.Round(damage * 2);
            }
            return damage;
        }

        public void eggBomb() { }

        public void explosion() { }

        public void firePunch(PokemonBase target)
        {
            isBurned(target, 10);
        }

        public float fissure(PokemonBase target, PokemonBase self, MoveResults moveRes)
        {
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

        public void highJumpKick() { }

        public void hornAttack() { }

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

        public void jumpKick() { }

        public void karateChop() { }

        public void leechLife() { }

        public void lowKick(PokemonBase self)
        {
            isFlinched(self, 30);
        }

        public void megaKick() { }

        public void megaPunch() { }

        public void payDay() { }

        public void peck() { }

        public float pinMissile(string name)
        {
            return multiAttack(Random.Range(2, 5), name);
        }

        public void poisonSting(PokemonBase target)
        {
            isPosioned(target, 30);
        }

        public void pound() { }

        public void quickAttack() { }

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

        public void razorLeaf() { }

        public void rockSlide(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void rockThrow() { }

        public void rollingKick(PokemonBase target)
        {
            isFlinched(target, 30);
        }

        public void scratch() { }

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

        public void slam() { }

        public void slash() { }

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

        public void strength() { }

        public void struggle() { }
        
        public void submission() { }

        public float superFang(PokemonBase target)
        {
            ignoreReflect = true;
            return target.curHp / 2f;
        }

        public void tackle() { }

        public void takeDown() { }

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

        public void viceGrip() { }

        public void vineWhip() { }

        public void waterFall(PokemonBase target)
        {
            isFlinched(target, 20);
        }

        public void wingAttack() { }

        public void wrap(PokemonBase target)
        {
            int rnd = Random.Range(4, 5);
            int dmg = (int)(target.curHp / 16f);
            target.team.addBind(rnd, dmg);

        }
    }
}
