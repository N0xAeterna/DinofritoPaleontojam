using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Para reproducir sonidos usando solamente un nombre
/// </summary>
public static class AudioManager
{
    #region Campos

    // soporte para audioclips y sus directorios
    static Dictionary<AudioClipName, AudioClip> audioClips;

    // soporte de sfx
    static AudioSource audioSource;
    // soporte de soundtracks
    static AudioSource soundtrackAudioSource;

    // soporte de ducking sfx
    static AudioSource duckingSource;
    static AudioMixer audioMixer;

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
    public static void Inicializar(AudioSource source, AudioSource duckedSource, AudioSource soundtrack, AudioMixer mixer)
    {
        inicializado = true;
        audioSource = source;
        duckingSource = duckedSource;

        soundtrackAudioSource = soundtrack;
        soundtrackAudioSource.playOnAwake = false;
        soundtrackAudioSource.loop = true;

        audioClips = new Dictionary<AudioClipName, AudioClip>();
        audioMixer = mixer;

        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[1];
        duckingSource.outputAudioMixerGroup = mixer.FindMatchingGroups("DuckSFX")[0];
        soundtrackAudioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Soundtrack")[0];

        // aqui van a agregar los clips de audio usando esta sintaxis
        audioClips.Add(AudioClipName.MenuBack, Resources.Load<AudioClip>("Sounds/MAIN_MENU_OCK_BACK"));
        audioClips.Add(AudioClipName.MenuConfirm, Resources.Load<AudioClip>("Sounds/SFX_CONFIRM_MENU-02"));
        audioClips.Add(AudioClipName.MenuSoundtrack, Resources.Load<AudioClip>("Sounds/MAIN_MENU_OCK_MUSIC_LOOP"));
        audioClips.Add(AudioClipName.MinigameOneSoundtrack, Resources.Load<AudioClip>("Sounds/MINI_GAME_1_OCK_MUSIC_LOOP"));
        audioClips.Add(AudioClipName.MinigameTwoSoundtrack, Resources.Load<AudioClip>("Sounds/MINI_GAME_2_OCK_MUSIC_LOOP"));
        audioClips.Add(AudioClipName.MinigameThreeSoundtrack, Resources.Load<AudioClip>("Sounds/MINI_GAME_3_OCK_MUSIC_LOOP"));
        audioClips.Add(AudioClipName.MusicalSFXOne, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK_1st"));
        audioClips.Add(AudioClipName.MusicalSFXTwo, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK_2nd"));
        audioClips.Add(AudioClipName.MusicalSFXThree, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK_3rd"));
        audioClips.Add(AudioClipName.MusicalSFXCropolito, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK_CROPOLITO"));
        audioClips.Add(AudioClipName.MusicalSFXFail, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK_FAIL"));
        audioClips.Add(AudioClipName.MusicalSFXGo, Resources.Load<AudioClip>("Sounds/MUSICAL_SFX_OCK-GO"));
    }

    /// <summary>
    /// Reproduce un clip de audio en el bus SFX (sound effect), en caso de que ducked sea true, lo reproduce en DuckSFX
    /// </summary>
    /// <param name="nombre">Nombre de pista</param>
    /// <param name="ducked">Ducking activado o no</param>
    public static void PlayOneShot(AudioClipName nombre, bool ducked)
    {
        if (ducked)
            duckingSource.PlayOneShot(audioClips[nombre]);
        else
            audioSource.PlayOneShot(audioClips[nombre]);
    }

    /// <summary>
    /// Reproduce una pista en loop con su propio AudioSource, en caso de haber un AudioSource sin usar en la coleccion, lo utiliza en lugar de crear uno nuevo
    /// </summary>
    /// <param name="nombre">Nombre de la pista</param>
    public static void PlaySoundtrack(AudioClipName nombre, float initialVolume = 0f, float targetVolume = 1f, float fadingTime = 2f)
    {
        targetVolume = Mathf.Clamp01(targetVolume);
        fadingTime = (fadingTime <= 0f) ? 0.1f : fadingTime;

        soundtrackAudioSource.volume = 0.0f;
        soundtrackAudioSource.clip = audioClips[nombre];
        soundtrackAudioSource.Play();

        soundtrackAudioSource.gameObject.GetComponent<GameAudioSource>().StopAllCoroutines();
        soundtrackAudioSource.gameObject.GetComponent<GameAudioSource>().StartCoroutine(FadeIn(targetVolume, fadingTime, soundtrackAudioSource));
    }

    /// <summary>
    /// Detiene todas las pistas de soundtracks y elimina los AudioSource para no tener
    /// AudioSource innecesarios
    /// </summary>
    public static void StopSoundtrack()
    {
        soundtrackAudioSource.Stop();
    }

    /// <summary>
    /// Fades in the track to target volume within the fading time
    /// </summary>
    static IEnumerator<WaitForSeconds> FadeIn(float targetVolume, float fadingTime, AudioSource source)
    {
        if(source.volume < targetVolume)
        {
            float increment = (targetVolume - source.volume) / (fadingTime * 100);
            while (source.volume < targetVolume)
            {
                source.volume += increment;
                yield return new WaitForSeconds(0.0100f);
            }
        }
    }

    /// <summary>
    /// Fades out the track to target volume within the fading time
    /// </summary>
    static IEnumerator<WaitForSeconds> FadeOut(float targetVolume, float fadingTime, AudioSource source)
    {
        if (targetVolume < source.volume)
        {
            float decrement = (source.volume - targetVolume) / (fadingTime * 100f);
            while (source.volume > targetVolume)
            {
                source.volume -= decrement;
                yield return new WaitForSeconds(0.0100f);
            }

            source.Stop();
        }
    }

    #endregion
}
