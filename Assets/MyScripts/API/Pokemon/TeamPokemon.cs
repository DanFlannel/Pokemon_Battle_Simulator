﻿using FBG.Attack;
using FBG.Battle;
using FBG.Data;
using FBG.JSON;
using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FBG.Base
{
    public class TeamPokemon : TeamEffects
    {
        private TeamPokemon instance;
        private BattleSimulator sim;
        private GameObject GUIHolder;

        public TeamPokemon enemyTeam;
        public List<PokemonBase> pokemon = new List<PokemonBase>();

        public int teamSize;
        public int curIndex;

        public string teamName;

        public GUIReferences guiRef;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }

        public TeamPokemon(int size, string name, ref BattleSimulator sim, GameObject gui)
        {
            teamSize = size;
            instance = this;
            teamName = name;
            this.sim = sim;

            GUIHolder = gui;
            guiRef = new GUIReferences(GUIHolder);
            SetupTeamEffects();
        }

        public void assignEnemyTeam(ref TeamPokemon team)
        {
            enemyTeam = team;
        }

        public void swap(int index)
        {
            curIndex = index;
            enemyTeam.hasLeechSeed = false;
            pokemon[index].nvCurDur = 0;
        }

        public int getRndPokemon()
        {
            List<int> tmp = new List<int>();
            for (int i = 0; i < pokemon.Count; i++)
            {
                if (pokemon[i].curHp > 0 && i != curIndex)
                {
                    tmp.Add(i);
                }
            }
            if (tmp.Count == 0)
            {
                return -1;
            }
            return tmp[UnityEngine.Random.Range(0, tmp.Count)];
        }

        public MoveResults getMoveResults(int index)
        {
            string atkName = curPokemon.atkMoves[index];
            if (curPokemon.nextAttack != "")
            {
                atkName = curPokemon.nextAttack;
            }
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
                BattleSimulator.Instance.updateGUI(ref instance);
                return;
            }

            if (!isAlive(pkmn))
            {
                return;
            }

            if (nvHaltingEffect())
            {
                BattleSimulator.Instance.addMoveHistory(curPokemon, curPokemon.atkMoves[index].ToString());
                return;
            }

            MoveResults move = getMoveResults(index);
            sim.routine.queue.AddCoroutineToQueue(sim.routine.usedMoveText(curPokemon.Name, move.name));

            if (DexHolder.attackDex.getAttack(move.name).cat == Consts.Status)
            {
                if (move.dmgReport.stageDelta != 0)
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.statusAffected(move));
                }
            }

            if (!move.failed && move.hit)
            {
                applyDamage(move);
                applyHeal(move);
                applyRecoil(move);
                curPokemon.curPP[index]--;
            }

            if (move.failed)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.failedMove());
            }

            if (!move.hit && !move.failed)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.missed(curPokemon));
            }

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
            if (move.statusEffect != "")
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.addNV(enemyTeam.curPokemon, move.statusEffect));
            }

            if (move.flinched)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.flinched(curPokemon));
                return;
            }

            if (move.dmgReport.damage != 0)
            {
                if (enemyTeam.curPokemon.substituteHealth > 0 && !DexHolder.attackDex.checkFlag(move.name, "authentic"))
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.substituteHit(enemyTeam.curPokemon));
                    enemyTeam.curPokemon.substituteHealth -= move.dmgReport.damage;
                    if (enemyTeam.curPokemon.substituteHealth <= 0)
                    {
                        sim.routine.queue.AddCoroutineToQueue(sim.routine.substituteFaded(enemyTeam.curPokemon));
                    }
                    return;
                }

                sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(enemyTeam.curPokemon, (int)move.dmgReport.damage));

                if (move.crit)
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.criticalHit());
                }

                sim.routine.queue.AddCoroutineToQueue(sim.routine.effectiveText(move.name, enemyTeam.curPokemon));
            }
        }

        private void applyHeal(MoveResults move)
        {
            if (move.dmgReport.heal == 0) { return; }
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyHeal(curPokemon, (int)move.dmgReport.heal));
        }

        private void applyRecoil(MoveResults move)
        {
            if (move.dmgReport.recoil == 0) { return; }

            if (move.name.ToLower() != "substitute")
            {
                string text = string.Format("{0} was hurt by recoil.", curPokemon.Name);
                sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 1f));
            }

            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyRecoil(curPokemon, (int)move.dmgReport.recoil));
        }

        public void endOfTurnDamage(PokemonBase self)
        {
            if (self.curHp <= 0) { return; }

            applyBindDamage(self);
            applyNVDamage(self);
            applyLeechSeed(self, enemyTeam.curPokemon);
        }

        private void applyBindDamage(PokemonBase self)
        {
            if (!isBound) { return; }
            string ending = "";
            string text = "";
            int damage = Mathf.RoundToInt(bindDamage);
            ending = "bind";
            text = string.Format("{0} was hurt by {1}", self.Name, ending);
            Debug.Log(string.Format("Applying bind damage: {0} to {1}", bindDamage, self.Name));

            sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 2f));
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(self, damage));
        }

        /// <summary>
        /// applying the status_A, non volitle status damage to a pokemon
        /// </summary>
        /// <param name="self">the pokemon the damage will be applied to</param>
        private void applyNVDamage(PokemonBase self)
        {
            if (!isDamagingNV(self.status_A)) { return; }

            string text = "";
            int damage = 0;
            string ending = self.status_A.ToString();
            self.nvCurDur++;

            if (self.status_A == nonVolitileStatusEffects.burned)
            {
                damage = Mathf.RoundToInt((float)self.maxHP / 8f);
            }

            if (self.status_A == nonVolitileStatusEffects.poisioned)
            {
                ending = "poison";
                damage = Mathf.RoundToInt((float)self.maxHP / 16f);
            }

            if (self.status_A == nonVolitileStatusEffects.toxic)
            {
                damage = Mathf.RoundToInt((float)self.maxHP / 16f) * self.nvCurDur;
            }

            Debug.Log(string.Format("Applying {0} damage to {1} damage: {2}", ending, self.Name, damage));

            text = string.Format("{0} was hurt by {1}", self.Name, ending);

            sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 2f));
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(self, damage));
        }

        public void applyLeechSeed(PokemonBase self, PokemonBase target)
        {
            if (target.team.hasLeechSeed)
            {
                int dmg = Mathf.RoundToInt(target.maxHP / 8f);

                string text = "";
                if (target.curHp > 0)
                {
                    text = string.Format("{0} was hurt by leech seed", target.Name);
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 1.5f));
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(target, dmg));
                }

                text = string.Format("{0} was healed by leech seed");
                sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 1.5f));
                sim.routine.queue.AddCoroutineToQueue(sim.routine.applyHeal(self, dmg));
            }
        }

        /// <summary>
        /// is the pokemon stopped by a non volitile status effect
        /// </summary>
        /// <returns></returns>
        private bool nvHaltingEffect()
        {
            if (curPokemon.status_A != nonVolitileStatusEffects.none)
            {
                nonVolitleMove nv = Utilities.isMoveHaltedByNV(curPokemon);
                if (nv.text != "")
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(nv.text, 2f));
                    Debug.Log(string.Format("Halted: {0} Text: {1}", nv.isAffected, nv.text));
                }
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
            for (int i = 0; i < enemyTeam.pokemon.Count; i++)
            {
                if (enemyTeam.pokemon[i].curHp >= 0)
                {
                    return;
                }
            }
            Debug.Log(string.Format("{0} won!"));
            //prompt victory panel
        }
    }

    public class GUIReferences
    {
        public Text name;
        public Text level;
        public Text health;
        public Slider slider;

        public GUIReferences(GameObject gui)
        {
            name = gui.transform.Find("Name").GetComponent<Text>();
            level = gui.transform.Find("Level").GetComponent<Text>();
            health = gui.transform.Find("HealthValue").GetComponent<Text>();
            slider = gui.GetComponentInChildren<Slider>();
        }
    }
}