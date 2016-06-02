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

        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        instance = this;
    }

    void Start()
    {


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
