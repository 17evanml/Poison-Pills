using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused; 
    public GameObject pauseMenu;

    private void PauseUnpause()
    {
        pauseMenu.SetActive(!isGamePaused);
        isGamePaused = !isGamePaused;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause(); 
        }
    }

    public void Resume()
    {
        if (isGamePaused)
        {
            PauseUnpause();
        }
    }

}
