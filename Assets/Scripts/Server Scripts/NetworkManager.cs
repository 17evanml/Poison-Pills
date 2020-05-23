using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;
    private int players = 0;
    public TurnSystem turnSystem;
    public GameObject cursorPrefab;
    List<int> targets;
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            InstantiatePlayer();
        }
    }


    private void Start()
    {
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
        Debug.Log("players");
        turnSystem = new TurnSystem(players, this);
        GenerateTargets();
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

    public void AdvanceTurn()
    {
        Debug.Log("Advance");
        turnSystem.AdvanceTurn();
        ServerSend.StartTurn(turnSystem.GetCurrentPlayer());
    }

    public void PlayerAuthUpdate(int playerId, bool[] authorities)
    {
        //Debug.Log("Sent to players");   
        ServerSend.UpdateAuthority(playerId, authorities);
    }

    public int[] GiveTargets(int selfID)
    {
        int[] ret = new int[2];
        int target1 = Random.Range(0, targets.Count-1);
        int target2 = Random.Range(0, targets.Count-1);
        while(targets[target1] == selfID) {
            int select = Random.Range(0, targets.Count-1);
        }
        ret[0] = targets[target1];
        targets.RemoveAt(target1);
        while (targets[target2] == selfID)
        {
            int select = Random.Range(0, targets.Count-1);
        }
        ret[1] = targets[target2];
        targets.RemoveAt(target2);
        return ret;
    }

    public void GenerateTargets()
    {
        targets = new List<int>();
        for (int i = 1; i <= players; i++)
        {
            for(int j = 0; j < 20; j++)
            {
                targets.Add(i);
            }
        }
    }




}
