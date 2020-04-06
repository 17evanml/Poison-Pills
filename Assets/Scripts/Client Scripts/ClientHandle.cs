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
        Color32 _color = _packet.ReadColor();
        GameManager.instance.SpawnCursor(_id, _username, _position, _rotation, _color);
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
        //Add cups to cupmanager
        Goal goal1 = _packet.ReadGoal();
        //Debug.Log(goal1);
        Goal goal2 = _packet.ReadGoal();
        //Debug.Log(goal2);
        GameManager.instance.GetComponent<GoalDisplay>().goal = goal1;
        UIManager.instance.gameMenu.SetActive(true); 
        GameManager.instance.GetComponent<GoalDisplay>().Initialize();

        CursorGameManager.Instance.CreateAllCups();
    }

    public static void ReceivePill(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Pill _pill = _packet.ReadPill();
        CupManager.Instance.cupInfos[_id].ReceivePill(_pill);
    }

    public static void UpdateAuthority(Packet _packet)
    {
        int size = _packet.ReadInt();
        bool[] authorities = new bool[size];
        for(int i = 0; i < authorities.Length; i++)
        {
            authorities[i] = _packet.ReadBool();
        }

        GameManager.cursors[Client.instance.myId].GetComponent<CursorController>().SetAuthorities(authorities);
    }

    public static void StartTurn(Packet _packet)
    {
        int currentPlayer = _packet.ReadInt();
        for(int i = 1; i <= GameManager.cursors.Count; i++)
        {
            if(currentPlayer == i)
            {
                GameManager.cursors[i].StartTurn();
            }
            else
            {
                GameManager.cursors[i].EndTurn();
            }
        }
    }
}
