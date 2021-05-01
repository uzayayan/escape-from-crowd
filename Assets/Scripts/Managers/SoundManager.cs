using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
    #region Public Fields

    public Action<bool> MuteStateChanged;

    #endregion
    #region Serializable Fields

    [SerializeField] private List<Sound> Sounds;

    #endregion

    /// <summary>
    /// Awake
    /// </summary>
    protected override void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();

            source.clip = sound.Clip;
            source.pitch = sound.Pitch;
            source.volume = sound.Volume;
            source.loop = sound.IsLoop;

            sound.Source = source;  
        }
        
        SetMuteState(PlayerPrefs.GetInt(CommonTypes.SOUND_STATE_KEY) == 0);
        
        base.Awake();
    }
    
    /// <summary>
    /// This function helper for play sound with 'Sound Type'.
    /// </summary>
    /// <param name="soundType"></param>
    public void Play(SoundType soundType)
    {
        Sound targetSound = Sounds.SingleOrDefault(x => x.Type == soundType);

        if (targetSound == null)
        {
            Debug.LogError($"Target sound is null. Type : {soundType}");
            return;
        }

        targetSound.Source.PlayOneShot(targetSound.Clip);
    }

    /// <summary>
    /// This function helper for play sound with 'Audio Clip'.
    /// </summary>
    /// <param name="audioClip"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void Play(AudioClip audioClip, float volume = 1, float pitch = 1)
    {
        if (audioClip == null)
            return;

        AudioSource audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        audioSource.Play();
        
        Destroy(audioSource, audioClip.length);
    }

    /// <summary>
    /// This function helper for set sound state.
    /// </summary>
    /// <param name="state"></param>
    public void SetMuteState(bool state)
    {
        AudioListener.volume = state ? 1 : 0;
        PlayerPrefs.SetInt(CommonTypes.SOUND_STATE_KEY, state ? 0 : 1);
        
        MuteStateChanged?.Invoke(state);
    }
}

