using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundController : MonoBehaviour
{
    [SerializeField] private AudioClip musicClip;
    [SerializeField] private AudioClip[] effectClips;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        // Configurar el clip de música del menú principal
        if (musicClip != null)
        {
            audioManager.SetMusicClip(musicClip);
            audioManager.PlayMusic();
        }

        // Configurar los clips de efectos del menú principal
        foreach (AudioClip clip in effectClips)
        {
            audioManager.AddEffectClip(clip);
        }
    }
}

