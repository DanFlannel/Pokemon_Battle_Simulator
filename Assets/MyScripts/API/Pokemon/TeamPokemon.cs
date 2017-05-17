using FBG.Attack;
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
        public TeamPokemon enemyTeam;
        public List<PokemonBase> pokemon = new List<PokemonBase>();
        public int teamSize;
        public int curIndex;
        public string teamName;
        private GameObject GUIHolder;
        public GUIReferences guiRef;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }
        private BattleSimulator sim;

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
            if (enemyTeam.hasLeechSeed)
            {
                enemyTeam.hasLeechSeed = false;
            }
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

            MoveResults move = getMoveResults(index);
            sim.routine.queue.AddCoroutineToQueue(sim.routine.usedMoveText(curPokemon.Name, move.name));

            if (!isAlive(pkmn))
            {
                return;
            }
            else if (checkNVStatus())
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.blockingNV(curPokemon));
            }
            else if (move.flinched)
            {
                //add the flinched text
            }
            else
            {
                applyDamage(move);
                applyHeal(move);
                applyRecoil(move);
            }
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
            if (move.statusEffect == "" && move.dmgReport.damage == 0)
            {
                return;
            }

            if (move.statusEffect != "")
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.addNV(enemyTeam.curPokemon, move.statusEffect));
            }

            if (move.dmgReport.damage != 0)
            {
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
            if (move.dmgReport.heal + curPokemon.curHp > curPokemon.maxHP)
            {
                move.dmgReport.heal = curPokemon.maxHP - curPokemon.curHp;
            }

            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyHeal(curPokemon, (int)move.dmgReport.heal));

            //curPokemon.curHp += (int)move.dmgReport.heal;
        }

        private void applyRecoil(MoveResults move)
        {
            if (move.dmgReport.recoil == 0) { return; }
            if (curPokemon.curHp - move.dmgReport.recoil <= 0)
            {
                move.dmgReport.recoil = curPokemon.curHp;
            }

            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyRecoil(curPokemon, (int)move.dmgReport.recoil));

            //curPokemon.curHp -= (int)move.dmgReport.recoil;
        }

        public void endOfTurnDamage(PokemonBase self)
        {
            if (self.curHp < 0) { return; }
            applyBindDamage(self);
            applyNVDamage(self);
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

        private void applyNVDamage(PokemonBase self)
        {
            if (!isDamagingNV(self.status_A)) { return; }

            string text = "";
            int damage = 0;

            self.nvCurDur++;

            if (self.status_A == nonVolitileStatusEffects.burned)
            {
                damage = Mathf.RoundToInt((float)self.maxHP / 8f);
            }

            if (self.status_A == nonVolitileStatusEffects.poisioned)
            {
                damage = Mathf.RoundToInt((float)self.maxHP / 16f);
            }

            if (self.status_A == nonVolitileStatusEffects.toxic)
            {
                damage = Mathf.RoundToInt((float)self.maxHP / 16f) * self.nvCurDur;
            }

            Debug.Log(string.Format("Applying {0} damage to {1} damage: {2}", self.status_A.ToString(), self.Name, damage));

            text = string.Format("{0} was hurt by {1}", self.Name, self.status_A.ToString());

            sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(text, 2f));
            sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(self, damage));
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