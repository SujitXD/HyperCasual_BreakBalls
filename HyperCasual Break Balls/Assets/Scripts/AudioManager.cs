using UnityEngine.Audio;
using UnityEngine;
using System;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    public bool dontDestroyOnLoad;
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.audioOutput;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
        }
    }

    private void Start()
    {
        Play("MainMenu");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioname == name);

        if (s == null)
        {
            return;
        }

        s.source.Play();
    }
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioname == name);

        if (s == null)
        {
            return;
        }

        s.source.PlayOneShot(s.source.clip);
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioname == name);

        if (s == null)
        {
            return;
        }

        s.source.Pause();
    }

    public void SmoothPlay(string _from, string _to)
    {
        Sound from = Array.Find(sounds, sound => sound.audioname == _from);
        Sound to = Array.Find(sounds, sound => sound.audioname == _to);

        if (from == null && to == null)
        {
            return;
        }

        float toVolume = to.source.volume;
        to.source.volume = 0f;
        to.source.Play();

        //print("stopping " + from.audioname);
        //print("starting " + to.audioname);

        DOTween.To(() => from.source.volume, x => from.source.volume = x, 0f, 1f).OnComplete(() => { 
            from.source.Stop();
            from.source.volume = 1f;
        });
        DOTween.To(() => to.source.volume, x => to.source.volume = x, toVolume, 1f);
    }
    public float AudioLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioname == name);

        if (s == null)
            return 0f;

        return s.source.clip.length;
    }
    public void VolumeControl(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.audioname == name);

        if (s == null)
            return;

        s.source.volume = 0f;
    }
}

