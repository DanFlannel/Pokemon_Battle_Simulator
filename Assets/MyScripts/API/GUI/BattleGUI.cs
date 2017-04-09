using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FBG.Base;
using FBG.Attack;

namespace FBG.Battle
{
    public class BattleGUI : MonoBehaviour
    {
        public Button[] atkBtns;
        public Button[] swapBtns;
        public GameObject swapPanel;
        private BattleSimulator sim;
        public int swapIndex;

        public void setSimulator(ref BattleSimulator sim)
        {
            this.sim = sim;
            toggleSwapPanel(false);
        }

        public void checkButtonNames(PokemonBase pkmon)
        {
            for (int i = 0; i < atkBtns.Length; i++)
            {
                if (pkmon.atkMoves[i] != atkBtns[i].GetComponentInChildren<Text>().text)
                {
                    setButtonNames(pkmon.atkMoves);
                }
            }
        }

        public void updateSwapPanel()
        {
            for (int i = 0; i < swapBtns.Length; i++)
            {
                if (swapBtns[i].GetComponentInChildren<Text>().text != sim.redTeam.pokemon[i].Name)
                {
                    swapBtns[i].GetComponentInChildren<Text>().text = sim.redTeam.pokemon[i].Name;
                }
            }
        }

        public void setButtonNames(List<string> atkNames)
        {
            //Debug.Log(atkNames.Count);
            for (int i = 0; i < atkNames.Count; i++)
            {
                Text t = atkBtns[i].GetComponentInChildren<Text>();
                t.text = atkNames[i];
            }
        }

        public void attakButton(int index)
        {
            if (!sim.redTeamAttack(index))
            {
                return;
            }
            sim.blueTeamAttack();
        }

        public void selectSwapPokemon(int index)
        {
            if (sim.redTeam.pokemon[index].curHp <= 0)
            {
                Debug.Log("Selecting dead pokemon");
                return;
            }
            swapIndex = index;
        }

        public void swapButton()
        {
            toggleSwapPanel();
            if (swapIndex != sim.redTeam.curIndex)
            {
                sim.swapPokemon(sim.redTeam, swapIndex);
            }
        }

        public void promptSwap(ref TeamPokemon team)
        {
            if(team == sim.blueTeam)
            {
                
                int index = team.getRndPokemon();
                if(index == -1)
                {
                    Debug.Log("All enemies dead");
                    return;
                }
                Debug.Log(string.Format("enemy pokemon force swapped: {0} ", index));
                team.swap(index);
                sim.swapPokemon(team, index);
                sim.changeSprites(ref team);
                return;
            }
            Debug.Log("Prompting swap panel");
            toggleSwapPanel();
        }

        public void toggleSwapPanel()
        {
            swapPanel.SetActive(!swapPanel.activeInHierarchy);
            updateSwapPanel();
        }

        private void toggleSwapPanel(bool b)
        {
            swapPanel.SetActive(b);
            updateSwapPanel();
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