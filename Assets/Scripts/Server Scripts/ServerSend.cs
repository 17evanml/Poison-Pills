using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerSend
{
    /// <summary>Sends a packet to a client via TCP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendTCPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].tcp.SendData(_packet);
    }

    /// <summary>Sends a packet to a client via UDP.</summary>
    /// <param name="_toClient">The client to send the packet the packet to.</param>
    /// <param name="_packet">The packet to send to the client.</param>
    private static void SendUDPData(int _toClient, Packet _packet)
    {
        _packet.WriteLength();
        Server.clients[_toClient].udp.SendData(_packet);
    }

    /// <summary>Sends a packet to all clients via TCP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].tcp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via TCP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
    }

    /// <summary>Sends a packet to all clients via UDP.</summary>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            Server.clients[i].udp.SendData(_packet);
        }
    }
    /// <summary>Sends a packet to all clients except one via UDP.</summary>
    /// <param name="_exceptClient">The client to NOT send the data to.</param>
    /// <param name="_packet">The packet to send.</param>
    private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
    {
        _packet.WriteLength();
        for (int i = 1; i <= Server.MaxPlayers; i++)
        {
            if (i != _exceptClient)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
    }

    #region Packets
    /// <summary>Sends a welcome message to the given client.</summary>
    /// <param name="_toClient">The client to send the packet to.</param>
    /// <param name="_msg">The message to send.</param>
    public static void Welcome(int _toClient, string _msg)
    {
        using (Packet _packet = new Packet((int)ServerPackets.welcome))
        {
            _packet.Write(_msg);
            _packet.Write(_toClient);

            SendTCPData(_toClient, _packet);
        }
    }

    /// <summary>Tells a client to spawn a player.</summary>
    /// <param name="_toClient">The client that should spawn the player.</param>
    /// <param name="_cursor">The player to spawn.</param>
    public static void SpawnCursor(int _toClient, ServerCursor _cursor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.spawnCursor))
        {
            _packet.Write(_cursor.id);
            _packet.Write(_cursor.username);
            _packet.Write(_cursor.transform.position);
            _packet.Write(_cursor.transform.rotation);
            _packet.Write(_cursor.cursorColor);
            Debug.Log("Sending the color");
            Debug.Log(_cursor.cursorColor);
            _packet.Write(_cursor.pill1Color);
            Debug.Log(_cursor.pill1Color);
            _packet.Write(_cursor.pill2Color);
            Debug.Log(_cursor.pill2Color);
            SendTCPData(_toClient, _packet);
        }
    }

    /// <summary>Sends a player's updated position to all clients.</summary>
    /// <param name="_player">The player whose position to update.</param>
    public static void CursorPosition(ServerCursor _cursor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
        {
            _packet.Write(_cursor.id);
            _packet.Write(_cursor.cursorPos);

            SendUDPDataToAll(_cursor.id, _packet);
        }
    }

    public static void PlayerDisconnect(int _toClient, ServerCursor _cursor)
    {
        using (Packet _packet = new Packet((int)ServerPackets.playerDisconnect))
        {
            _packet.Write(_cursor.id);
            SendTCPDataToAll(_cursor.id, _packet);
        }
    }

    public static void BeginGame(ServerCursor _cursor, Goal g1, Goal g2)
    {
        using (Packet _packet = new Packet((int)ServerPackets.beginGame))
        {
            _packet.Write(g1);
            _packet.Write(g2);
            //Add all cups to cup manager for each player
            SendTCPData(_cursor.id, _packet);
        }
    }

    public static void ReceivePill(ServerCursor _cursor, Pill _pill)
    {
        using (Packet _packet = new Packet((int)ServerPackets.receivePill))
        {
            _packet.Write(_cursor.id);
            _packet.Write(_pill);

            SendTCPDataToAll(_packet);
        }
    }

    public static void UpdateAuthority(int _toClient, bool[] authorities)
    {
        using (Packet _packet = new Packet((int)ServerPackets.updateAuthority))
        {
            _packet.Write(authorities.Length);
            for (int i = 0; i < authorities.Length; i++)
            {
                _packet.Write(authorities[i]);
            }
            SendTCPData(_toClient, _packet);
        }

    }

    public static void StartTurn(int _toClient)
    {
        using (Packet _packet = new Packet((int)ServerPackets.startTurn))
        {
            _packet.Write(_toClient);

            SendTCPDataToAll(_packet);
        }

    }

    public static void EndRound(int[] points, bool[] deaths)
    {
        using (Packet _packet = new Packet((int)ServerPackets.endRound))
        {
            _packet.Write(points.Length);
            for(int i = 0; i < points.Length; i++)
            {
                _packet.Write(points[i]);
                _packet.Write(deaths[i]);
            }
            SendTCPDataToAll(_packet);
        }
    }


    public static void ServerClose()
    {
        using (Packet _packet = new Packet((int)ServerPackets.serverClose))
        {
            SendTCPDataToAll(_packet);
        }
    }

    #endregion
}
