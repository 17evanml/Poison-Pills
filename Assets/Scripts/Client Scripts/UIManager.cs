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
    public Image goal_1Background; // Background image for Goal 1
    public TMP_Text text_goal_2; // Text Reference used to display Goal 2
    public Goal goal_2; // Data for Goal 2
    public Image goal_2Background; // Background image for Goal 2

    public Image timerBackground;


    [Header("Score References")]
    public TMP_Text score; // Text Reference used to display Score Standing

    [Header("Pill Display References")]
    public List<PillDisplay> pillDisplays = new List<PillDisplay>(); // List Containing All Pill Display Buttons
    [HideInInspector] public int playerCount; // Number of Players in the Game
    public GameObject displayPrefab; // Prefab for the Pill Display Button
    public Vector2 offset; // Offset for each Display Prefab from the Cup
    public Canvas canvas; // Reference to the Canvas

    [Header("Reveal Canvas References")]
    public GameObject revealCanvas;
    public GameObject goalEntryUnfinished;
    public GameObject[] revealedGoals;
    public TMP_Text goal1;
    public TMP_Text goal2;
    public Vector3 originalPosition = new Vector3(-121, -113, 0);
    public float yOffset;
    private int currentPlayer = 0;

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
        NetworkManager.instance.SendGoals();
        NetworkManager.instance.BeginGame();
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

        goal_1Background.color = GameManager.cursors[goal_1.id].cursorColor;
        goal_2Background.color = GameManager.cursors[goal_2.id].cursorColor;

        // WILL BE REMOVED
        
    }

    public void InitializePillDisplays()
    {
        // Spawns a PillDisplay prefab for each player in the Game
        for (int i = 1; i <= playerCount; i++)
        {
            GameObject newDisplay = Instantiate(displayPrefab, Vector3.zero, Quaternion.identity); // Initializes Prefab
            newDisplay.transform.parent = canvas.gameObject.transform; // Puts it in the Canvas

            // Sets the Position of the Button in the Canvas
            Vector2 objPos = Camera.main.WorldToScreenPoint(GameManager.cups[i].transform.position);
            objPos = objPos - canvas.GetComponent<RectTransform>().anchoredPosition + offset;
            newDisplay.GetComponent<RectTransform>().anchoredPosition = objPos;

            pillDisplays.Add(newDisplay.GetComponent<PillDisplay>()); // Adds the Pill Display to the List
            pillDisplays[i - 1].color_fill = GameManager.cups[i].color; // Changes the Color of the Pill Prefab to match the Player
        }
    }

    ///<summary> Formats and Returns a String that represents a Goal in the Game.</summary>
    ///<param name="goal"> The Goal that needs to be formatted and returned. </param>
    ///<returns> A Formatted String containing a Goal. </returns>
    string FormatGoal(Goal goal)
    {
        string tempGoal;

        if (goal.goalState == Goal.GoalState.Kill) { tempGoal = "Kill "; } // Sets First word
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
        //Debug.Log(points.Length);
        string tempPoints = "";
        for (int i = 1; i < points.Length; i++)
        {
            //Debug.Log($"Index: {i}");
            tempPoints += GameManager.cursors[i].username;
            tempPoints += ": ";
            tempPoints += points[i];
            tempPoints += "\n";
        }
        score.text = tempPoints;
        return tempPoints;
    }
    
    public void UpdateCurrentPlayerColor(int player)
    {
        timerBackground.color = GameManager.cursors[player].cursorColor;
    }
    #endregion


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ToggleRevealUI();
        }
    }
    #region Reveal UI
    public void InitializeRevealButtons()
    {  
        //this is a terrible way to do this. Arjun and Evan should talk about the best way to solve this.
        revealedGoals[Client.instance.myId-1].transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => { OnRevealClick(false);});
        revealedGoals[Client.instance.myId-1].transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { OnRevealClick(true);});
        revealedGoals[Client.instance.myId - 1].transform.GetChild(1).GetComponent<TMP_Text>().text = $"{goal_1.goalState} {GameManager.cursors[goal_1.id].username}";
        revealedGoals[Client.instance.myId - 1].transform.GetChild(2).GetComponent<TMP_Text>().text = $"{goal_2.goalState} {GameManager.cursors[goal_2.id].username}";
    }
    

    public void OnRevealClick(bool right)
    {
        //currentGoals.text += GameManager.cursors[Client.instance.myId].username + ": ";
        if (right)
        {
            //currentGoals.text += goal2.text + "\n";
            ClientSend.RevealTarget(goal_1);
            WriteRevealedGoal(goal_1);
        }
        else
        {
            //currentGoals.text += goal1.text + "\n";
            ClientSend.RevealTarget(goal_2);
            WriteRevealedGoal(goal_2);
        }
        // Initialize Next UI
        // Talk to Evan if we should seperate the 2 UI Canvas
    }

    public void NextGoalLine()
    {
        revealedGoals[currentPlayer].SetActive(true);
        currentPlayer++;
    }
    public void WriteRevealedGoal(Goal goal)
    {
        revealedGoals[goal.myId-1].transform.GetChild(1).gameObject.SetActive(false);
        revealedGoals[goal.myId-1].transform.GetChild(2).gameObject.SetActive(false);
        revealedGoals[goal.myId-1].transform.GetChild(3).gameObject.SetActive(true);
        revealedGoals[goal.myId-1].transform.GetChild(4).gameObject.SetActive(false);
        revealedGoals[goal.myId-1].transform.GetChild(3).GetComponent<TMP_Text>().text = $"{goal.goalState} {GameManager.cursors[goal.id].username}";
    }

    public void ResetRevealedGoals()
    {
        for(int i = 0; i < revealedGoals.Length; i++)
        {
            revealedGoals[i].transform.GetChild(1).gameObject.SetActive(true);
            revealedGoals[i].transform.GetChild(2).gameObject.SetActive(true);
            revealedGoals[i].transform.GetChild(3).gameObject.SetActive(false);
            revealedGoals[i].transform.GetChild(4).gameObject.SetActive(true);
        }
    }

    public void SetOrderNumber()
    {
        revealedGoals = new GameObject[GameManager.cursors.Count];
        Debug.Log(revealedGoals.Length);
        for(int i = 0; i < revealedGoals.Length; i++)
        {
            revealedGoals[i] = Instantiate(goalEntryUnfinished, revealCanvas.transform, false);
            revealedGoals[i].transform.localPosition = originalPosition + -transform.up * yOffset*i;
            TMP_Text playerName = revealedGoals[i].transform.GetChild(0).GetComponent<TMP_Text>();
            playerName.text = GameManager.cursors[i+1].username;
            playerName.color = GameManager.cursors[i + 1].cursorColor;
            revealedGoals[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "";
            revealedGoals[i].transform.GetChild(2).GetComponent<TMP_Text>().text = "";
            revealedGoals[i].SetActive(false);
        }
    }
    #endregion

    #region Canvas Toggles
    public void ToggleRevealUI()
    {
        if(revealCanvas.activeSelf == true)
        {
            revealCanvas.SetActive(false);
        } else
        {
            revealCanvas.SetActive(true);
        }
    }

    public void TogglePlaceUI()
    {

    }

    public void ToggleEndUI()
    {

    }
    #endregion
}
