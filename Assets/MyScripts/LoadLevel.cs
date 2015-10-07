using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadLevel : MonoBehaviour {


    public string levelName;
    public Image map;
    public float progress;
    AsyncOperation async;


	// Use this for initialization
	void Start () {
        startLoading();
    }

    private void startLoading()
    {
        StartCoroutine(loadLevel());
    }

    public void loadBattle()
    {
        async.allowSceneActivation = true;
    }

    IEnumerator loadLevel()
    {
        async = Application.LoadLevelAsync(levelName);
        progress = async.progress;
        Debug.Log(progress);
        map.fillAmount = progress;
        async.allowSceneActivation = false;
        yield return (0);
       
    }

    public void loadLevelInput(string name)
    {
        Application.LoadLevel(name);
    }
}
