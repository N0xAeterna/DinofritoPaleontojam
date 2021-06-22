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
    static AudioSource duckingSource;
    static AudioSource audioSource;
    static List<AudioSource> soundtrackAudioSources;
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
    public static void Inicializar(AudioSource source, AudioSource duckedSource, AudioMixer mixer)
    {
        inicializado = true;
        audioSource = source;
        duckingSource = duckedSource;

        audioClips = new Dictionary<AudioClipName, AudioClip>();
        soundtrackAudioSources = new List<AudioSource>();

        audioMixer = mixer;

        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[1];
        duckingSource.outputAudioMixerGroup = mixer.FindMatchingGroups("DuckSFX")[0];

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
    /// Reproduce un clip de audio
    /// </summary>
    /// <param name="nombre">Nombre del clip a reproducir</param>
    public static void PlayOneShot(AudioClipName nombre, bool ducked)
    {
        if (ducked)
            duckingSource.PlayOneShot(audioClips[nombre]);
        else
            audioSource.PlayOneShot(audioClips[nombre]);
    }

    public static void PlaySoundtrack(AudioClipName nombre)
    {
        bool playing = false;
        if(audioSource != null)
        {
            if(soundtrackAudioSources.Count > 0)
            {
                foreach(AudioSource soundtrackAudioSource in soundtrackAudioSources)
                {
                    if(!playing)
                    {
                        if(soundtrackAudioSource.clip == audioClips[nombre])
                        {
                            if (!soundtrackAudioSource.isPlaying)
                                soundtrackAudioSource.Play();

                            playing = true;
                        }
                        else if(!soundtrackAudioSource.isPlaying)
                        {
                            soundtrackAudioSource.clip = audioClips[nombre];
                            soundtrackAudioSource.loop = true;
                            soundtrackAudioSource.Play();
                            playing = true;
                        }
                    }               
                }

                if (!playing)
                    AddSoundtrackAudioSourceAndPlay(nombre);
            }
            else
            {
                AddSoundtrackAudioSourceAndPlay(nombre);
            }
        }
    }

    public static void StopSoundtrack(AudioClipName nombre)
    {
        foreach(AudioSource soundtrackAudioSource in soundtrackAudioSources)
        {
            if(soundtrackAudioSource == audioClips[nombre])
            {
                soundtrackAudioSource.Stop();
            }
        }
    }

    public static void StopAllSoundtracks()
    {
        foreach(AudioSource soundTrackAudioSource in soundtrackAudioSources)
        {
            soundTrackAudioSource.Stop();
            soundTrackAudioSource.gameObject.GetComponent<GameAudioSource>().DestroyAudioSource(soundTrackAudioSource);
        }

        soundtrackAudioSources.RemoveRange(0, soundtrackAudioSources.Count);
    }

    static void AddSoundtrackAudioSourceAndPlay(AudioClipName nombre)
    {
        soundtrackAudioSources.Add(audioSource.gameObject.AddComponent<AudioSource>());

        soundtrackAudioSources[soundtrackAudioSources.Count - 1].playOnAwake = false;
        soundtrackAudioSources[soundtrackAudioSources.Count - 1].Stop();

        soundtrackAudioSources[soundtrackAudioSources.Count - 1].outputAudioMixerGroup = audioMixer.FindMatchingGroups("Soundtrack")[0];

        soundtrackAudioSources[soundtrackAudioSources.Count - 1].clip = audioClips[nombre];
        soundtrackAudioSources[soundtrackAudioSources.Count - 1].loop = true;

        soundtrackAudioSources[soundtrackAudioSources.Count - 1].Play();
    }
    #endregion
}
