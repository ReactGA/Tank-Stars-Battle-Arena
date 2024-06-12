using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
          s.source =  gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

   public void play(string name)
    {
       Sound s= Array.Find(sounds, Sound => Sound.name == name);
        s.source.Play();
    }


public void StopPlaying (string sound)
 {
  Sound s = Array.Find(sounds, item => item.name == sound);
  if (s == null)
  {
   Debug.LogWarning("Sound: " + name + " not found!");
   return;
  }

 
  s.source.Stop ();
 }


}
