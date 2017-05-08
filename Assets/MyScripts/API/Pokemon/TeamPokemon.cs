﻿using FBG.Attack;
using FBG.Battle;
using FBG.Data;
using FBG.JSON;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace FBG.Base
{
    public class TeamPokemon : TeamEffects
    {
        private TeamPokemon instance;
        public TeamPokemon enemyTeam;
        public List<PokemonBase> pokemon = new List<PokemonBase>();
        public int teamSize;
        public int curIndex;
        public string teamName;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }
        private BattleSimulator sim;

        public TeamPokemon(int size, string name, ref BattleSimulator sim)
        {
            teamSize = size;
            instance = this;
            teamName = name;
            this.sim = sim;
            SetupTeamEffects();
        }

        public void assignEnemyTeam(ref TeamPokemon team)
        {
            enemyTeam = team;
        }

        public void swap(int index)
        {
            curIndex = index;
            if (enemyTeam.hasLeechSeed)
            {
                enemyTeam.hasLeechSeed = false;
            }
            pokemon[index].nvCurDur = 0;
        }

        public int getRndPokemon()
        {
            List<int> tmp = new List<int>();
            for(int i = 0; i < pokemon.Count; i++)
            {
                if(pokemon[i].curHp > 0 && i != curIndex)
                {
                    tmp.Add(i);
                }
            }
            if(tmp.Count == 0)
            {
                return -1;
            }
            return tmp[UnityEngine.Random.Range(0, tmp.Count)];
        }

        public MoveResults getMoveResults(int index)
        {
            string atkName = curPokemon.atkMoves[index];
            PokemonBase self = curPokemon;
            PokemonBase tar = enemyTeam.curPokemon;
            MoveResults result = AtkCalc.calculateAttackEffect(tar, self, atkName);
            return result;
        }

        public void takeTurn(int index, bool isSwapping, PokemonBase pkmn)
        {
            if (isSwapping)
            {
                Debug.Log("Swapping Pokemon");
                swap(index);
                BattleSimulator.Instance.changeSprites(ref instance);
                return;
            }

            if (!isAlive(pkmn))
            {
                return;
            }

            if (checkNVStatus())
            {
                return;
            }
            MoveResults move = getMoveResults(index);

            sim.routine.queue.AddCoroutineToQueue(sim.routine.usedMoveText(curPokemon.Name, move.name));

            applyDamage(move);
            applyHeal(move);
            applyRecoil(move);
            curPokemon.curPP[index]--;
            BattleSimulator.Instance.addMoveHistory(move, curPokemon);
        }

        public bool isAlive(PokemonBase pkmn)
        {
            if (pkmn.curHp <= 0)
            {
                Debug.Log("Dead pokemon, prompting swap");
                BattleSimulator.Instance.battleGUI.promptSwap(ref instance, true);
                hasLeechSeed = false;
                return false;
            }
            return true;
        }

        private void applyDamage(MoveResults move)
        {
            if (move.dmgReport.damage == 0) { return; }
            if (enemyTeam.curPokemon.curHp - move.dmgReport.damage <= 0)
            {
                move.dmgReport.damage = enemyTeam.curPokemon.curHp;
            }
            enemyTeam.curPokemon.curHp -= (int)move.dmgReport.damage;
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage());
        }

        private void applyHeal(MoveResults move)
        {
            if (move.dmgReport.heal == 0) { return; }
            if (move.dmgReport.heal + curPokemon.curHp > curPokemon.maxHP)
            {
                move.dmgReport.heal = curPokemon.maxHP - curPokemon.curHp;
            }
            curPokemon.curHp += (int)move.dmgReport.heal;
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyHeal());
        }

        private void applyRecoil(MoveResults move)
        {
            if (move.dmgReport.recoil == 0) { return; }
            if (curPokemon.curHp - move.dmgReport.recoil <= 0)
            {
                move.dmgReport.recoil = curPokemon.curHp;
            }
            curPokemon.curHp -= (int)move.dmgReport.recoil;
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyRecoil());
        }

        private bool checkNVStatus()
        {
            if (curPokemon.status_A != nonVolitileStatusEffects.none)
            {
                nonVolitleMove nv = Utilities.isMoveHaltedByNV(curPokemon);
                if (nv.isAffected)
                {
                    return true;
                }
            }
            return false;
        }

        public void generateRandomTeam()
        {
            corePokemonData data;
            int id;
            int level = 75;
            List<attackIndex> attacks = new List<attackIndex>();
            PokemonBase tmpPokemon;

            //unfortunately we have to generate it twice.
            for (int i = 0; i < teamSize; i++)
            {
                id = UnityEngine.Random.Range(0, 151);
                //id = 0;
                //level = 100;
                data = DexHolder.pokeDex.getStats(id);
                attacks = MoveSets.masterGetAttacks(id);
                //print(string.Format("{0}", attacks.Count));
                tmpPokemon = new PokemonBase(level, data, attacks, ref instance);
                pokemon.Add(tmpPokemon);
            }
        }

        public void victory()
        {

        }
    }
}