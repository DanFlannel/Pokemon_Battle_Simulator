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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void checkButtonNames(List<PokemonBase> list, int index)
        {
            for (int i = 0; i < btns.Length; i++)
            {
                if (list[index].atkMoves[i] != btns[i].GetComponentInChildren<Text>().text)
                {
                    setButtonNames(list[index].atkMoves);
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
    }
}