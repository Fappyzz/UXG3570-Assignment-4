using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    #region Collection of Sounds
    public Sound[] sfxSounds;
    public Sound[] musicSounds;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    #endregion


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("AmbienceTheme");
    }

    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds, s => s.name == name);

        if (sound == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound SFX = Array.Find(sfxSounds, s => s.name == name); 

        if (SFX == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
        {
            sfxSource.PlayOneShot(SFX.clip);
        }
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}
