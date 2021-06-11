using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Para reproducir sonidos usando solamente un nombre
/// </summary>
public static class AudioManager
{
    #region Campos

    // soporte para audioclips y sus directorios
    static Dictionary<AudioClipName, AudioClip> audioClips;
    static AudioSource audioSource;

    // soporte de estado
    static bool inicializado = false;

    #endregion

    #region Propiedades

    /// <summary>
    /// Estado de inicializacion del AudioManager
    /// </summary>
    /// <value>Verdadero si ya se inicializo</value>
    public static bool Inicializado
    {
        get { return inicializado; }
    }

    #endregion

    #region Metodos

    /// <summary>
    /// Asigna la fuente principal de audio y agrega los clips de audio
    /// </summary>
    /// <param name="source">Fuente de audio principal</param>
    public static void Inicializar(AudioSource source)
    {
        inicializado = true;
        audioSource = source;

        audioClips = new Dictionary<AudioClipName, AudioClip>();

        // aqui van a agregar los clips de audio usando esta sintaxis
        audioClips.Add(AudioClipName.Prueba, Resources.Load<AudioClip>("Sounds/Prueba"));
    }

    /// <summary>
    /// Reproduce un clip de audio
    /// </summary>
    /// <param name="nombre">Nombre del clip a reproducir</param>
    public static void PlayOneShot(AudioClipName nombre)
    {
        audioSource.PlayOneShot(audioClips[nombre]);
    }

    #endregion
}
