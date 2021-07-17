using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _AudioTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameOneSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameTwoSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.StopAllSoundtracks();
            AudioManager.PlaySoundtrack(AudioClipName.MinigameThreeSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.StopAllSoundtracks();
        }
    }
}
