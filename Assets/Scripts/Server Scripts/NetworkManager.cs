using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    const int SURIVIALPOINTS = 1;
    const int KILLPOINTS = 2;
    public static NetworkManager instance;
    private int players = 0;
    public TurnSystem turnSystem;
    public GameObject cursorPrefab;
    List<int> targets;
    List<Goal> goals;
    public int[] playerPoints;
    public Color32[] playerColors;
    public Color32[] pill1Colors;
    public Color32[] pill2Colors;
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            debugSetRound(1);
        }
    }


    private void Start()
    {
        goals = new List<Goal>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        Server.Start(50, 6942);
    }

    public ServerCursor InstantiatePlayer()
    {
        return Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity).GetComponent<ServerCursor>();
    }

    public void BeginGame()
    {
        turnSystem = new TurnSystem(players, this);
        GenerateTargets();
        playerPoints = new int[players + 1];
        ServerSend.StartTurn(turnSystem.GetCurrentPlayer());
    }


    public void Connect()
    {
        players++;
        Debug.Log("Connected: " + players);
    }

    public void Disconnect(int id)
    {
        players--;
    }


    public void CloseServer()
    {
        for (int i = 0; i < Server.clients.Count; i++)
        {
            Server.clients[i].tcp.Disconnect();
        }

    }

    public void AdvanceTurn()
    {
        //Debug.Log("Advance");
        turnSystem.AdvanceTurn();
        ServerSend.StartTurn(turnSystem.GetCurrentPlayer());
    }

    public void PlayerAuthUpdate(int playerId, bool[] authorities)
    {
        //Debug.Log("Sent to players");   
        ServerSend.UpdateAuthority(playerId, authorities);
    }

    public Goal[] GiveTargets(int selfID)
    {
        Goal[] ret = new Goal[2];
        //for debug purposes only
        if (players == 1)
        {
            ret[0] = new Goal(selfID, 1, Goal.GoalState.die);
            ret[1] = new Goal(selfID, 1, Goal.GoalState.die);
            goals.Add(ret[0]);
            goals.Add(ret[1]);
            return ret;
        }
        else if (players == 2)
        {
            if (selfID == 1)
            {
                ret[0] = new Goal(selfID, 2, Goal.GoalState.die);
                ret[1] = new Goal(selfID, 2, Goal.GoalState.die);
            }
            else
            {
                ret[0] = new Goal(selfID, 1, Goal.GoalState.die);
                ret[1] = new Goal(selfID, 1, Goal.GoalState.die);
            }
            ret[0] = new Goal(selfID, 1, Goal.GoalState.die);
            ret[1] = new Goal(selfID, 1, Goal.GoalState.die);
            goals.Add(ret[0]);
            goals.Add(ret[1]);
            return ret;
        }

        int goalState1 = Random.Range(0, 2);
        int goalState2 = Random.Range(0, 2);
        int target1 = Random.Range(0, targets.Count - 1);
        int target2 = Random.Range(0, targets.Count - 1);
        while (targets[target1] == selfID)
        {
            target1 = Random.Range(0, targets.Count - 1);
        }
        ret[0] = new Goal(selfID, targets[target1], (Goal.GoalState)goalState1);
        targets.RemoveAt(target1);
        while (targets[target2] == selfID || targets[target2] == ret[0].id)
        {
            target2 = Random.Range(0, targets.Count - 1);
        }
        ret[1] = new Goal(selfID, targets[target2], (Goal.GoalState)goalState2); ;
        targets.RemoveAt(target2);

        goals.Add(ret[0]);
        goals.Add(ret[1]);
        foreach (Goal goal in goals)
        {
            Debug.Log(goal);
        }
        return ret;
    }

    public void GenerateTargets()
    {
        targets = new List<int>();
        for (int i = 1; i <= players; i++)
        {
            //Debug.Log(i);
            for (int j = 0; j < 4; j++)
            {
                targets.Add(i);
            }
        }
    }

    public void debugSetRound(int _round)
    {
        turnSystem.debugSetRound(_round);
    }

    public void CalculatePoints()
    {
        bool[] deaths = new bool[players + 1];
        for (int i = 1; i <= players; i++)
        {
            deaths[i] = !Server.clients[i].cup.isAlive();
            //Debug.Log($"Player: {i} is {deaths[i]}");
            if (!deaths[i])
            {
                playerPoints[i] += SURIVIALPOINTS;
            }
        }

        foreach (Goal goal in goals)
        {
            if (goal.goalState.Equals(Goal.GoalState.die) && deaths[goal.id])
            {
                playerPoints[goal.myId] += KILLPOINTS;
            }
            else if (goal.goalState.Equals(Goal.GoalState.live) && !deaths[goal.id])
            {
                playerPoints[goal.myId] += KILLPOINTS;
            }
        }

        UpdatePoints(playerPoints, deaths);
    }

    public void UpdatePoints(int[] points, bool[] deaths)
    {
        ServerSend.EndRound(points, deaths);
    }

    public void ServerClose()
    {
        ServerSend.ServerClose();
        for (int i = 1; i <= players; i++)
        {
            if (Server.clients[i] != null)
            {
                Destroy(Server.clients[i].cursor.gameObject);
            }
        }
        Server.Close();
    }
}
