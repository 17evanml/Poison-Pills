using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject StartMenu;
    public GameObject HostMenu;
    public GameObject JoinMenu;
    public GameObject GameDisplay;
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    //public Button beginGame;
    //public Button selfConnect;

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

    public void HostGame()
    {
        StartMenu.SetActive(false);
        HostMenu.SetActive(true);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
        //Instantiate(server);
    }

    public void JoinFriends()
    {
        StartMenu.SetActive(false);
        JoinMenu.SetActive(true);
    }

    /// <summary>Attempts to connect to the server.</summary>
    public void ConnectToServer()
    {
        JoinMenu.SetActive(false);
        usernameField.interactable = false;
        Client.instance.ip = ipField.text;
        Client.instance.port = int.Parse(portField.text);
        Client.instance.ConnectToServer();
    }


    public void SelfConnect()
    {
        Client.instance.ip = "127.0.0.1";
        Client.instance.port = 6942;
        usernameField.text = "jeff";
        Client.instance.ConnectToServer();
    }

    public void BeginGame()
    {
        HostMenu.SetActive(false);
        NetworkManager.instance.BeginGame();
        foreach (ServerClient client in Server.clients.Values)
        {
            if (client.cursor != null)
            {
                Goal[] targets = NetworkManager.instance.GiveTargets(client.id);
                
                ServerSend.BeginGame(client.cursor, targets[0], targets[1]);
            }
        }
    }

    public void EndHost()
    {
        NetworkManager.instance.ServerClose();
        Server.clients.Clear();
        SceneManager.UnloadSceneAsync(1);
    }
}
