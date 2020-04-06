using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem
{
    private NetworkManager listener;
    private int numPlayers;
    private int currentPlayer;
    private bool dir = true;
    private bool[][] playerActions;
    private enum RoundType { Reveal, Place, End }
    public enum Actions { Reveal, Place }
    private RoundType round = RoundType.Reveal;
    public TurnSystem(int _numplayers, NetworkManager _listener)
    {
        listener = _listener;
        for (int i = 0; i < numPlayers; i++)
        {
            playerActions = new bool[numPlayers][];
            playerActions[i] = new bool[2];
            for (int j = 0; j < playerActions[i].Length; j++)
            {
                playerActions[i][j] = false;
            }
        }
        numPlayers = _numplayers;
        currentPlayer = 0;
        playerActions[0][0] = true;
    }

    public bool[] getPlayerAuthority(int playerId)
    {
        playerId--;
        return playerActions[playerId];
    }

    public bool[] AdvanceTurn()
    {
        if (round == RoundType.Reveal)
        {
            ResetAuthority(currentPlayer);
            NextPlayer();
            if (currentPlayer == 0)
            {
                AdvanceRound();
            }
            else
            {
                AuthorityOn(currentPlayer, Actions.Reveal);
            }
        }
        else if (round == RoundType.Place)
        {
            ResetAuthority(currentPlayer);
            NextPlayerBanana();
            if (currentPlayer == 0)
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
        }

        return playerActions[currentPlayer];
    }

    private void ResetAuthority(int playerIndex)
    {
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
        listener.PlayerAuthUpdate(playerIndex, playerActions[playerIndex + 1]);
    }
}
