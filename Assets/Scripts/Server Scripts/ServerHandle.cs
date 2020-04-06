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
        Vector2 newPos = _packet.ReadVector3();
        //Debug.Log($"Received Position : {newPos}");
        Server.clients[_fromClient].cursor.SetPos(newPos);
    }

    public static void PlacePill(int _fromClient, Packet _packet)
    {
        int _clientIdCheck = _packet.ReadInt();
        string _username = _packet.ReadString();
        int _cupId = _packet.ReadInt();
        Pill _pill = _packet.ReadPill();
        Debug.Log(_pill);
        Debug.Log(_cupId);
        if (_fromClient != _clientIdCheck)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
        }
        else if (NetworkManager.instance.turnSystem.getPlayerAuthority(_clientIdCheck)[(int)TurnSystem.Actions.Place] == false)
        {
            Debug.Log($"Player \"{_username}\" (ID: {_fromClient}) does not have authority to place a pill)");
        }
        else
        {
            Server.clients[_cupId].cup.AddPill(_pill);
            NetworkManager.instance.AdvanceTurn();
        }

    }
}
