using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// A simple script to generate a random background when the scene loads.
/// This has to be attatched to the raw image for the random background image to load.
/// </summary>
public class RandomBackground : MonoBehaviour {

    public Texture2D[] backgrounds;
    private RawImage raw;
    private int rnd;

	// Use this for initialization
	void Start () {
        setRndBackground();
	}

    private void setRndBackground()
    {
        raw = this.GetComponent<RawImage>();
        rnd = UnityEngine.Random.Range(0, backgrounds.Length);
        raw.texture = backgrounds[rnd];
    }
}
