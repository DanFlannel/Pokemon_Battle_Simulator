using UnityEngine;

/// <summary>
/// This class is to create singletons of any gameobject it is attatched to
/// </summary>
public class DontDestroy : MonoBehaviour
{
    private static DontDestroy instance = null;

    /// <summary>
    /// Game instance singleton
    /// </summary>
    public static DontDestroy Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        //Destorys any copt object that was created when the scene loaded
        DontDestroyOnLoad(this);
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        instance = this;
    }
}