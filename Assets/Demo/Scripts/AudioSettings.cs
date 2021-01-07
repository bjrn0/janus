using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class AudioSettings
{
    public string Name;
    public AudioClip Clip;
    public bool Loop;
    public bool PlayOnAwake;
    [HideInInspector]
    public AudioSource Source;
    [Range(0f,1f)]
    public float Volume;
    [Range(0f,1f)]
    public float Pitch;
    public int Priority = 0;

}
