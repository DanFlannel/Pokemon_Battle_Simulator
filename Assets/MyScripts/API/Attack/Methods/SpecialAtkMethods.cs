using FBG.Attack;
using FBG.Base;
using FBG.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAtkMethods : BaseMoves
{
    public void absorb()
    {
        heal = damage / 2f;
    }

    public void acid(PokemonBase target)
    {
        if (!Chance_100(10)) { return; }
        changeStats(Consts.spDefense, -1, target);
        stageName = Consts.spDefense;
        stageDiff = -1;
        stagePokemon = target.Name;

    }

    public void auroraBeam(PokemonBase target)
    {
        if (!Chance_100(10)) { return; }
        changeStats(Consts.attack, -1, target);
        stageName = Consts.attack;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void blizzard(PokemonBase target)
    {
        isFrozen(target, 10);
    }

    public void bubble(PokemonBase target)
    {
        if (!Chance_100(10)) { return; }

        changeStats(Consts.speed, -1, target);
        stageName = Consts.speed;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void bubbleBeam(PokemonBase target)
    {
        if (!Chance_100(10)) { return; }

        changeStats(Consts.speed, -1, target);
        stageName = Consts.speed;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void confusion(PokemonBase target)
    {
        int rnd = UnityEngine.Random.Range(1, 4);
        isConfused(target, 10, rnd);
    }

    public void dragonRage()
    {
        ignoreLightScreen = true;
        damage = 40;
    }

    public void dreamEater(PokemonBase target)
    {
        if (target.status_A == nonVolitileStatusEffects.sleep)
        {
            heal = Mathf.Round(damage / 2f);
        }
        else
        {
            damage = 0;
        }
    }

    public void ember(PokemonBase target)
    {
        isBurned(target, 10);
    }

    public void fireBlast(PokemonBase target)
    {
        isBurned(target, 10);
    }

    public void fireSpin(PokemonBase target)
    {
        int rnd = Random.Range(4, 5);
        float fSpingDmg = target.maxHP / 16f;
        target.team.addBind(rnd, fSpingDmg);
    }

    public void flameThrower(PokemonBase target)
    {
        isBurned(target, 10);
    }

    public void gust(PokemonBase target)
    {
        if (target.position == pokemonPosition.flying)
        {
            damage *= 2f;
            moveRes.ignoreSemiInvulerable = true;
        }
    }

    public void hydroPump() { }

    public void hyperBeam(PokemonBase self, string atkName, float baseDamage)
    {
        ReChargeMove(self, atkName, baseDamage);
    }

    public void iceBeam(PokemonBase target)
    {
        isFrozen(target, 10);
    }

    public void megaDrain()
    {
        heal = Mathf.Round(damage / 2f);
    }

    public void nightShade(PokemonBase target)
    {
        damage = levelBasedDamage(target);
    }

    public void petalDance(string tempname, PokemonBase self)
    {
        int dur = Random.Range(2, 3);
        repeatAttack_Confused petalDance = new repeatAttack_Confused(tempname, dur, self);
        if (!hasEffector(self, tempname))
        {
            self.effectors.Add(petalDance);
        }
    }

    public void psybeam(PokemonBase target)
    {
        int rnd = Random.Range(1, 4);
        isConfused(target, 10, rnd);
    }

    public void psychic(PokemonBase target)
    {
        if (!Chance_100(10)) { return; }
        changeStats(Consts.spDefense, -1, target);
        stageName = Consts.spDefense;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void psywave(PokemonBase target)
    {
        float mod = Random.Range(.5f, 1.5f);
        damage = levelBasedDamage(target) * mod;
    }

    public void razorwind(PokemonBase self, string atkName, float baseDamage)
    {
        damage = ChargingMove(self, atkName, baseDamage);
    }

    public void sludge(PokemonBase target)
    {
        isPosioned(target, 30);
    }

    public void smog(PokemonBase target)
    {
        isPosioned(target, 40);
    }

    public void solarbeam(PokemonBase self, string atkName, float baseDamage)
    {
        damage = ChargingMove(self, atkName, baseDamage);
    }

    public void sonicBoom(PokemonBase target)
    {
        ignoreLightScreen = true;
        if (checkTypes(target, Consts.Ghost))
        {
            damage = 0;
        }
        else
        {
            damage = 20f;
        }
    }

    public void surf(PokemonBase target) {
        if(target.position == pokemonPosition.underwater)
        {
            damage *= 2f;
            moveRes.ignoreSemiInvulerable = true;
        }
    }

    public void swift() {
        moveRes.ignoreSemiInvulerable = true;
    }

    public void thunder(PokemonBase target)
    {
        isParalized(target, 30);
        if(target.position == pokemonPosition.flying)
        {
            moveRes.ignoreSemiInvulerable = true;
        }
    }

    public void thunderShock(PokemonBase target)
    {
        isParalized(target, 10);
    }

    public void thunderBolt(PokemonBase target)
    {
        isParalized(target, 10);
    }

    public void triAttack(PokemonBase target)
    {
        isParalized(target, 6.67f);
        isBurned(target, 6.67f);
        isFrozen(target, 6.67f);
    }

    public void waterGun() { }
}
