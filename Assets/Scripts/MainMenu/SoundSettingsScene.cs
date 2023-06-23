using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider volumeSlider;
    public AudioSource audioSource;

    private void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f); // Establecer el valor del Slider al volumen almacenado en PlayerPrefs (o 1f si no se encuentra)
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Guardar el volumen en PlayerPrefs para que persista entre sesiones de juego
    }
}
