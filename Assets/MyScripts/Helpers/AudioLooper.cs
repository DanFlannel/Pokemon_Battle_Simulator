using UnityEngine;
using System.Collections;

public class AudioLooper : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource aSource;

    public int[] levels;

    public float offset;
    public float loopStart;
    public float loopEnd;
    public float curValue;

    private Coroutine routine;

    // Use this for initialization
    void OnEnable()
    {
        aSource = this.GetComponent<AudioSource>();
        startAudio();
    }

    private void OnDisable()
    {
        stopAudio();
        aSource.clip = null;
    }

    void Update()
    {
        checkLoop();
    }

    private float Normalized(float n)
    {
        float normalized;

        normalized = n / clip.length;

        return normalized;
    }

    public void startAudio()
    {
        aSource.clip = clip;
        aSource.Play();
        aSource.time = offset;
        //Debug.Log(string.Format("Audio starting at: {0}s", offset));
    }

    public void checkLoop()
    {
        curValue = aSource.time;
        if(aSource.time > loopEnd)
        {
            aSource.time = loopStart;
        }
    }

    public void stopAudio()
    {
        aSource.Stop();
    }
}
