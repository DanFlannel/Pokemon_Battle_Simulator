using UnityEngine;
using System.Collections;
using FlipWebApps.BeautifulTransitions.Scripts.Transitions;
using FlipWebApps.BeautifulTransitions.Scripts.Transitions.Screen;
using UnityEngine.SceneManagement;

public class SceneTransitionAfterSeconds : MonoBehaviour {

    public float TransitionAfter;
    public string sceneName;

	// Update is called once per frame
	void Update () {
        TransitionAfter -= Time.deltaTime;
        if(TransitionAfter <= 0)
        {
            Transition();
        }
	}

    private void Transition()
    {  
        this.GetComponent<TransitionManager>().TransitionOutAndLoadScene(sceneName);
    }
}
