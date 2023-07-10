using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    public Slider sliderGeneralVolume;
    public Slider sliderMusic;
    public Slider sliderEffects;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        // Configurar los valores iniciales de los sliders
        sliderGeneralVolume.value = audioManager.GeneralVolume * 100f; // Ajustar el valor a un rango de 0-100
        sliderMusic.value = audioManager.MusicVolume * 100f;
        sliderEffects.value = audioManager.EffectsVolume * 100f;

        // Asignar los eventos de cambio de valor de los sliders
        sliderGeneralVolume.onValueChanged.AddListener(value => OnGeneralVolumeChanged(value));
        sliderMusic.onValueChanged.AddListener(value => OnMusicVolumeChanged(value));
        sliderEffects.onValueChanged.AddListener(value => OnEffectsVolumeChanged(value));
    }

    private void OnGeneralVolumeChanged(float value)
    {
        // Ajustar el rango de valores del deslizador a 0-100
        float volume = Mathf.Clamp(value, 0f, 100f);

        // Normalizar el valor a un rango de 0-1
        float normalizedVolume = volume / 100f;

        // Configurar el volumen general utilizando el valor normalizado
        audioManager.GeneralVolume = normalizedVolume;
    }

    private void OnMusicVolumeChanged(float value)
    {
        // Ajustar el rango de valores del deslizador a 0-100
        float volume = Mathf.Clamp(value, 0f, 100f);

        // Normalizar el valor a un rango de 0-1
        float normalizedVolume = volume / 100f;

        // Configurar el volumen de la música utilizando el valor normalizado
        audioManager.MusicVolume = normalizedVolume;
    }

    private void OnEffectsVolumeChanged(float value)
    {
        // Ajustar el rango de valores del deslizador a 0-100
        float volume = Mathf.Clamp(value, 0f, 100f);

        // Normalizar el valor a un rango de 0-1
        float normalizedVolume = volume / 100f;

        // Configurar el volumen de los efectos utilizando el valor normalizado
        audioManager.EffectsVolume = normalizedVolume;
    }
}
