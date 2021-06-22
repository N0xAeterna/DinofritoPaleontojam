using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _SoundTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlaySoundtrack(AudioClipName.MenuSoundtrack);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.PlayOneShot(AudioClipName.MenuBack);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.PlayOneShot(AudioClipName.MenuConfirm);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXFail);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXGo);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameOneSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameTwoSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameThreeSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MenuSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameOneSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameTwoSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameThreeSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MenuSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXCropolito);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXOne);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXTwo);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXThree);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.StopAllSoundtracks();
        }
    }
}
