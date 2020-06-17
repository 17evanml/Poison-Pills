using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("MenuPanels")]
    public GameObject StartMenu;
    public GameObject HostMenu;
    public GameObject JoinMenu;
    [Header("Ingame UI")]
    public GameObject GameDisplay;

    [Header("Networking Fields")]
    public InputField usernameField;
    public InputField ipField;
    public InputField portField;
    [Header("RevealManager -- will need to get rid of this later")]
    public RevealManager revealManager;

    [Header("Goal References")]
    public TMP_Text text_goal_1; // Text Reference used to display Goal 1
    public Goal goal_1; // Data for Goal 1
    public TMP_Text text_goal_2; // Text Reference used to display Goal 2
    public Goal goal_2; // Data for Goal 2

    [Header("Score References")]
    public TMP_Text score; // Text Reference used to display Score Standing

    [Header("Pill Display References")]
    public List<PillDisplay> pillDisplays = new List<PillDisplay>(); // List Containing All Pill Display Buttons
    [HideInInspector] public int playerCount; // Number of Players in the Game
    public GameObject displayPrefab; // Prefab for the Pill Display Button
    public Vector2 offset; // Offset for each Display Prefab from the Cup
    public Canvas canvas; // Reference to the Canvas

    [Header("Reveal Canvas References")]
    public TMP_Text currentGoals;
    public TMP_Text goal1;
    public TMP_Text goal2;

    //public Button beginGame;
    //public Button selfConnect;
    #region Start Menu Button Functions
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
    #endregion

    #region Goal and score UI
    ///<summary> Initializes the UI for the Player for goal text and score text.</summary>
    public void InitializeGoals()
    {
        text_goal_1.text = FormatGoal(goal_1); // Formats the Text for Goal 1
        text_goal_2.text = FormatGoal(goal_2); // Formats the Text for Goal 2
        score.text = FormatScore(); // Formats the Text for the Score

        // WILL BE REMOVED
        // Spawns a PillDisplay prefab for each player in the Game
        for (int i = 1; i <= playerCount; i++)
        {
            GameObject newDisplay = Instantiate(displayPrefab, Vector3.zero, Quaternion.identity); // Initializes Prefab
            newDisplay.transform.parent = canvas.gameObject.transform; // Puts it in the Canvas

            // Sets the Position of the Button in the Canvas
            Vector2 objPos = Camera.main.WorldToScreenPoint(CupManager.Instance.cupInfos[i].transform.position);
            objPos = objPos - canvas.GetComponent<RectTransform>().anchoredPosition + offset;
            newDisplay.GetComponent<RectTransform>().anchoredPosition = objPos;

            pillDisplays.Add(newDisplay.GetComponent<PillDisplay>()); // Adds the Pill Display to the List
            pillDisplays[i - 1].color_fill = CupManager.Instance.cupInfos[i].color; // Changes the Color of the Pill Prefab to match the Player
        }
    }

    ///<summary> Formats and Returns a String that represents a Goal in the Game.</summary>
    ///<param name="goal"> The Goal that needs to be formatted and returned. </param>
    ///<returns> A Formatted String containing a Goal. </returns>
    string FormatGoal(Goal goal)
    {
        string tempGoal;

        if (goal.goalState == Goal.GoalState.die) { tempGoal = "Kill "; } // Sets First word
        else { tempGoal = "Save "; }

        tempGoal += GameManager.cursors[goal.id].username; // Sets Second word
        return tempGoal;
    }

    ///<summary> Formats and Returns a String that represents the Score Standing in the Game.</summary>
    ///<returns> A Formatted String containing a the Score Standing. </returns>
    string FormatScore()
    {
        return null;
    }

    public string tempScore(int[] points)
    {
        Debug.Log(points.Length);
        string tempPoints = "";
        for (int i = 1; i < points.Length; i++)
        {
            Debug.Log($"Index: {i}");
            tempPoints += GameManager.cursors[i].username;
            tempPoints += ": ";
            tempPoints += points[i];
            tempPoints += "\n";
        }
        score.text = tempPoints;
        return tempPoints;
    }
    #endregion

    #region Reveal UI
    public void InitializeRevealButtons()
    {  
        //this is a terrible way to do this. Arjun and Evan should talk about the best way to solve this.
        goal1.GetComponentInParent<Button>().onClick.AddListener(() => { OnRevealClick(false);});
        goal2.GetComponentInParent<Button>().onClick.AddListener(() => { OnRevealClick(true);});
        goal1.text = GameManager.cursors[goal_1.id].username;
        goal2.text = GameManager.cursors[goal_2.id].username;
    }
    

    public void OnRevealClick(bool right)
    {
        currentGoals.text += GameManager.cursors[Client.instance.myId].username + ": ";
        if (right)
        {
            currentGoals.text += goal2.text + "\n";
        }
        else
        {
            currentGoals.text += goal1.text + "\n";
        }
        goal1.GetComponentInParent<Button>().gameObject.SetActive(false);
        goal2.GetComponentInParent<Button>().gameObject.SetActive(false);
        // Initialize Next UI
        // Talk to Evan if we should seperate the 2 UI Canvas
    }
    #endregion
}
