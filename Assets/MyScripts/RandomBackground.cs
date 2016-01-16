using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RandomBackground : MonoBehaviour {

    public Texture2D[] backgrounds;
    private RawImage raw;
    private int rnd;

	// Use this for initialization
	void Start () {
        raw = this.GetComponent<RawImage>();
        rnd = Random.Range(0, backgrounds.Length);
        raw.texture = backgrounds[rnd];
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
