using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            AudioManager.PlayOneShot(AudioClipName.MenuBack, false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.PlayOneShot(AudioClipName.MenuConfirm, false);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXFail, false);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXGo, true);
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
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXCropolito, true);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXOne, true);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXTwo, true);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            AudioManager.PlayOneShot(AudioClipName.MusicalSFXThree, true);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            AudioManager.StopAllSoundtracks();
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene("ExcavationSite");
        }
    }
}
