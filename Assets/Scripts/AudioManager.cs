using UnityEngine.Audio; 
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    public Sound[] musicTracks;
    public AudioClip[] sfxClips;

    private AudioSource sfxSource; 
    private IEnumerator coroutine;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound music in musicTracks)
        {
            music.source = gameObject.AddComponent<AudioSource>(); 
            music.source.clip = music.audioClip;
            music.source.loop = music.loop;
            music.source.volume = music.volume;
            music.source.pitch = music.pitch; 
        }
    }

    public void AdjustVolume(float newVolume)
    {
        StopCoroutine(coroutine);
        coroutine = AdjustVolumeHelper(newVolume, 1f);
        StartCoroutine(coroutine);
    }

    private IEnumerator AdjustVolumeHelper(float newVolume, float lerpSpeed)
    {
        while (AudioListener.volume != newVolume)
        {
            // Lerps the Master Volume to the new Volume
            // LerpSpeed affects how fast the volume changes
            AudioListener.volume = Mathf.Lerp(AudioListener.volume, newVolume, Time.deltaTime * lerpSpeed);

            // Makes it so the Loop isn't infinite. Play with the floats to change the volume clip boundary.
            if (AudioListener.volume > newVolume - 0.1f || AudioListener.volume < newVolume + 0.1f)
            {
                AudioListener.volume = newVolume;
                break;
            }
            yield return null; // Continues in the next frame
        }
    }

    public void PlayMusic(string name)
    {
        Sound music = Array.Find(musicTracks, track => track.name == name);
        if (music == null)
        {
            Debug.LogWarning("Music could not be played: " + name + " is not found."); 
            return; 
        } else
        {
            music.source.Play(); 
        }
    }

    public void PlaySFX(string name)
    {
        AudioClip sfx = Array.Find(sfxClips, clip => clip.name == name);
        if (sfx == null)
        {
            Debug.LogWarning("SFX could not be played: " + name + " is not found.");
            return;
        }
        else
        {
            sfxSource.PlayOneShot(sfx, 0.5f); 
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
