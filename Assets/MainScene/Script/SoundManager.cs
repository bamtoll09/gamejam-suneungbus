using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public AudioClip bgm,
                     button,
                     fireBgm,
                     waterBgm,
                     foodBgm;

    public AudioSource audioSource;

    public static SoundManager GetInstance() { return instance; }

    void Awake()
    {
        if (!instance)
        {
            instance = (SoundManager)FindObjectOfType(typeof(SoundManager));
            if (!instance)
                Debug.LogError("There needs to be one active SoundManager script on a SoundManager in your scene.");
        }

        DontDestroyOnLoad(instance);
    }

    void Update()
    {
        //if (ToggleSound.instance)
        //    audioSource.volume = 0.0f;
        //else
        //    audioSource.volume = 1.0f;
    }

    public void PlayBGM()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = bgm;
        audioSource.loop = false;
        audioSource.Play();
    }

    public void PlayButton()
    {
        audioSource.PlayOneShot(button);
    }

    public void PlayFireBgm()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = fireBgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayWaterBgm()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = waterBgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFoodBgm()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = foodBgm;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void Pause()
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public void UnPause()
    {
        if (!audioSource.isPlaying)
            audioSource.UnPause();
    }

    public void SetPitch(float val)
    {
        audioSource.pitch = val;
    }
}
