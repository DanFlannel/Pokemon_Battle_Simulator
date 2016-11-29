using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class FBG_BattleGUI : MonoBehaviour {

    public Button[] btns;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setButtonNames(List<string> atkNames)
    {
        //Debug.Log(atkNames.Count);
        for(int i = 0; i < atkNames.Count; i++)
        {
            Text t = btns[i].GetComponentInChildren<Text>();
            t.text = atkNames[i];
        }
    }
}
