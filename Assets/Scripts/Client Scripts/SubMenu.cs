using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    public GameObject returnMenu;
    public GameObject currentMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isReturnMenuEnabled = returnMenu.activeSelf;
            bool isCurrentMenuEnabled = currentMenu.activeSelf;

            if (isCurrentMenuEnabled && !isReturnMenuEnabled)
            {
                currentMenu.SetActive(!isCurrentMenuEnabled);
                returnMenu.SetActive(!isReturnMenuEnabled);
            }
        }
    }
}
