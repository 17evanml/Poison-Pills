using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject returnMenu;
    public GameObject optionsMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isReturnMenuEnabled = returnMenu.activeSelf;
            bool isOptionsEnabled = optionsMenu.activeSelf;

            if (isOptionsEnabled && !isReturnMenuEnabled)
            {
                optionsMenu.SetActive(!isOptionsEnabled);
                returnMenu.SetActive(!isReturnMenuEnabled);

                isOptionsEnabled = !isOptionsEnabled;
                isReturnMenuEnabled = !isReturnMenuEnabled; 
            }
        }
    }
}
