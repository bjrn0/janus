using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSettings[] Sounds;
    public static AudioManager AudioManagerInstance;
    
    private void Awake()
    {
        if (AudioManagerInstance == null) AudioManagerInstance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (AudioSettings sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            sound.Source.loop = sound.Loop;
            sound.Source.playOnAwake = sound.PlayOnAwake;
            sound.Source.priority = sound.Priority;
        }
    }

    public void Play(string name)
    {
        AudioSettings selectedSound = Array.Find(Sounds, sound => sound.Name == name);
        selectedSound?.Source.Play();
    }
    
    
}
