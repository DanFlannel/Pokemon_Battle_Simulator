using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AboutScene : MonoBehaviour {

    public Text applicationPath;
    public Text compare;

	// Use this for initialization
	void Start () {
        applicationPath.text = Application.dataPath;
        compare.text = Application.dataPath + "/Resources/Sprites/Front/ NAME .gif";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
