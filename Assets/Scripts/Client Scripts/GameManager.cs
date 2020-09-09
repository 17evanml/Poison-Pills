using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    [Header("Client Object dictionaries")]
    public static GameManager instance;

    public static Dictionary<int, CursorManager> cursors = new Dictionary<int, CursorManager>();
    public static Dictionary<int, CupInfo> cups = new Dictionary<int, CupInfo>();
    public int players = 0;

    [Header("Client objects")]
    public GameObject localCursorPrefab;
    public GameObject cursorPrefab;
    public GameObject cupPrefab;

    [Header("Client side buttons")]
    public GameObject canvas;
    public RectTransform canvasRectTransform;
    public RectTransform buttonParent;
    public Button offClick;
    public Button poisonClick;
    public Button fakeClick;
    public Vector2 offset;

    public GUIStyle textStyle;

    [Header("Cup placement")]
    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private float radius = 5f;
    [Header("Round Information")]
    public TurnSystem.RoundType round = TurnSystem.RoundType.Setup;

    [Header("Scorekeeping")]
    public PlayerScore[] playerScores;
    public Goal[] revealedGoals;
    private int games = 1;
    public int Games
    {
        get { return games; }
    }
    private int currentPlayer = 0;
    public int CurrentPlayer
    {
        get { return currentPlayer; }
        set { currentPlayer = value; }
    }

    #region GameManager
    private void Awake()
    {
        canvas.SetActive(true);
        canvas.SetActive(false);
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


    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnCursor(int _id, string _username, Vector3 _position, Quaternion _rotation, Color32 _cursorColor, Color32 _pill1Color, Color32 _pill2Color)
    {
        GameObject _cursor;
        if (_id == Client.instance.myId)
        {
            _cursor = Instantiate(localCursorPrefab, _position, _rotation);
        }
        else
        {
            _cursor = Instantiate(cursorPrefab, _position, _rotation);
        }
        CursorManager cm = _cursor.GetComponent<CursorManager>();
        cm.id = _id;
        cm.username = _username;
        cm.cursorColor = _cursorColor;
        cm.pill1Color = _pill1Color;
        cm.pill2Color = _pill2Color;
        cursors.Add(_id, _cursor.GetComponent<CursorManager>());
        players++;
    }


    public void UpdateScores(int[] scores, bool[] deaths)
    {
        foreach (PlayerScore player in playerScores)
        {
            player.score += scores[player.playerID];
            player.survived = deaths[player.playerID];
        }
        Array.Sort<PlayerScore>(playerScores);
    }

    public string[] GenerateStandings()
    {
        string[] ret = new string[cursors.Count];
        for(int i = 0; i < playerScores.Length; i++)
        {
            if(i == 0)
            {
                ret[i] = $"1st: {cursors[playerScores[i].playerID].username}";
            } else if (i == 1)
            {
                ret[i] = $"2nd: {cursors[playerScores[i].playerID].username}";
            }
            else if (i == 2)
            {
                ret[i] = $"3rd: {cursors[playerScores[i].playerID].username}";
            } else
            {
                ret[i] = $"{i+1}th: {cursors[playerScores[i].playerID].username}";
            }
        }
        return ret;
    }

    public string[] GeneratePointValues()
    {
        string[] ret = new string[cursors.Count];
        for(int i = 0; i < playerScores.Length; i++)
        {
            ret[i] = $"{i+1}. {cursors[playerScores[i].playerID].username}, {playerScores[i].score}";
        }
        return ret;
    }

    public void Disconnect()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void SetCurrentPlayer(int cp)
    {
        CurrentPlayer = cp;
    }
    #endregion

    #region CursorGameManger
    public void SwapPillPosition()
    {
        if (UnityEngine.Random.Range(0, 2) == 1)
        {
            Debug.Log("Randomly swapping pill position.");
            Vector3 temp = poisonClick.transform.position;
            poisonClick.transform.position = fakeClick.transform.position;
            fakeClick.transform.position = temp;
        }
    }

    private void CreateCup(CursorManager cm)
    {
        GameObject g = Instantiate(cupPrefab);

        // // This Part does not do anything
        // Debug.Log("Before: " + g.GetComponent<Renderer>().materials[0].color);
        // g.GetComponent<Renderer>().materials[0].color = cm.cursorColor;
        // Debug.Log("After: " + g.GetComponent<Renderer>().materials[0].color);

        // This part Sets the Entire Mesh to be one Color
        var block = new MaterialPropertyBlock();
        block.SetColor("_BaseColor", cm.cursorColor);
        g.GetComponent<Renderer>().SetPropertyBlock(block);

        CupInfo c = g.GetComponent<CupInfo>();
        if (c)
        {
            c.Initialize(cm.id, cm.name, cm.cursorColor);
            c.color = cm.cursorColor;
        }
        GameManager.instance.AddCup(c);
    }

    public void BeginTurn(int player, TurnSystem.RoundType _round)
    {
        cursors[player].StartTurn();
        Debug.Log($"Last Round: {round}, Current Round{_round} ");
        if (_round != round)
        {
            round = _round;
            if (round == TurnSystem.RoundType.Reveal)
            {
                if(games > 1)
                {
                    UIManager.instance.ToggleEndUI();
                }
                UIManager.instance.ToggleRevealUI();
                UIManager.instance.InitializeRevealButtons(); // Calls Initialize on Reveal Manager
                UIManager.instance.NextGoalLine();
            }
            else if (round == TurnSystem.RoundType.Place)
            {
                UIManager.instance.ToggleRevealUI();
                UIManager.instance.TogglePlaceUI();
            }
            else if (round == TurnSystem.RoundType.End)
            {
                UIManager.instance.ResetRevealedGoals();
                UIManager.instance.WriteEndScreen();
                UIManager.instance.ToggleEndUI();
                games++;
            }
        } else if(round == TurnSystem.RoundType.Reveal)
        {
            UIManager.instance.NextGoalLine();
        }
    }


    public void CreateAllCups()
    {
        foreach (CursorManager cm in cursors.Values)
        {
            CreateCup(cm);
        }
    }

    public void GenerateScoreList()
    {
        playerScores = new PlayerScore[cursors.Count];
        revealedGoals = new Goal[cursors.Count];
        for(int i = 0; i < playerScores.Length; i++)
        {
            playerScores[i] = new PlayerScore(i + 1);
        }
    }

    public void NextTurn()
    {
        //this needs to be called for all players
    }
    #endregion

    #region CupGameManger
    public void AddCup(CupInfo cup)
    {
        if (cup == null)
        {
            Debug.LogError("null parameter in AddCup");
            return;
        }

        cups.Add(cup.id, cup);
        RepositionCups();
    }

    public void RemoveCup(CupInfo cup)
    {
        if (cup == null)
        {
            Debug.LogError("null parameter in RemoveCup");
            return;
        }

        cups.Remove(cup.id);
        Destroy(cup.transform.gameObject);
        RepositionCups();
    }

    private void RepositionCups()
    {
        if (cups.Count == 0)
        {
            return;
        }

        //Calculate the position for all the cups
        float cupAngle = 180f / (cups.Count + 1f);
        int index = 1;
        float d2r = Mathf.PI / 180f;

        foreach (CupInfo c in cups.Values)
        {
            c.transform.position = origin + new Vector3(Mathf.Cos(cupAngle * index * d2r), 0, Mathf.Sin(cupAngle * index * d2r)) * radius;
            index++;
        }
    }


    public int Count()
    {
        return cups.Count;
    }

    #endregion
}
