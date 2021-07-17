using UnityEngine;

public class _AudioTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameOneSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameTwoSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AudioManager.PlaySoundtrack(AudioClipName.MinigameThreeSoundtrack);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.StopSoundtrack();
        }
    }
}
