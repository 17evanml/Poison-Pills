using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterVolumeChanger : MonoBehaviour
{
<<<<<<< HEAD
    // [Range(0f, 1.0f)]
    // public float masterVolume = 1.0f;

    // void Update()
    // {
    //     AudioListener.volume = masterVolume;

    // }

    // public void SetMasterVolume(float vol)
    // {
    //     masterVolume = vol; 
    // }
=======
    [Range(0f, 1.0f)]
    public float masterVolume = 1.0f;

    void Update()
    {
        AudioListener.volume = masterVolume;

    }

    public void SetMasterVolume(float vol)
    {
        masterVolume = vol; 
    }
>>>>>>> 3602137ebc29dc46c72881969cf02ff8f979a11b

}