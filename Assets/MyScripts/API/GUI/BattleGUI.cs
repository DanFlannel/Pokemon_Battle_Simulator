using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FBG.Base;
using FBG.Attack;
using FBG.Data;
using FBG.JSON;

namespace FBG.Battle
{
    public class BattleGUI : MonoBehaviour
    {
        public Button[] atkBtns;

        [Header("Swap info")]
        public Button[] swapBtns;
        public GameObject swapPanel;
        
        [Header("Panels")]
        public GameObject movePanel;
        public GameObject endPanel;
        public GameObject overlay;
        public GameObject textPanel;

        [Header("Info")]
        public int moveIndex;
        public int swapIndex;

        private BattleSimulator sim;
        private swapInfoPanel swapInfo;
        private moveInfoPanel moveInfo;

        public void setSimulator(ref BattleSimulator sim)
        {
            this.sim = sim;
            toggleSwapPanel(false);
            toggleTextPanel(false);
            toggleEndPanel(false);
            overlay.SetActive(false);

            moveIndex = -1;

            swapInfo = new swapInfoPanel(swapPanel.transform.Find("Info_Panel").gameObject);
            swapInfo.update(sim.redTeam.pokemon[0]);

            moveInfo = new moveInfoPanel(movePanel);
            moveInfo.reset();
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
                if (i < sim.teamSize)
                {
                    if (swapBtns[i].GetComponentInChildren<Text>().text != sim.redTeam.pokemon[i].Name)
                    {
                        PokemonBase pkmn = sim.redTeam.pokemon[i];
                        Transform t = swapBtns[i].transform;
                        swapButton swapBtn = new Battle.swapButton(t.Find("Info"));
                        swapBtn.update(pkmn);
                    }
                }
                else
                {
                    swapBtns[i].gameObject.SetActive(false);
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
            if (sim.isTurnRunning)
            {
                return;
            }

            moveInfo.update(sim.redTeam.curPokemon, index);
            if(moveIndex != index)
            {
                moveIndex = index;
                return;
            }

            if (!checkPP(index)) { return; }
            sim.redTeamAttack(index);
        }

        private bool checkPP(int index)
        {
            if(sim.redTeam.curPokemon.curPP[index] > 0)
            {
                return true;
            }
            return false;
        }

        public void selectSwapPokemon(int index)
        {
            swapInfo.update(sim.redTeam.pokemon[index]);
            if (sim.redTeam.pokemon[index].curHp <= 0)
            {
                Debug.Log("Selecting dead pokemon");
                return;
            }
            swapIndex = index;
        }

        public void swapButton()
        {
            if(swapIndex == sim.redTeam.curIndex)
            {
                toggleSwapPanel();
                return;
            }
            if (swapIndex != sim.redTeam.curIndex && sim.redTeam.pokemon[swapIndex].curHp > 0 && !sim.redTeam.isBound)
            {
                sim.swapPokemon(sim.redTeam, swapIndex);
                toggleSwapPanel();
            }
        }

        public void promptSwap(ref TeamPokemon team, bool isForced)
        {
            Debug.Log("Called prompt swap");
            if(team == sim.blueTeam)
            {
                int index = team.getRndPokemon();
                if(index == -1)
                {
                    //Debug.Log("All enemies dead");
                    return;
                }
                Debug.Log(string.Format("prompting enemy swap {0} ", index));
                team.swap(index);
                sim.swapPokemon(team, index);
                sim.updateGUI(ref team);
                return;
            }
            Debug.Log("Prompting player swap");
            if (!swapPanel.activeInHierarchy)
            {
                toggleSwapPanel();
            }
        }

        public void toggleSwapPanel()
        {
            //Debug.Log("Toggle swap panel");
            swapPanel.SetActive(!swapPanel.activeInHierarchy);
            updateSwapPanel();
        }

        public void toggleSwapPanel(bool b)
        {
            swapPanel.SetActive(b);
            updateSwapPanel();
        }

        public void toggleTextPanel()
        {
            textPanel.SetActive(!textPanel.activeInHierarchy);
        }

        public void toggleTextPanel(bool b)
        {
            textPanel.SetActive(b);
        }

        public void toggleEndPanel()
        {
            endPanel.SetActive(!endPanel.activeInHierarchy);
            overlay.SetActive(!overlay.activeInHierarchy);
        }

        public void toggleEndPanel(bool b)
        {
            endPanel.SetActive(b);
            overlay.SetActive(b);
        }

        public Text getDisplayText()
        {
            return textPanel.GetComponentInChildren<Text>();
        }

        public void changePokemon_GUI(GifRenderer r, PokemonBase pkmn, int id)
        {
            updatePokemonPanel(pkmn);
            r.ChangeSprite(pkmn.Name, id);
        }

        private void updatePokemonPanel(PokemonBase pkmn)
        {
            GUIReferences gui = pkmn.team.guiRef;
            gui.name.text = string.Format("{0}", pkmn.Name);
            gui.level.text = string.Format("LvL {0}", pkmn.Level);
            gui.health.text = string.Format("{0}/{1}", pkmn.curHp, pkmn.maxHP);

            float sliderValue = (float)pkmn.curHp / (float)pkmn.maxHP;
            gui.slider.value = sliderValue;
        }
    }

    public class swapButton
    {
        public Button btn;
        public RawImage icon;
        public Text name;
        public Text statusA;
        public Text hp;
        public Slider healthBar;
        public Text atk1;
        public Text atk2;
        public Text atk3;
        public Text atk4;
        

        public swapButton(Transform t)
        {
            btn = t.GetComponent<Button>();

            icon = t.Find("Icon").GetComponent<RawImage>();
            name = t.Find("Name").GetComponent<Text>();
            statusA = t.Find("StatusA").GetComponent<Text>();
            hp = t.Find("Health").GetComponent<Text>();
            healthBar = t.Find("Healthbar").GetComponent<Slider>();
            atk1 = t.Find("Attack1").GetComponent<Text>();
            atk2 = t.Find("Attack2").GetComponent<Text>();
            atk3 = t.Find("Attack3").GetComponent<Text>();
            atk4 = t.Find("Attack4").GetComponent<Text>();
        }

        public void update(PokemonBase pkmn)
        {
            if(pkmn.curHp <= 0)
            {
                btn.interactable = false;
            }
            float hpValue = (pkmn.curHp / pkmn.maxHP);

            name.text = pkmn.Name;
            string statusAText = pkmn.status_A.ToString();
            if (pkmn.status_A == nonVolitileStatusEffects.none)
            {
                statusAText = "";
            }
            statusA.text = statusAText;
            hp.text = string.Format("{0}/{1}", pkmn.curHp, pkmn.maxHP);
            healthBar.value = hpValue;

            atk1.text = pkmn.atkMoves[0];
            atk2.text = pkmn.atkMoves[1];
            atk3.text = pkmn.atkMoves[2];
            atk4.text = pkmn.atkMoves[3];

            string path = string.Format("Icons/{0}", pkmn.ID);
            object o = Resources.Load(path);
            Texture2D img = o as Texture2D;
            icon.texture = img;
            //icon.sprite = img;
        }
    }

    public class swapInfoPanel
    {
        public GameObject go;
        public Text name;
        public RawImage icon;
        public Text type1;
        public Text type2;
        public Text weightValue;
        public Text heightValue;

        public swapInfoPanel(GameObject go)
        {
            Transform t = go.transform;
            name = t.Find("Name").GetComponent<Text>();
            icon = t.Find("Icon").GetComponent<RawImage>();
            type1 = t.Find("Type1").GetComponent<Text>();
            type2 = t.Find("Type2").GetComponent<Text>();
            weightValue = t.Find("WeightValue").GetComponent<Text>();
            heightValue = t.Find("HeightValue").GetComponent<Text>();
        }

        public void update(PokemonBase pkmn)
        {
            name.text = pkmn.Name;

            string path = string.Format("Icons/{0}", pkmn.ID);
            object o = Resources.Load(path);
            Texture2D img = o as Texture2D;
            icon.texture = img;

            PokemonJsonData data = DexHolder.pokeDex.getData(pkmn.Name);
            type1.text = pkmn.type1;
            type2.text = pkmn.type2;

            heightValue.text = string.Format("{0} m", data.height);
            weightValue.text = string.Format("{0} kg", data.weight);
        }

    }

    public class moveInfoPanel
    {
        public Transform panel;
        public Text pp;
        public Text type;
        public Text cat;

        public moveInfoPanel(GameObject go)
        {
            this.panel = go.transform;
            pp = panel.Find("PP").GetComponent<Text>();
            type = panel.Find("Type").GetComponent<Text>();
            cat = panel.Find("Category").GetComponent<Text>();
        }

        public void update(PokemonBase pkmn, int index)
        {
            if(index == -1)
            {
                reset();
                return;
            }

            pp.text = string.Format("PP: {0}/{1}", pkmn.curPP[index], pkmn.maxPP[index]);
            attacks atk = DexHolder.attackDex.getAttack(pkmn.atkMoves[index]);
            type.text = atk.type;
            cat.text = atk.cat;
        }

        public void reset()
        {
            pp.text = "PP: ";
            type.text = "Type";
            cat.text = "Category";
        }
    }
}