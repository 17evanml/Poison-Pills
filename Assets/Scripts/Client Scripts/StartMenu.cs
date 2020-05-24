using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    // load new scenes or hook up netcode for hosting/joining a game here

    void Awake()
    {
        AudioManager.instance.PlayMusic(Constants.SM_MUSIC); 
    }

    public void Quit()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
