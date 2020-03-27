using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject server;
    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    public Button serverStartButton;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }

    public void ConnectToServer()
    {
        startMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ip = ipField.text;
        Client.instance.port = int.Parse(portField.text);
        Client.instance.ConnectToServer();
    }

    public void StartServer()
    {
        Instantiate(server);
        serverStartButton.enabled = false;
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

    }
}
