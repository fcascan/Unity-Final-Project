using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioConfiguration : MonoBehaviour
{
    public AudioSource generalAudioSource;
    public AudioSource musicAudioSource;
    public AudioSource effectsAudioSource;

    public Slider generalVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;

    private void Start()
    {
        // Configurar los valores iniciales de los sliders según los volúmenes actuales
        generalVolumeSlider.value = generalAudioSource.volume;
        musicVolumeSlider.value = musicAudioSource.volume;
        effectsVolumeSlider.value = effectsAudioSource.volume;
    }

    public void OnGeneralVolumeChanged(float volume)
    {
        generalAudioSource.volume = volume;
        musicAudioSource.volume = volume;
        effectsAudioSource.volume = volume;
    }

    public void OnMusicVolumeChanged(float volume)
    {
        musicAudioSource.volume = volume;
    }

    public void OnEffectsVolumeChanged(float volume)
    {
        effectsAudioSource.volume = volume;
    }
}

