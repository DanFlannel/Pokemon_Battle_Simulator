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

        public void swap(int index)
        {
            curIndex = index;
            if (enemyTeam.hasLeechSeed)
            {
                enemyTeam.hasLeechSeed = false;
            }
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

        public void takeTurn(int index, bool isSwapping)
        {
            if (isSwapping)
            {
                Debug.Log("Swapping Pokemon");
                swap(index);
                BattleSimulator.Instance.changeSprites(ref instance);
                return;
            }

            if (!checkCurPokemon())
            {
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
            curPokemon.curPP[index]--;
            if(enemyTeam.curPokemon.curHp <= 0)
            BattleSimulator.Instance.addMoveHistory(move, curPokemon);
        }

        public bool checkCurPokemon()
        {
            if (curPokemon.curHp <= 0)
            {
                Debug.Log("Dead pokemon, prompting swap");
                BattleSimulator.Instance.battleGUI.promptSwap(ref instance, true);
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
