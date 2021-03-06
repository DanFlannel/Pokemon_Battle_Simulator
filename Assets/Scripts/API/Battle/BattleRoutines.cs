﻿using CoroutineQueueHelper;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Attack;
using Base;
using Data;

namespace Battle
{
    public class BattleRoutines : MonoBehaviour
    {
        public CoroutineList queue;
        private GameObject reference;
        public BattleSimulator sim;

        public bool swapInput;
        public int swapIndex;

        public void Initalize(ref BattleSimulator sim)
        {
            this.sim = sim;
            reference = new GameObject();
            reference.name = "CoroutineHelper";
            queue = reference.AddComponent<CoroutineList>();
            swapInput = false;
        }

        public IEnumerator takeTurn()
        {
            sim.isTurnRunning = true;
            swapInput = false;
            sim.blueTeamAttack();

            List<TurnInformation> info = new List<TurnInformation>();
            info.Add(new TurnInformation(sim.redTeam.curPokemon, sim.redMoveIndex, sim.isRedSwapping));
            info.Add(new TurnInformation(sim.blueTeam.curPokemon, sim.blueMoveIndex, sim.isBlueSwapping));
            TurnOrder turn = new TurnOrder(info);

            for (int i = 0; i < turn.order.Count; i++)
            {
                yield return StartCoroutine(pokemonMove(turn, i));
            }

            for (int i = 0; i < turn.speedDetermined.Count; i++)
            {
                yield return StartCoroutine(EndOfTurnPokemon(turn, i));
            }

            yield return StartCoroutine(EndOfTurnTeam(sim.redTeam));
            yield return StartCoroutine(EndOfTurnTeam(sim.blueTeam));

            yield return StartCoroutine(resetTurn(sim));
        }

        #region IEnumerators for the turn

        private IEnumerator pokemonMove(TurnOrder turn, int i)
        {
            PokemonBase pkmn = turn.order[i].pokemon;
            pkmn.team.takeTurn(turn.order[i].moveIndex, turn.order[i].isSwapping, pkmn);
            yield return StartCoroutine(queue.masterIEnumerator());
        }

        private IEnumerator EndOfTurnPokemon(TurnOrder turn, int i)
        {
            PokemonBase pkmn = turn.speedDetermined[i].pokemon;
            //Debug.LogWarning(string.Format("{0}'s end of turn", pkmn.Name));

            pkmn.team.checkEffectors(pkmn);
            pkmn.team.endOfTurnDamage(pkmn);
            pkmn.team.isAlive(pkmn.team.curPokemon);
            yield return null;
        }

        private IEnumerator EndOfTurnTeam(TeamPokemon team)
        {
            team.EndOfTurn();
            team.victory();
            yield return StartCoroutine(queue.masterIEnumerator());
            yield return null;
        }

        private IEnumerator resetTurn(BattleSimulator sim)
        {
            sim.resetTurn();
            yield return null;
        }

        #endregion IEnumerators for the turn

        //tests

        public IEnumerator wait(float sec)
        {
            yield return new WaitForSeconds(sec);
            Debug.Log("waited " + sec + " seconds");
            yield return null;
        }

        //..Pokemon basic turn moves

        public IEnumerator usedMoveText(string pkName, string move)
        {
            string text = string.Format("{0} used {1}.", pkName, move);
            //Debug.Log("move text coroutine: " + text);
            yield return StartCoroutine(displayText(text, 2f));

            yield return null;
        }

        public IEnumerator applyDamage(PokemonBase pkmn, int dmg)
        {
            yield return StartCoroutine(changeHealthbar(pkmn, -dmg));

            if (pkmn.curHp == 0)
            {
                yield return StartCoroutine(faintedText(pkmn));
            }
            yield return null;
        }

        public IEnumerator applyDamage(PokemonBase pkmn, int dmg, MoveResults move)
        {
            yield return StartCoroutine(changeHealthbar(pkmn, -dmg));

            if (move.crit)
            {
                yield return StartCoroutine(criticalHit());
            }

            yield return StartCoroutine(effectiveText(move.atkName, pkmn));

            if (pkmn.curHp == 0)
            {
                yield return StartCoroutine(faintedText(pkmn));
            }
            yield return null;
        }

        public IEnumerator applyHeal(PokemonBase pkmn, int heal)
        {
            //Debug.Log("healing coroutine");
            yield return StartCoroutine(changeHealthbar(pkmn, heal));
            yield return null;
        }

        public IEnumerator applyRecoil(PokemonBase pkmn, int recoil)
        {
            //Debug.Log("recoil coroutine");
            yield return StartCoroutine(changeHealthbar(pkmn, -recoil));

            yield return null;
        }

        //..Text Effects

        /// <summary>
        /// Shows how effective the attack was against the pokemon, only shows immune, not very effective and super effective moves
        /// </summary>
        /// <param name="atkName">name of the attack</param>
        /// <param name="target">target pokemon</param>
        /// <returns></returns>
        public IEnumerator effectiveText(string atkName, PokemonBase target)
        {
            attacks attack = DexHolder.attackDex.getAttack(atkName);
            //DamageMultipliers.getEffectiveness(targetPokemon.damageMultiplier, attackType);
            float multiplier = DamageMultipliers.getEffectiveness(target.damageMultiplier, attack.type);
            Debug.Log(string.Format("Text multiplier {0}", multiplier));
            if (multiplier != 1)
            {
                string text = "";
                if (multiplier == 0)
                {
                    text = string.Format("{0} is immune!", target.Name);
                }
                else if (multiplier < 1)
                {
                    text = "It was not very effective.";
                }
                else
                {
                    text = "It was super effective!";
                }
                yield return StartCoroutine(displayText(text, 2f));
            }
            yield return null;
        }

        /// <summary>
        /// Text that is shown when the target pokemon gets a new on volitle status effect
        /// </summary>
        /// <param name="target">target pokemon</param>
        /// <param name="status">name of the non volitle status</param>
        /// <returns></returns>
        public IEnumerator addNV(string target, nonVolitileStatusEffects status)
        {
            string ending = status.ToString();
            if (status == nonVolitileStatusEffects.sleep)
            {
                ending = "asleep";
            }
            if (status == nonVolitileStatusEffects.toxic)
            {
                ending = "badly poisoned";
            }

            string text = string.Format("{0} is now {1}!", target, ending);
            yield return StartCoroutine(displayText(text, 2f));

            if (target == sim.redTeam.curPokemon.Name)
            {
                sim.redTeam.curPokemon.status_A = status;
                sim.redTeam.guiRef.updateStatus_A(sim.redTeam.curPokemon);
            }
            else
            {
                sim.blueTeam.curPokemon.status_A = status;
                sim.blueTeam.guiRef.updateStatus_A(sim.blueTeam.curPokemon);
            }

            yield return null;
        }

        /// <summary>
        /// Handles the text that is shown when a pokemon cannot move due to their non volitle status effect
        /// </summary>
        /// <param name="pkmn">pokemon being affected</param>
        /// <returns></returns>
        public IEnumerator blockingNV(PokemonBase pkmn)
        {
            nonVolitileStatusEffects effect = pkmn.status_A;
            string ending = effect.ToString();
            if (effect == nonVolitileStatusEffects.sleep)
            {
                ending = "asleep";
            }
            string text = string.Format("{0} tried to move but is {1}", pkmn.Name, ending);
            yield return StartCoroutine(displayText(text, 1f));

            yield return null;
        }

        /// <summary>
        /// Handles the critcal hit text
        /// </summary>
        /// <returns></returns>
        public IEnumerator criticalHit()
        {
            string text = "Ciritcal Hit!";
            yield return StartCoroutine(displayText(text, 1f));
            yield return null;
        }

        /// <summary>
        /// handles the flinching text
        /// </summary>
        /// <param name="pkmn">pokemon that flinched</param>
        /// <returns></returns>
        public IEnumerator flinched(PokemonBase pkmn)
        {
            string text = string.Format("{0} flinched!", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator statusAffected(MoveResults move)
        {
            string text;
            if (move.dmgReport.stagePokemon == "all")
            {
                text = string.Format("{0}", move.dmgReport.stageName);
            }
            else
            {
                string delta = stageChange(move.dmgReport.stageDelta);
                text = string.Format("{0}'s {1} {2}", move.dmgReport.stagePokemon, move.dmgReport.stageName.ToUpper(), delta);
            }
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator failedMove()
        {
            string text = "It Failed!";
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator missed(PokemonBase pkmn)
        {
            string text = string.Format("{0} missed!", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator substituteHit(PokemonBase pkmn)
        {
            string text = string.Format("{0}'s substitute was damaged.", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator substituteFaded(PokemonBase pkmn)
        {
            pkmn.hasSubstitute = false;
            pkmn.substituteHealth = 0;
            string text = string.Format("{0}'s substitute faded!", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        //..Pokemon Effects

        public IEnumerator throwPokeball()
        {
            yield return null;
        }

        public IEnumerator pokemonOut(PokemonBase pkmn)
        {
            yield return StartCoroutine(throwPokeball());

            string text = string.Format("Go {0}!", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));

            yield return null;
        }

        public IEnumerator pokemonReturn(PokemonBase pkmn)
        {
            string text = string.Format("{0} return.", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return null;
        }

        public IEnumerator pokemonFainted()
        {
            yield return null;
        }

        /// <summary>
        /// Displaying the fainted text
        /// </summary>
        /// <param name="pkmn"></param>
        /// <returns></returns>
        public IEnumerator faintedText(PokemonBase pkmn)
        {
            string text = string.Format("{0} fainted!", pkmn.Name);
            yield return StartCoroutine(displayText(text, 2f));
            yield return StartCoroutine(pokemonFainted());
            yield return StartCoroutine(swapPokemon(pkmn, 0, true));
            yield return null;
        }

        //..Swaping Pokemon

        public IEnumerator swapPokemon(PokemonBase cur, int index, bool forced)
        {
            if (!cur.team.canSwitch())
            {
                yield break;
            }

            if (cur.team.type == TeamPokemon.TeamType.Player)
            {
                //open up the swap panel and wait for an input to be made
                if (forced)
                {
                    Debug.Log("Forcing player switch");
                    sim.battleGUI.toggleSwapPanel(true);
                    yield return StartCoroutine(waitForSwapInput());
                    index = swapIndex;
                }
                yield return StartCoroutine(pokemonReturn(cur));
                cur.team.swap(index);
                yield return StartCoroutine(pokemonOut(cur.team.pokemon[index]));
            }
            else
            {
                yield return StartCoroutine(AISwapPokemon(cur.team));
            }
        }

        public IEnumerator AISwapPokemon(TeamPokemon team)
        {
            int index = team.getRndPokemon();
            yield return StartCoroutine(pokemonReturn(team.curPokemon));
            team.swap(index);
            yield return StartCoroutine(pokemonOut(team.curPokemon));

            yield return null;
        }

        public IEnumerator waitForSwapInput()
        {
            Debug.Log("Waiting for user Input");
            while (!swapInput)
            {
                //Debug.Log("Waiting for user input");
                yield return null;
            }
            swapInput = false;
            yield return null;
        }

        //Effects

        public IEnumerator victory(string text)
        {
            yield return StartCoroutine(displayText(text, 3f));
            sim.battleGUI.toggleEndPanel(true);
            yield return null;
        }

        /// <summary>
        /// handler for all of the text to be displayed
        /// </summary>
        /// <param name="text">text to be displayed</param>
        /// <param name="wait">the delay before moving forward</param>
        /// <returns></returns>
        public IEnumerator displayText(string text, float wait)
        {
            sim.battleGUI.toggleTextPanel(true);
            float delay = .025f;
            string curText = "";
            sim.battleGUI.getDisplayText().text = curText;
            int index = 0;
            while (curText != text)
            {
                yield return new WaitForSeconds(delay);
                index++;
                if (index > text.Length)
                {
                    break;
                }
                curText = text.Substring(0, index);
                sim.battleGUI.getDisplayText().text = curText;
                yield return null;
            }
            //adding a wait at the end of this just because
            yield return new WaitForSeconds(wait);
            yield return null;
        }

        /// <summary>
        /// Handles the health bar's movements
        /// </summary>
        /// <param name="pkmn">pokemon whose is being affected</param>
        /// <param name="delta">the delta change in the pokemon's health</param>
        /// <returns></returns>
        public IEnumerator changeHealthbar(PokemonBase pkmn, int delta)
        {
            delta = checkDelta(pkmn, delta);
            Slider slider = pkmn.team.guiRef.slider;
            Text healthNum = pkmn.team.guiRef.health;
            float speed = .0125f;
            float dmgNormalized = ((pkmn.curHp + (float)delta) / pkmn.maxHP);
            //Debug.Log(string.Format("curhp: {0} delta: {1} formula: {2}/{3} res: {4}", pkmn.curHp, delta, pkmn.curHp + delta, pkmn.maxHP, dmgNormalized));

            dmgNormalized = (float)Math.Round((decimal)dmgNormalized, 3);
            float roundedValue = (float)Math.Round((decimal)slider.value, 3);
            Debug.Log(string.Format("slider targets, normalized: {0} rounded: {1}", dmgNormalized, roundedValue));
            float totalDiff = 0;

            while (roundedValue != dmgNormalized)
            {
                float diff = Mathf.Lerp(slider.value, dmgNormalized, speed);
                slider.value = diff;
                totalDiff += diff;
                roundedValue = (float)Math.Round((decimal)slider.value, 4);

                if (healthNum != null)
                {
                    int curHPValue = (int)(pkmn.maxHP * slider.value);
                    healthNum.text = string.Format("{0}/{1}", curHPValue, pkmn.maxHP);
                    if (curHPValue == (pkmn.curHp + delta)) { break; }
                }
                yield return null;
            }

            pkmn.curHp += delta;

            if (healthNum != null)
            {
                healthNum.text = string.Format("{0}/{1}", pkmn.curHp, pkmn.maxHP);
            }
            yield return null;
        }

        /// <summary>
        /// checks the delta in the pokemon's health and ensures the Hp doenst have more than their max hp or less than 0
        /// </summary>
        /// <param name="pkmn">pokemon that is being affected</param>
        /// <param name="delta">change in their health</param>
        /// <returns></returns>
        private int checkDelta(PokemonBase pkmn, int delta)
        {
            if ((pkmn.curHp + delta) <= 0)
            {
                delta = -pkmn.curHp;
            }
            if ((pkmn.curHp + delta) >= pkmn.maxHP)
            {
                delta = pkmn.maxHP - pkmn.curHp;
            }
            return delta;
        }

        /// <summary>
        /// gets the string version of the increase or decrease in a stat.
        /// </summary>
        /// <param name="delta">the stat change</param>
        /// <returns>a string describing the change</returns>
        private string stageChange(int delta)
        {
            string text;

            if (delta <= -2)
            {
                text = "decreased greatly!";
            }
            else if (delta == -1)
            {
                text = "decreased.";
            }
            else if (delta == 0)
            {
                text = "";
                Debug.Log("Error found a 0 delta");
            }
            else if (delta == 1)
            {
                text = "increased.";
            }
            else
            {
                text = "increased greatly!";
            }
            return text;
        }

        public void OnSwapInput(int index)
        {
            swapInput = true;
            swapIndex = index;
        }
    }
}