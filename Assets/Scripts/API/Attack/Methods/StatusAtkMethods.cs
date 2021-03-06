﻿using UnityEngine;

using Attack;
using Base;
using Battle;
using Data;

public class StatusAtkMethods : BaseMoves
{
    public void acidArmor(PokemonBase target)
    {
        changeStats(Consts.defense, 2, target);
        stageName = Consts.defense;
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void agility(PokemonBase target)
    {
        changeStats(Consts.speed, 2, target);
        stageName = Consts.speed;
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void amnesia(PokemonBase target)
    {
        changeStats(Consts.spDefense, 2, target);
        stageName = Consts.spDefense;
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void barrier(PokemonBase target)
    {
        changeStats(Consts.defense, 2, target);
        stageName = Consts.defense;
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void confuseRay(PokemonBase target)
    {
        int rnd = Random.Range(1, 4);
        isConfused(target, 10, rnd);
    }

    public void conversion(PokemonBase target)
    {
        //tempList = genAttacks.get_playerAttackName();
        //string tempName = tempList[0];
        string name = target.atkMoves[0];
        int attack_index = AtkCalc.getAttackListIndex(name);
        string attack_type = MoveSets.attackList[attack_index].type;
        target.type1 = attack_type;
        string[] types = new string[2];
        types[0] = target.type1;
        types[1] = target.type2;
        target.damageMultiplier = DamageMultipliers.createMultiplier(types);
    }

    public void defenseCurl(PokemonBase target)
    {
        changeStats(Consts.defense, 1, target);
        stageName = Consts.defense;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void disable(PokemonBase self, PokemonBase target, string tempName)
    {
        string moveName = BattleSimulator.Instance.moveHistory[BattleSimulator.Instance.moveHistory.Count - 1].attackName;
        disable dis = new disable(tempName, 4, target, moveName);

        if (!hasEffector(target, tempName))
        {
            target.effectors.Add(dis);
        }
    }

    public void doubleTeam(PokemonBase target)
    {
        changeStats(Consts.evasion, 1, target);
        stageName = Consts.evasion;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void flash(PokemonBase target)
    {
        changeStats(Consts.accuracy, -1, target);
        stageName = Consts.accuracy;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void focusEnergy(PokemonBase target)
    {
        target.changeCritStage(2);
        stageName = "Critical Strike";
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void growl(PokemonBase target)
    {
        changeStats(Consts.attack, -1, target);
        stageName = Consts.attack;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void growth(PokemonBase target)
    {
        changeStats(Consts.spAttack, 1, target);
        changeStats(Consts.attack, 1, target);
        stageName = Consts.attack + " & " + Consts.spAttack;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void harden(PokemonBase target)
    {
        changeStats(Consts.defense, 1, target);
        stageName = Consts.defense;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void haze(PokemonBase self, PokemonBase target)
    {
        self.resetStatStages();
        target.resetStatStages();
        stageName = "reset all stat changes";
        stageDiff = 0;
        stagePokemon = "all";
    }

    public void hypnosis(PokemonBase target)
    {
        int rnd = Random.Range(1, 3);
        isSleep(target, 100, rnd);
    }

    public void kinesis(PokemonBase target)
    {
        changeStats(Consts.accuracy, -1, target);
        stageName = Consts.accuracy;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void leechSeed(PokemonBase target)
    {
        if (checkTypes(target, Consts.Grass) || target.hasSubstitute)
        {
            moveRes.failed = true;
            return;
        }
        target.team.hasLeechSeed = true;
    }

    public void leer(PokemonBase target)
    {
        changeStats(Consts.defense, -1, target);
        stageName = Consts.defense;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void lightScreen(PokemonBase target)
    {
        target.team.addLightScreen(5);
    }

    public void lovelyKiss(PokemonBase target)
    {
        int rnd = Random.Range(1, 3);
        isSleep(target, 100, rnd);
    }

    public void meditate(PokemonBase target)
    {
        changeStats(Consts.attack, 1, target);
        stageName = Consts.attack;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void metronome(PokemonBase self, PokemonBase target)
    {
        int rnd = Random.Range(0, MoveSets.attackList.Count);
        string atkName = MoveSets.attackList[rnd].name;
        moveRes = AtkCalc.calculateAttack(target, self, atkName);
        damage = moveRes.dmgReport.damage;
        heal = moveRes.dmgReport.heal;
        recoil = moveRes.dmgReport.recoil;
        stageName = moveRes.dmgReport.stageName;
        stageDiff = moveRes.dmgReport.stageDelta;
        stagePokemon = moveRes.dmgReport.stagePokemon;
    }

    public void mimic(PokemonBase target)
    {
        int n = 0;
        int index = BattleSimulator.Instance.moveHistory.Count - 1;
        if (BattleSimulator.Instance.moveHistory.Count == 0)
        {
            moveRes.failed = true;
            return;
        }
        string attack = BattleSimulator.Instance.moveHistory[index].attackName;
        for (int i = 0; i < target.atkMoves.Count; i++)
        {
            if (target.atkMoves[i].ToLower() == "mimic")
            {
                n = i;
                break;
            }
        }
        target.atkMoves[n] = attack;
    }

    public void minimize(PokemonBase target)
    {
        target.position = pokemonPosition.minimized;
        changeStats(Consts.evasion, 1, target);
        stageName = Consts.evasion;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void mirrorMove(PokemonBase self, PokemonBase target)
    {
        int index = BattleSimulator.Instance.moveHistory.Count - 1;
        string mirrorAttack = BattleSimulator.Instance.moveHistory[index].attackName;
        if (!DexHolder.attackDex.checkFlag(mirrorAttack, "mirror"))
        {
            moveRes.failed = true;
            return;
        }
        AtkCalc.calculateAttack(target, self, mirrorAttack);
    }

    public void mist(PokemonBase target)
    {
        target.team.addMist();
    }

    public void poisonGas(PokemonBase target)
    {
        isPosioned(target, 100);
    }

    public void poisonPowder(PokemonBase target)
    {
        isPosioned(target, 100);
    }

    public void recover(PokemonBase target)
    {
        heal = target.maxHP / 2f;
    }

    public void reflect(PokemonBase target)
    {
        target.team.addReflect(5);
    }

    public void rest(PokemonBase target, int duration)
    {
        if (target.status_A != nonVolitileStatusEffects.sleep)
        {
            target.nvDur = duration + 1;
            target.status_A = nonVolitileStatusEffects.sleep;
            Debug.Log(string.Format("{0} is now asleep", target.Name));
            moveRes.statusAEffect = nonVolitileStatusEffects.sleep;
            moveRes.statusTarget = target.Name;
            heal = target.maxHP;
        }
    }

    public void roar(PokemonBase target)
    {
        rndSwap(target);
    }

    public void sandAttack(PokemonBase target)
    {
        changeStats(Consts.accuracy, -1, target);
        stageName = Consts.accuracy;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void screech(PokemonBase target)
    {
        changeStats(Consts.defense, -2, target);
        stageName = Consts.defense;
        stageDiff = -2;
        stagePokemon = target.Name;
    }

    public void sharpen(PokemonBase target)
    {
        changeStats(Consts.attack, 1, target);
        stageName = Consts.attack;
        stageDiff = 1;
        stagePokemon = target.Name;
    }

    public void sing(PokemonBase target)
    {
        int rnd = Random.Range(1, 3);
        isSleep(target, 100, rnd);
    }

    public void smokeScreen(PokemonBase target)
    {
        changeStats(Consts.accuracy, -1, target);
        stageName = Consts.accuracy;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void softBoiled(PokemonBase target)
    {
        heal = target.maxHP / 2f;
    }

    public void splash()
    {
    }

    public void spore(PokemonBase target)
    {
        int rnd = UnityEngine.Random.Range(1, 3);
        isSleep(target, 100, rnd);
    }

    public void stringShot(PokemonBase target)
    {
        changeStats(Consts.speed, -2, target);
        stageName = Consts.speed;
        stageDiff = -2;
        stagePokemon = target.Name;
    }

    public void stunSpore(PokemonBase target)
    {
        isParalized(target, 100);
    }

    public void substitute(PokemonBase target)
    {
        if (target.curHp <= Mathf.Round(target.maxHP / 4f) || target.hasSubstitute)
        {
            moveRes.failed = true;
            return;
        }

        recoil = Mathf.Round(target.maxHP / 4f);
        target.substituteHealth = recoil;
        target.hasSubstitute = true;
    }

    public void supersonic(PokemonBase target)
    {
        int rnd = UnityEngine.Random.Range(1, 4);
        isConfused(target, 100, rnd);
    }

    public void swordsDance(PokemonBase target)
    {
        changeStats(Consts.attack, 2, target);
        stageName = Consts.attack;
        stageDiff = 2;
        stagePokemon = target.Name;
    }

    public void tailWhip(PokemonBase target)
    {
        changeStats(Consts.defense, -1, target);
        stageName = Consts.attack;
        stageDiff = -1;
        stagePokemon = target.Name;
    }

    public void teleport()
    {
    }

    public void thunderWave(PokemonBase target)
    {
        isParalized(target, 100);
    }

    /// <summary>
    /// Applying toxic to a pokemon if they arent either steel or a poison type pokemon and have no other status effects
    /// </summary>
    /// <param name="isPlayer">is the player attacking</param>
    public void toxic(PokemonBase self, PokemonBase target)
    {
        if (checkTypes(target, Consts.Steel, Consts.Poison)) { return; }

        if (checkTypes(self, Consts.Poison))
        {
            moveRes.hit.ignore(true);
        }

        if (target.status_A == nonVolitileStatusEffects.none)
        {
            //target.status_A = nonVolitileStatusEffects.toxic;
            target.nvCurDur = 0;
            Debug.Log(string.Format("{0} is now badly posioned", target.Name));
            moveRes.statusAEffect = nonVolitileStatusEffects.toxic;
            moveRes.statusTarget = target.Name;
        }
        else if (target.status_A != nonVolitileStatusEffects.none)
        {
            moveRes.failed = true;
        }
    }

    public void transform(PokemonBase self, PokemonBase target)
    {
        self.atkMoves = target.atkMoves;
    }

    public void whirlwind(PokemonBase target)
    {
        rndSwap(target);
        moveRes.hit.ignore(pokemonPosition.flying);
    }

    public void withdraw(PokemonBase target)
    {
        changeStats(Consts.defense, 1, target);
        stageName = Consts.defense;
        stageDiff = 1;
        stagePokemon = target.Name;
    }
}