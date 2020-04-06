using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            bool isStartMenuEnabled = startMenu.activeSelf;
            bool isOptionsEnabled = optionsMenu.activeSelf;

            if (isOptionsEnabled && !isStartMenuEnabled)
            {
                optionsMenu.SetActive(!isOptionsEnabled);
                startMenu.SetActive(!isStartMenuEnabled);
            }
        }
    }
}
