using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicController : MonoBehaviour
{
    public static float volume = 0.5f;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = volume;
    }

    private void Update()
    {
        audioSource.volume = volume;
    }

    public void startPlaying()
    {
        audioSource.Play();
    }

    public void stopPlaying()
    {
        audioSource.Stop();
    }

    public void pausePlaying()
    {
        audioSource.Pause();
    }

    public void ubPausePlaying()
    {
        audioSource.UnPause();
    }
}
