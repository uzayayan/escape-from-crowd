using System;
using UnityEngine;

[Serializable]
public class Sound
{
    [HideInInspector] public AudioSource Source;
    
    public AudioClip Clip;
    public SoundType Type;
    [Range(0, 1)] public float Volume = 1;
    [Range(-3, 3)] public float Pitch = 1;
    public bool IsLoop;
}