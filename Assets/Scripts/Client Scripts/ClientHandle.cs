using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        // Now that we have the client's id, connect UDP
        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void SpawnCursor(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector3 _position = _packet.ReadVector3();
        Quaternion _rotation = _packet.ReadQuaternion();
        //Debug.Log("Receiving the color");
        Color32 _cursorColor = _packet.ReadColor();
        //Debug.Log(_cursorColor);
        Color32 _pill1Color = _packet.ReadColor();
        //Debug.Log(_pill1Color);
        Color32 _pill2Color = _packet.ReadColor();
        //Debug.Log(_pill2Color);
        GameManager.instance.SpawnCursor(_id, _username, _position, _rotation, _cursorColor, _pill1Color, _pill2Color);
    }

    public static void CursorPosition(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector3 _position = _packet.ReadVector3();
        //Debug.Log($"{_id} is at {_position}");
        if (GameManager.cursors.ContainsKey(_id))
        {
            GameManager.cursors[_id].SetMousePosition(_position);
        }
    }

    public static void PlayerRotation(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Quaternion _rotation = _packet.ReadQuaternion();

        GameManager.cursors[_id].transform.rotation = _rotation;
    }

    public static void PlayerDisconnect(Packet _packet)
    {
        int _id = _packet.ReadInt();
        print("delete");
        //ThreadManager.ExecuteOnMainThread(() =>
        //{
        Destroy(GameManager.cursors[_id].gameObject);
        //});
    }

    public static void BeginGame(Packet _packet)
    {
        GameManager.instance.CreateAllCups();
        Camera.main.GetComponent<CameraManager>().SwitchCameraPosition();
        UIManager.instance.SetOrderNumber();
        GameManager.instance.GenerateScoreList();
        UIManager.instance.PopulateMenuUI();
        UIManager.instance.PopulateEndScreen();
        UIManager.instance.WriteMenuUI();
    }

    public static void BeginSection(Packet _packet)
    {
        //Add cups to cupmanager
        Goal goal1 = _packet.ReadGoal();
        //Debug.Log(goal1);
        Goal goal2 = _packet.ReadGoal();
        //Debug.Log(goal2);
        UIManager.instance.goal_1 = goal1; // Sets the First Goal
        UIManager.instance.goal_2 = goal2; // Sets the Second Goal
        //UIManager.instance.GameDisplay.SetActive(true);
        UIManager.instance.InitializeGoals(); // Calls Initialize in Display Manager

        UIManager.instance.playerCount = GameManager.cursors.Count; // Sets the Number of Displays Needed
    }

    public static void ReceivePill(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Pill _pill = _packet.ReadPill();
        GameManager.cups[_id].ReceivePill(_pill);
    }

    public static void UpdateAuthority(Packet _packet)
    {
        int size = _packet.ReadInt();
        bool[] authorities = new bool[size];
        for (int i = 0; i < authorities.Length; i++)
        {
            authorities[i] = _packet.ReadBool();
        }

        GameManager.cursors[Client.instance.myId].GetComponent<CursorController>().SetAuthorities(authorities);
    }

    public static void StartTurn(Packet _packet)
    {
        int currentPlayer = _packet.ReadInt();
        TurnSystem.RoundType round = (TurnSystem.RoundType)_packet.ReadInt();
        UIManager.instance.UpdateCurrentPlayerColor(currentPlayer);
        GameManager.instance.SetCurrentPlayer(currentPlayer);
        for (int i = 1; i <= GameManager.cursors.Count; i++)
        {
            if (currentPlayer == i)
            {
                GameManager.instance.BeginTurn(i, round);
            }
            else
            {
                GameManager.cursors[i].EndTurn();
            }
        }
    }

    public static void EndRound(Packet _packet)
    {
        int players = _packet.ReadInt();
        int[] points = new int[players];
        bool[] deaths = new bool[players];
        for(int i = 0; i < points.Length; i++)
        {
            points[i] = _packet.ReadInt();
            deaths[i] = _packet.ReadBool();
        }
        Debug.Log("EndRound Clientside");
        GameManager.instance.UpdateScores(points, deaths);
    }


    public static void ServerClose(Packet _packet)
    {
        for(int i = 1; i <= GameManager.cursors.Count; i++)
        {
            Destroy(GameManager.cursors[i].gameObject);
            //Destroy(GameManager.cups[i]);
        }
        GameManager.cursors.Clear();

        Client.instance.Disconnect();
    }

    public static void RevealTarget(Packet _packet)
    {
        Goal goal = _packet.ReadGoal();
        UIManager.instance.WriteRevealedGoal(goal);
        //GameManager.instance.revealedGoals[0];
    }
}
