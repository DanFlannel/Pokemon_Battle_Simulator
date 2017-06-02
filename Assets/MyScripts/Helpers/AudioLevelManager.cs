using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioLevelManager : MonoBehaviour {

    public GameObject[] gos;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        checkAudio(scene.buildIndex);
        //Debug.Log("Level Loaded");
        //Debug.Log(scene.name);
        //Debug.Log(mode);
    }

    private void checkAudio(int levelID)
    {
        for(int i = 0; i < gos.Length; i++)
        {
            AudioLooper al = gos[i].GetComponent<AudioLooper>();
            bool enable = false;
            for(int n = 0; n < al.levels.Length; n++)
            {
                if(levelID == al.levels[n])
                {
                    enable = true;
                }
            }
            al.enabled = enable;
        }
    }
}
