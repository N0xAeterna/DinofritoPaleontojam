using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioStart : MonoBehaviour
{
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0: AudioManager.StopAllSoundtracks(); AudioManager.PlaySoundtrack(AudioClipName.MenuSoundtrack); break;
            case 1: AudioManager.StopAllSoundtracks(); AudioManager.PlaySoundtrack(AudioClipName.MinigameOneSoundtrack); break;
            case 2: AudioManager.StopAllSoundtracks(); AudioManager.PlaySoundtrack(AudioClipName.MinigameTwoSoundtrack); break;
            case 3: AudioManager.StopAllSoundtracks(); AudioManager.PlaySoundtrack(AudioClipName.MinigameThreeSoundtrack); break;
        }
    }

    public void ClickConfirmar()
    {
        AudioManager.PlayOneShot(AudioClipName.MenuConfirm, false);
    }

    public void ClickAtras()
    {
        AudioManager.PlayOneShot(AudioClipName.MenuBack, false);
    }

    public void GoGame()
    {
        AudioManager.PlayOneShot(AudioClipName.MusicalSFXGo, true);
    }
}
