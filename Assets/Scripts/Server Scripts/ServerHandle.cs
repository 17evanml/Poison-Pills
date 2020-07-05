using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerHandle
{
    public static void WelcomeReceived(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();

        Debug.Log($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        NetworkManager.instance.Connect();
        Server.clients[_fromClient].SendIntoGame(_username);
    }

    public static void CursorMovement(int _fromClient, Packet _packet)
    {
        Vector3 newPos = _packet.ReadVector3();
        //Debug.Log($"Received Position : {newPos}");
        Server.clients[_fromClient].cursor.SetPos(newPos);
    }

    public static void PlacePill(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        int _cupId = _packet.ReadInt();
        Pill _pill = _packet.ReadPill();
        //Debug.Log(_pill.pillColor);
        //Debug.Log(_pill.playerColor);
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        else if (NetworkManager.instance.turnSystem.GetPlayerAuthority(_clientIdCheck)[(int)TurnSystem.Actions.Place] == false)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) does not have authority to place a pill)");
        }
        else
        {
            Server.clients[_cupId].cup.AddPill(_pill);
            ServerSend.ReceivePill(Server.clients[_cupId].cursor, _pill);
            NetworkManager.instance.AdvanceTurn();
        }

    }

    public static void RevealTarget(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        Goal _goal = _packet.ReadGoal();
        //Debug.Log("Reveal");
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        else if (NetworkManager.instance.turnSystem.GetPlayerAuthority(_clientIdCheck)[(int)TurnSystem.Actions.Reveal] == false)
        {
            Debug.Log($"Player (ID: {_fromClient}) does not have authority to reveal their target)");
        }
        else
        {
            Debug.Log("Reveal Packet Received... Sending to clients");
            ServerSend.RevealTarget(_fromClient, _goal);
            NetworkManager.instance.AdvanceTurn();
        }

    }
}
