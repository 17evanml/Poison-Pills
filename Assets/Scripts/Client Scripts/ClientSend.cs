using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    /// <summary>Sends a packet to the server via TCP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to the server via UDP.</summary>
    /// <param name="_packet">The packet to send to the sever.</param>
    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    /// <summary>Lets the server know that the welcome message was received.</summary>
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    /// <summary>Sends player input to the server.</summary>
    /// <param name="_inputs"></param>
    public static void CursorMovement(Vector3 _position)
    {
        using (Packet _packet = new Packet((int)ClientPackets.cursorMovement))
        {
            //print("sent position");
            _packet.Write(_position);
            //_packet.Write(GameManager.cups[Client.instance.myId].transform.rotation);

            SendUDPData(_packet);
        }
    }

    public static void PlacePill(Pill _pill, CupInfo _cup)
    {
        Debug.Log("Send placepill packet");
        using (Packet _packet = new Packet((int)ClientPackets.placePill))
        {
            //Debug.Log($"Pill Colors: Pill: {_pill.pillColor}, Player {_pill.playerColor} ");
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);
            _packet.Write(_cup.id);
            _packet.Write(_pill);

            SendTCPData(_packet);
        }

    }

    public static void RevealTarget(Goal _target)
    {
        Debug.Log("Send Reveal packet");
        using (Packet _packet = new Packet((int)ClientPackets.revealTarget))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(_target);
            SendTCPData(_packet);
        }

    }
    #endregion
}
