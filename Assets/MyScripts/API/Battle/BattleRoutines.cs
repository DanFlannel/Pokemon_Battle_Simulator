using FBG.Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FBG.Battle
{
    public class BattleRoutines : MonoBehaviour
    {

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
                PokemonBase pkmn = turn.order[i].pokemon;
                yield return StartCoroutine(pokemonMove(turn, i));
            }

            if (turn.swapped)
            {
                info = turn.speedOnly(sim.redTeam.curPokemon, sim.blueTeam.curPokemon);
            }

            for (int i = 0; i < turn.speedDetermined.Count; i++)
            {
                PokemonBase pkmn = turn.speedDetermined[i].pokemon;
                yield return StartCoroutine(EndOfTurnPokemon(pkmn));
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

        {
            PokemonBase pkmn = turn.order[i].pokemon;
            pkmn.team.takeTurn(turn.order[i].moveIndex, turn.order[i].isSwapping, pkmn);
            yield return null;
        }

        private IEnumerator EndOfTurnPokemon(PokemonBase pkmn)
        {
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
            pkmn.team.checkPokemon(pkmn);
            yield return null;
        }

        private IEnumerator resetTurn(BattleSimulator sim)
        {
            sim.resetTurn();
            yield return null;
        }

        #endregion

        #region Effects



        #endregion

    }

}
