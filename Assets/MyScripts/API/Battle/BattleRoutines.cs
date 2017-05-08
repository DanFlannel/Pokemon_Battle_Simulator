using CoroutineQueueHelper;
using FBG.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        public IEnumerator applyDamage()
        {
            Debug.Log("damage coroutine");
            yield return null;  
        }

        public IEnumerator applyHeal()
        {
            Debug.Log("healing coroutine");
            yield return null;
        }

        public IEnumerator applyRecoil()
        {
            Debug.Log("recoil coroutine");
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

        //other

        public IEnumerator victory()
        {
            yield return null;
        }

        public IEnumerator displayText(string text)
        {
            yield return null;
        }

    }

}
