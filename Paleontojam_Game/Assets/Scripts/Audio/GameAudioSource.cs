using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Fuente principal de audio del juego (singleton)
/// </summary>
public class GameAudioSource : MonoBehaviour
{
    // soporte de fuente de audio
    AudioSource source;
    AudioSource ducking;
    AudioSource soundtrack;
    [SerializeField]
    AudioMixer mixer = null;

    /// <summary>
    /// Inicializa el AudioManager si no se ha iniciado aun, se destruye si ya
    /// </summary>
    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        ducking = gameObject.AddComponent<AudioSource>();
        soundtrack = gameObject.AddComponent<AudioSource>();

        if (!AudioManager.Inicializado)
        {
            AudioManager.Inicializar(source, ducking, soundtrack, mixer);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DestroyAudioSource(AudioSource source)
    {
        Destroy(source);
    }
}
