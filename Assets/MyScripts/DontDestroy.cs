using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

    private static DontDestroy instance = null;

    // Game Instance Singleton
    public static DontDestroy Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
	
	}

    void Start()
    {

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
