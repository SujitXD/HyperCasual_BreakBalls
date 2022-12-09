using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string audioname;

    public AudioClip clip;
    public AudioMixerGroup audioOutput;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;
    //public bool randomCall;

    [HideInInspector]
    public AudioSource source;
}


