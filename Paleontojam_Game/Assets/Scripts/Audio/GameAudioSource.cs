using UnityEngine;

/// <summary>
/// Fuente principal de audio del juego (singleton)
/// </summary>
public class GameAudioSource : MonoBehaviour
{
    // soporte de fuente de audio
    AudioSource source;

    /// <summary>
    /// Inicializa el AudioManager si no se ha iniciado aun, se destruye si ya
    /// </summary>
    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();

        if (!AudioManager.Inicializado)
        {
            AudioManager.Inicializar(source);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
