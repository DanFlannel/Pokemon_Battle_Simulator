using System;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class handles how the levels are loaded. Trying out asynchonization and other methods
/// </summary>
public class LoadLevel : MonoBehaviour
{
    /// <summary>
    /// Loads the scene based on the scene number associated it within the build heiarchy
    /// This class is a bit riskier to use because of the fact that level numbers might change
    /// </summary>
    /// <param name="n">Scene number in the build heiarchy</param>
    public void loadDesignatedLevelInt(int n)
    {
        Debug.Log("Loading Designated Level" + n);
        Console.WriteLine("PK: Loading Designeted Level" + n);
        SceneManager.LoadScene(n);
    }

    /// <summary>
    /// This loads levels based off of their names in the projects
    /// A much more stable method as names wont change with scenes
    /// Only new scenes added
    /// </summary>
    /// <param name="s">Name of the scene to load</param>
    public void loadDesignatedLevelString(string s)
    {
        Debug.Log("Loading Designated Level" + s);
        Console.WriteLine("PK: Loading Designeted Level" + s);
        SceneManager.LoadScene(s);
    }
}