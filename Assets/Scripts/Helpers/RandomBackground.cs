using System.Collections;

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A simple script to generate a random background when the scene loads.
/// This has to be attatched to the raw image for the random background image to load.
/// </summary>
public class RandomBackground : MonoBehaviour
{
    public Sprite[] backgrounds;
    public Image img;
    private int rnd;

    // Use this for initialization
    private void Start()
    {
        setRndBackground();
    }

    private void setRndBackground()
    {
        rnd = UnityEngine.Random.Range(0, backgrounds.Length);
        img.sprite = backgrounds[rnd];
    }
}