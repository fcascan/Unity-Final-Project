using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void OpenScreenSettings()
    {
        SceneManager.LoadScene("ScreenSettingsScene");
    }

    public void OpenSoundSettings()
    {
        SceneManager.LoadScene("SoundSettingsScene");
    }
    public void OpenCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

