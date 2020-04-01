using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject lobbyMenu;
    public GameObject server;
    public GameObject startMenu;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    public Button startServer;
    public Button beginGame;
    public Button selfConnect;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    /// <summary>Attempts to connect to the server.</summary>
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
        SceneManager.LoadScene(0, LoadSceneMode.Additive);
        lobbyMenu.SetActive(true);
        startMenu.SetActive(false);

        //Instantiate(server);
    }

    public void SelfConnect()
    {
        Client.instance.ip = "127.0.0.1";
        Client.instance.port = 6942;
        Client.instance.ConnectToServer();
    }

    public void BeginGame()
    {
        lobbyMenu.SetActive(false);
        foreach (ServerClient client in Server.clients.Values)
        {
            if (client.cursor != null)
            {
                ServerSend.BeginGame(client.cursor);
            }
        }
    }
}
