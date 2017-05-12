using CoroutineQueueHelper;
using FBG.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FBG.Battle
{
    public class BattleRoutines : MonoBehaviour
    {

        public CoroutineList queue;
        private GameObject reference;

        private void Start()
        {
            reference = new GameObject();
            reference.name = "CoroutineHelper";
            queue = reference.AddComponent<CoroutineList>();
        }

        public IEnumerator takeTurn(BattleSimulator sim)
        {
            sim.isTurnRunning = true;
            ClearLog.ClearLogConsole();

            sim.blueTeamAttack();

            List<TurnInformation> info = new List<TurnInformation>();
            info.Add(new TurnInformation(sim.redTeam.curPokemon, sim.redMoveIndex, sim.isRedSwapping));
            info.Add(new TurnInformation(sim.blueTeam.curPokemon, sim.blueMoveIndex, sim.isBlueSwapping));
            TurnOrder turn = new TurnOrder(info);

            for (int i = 0; i < turn.order.Count; i++)
            {
                yield return StartCoroutine(pokemonMove(turn, i));
            }

            if (turn.swapped)
            {
                info = turn.speedOnly(sim.redTeam.curPokemon, sim.blueTeam.curPokemon);
            }

            for (int i = 0; i < turn.speedDetermined.Count; i++)
            {
                yield return StartCoroutine(EndOfTurnPokemon(turn, i));
            }

            yield return StartCoroutine(EndOfTurnTeam(sim.redTeam));
            yield return StartCoroutine(EndOfTurnTeam(sim.blueTeam));

            for (int i = 0; i < turn.order.Count; i++)
            {
                yield return StartCoroutine(checkPokemon(turn, i));
            }
            sim.isTurnRunning = false;
        }

        #region IEnumerators for the turn

        private IEnumerator pokemonMove(TurnOrder turn, int i)
        {
            PokemonBase pkmn = turn.order[i].pokemon;
            pkmn.team.takeTurn(turn.order[i].moveIndex, turn.order[i].isSwapping, pkmn);

            //queue.AddCoroutineToQueue(testWait(1f));
            //queue.AddCoroutineToQueue(testWait(1f));
            //queue.AddCoroutineToQueue(testWait(1f));
            //yield return new WaitUntil(() => queue.isQueueRunning() == false);
            yield return StartCoroutine(queue.masterIEnumerator());
            
        }

        public Coroutine waiting(float sec)
        {
            return StartCoroutine(testWait(sec));
        }

        private IEnumerator EndOfTurnPokemon(TurnOrder turn, int i)
        {
            PokemonBase pkmn = turn.speedDetermined[i].pokemon;

            pkmn.team.checkEffectors(pkmn);
            pkmn.team.endOfTurnDamage(pkmn);
            yield return null;
        }

        private IEnumerator EndOfTurnTeam(TeamPokemon team)
        {
            team.EndOfTurn();
            yield return null;
        }

        private IEnumerator checkPokemon(TurnOrder turn, int i)
        {
            PokemonBase pkmn = turn.order[i].pokemon;
            pkmn.team.isAlive(pkmn);
            yield return null;
        }

        private IEnumerator resetTurn(BattleSimulator sim)
        {
            sim.resetTurn();
            yield return null;
        }

        #endregion

        public IEnumerator testWait(float sec)
        {
            yield return new WaitForSeconds(sec);
            Debug.Log("waited " + sec + " seconds");
            yield return null;
        }

        //pokemon basic turn moves

        public IEnumerator usedMoveText(string pkName, string move)
        {
            string text = string.Format("{0} used {1}", pkName, move);
            Debug.Log("move text coroutine: " + text);
            yield return null;
        }

        public IEnumerator applyDamage(PokemonBase pkmn, int dmg)
        {
            Debug.Log("damage coroutine");
            yield return StartCoroutine(changeHealthbar(pkmn, -dmg));
            yield return null;  
        }

        public IEnumerator applyHeal(PokemonBase pkmn, int heal)
        {
            Debug.Log("healing coroutine");
            yield return StartCoroutine(changeHealthbar(pkmn, heal));
            yield return null;
        }

        public IEnumerator applyRecoil(PokemonBase pkmn, int recoil)
        {
            Debug.Log("recoil coroutine");
            yield return StartCoroutine(changeHealthbar(pkmn, -recoil));
            yield return null;
        }

        public IEnumerator effectiveText()
        {
            yield return null;
        }

        //swapping or chaning pokemon

        public IEnumerator throwPokeball()
        {
            yield return null;
        }

        public IEnumerator pokemonOut()
        {
            yield return null;
        }

        public IEnumerator pokemonReturn()
        {
            yield return null;
        }

        public IEnumerator pokemonFainted()
        {
            yield return null;
        }

        public IEnumerator swapPokemon()
        {
            yield return null;
        }

        public IEnumerator faintedText()
        {
            yield return null;
        }

        //Effects

        public IEnumerator victory()
        {
            yield return null;
        }

        public IEnumerator displayText(string text)
        {
            yield return null;
        }

        public IEnumerator changeHealthbar(PokemonBase pkmn, int delta)
        {
            Slider slider = pkmn.team.GUIHolder.GetComponentInChildren<Slider>();
            Text healthNum = pkmn.team.GUIHolder.transform.FindChild("HealthValue").GetComponent<Text>();
            float speed = .01f;
            float dmgNormalized = ((pkmn.curHp + (float)delta) / pkmn.maxHP);
            //Debug.Log(string.Format("curhp: {0} delta: {1} formula: {2}/{3} res: {4}", pkmn.curHp, delta, pkmn.curHp + delta, pkmn.maxHP, dmgNormalized));

            dmgNormalized = (float)Math.Round((decimal)dmgNormalized, 3);
            float roundedValue = (float)Math.Round((decimal)slider.value, 3);
            Debug.Log(string.Format("slider targets, normalized: {0} rounded: {1}", dmgNormalized, roundedValue));
            float totalDiff = 0;

            while(roundedValue != dmgNormalized)
            {
                float diff = Mathf.Lerp(slider.value, dmgNormalized, speed);
                slider.value = diff;
                totalDiff += diff;
                roundedValue = (float)Math.Round((decimal)slider.value, 4);

                if (healthNum != null)
                {
                    int curHPValue = (int)(pkmn.maxHP * slider.value);
                    healthNum.text = string.Format("{0}/{1}", curHPValue, pkmn.maxHP);
                    if(curHPValue == (pkmn.curHp + delta)) { break; }
                }
                yield return null;
            }

            pkmn.curHp += delta;

            if(healthNum != null)
            {
                healthNum.text = string.Format("{0}/{1}", pkmn.curHp, pkmn.maxHP);
            }
            
            yield return null;
        }

    }

}
