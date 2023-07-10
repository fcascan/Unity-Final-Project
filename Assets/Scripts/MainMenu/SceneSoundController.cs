using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSoundController : MonoBehaviour
{
    public AudioClip musicClip;
    public AudioClip[] effectClips;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;

        // Configurar el clip de música de la escena
        if (musicClip != null)
        {
            audioManager.SetMusicClip(musicClip);
            audioManager.PlayMusic();
        }

        // Configurar los clips de efectos de la escena
        foreach (AudioClip clip in effectClips)
        {
            audioManager.AddEffectClip(clip);
        }
    }
}
