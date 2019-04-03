using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The audio manager
/// </summary>
public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets whether or not the audio manager has been initialized
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initializes the audio manager
    /// </summary>
    /// <param name="source">audio source</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.BallHitPaddle,
        Resources.Load<AudioClip>("BallHitPaddle"));
        audioClips.Add(AudioClipName.BallHitStandardBlock,
        Resources.Load<AudioClip>("BallHitStandardBlock"));
        audioClips.Add(AudioClipName.BallHitBounsBlock,
        Resources.Load<AudioClip>("BallHitBounsBlock"));
        audioClips.Add(AudioClipName.BallHitSpeedUpBlock,
        Resources.Load<AudioClip>("BallHitSpeedUpBlock"));
        audioClips.Add(AudioClipName.BallHitFreezerBlock,
        Resources.Load<AudioClip>("BallHitFreezerBlock"));
        audioClips.Add(AudioClipName.GameLost,
        Resources.Load<AudioClip>("GameLost"));
        audioClips.Add(AudioClipName.GameWon,
        Resources.Load<AudioClip>("GameWon"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">name of the audio clip to play</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
