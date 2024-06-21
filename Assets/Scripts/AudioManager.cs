using System;
using UnityEngine;

public enum SoundState { On, Off, SFXOnly, BGOnly }
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;
    void Awake()
    {
        instance = this;

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public static void play(string name)
    {
        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            Sound s = Array.Find(instance.sounds, Sound => Sound.name == name);

            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            s.source.Play();
        }
    }


    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
    }

    public static void ChangeSoundState(SoundState soundState)
    {
        var val = soundState == SoundState.On ? 0 : soundState == SoundState.Off ? 1 : 2;
        PlayerPrefs.SetInt("Sound", val);

        if (val == 1)
        {
            var s_Arr = instance.gameObject.GetComponents<AudioSource>();
            foreach (var s in s_Arr) { s.Stop(); }
        }
        Debug.Log("Changed sound state to " + soundState);
    }

}
