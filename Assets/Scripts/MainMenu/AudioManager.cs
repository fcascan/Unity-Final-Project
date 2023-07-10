using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    private float generalVolume = 100f;
    private float musicVolume = 100f;
    private float effectsVolume = 100f;

    private AudioSource musicAudioSource;
    private AudioSource effectsAudioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Obtener las referencias de los componentes AudioSource
        musicAudioSource = GetComponent<AudioSource>();
        effectsAudioSource = GetComponentInChildren<AudioSource>();
    }

    public static AudioManager Instance
    {
        get { return instance; }
    }

    public float GeneralVolume
    {
        get { return generalVolume; }
        set
        {
            generalVolume = value;
            ApplyVolumeChanges();
        }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set
        {
            musicVolume = value;
            ApplyVolumeChanges();
        }
    }

    public float EffectsVolume
    {
        get { return effectsVolume; }
        set
        {
            effectsVolume = value;
            ApplyVolumeChanges();
        }
    }

    private void ApplyVolumeChanges()
    {
        musicAudioSource.volume = generalVolume * musicVolume;
        effectsAudioSource.volume = generalVolume * effectsVolume;
    }

    internal void SetMusicClip(AudioClip musicClip)
    {
        throw new NotImplementedException();
    }

    internal void PlayMusic()
    {
        throw new NotImplementedException();
    }

    internal void AddEffectClip(AudioClip clip)
    {
        throw new NotImplementedException();
    }
}

