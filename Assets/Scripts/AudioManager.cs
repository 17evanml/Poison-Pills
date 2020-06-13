using UnityEngine.Audio; 
using UnityEngine;
using System;
using System.Collections;
<<<<<<< HEAD
using UnityEngine.UI;

/// <summary> AudioManager regulates all Audial Components </summary>
public class AudioManager : MonoBehaviour {
    public static AudioManager instance { get; private set; }
    public Sound[] musicTracks;
    public AudioClip[] sfxClips;
    public AudioSource sfxSource; 

    public Slider mainSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    // private IEnumerator coroutine;

    /// <summary> Awake is called before the start of the first frame. </summary>
    private void Awake() {
        // Makes sure only 1 AudioManager is in use at any time.
        if (instance != null && instance != this) { 
            Destroy(gameObject);
            return;
        } else { 
            instance = this; 
        }

        DontDestroyOnLoad(gameObject); // AudioManager transfers between Scenes

        // Assigns an AudioSource for Each Sound
        foreach (Sound music in musicTracks) {
=======

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
>>>>>>> 3602137ebc29dc46c72881969cf02ff8f979a11b
            music.source = gameObject.AddComponent<AudioSource>(); 
            music.source.clip = music.audioClip;
            music.source.loop = music.loop;
            music.source.volume = music.volume;
            music.source.pitch = music.pitch; 
        }
<<<<<<< HEAD

        mainSlider.value = 1;
        musicSlider.value = 1;
        sfxSlider.value = 1;

        mainSlider.onValueChanged.AddListener(delegate {AdjustMainVolume(); });
        musicSlider.onValueChanged.AddListener(delegate {AdjustMusicVolume(); });
        sfxSlider.onValueChanged.AddListener(delegate {AdjustSFXVolume(); });

        AdjustMainVolume();
        AdjustMusicVolume();
        AdjustSFXVolume();
    }

    /// <summary> Adjusts the Master Volume of the Entire Scene. </summary>
    /// <param name="newVolume"> The Volume the Audio should be set to. </param>
    public void AdjustMainVolume() {
        AudioListener.volume = mainSlider.value;
        // StopCoroutine(coroutine);
        // coroutine = AdjustVolumeHelper(newVolume, 1f);
        // StartCoroutine(coroutine);
    }

    /// <summary> Adjusts the Volume of all Music AudioSources. </summary>
    /// <param name="newVolume"> The Volume the Audio should be set to. </param>
    public void AdjustMusicVolume() {
        foreach (Sound music in musicTracks) {
            music.source.volume = musicSlider.value;
        }
    }

    /// <summary> Adjusts the Volume of the SFX AudioSource. </summary>
    /// <param name="newVolume"> The Volume the Audio should be set to. </param>
    public void AdjustSFXVolume() {
        sfxSource.volume = sfxSlider.value;
    }

    public void ValueChangeCheck()
    {
        Debug.Log(mainSlider.value);
    }

    // private IEnumerator AdjustVolumeHelper(float newVolume, float lerpSpeed)
    // {
    //     while (AudioListener.volume != newVolume)
    //     {
    //         // Lerps the Master Volume to the new Volume
    //         // LerpSpeed affects how fast the volume changes
    //         AudioListener.volume = Mathf.Lerp(AudioListener.volume, newVolume, Time.deltaTime * lerpSpeed);

    //         // Makes it so the Loop isn't infinite. Play with the floats to change the volume clip boundary.
    //         if (AudioListener.volume > newVolume - 0.1f || AudioListener.volume < newVolume + 0.1f)
    //         {
    //             AudioListener.volume = newVolume;
    //             break;
    //         }
    //         yield return null; // Continues in the next frame
    //     }
    // }

    /// <summary> Plays an AudioSource. </summary>
    /// <param name="name"> The Name of the AudioClip to be played </param>
    public void PlayMusic(string name) {
        Sound music = Array.Find(musicTracks, track => track.name == name);
        if (music == null) {
            Debug.LogWarning("Music could not be played: " + name + " is not found."); 
            return; 
        } else {
=======
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
>>>>>>> 3602137ebc29dc46c72881969cf02ff8f979a11b
            music.source.Play(); 
        }
    }

<<<<<<< HEAD
    /// <summary> Plays an SFX. </summary>
    /// <param name="name"> The Name of the AudioClip to be played </param>
    public void PlaySFX(string name) {
        AudioClip sfx = Array.Find(sfxClips, clip => clip.name == name);
        if (sfx == null) {
            Debug.LogWarning("SFX could not be played: " + name + " is not found.");
            return;
        } else {
=======
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
>>>>>>> 3602137ebc29dc46c72881969cf02ff8f979a11b
            sfxSource.PlayOneShot(sfx, 0.5f); 
        }
    }

<<<<<<< HEAD
    /// <summary> Stop all Music. </summary>
    /// <param name="name"> The Name of the AudioClip to be played </param>
    public void StopMusic(string name) {
        Sound music = Array.Find(musicTracks, track => track.name == name);
        if (music == null) {
            Debug.LogWarning("No music to be stopped: " + name + " is not found.");
            return;
        } else {
=======
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
>>>>>>> 3602137ebc29dc46c72881969cf02ff8f979a11b
            music.source.Stop(); 
        }
    }
}
