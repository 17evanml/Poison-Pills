using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; 

    [HideInInspector]
    public AudioSource source;

    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(.5f, 2)]
    public float pitch = 1f; 

    public bool loop;
    
}
