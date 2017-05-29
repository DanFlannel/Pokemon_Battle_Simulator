using UnityEngine;
using System.Collections;

public class AudioLooper : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource aSource;

    public int[] levels;

    public bool loopedOnce;
    public bool finished;

    public float loopStart;
    public float loopEnd;

    // Use this for initialization
    void Start()
    {
        aSource = this.GetComponent<AudioSource>();
        StartRoutine();
    }

    void Update()
    {
        if (finished)
        {
            StartRoutine();
        }
    }

    public void StartRoutine()
    {
        finished = false;
        StartCoroutine(loopThis(loopStart, loopEnd));
    }

    public float Normalized(float n)
    {
        float normalized;

        normalized = n / clip.length;

        return normalized;
    }

    IEnumerator loopThis(float TimeFrom, float TimeTo)
    {
        aSource.clip = clip;
        if (loopedOnce)
        {
            aSource.time = loopStart;
        }
        else
        {
            aSource.time = 0;
            TimeFrom = 0;
        }

        loopedOnce = true;
        if (!aSource.isPlaying)
        {
            aSource.Play();
            yield return new WaitForSeconds(TimeTo - TimeFrom);
            Debug.Log("Stopped the clip!");
            aSource.Stop();
            finished = true;
        }

        
        yield return null;
    }
}
