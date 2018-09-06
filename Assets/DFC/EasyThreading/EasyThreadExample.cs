using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DFC;

public class EasyThreadExample : MonoBehaviour {

    public int index = 0;

	// Use this for initialization
	void Start () {

        Action a = new Action(() =>
        {
            EasyThread.ExecuteOnMainThread(new Action(() => { CreateTexture(4,5); }));
            Loop(1000);
            Loop(10000);
            EasyThread.ExecuteOnMainThread(CreateGameObject);
        });

        EasyThread.CreateSingleThread(a, "single");
        EasyThread.CreateLoopingThread(a, "loop", 4, 100);

        //EasyThread.StopAllThreads();
        EasyThread.CreateContinuousThread(Count, "Counter", 100, index);
	}

    private void CreateTexture(int w, int h)
    {
        Texture2D tex = new Texture2D(w, h);
        Debug.Log(string.Format("created tex: {0} {1}", tex.width, tex.height));
    }

    private void CreateGameObject()
    {
        GameObject go = new GameObject("Created In Thread Method!");
    }

    private void Loop(int num)
    {
        int total = 0;
        for(int i = 0; i < num; i++)
        {
            total++;
        }
        Debug.Log(string.Format("hi {0}", total));
    }

    private void Count()
    {
        for (int i = 0; i < 1000000; i++)
        {
            index++;
        }
    }
}
