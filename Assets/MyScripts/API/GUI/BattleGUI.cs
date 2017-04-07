using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FBG.Base;

namespace FBG.Battle
{
    public class BattleGUI : MonoBehaviour
    {
        public Button[] btns;
        private BattleSimulator sim;

        public void setSimulator(ref BattleSimulator sim)
        {
            this.sim = sim;
        }

        public void checkButtonNames(PokemonBase pkmon)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (pkmon.atkMoves[i] != btns[i].GetComponentInChildren<Text>().text)
                {
                    setButtonNames(pkmon.atkMoves);
                }
            }
        }

        public void setButtonNames(List<string> atkNames)
        {
            //Debug.Log(atkNames.Count);
            for (int i = 0; i < atkNames.Count; i++)
            {
                Text t = btns[i].GetComponentInChildren<Text>();
                t.text = atkNames[i];
            }
        }

        public void attakButton(int index)
        {
            sim.redTeamAttack(index);
            sim.blueTeamAttack();
        }

        public void swapButton(int index)
        {
            TeamPokemon team = sim.redTeam;
            sim.swapPokemon(team, index);
        }

        public void promptSwap()
        {

        }

        public void swapPanel()
        {

        }

        public void showBattleText(MoveResults MR)
        {

        }

        public void showBattleText(string s)
        {

        }

        public void changePokemonSprite(GifRenderer r, string name, int id)
        {
            r.ChangeSprite(name, id);
        }
    }
}