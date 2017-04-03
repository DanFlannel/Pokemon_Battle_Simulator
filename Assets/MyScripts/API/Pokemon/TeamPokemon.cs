using FBG.Attack;
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
        public List<int> aliveIndexs = new List<int>();
        public List<PokemonBase> pokemon = new List<PokemonBase>();
        public int teamSize;
        public int curIndex;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }

        public TeamPokemon(int size)
        {
            teamSize = size;
            instance = this;
            SetupTeamEffects();
        }

        public void assignEnemyTeam(ref TeamPokemon team)
        {
            enemyTeam = team;
        }

        public void OnSwap()
        {
            if (enemyTeam.hasLeechSeed)
            {
                enemyTeam.hasLeechSeed = false;
            }
        }

        public MoveResults getMoveResults(int index)
        {
            string atkName = curPokemon.atkMoves[index];
            PokemonBase self = curPokemon;
            PokemonBase tar = enemyTeam.curPokemon;
            MoveResults result = AtkCalc.calculateAttackEffect(tar, self, atkName);
            return result;
        }

        public void takeTurn(int index)
        {
            if (curPokemon.curHp <= 0)
            {
                //TODO prompt swap,
                return;
            }
            if (checkNVStatus())
            {
                return;
            }
            MoveResults move = getMoveResults(index);
            applyDamage(move);
            applyHeal(move);
            applyRecoil(move);
            BattleSimulator.Instance.addMoveHistory(move, curPokemon);
        }

        private void applyDamage(MoveResults move)
        {
            if (move.dmgReport.damage == 0) { return; }
            if (enemyTeam.curPokemon.curHp - move.dmgReport.damage <= 0)
            {
                move.dmgReport.damage = enemyTeam.curPokemon.curHp;
            }
            enemyTeam.curPokemon.curHp -= (int)move.dmgReport.damage;
            //now we have to force the other team to switch!
        }

        private void applyHeal(MoveResults move)
        {
            if (move.dmgReport.heal == 0) { return; }
            if (move.dmgReport.heal + curPokemon.curHp > curPokemon.maxHP)
            {
                move.dmgReport.heal = curPokemon.maxHP - curPokemon.curHp;
            }
            curPokemon.curHp += (int)move.dmgReport.heal;
        }

        private void applyRecoil(MoveResults move)
        {
            if (move.dmgReport.recoil == 0) { return; }
            if (curPokemon.curHp - move.dmgReport.recoil <= 0)
            {
                move.dmgReport.recoil = curPokemon.curHp;
            }
            curPokemon.curHp -= (int)move.dmgReport.recoil;
        }

        private bool checkNVStatus()
        {
            if (curPokemon.status_A != nonVolitileStatusEffects.none)
            {
                nonVolitleMove nv = Utilities.isMoveHaltedByNV(curPokemon);
                if (nv.isAffected)
                {

                    //TODO add text to a coroutine
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
    }
}
