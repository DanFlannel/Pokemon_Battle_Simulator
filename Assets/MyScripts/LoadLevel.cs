using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class LoadLevel : MonoBehaviour {


    public int levelName;
    public Image loadingbar;
    public float progress;
    AsyncOperation async;


	// Use this for initialization
	void Start () {
        //startLoading();
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
        async = SceneManager.LoadSceneAsync(levelName);
        progress = async.progress;
        //Debug.Log(progress);
        loadingbar.fillAmount = progress;
        async.allowSceneActivation = false;
        yield return (0);
       
    }

    public void loadDesignatedLevel(int n)
    {
        Debug.Log("PK: Loading Designated Level" + n);
        Console.WriteLine("PK: Loading Designeted Level" + n);
        SceneManager.LoadScene(n);
    }
}
