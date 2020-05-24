using UnityEngine.Audio; 
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public Sound[] musicTracks;
    public Sound[] sfxTracks;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return; 
        }

        foreach(Sound music in musicTracks)
        {
            music.source = gameObject.AddComponent<AudioSource>(); 
            music.source.clip = music.audioClip;
            music.source.loop = music.loop;
            music.source.volume = music.volume;
            music.source.pitch = music.pitch; 
        }

        foreach(Sound sfx in sfxTracks)
        {
            sfx.source = gameObject.AddComponent<AudioSource>();
            sfx.source.clip = sfx.audioClip;
            sfx.source.loop = sfx.loop;
            sfx.source.volume = sfx.volume;
            sfx.source.pitch = sfx.pitch;
        }
    }

    public void AdjustVolume(float newVolume)
    {
        AudioListener.volume = newVolume;
    }

    public void PlayMusic(string name)
    {
        Debug.Log("got to play music function"); 
        Sound music = Array.Find(musicTracks, track => track.name == name);
        if (music == null)
        {
            Debug.LogWarning("Music could not be played: " + name + " is not found."); 
            return; 
        } else
        {
            Debug.Log("playing track"); 
            music.source.Play(); 
        }
    }

    public void PlaySFX(string name)
    {
        Sound sfx = Array.Find(sfxTracks, track => track.name == name);
        if (sfx == null)
        {
            Debug.LogWarning("SFX could not be played: " + name + " is not found.");
            return;
        }
        else
        {
            sfx.source.Play();
        }
    }

    public void StopMusic(string name)
    {
        Sound music = Array.Find(musicTracks, track => track.name == name);
        if (music == null)
        {
            Debug.LogWarning("No music to be stopped: " + name + " is not found.");
            return;
        }
        else
        {
            music.source.Stop(); 
        }
    }
}
