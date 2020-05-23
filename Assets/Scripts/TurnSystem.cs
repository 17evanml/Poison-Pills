using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TurnSystem
{
    private NetworkManager listener;
    private int numPlayers;
    [SerializeField]
    private int currentPlayer;
    private bool dir = true;
    private bool[][] playerActions;
    private enum RoundType { Reveal, Place, End }
    public enum Actions { Reveal, Place }
    [SerializeField]
    private RoundType round = RoundType.Reveal;

    public void debugSetRound(int _round)
    {
        round = (TurnSystem.RoundType)_round;
        currentPlayer = 0;
        AuthorityOn(currentPlayer, Actions.Place);
        Debug.Log(round);

    }

    public TurnSystem(int _numPlayers, NetworkManager _listener)
    {
        listener = _listener;
        numPlayers = _numPlayers;
        playerActions = new bool[numPlayers][];

        for (int i = 0; i < numPlayers; i++)
        {
            playerActions[i] = new bool[2];
            for (int j = 0; j < playerActions[i].Length; j++)
            {
                playerActions[i][j] = false;
            }
        }
        currentPlayer = 0;
        Debug.Log((int)Actions.Reveal);
        playerActions[0][(int)Actions.Reveal] = true;
        NotifyManager(0);
        for (int i = 0; i < numPlayers; i++)
        {
            NotifyManager(i);
        }
    }

    public bool[] GetPlayerAuthority(int playerId)
    {
        playerId--;
        return playerActions[playerId];
    }

    public int GetCurrentPlayer()
    {
        return currentPlayer + 1;
    }

    public bool[] AdvanceTurn()
    {
        if (round == RoundType.Reveal)
        {
            ResetAuthority(currentPlayer);
            NextPlayer();
            Debug.Log(currentPlayer);
            if (currentPlayer == 0)
            {
                AdvanceRound();
                AuthorityOn(currentPlayer, Actions.Place);
            }
            else
            {
                AuthorityOn(currentPlayer, Actions.Reveal);
            }
        }
        else if (round == RoundType.Place)
        {
            Debug.Log(round);
            ResetAuthority(currentPlayer);
            NextPlayerBanana();
            if (currentPlayer == -1)
            {
                AdvanceRound();
            }
            else
            {
                AuthorityOn(currentPlayer, Actions.Place);
            }
        }
        else if (round == RoundType.End)
        {
            ResetAuthority(currentPlayer);
            NetworkManager.instance.CalculatePoints();
            for (int i = 0; i < numPlayers; i++)
            {
                Debug.Log($"Player {i+1}: {NetworkManager.instance.playerPoints[i+1]}");
            }
        }

        return playerActions[currentPlayer];
    }

    private void ResetAuthority(int playerIndex)
    {
        Debug.Log($"resetting authority for player {playerIndex}");
        for (int i = 0; i < playerActions[playerIndex].Length; i++)
        {
            playerActions[playerIndex][i] = false;
        }
        NotifyManager(playerIndex);
    }

    private void AuthorityOn(int playerIndex, params Actions[] authorities)
    {
        for (int i = 0; i < authorities.Length; i++)
        {
            if (authorities[i] == Actions.Reveal)
            {
                playerActions[playerIndex][(int)Actions.Reveal] = true;
            }
            else if (authorities[i] == Actions.Place)
            {
                playerActions[playerIndex][(int)Actions.Place] = true;
            }
        }
        NotifyManager(playerIndex);
    }

    private void AdvanceRound()
    {
        round++;
    }

    private void NextPlayer()
    {
        currentPlayer++;
        if (currentPlayer >= numPlayers)
        {
            currentPlayer = 0;
        }
    }

    private void NextPlayerBanana()
    {
        if (dir)
        {
            currentPlayer++;
        }
        else
        {
            currentPlayer--;
        }

        if (currentPlayer >= numPlayers)
        {
            dir = !dir;
            currentPlayer--;
        }
    }

    private void NotifyManager(int playerIndex)
    {
        playerIndex++;
        listener.PlayerAuthUpdate(playerIndex, playerActions[playerIndex - 1]);
    }
}
