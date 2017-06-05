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
        public enum TeamType
        {
            AI,
            Player
        };

        private TeamPokemon instance;
        private BattleSimulator sim;
        private GameObject GUIHolder;

        public TeamPokemon enemyTeam;
        public List<PokemonBase> pokemon = new List<PokemonBase>();

        public int teamSize;
        public int curIndex = 0;

        public string teamName;
        public TeamType type;

        public GUIReferences guiRef;
        public PokemonBase curPokemon { get { return pokemon[curIndex]; } }

        public TeamPokemon(int size, string name, TeamType type, ref BattleSimulator sim, GameObject gui)
        {
            teamSize = size;
            instance = this;
            teamName = name;
            this.sim = sim;
            this.type = type;

            GUIHolder = gui;
            guiRef = new GUIReferences(GUIHolder);
            guiRef.reset_status();
            SetupTeamEffects();
        }

        public void assignEnemyTeam(ref TeamPokemon team)
        {
            enemyTeam = team;
        }

        public void swap(int index)
        {
            Debug.Log(string.Format("swapping to index: {0}", index));
            curIndex = index;
            enemyTeam.hasLeechSeed = false;
            pokemon[index].nvCurDur = 0;
            sim.updatePokemonIndex();
            sim.updateGUI(ref instance);
            Debug.Log(string.Format("cur index: {0}, curpokemon: {1}", curIndex, curPokemon.Name));
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
            MoveResults result = AtkCalc.calculateAttack(tar, self, atkName);
            return result;
        }

        public void takeTurn(int index, bool isSwapping, PokemonBase target)
        {
            //catch for if we move second but die before we move, then we have already switched, therefore we used out turn and it is the end of it.
            if(target != curPokemon)
            {
                Debug.Log(string.Format("Expected: {0} Actual: {1}", curPokemon.Name, target.Name));
                target = curPokemon;
                return;
            }

            Debug.LogWarning(string.Format("{0}'s turn", target.Name));

            if (isSwapping)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.swapPokemon(curPokemon, index, false));
                Debug.Log("Turn Move is swapping");
                return;
            }

            if (!isAlive(target))
            {
                Debug.Log("Pokemon is dead");
                return;
            }

            if (nvHaltingEffect())
            {
                BattleSimulator.Instance.addMoveHistory(curPokemon, curPokemon.atkMoves[index].ToString());
                return;
            }

            MoveResults move = getMoveResults(index);
            sim.routine.queue.AddCoroutineToQueue(sim.routine.usedMoveText(curPokemon.Name, move.atkName));

            if (enemyTeam.curPokemon.curHp <= 0)
            {
                move.failed = true;
            }

            if (DexHolder.attackDex.getAttack(move.atkName).cat == Consts.Status)
            {
                if (move.dmgReport.stageDelta != 0)
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.statusAffected(move));
                }
            }

            if (!move.failed && move.hit.sucess)
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

            if (!move.hit.sucess && !move.failed)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.missed(curPokemon));
            }

            BattleSimulator.Instance.addMoveHistory(move, curPokemon, target);
        }

        public bool isAlive(PokemonBase pkmn)
        {
            Debug.Log(string.Format("Checking if {0} is alive", pkmn.Name));
            if (pkmn.curHp <= 0)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.swapPokemon(pkmn, 0, true));
                pkmn.team.hasLeechSeed = false;
                return false;
            }
            return true;
        }

        private void applyDamage(MoveResults move)
        {
            if (move.flinched)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.flinched(curPokemon));
                return;
            }

            if (move.dmgReport.damage != 0)
            {
                if (enemyTeam.curPokemon.substituteHealth > 0 && !DexHolder.attackDex.checkFlag(move.atkName, "authentic"))
                {
                    sim.routine.queue.AddCoroutineToQueue(sim.routine.substituteHit(enemyTeam.curPokemon));
                    enemyTeam.curPokemon.substituteHealth -= move.dmgReport.damage;

                    if (enemyTeam.curPokemon.substituteHealth <= 0)
                    {
                        sim.routine.queue.AddCoroutineToQueue(sim.routine.substituteFaded(enemyTeam.curPokemon));
                    }
                    return;
                }

                sim.routine.queue.AddCoroutineToQueue(sim.routine.applyDamage(enemyTeam.curPokemon, (int)move.dmgReport.damage, move));
            }

            if (move.statusAEffect != nonVolitileStatusEffects.none && move.hit.sucess)
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.addNV(move.statusTarget, move.statusAEffect));
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

            if (move.atkName.ToLower() != "substitute")
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
            if (self.team.bindDamage == 0) { return; }
            string text = "";
            int damage = Mathf.RoundToInt(bindDamage);

            text = string.Format("{0} was hurt by {1}", self.Name, self.team.bindName);

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
            nonVolitleMove nv = Utilities.isMoveHaltedByNV(curPokemon);
            if (nv.text != "")
            {
                sim.routine.queue.AddCoroutineToQueue(sim.routine.displayText(nv.text, 2f));
                Debug.Log(string.Format("NV TEXT: {0} Text: {1}", nv.isAffected, nv.text));
            }
            guiRef.updateStatus_A(curPokemon);
            return nv.isAffected;
        }

        public bool checkTeam()
        {
            for (int i = 0; i < pokemon.Count; i++)
            {
                if (pokemon[i].curHp > 0)
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
                if (enemyTeam.pokemon[i].curHp > 0)
                {
                    //Debug.Log("found a healthy pokemon");
                    return;
                }
            }

            string text = string.Format("{0} won!", teamName);
            if (!checkTeam())
            {
                text = "It's a draw!";
            }
            Debug.Log(string.Format("{0}", text));

            sim.routine.queue.AddCoroutineToQueue(sim.routine.victory(text));
        }
    }

    public class GUIReferences
    {
        private Transform gui;
        public Text name;
        public Text level;
        public Text health;
        public Slider slider;
        public Text status_a;

        public GUIReferences(GameObject go)
        {
            gui = go.transform;
            name = gui.Find("Name").GetComponent<Text>();
            level = gui.Find("Level").GetComponent<Text>();
            health = gui.Find("HealthValue").GetComponent<Text>();
            slider = gui.GetComponentInChildren<Slider>();
            status_a = gui.Find("Status_A").GetComponent<Text>();
        }

        public void update(PokemonBase pkmn)
        {
            name.text = pkmn.Name;
            level.text = string.Format("LVL {0}", pkmn.Level);
            health.text = string.Format("{0}/{1}", pkmn.curHp, pkmn.maxHP);
            updateStatus_A(pkmn);
            float sliderValue = (float)pkmn.curHp / (float)pkmn.maxHP;
            slider.value = sliderValue;
        }

        //this should be redone
        public void updateStatus_A(PokemonBase pkmn)
        {
            string text = pkmn.status_A.ToString();
            if (pkmn.status_A == nonVolitileStatusEffects.none)
            {
                reset_status();
                return;
            }
            status_a.text = string.Format("{0}", text);
        }

        public void reset_status()
        {
            string text = "";
            status_a.text = text;
        }
    }
}