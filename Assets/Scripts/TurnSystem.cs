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
    private int startingPlayer;
    [SerializeField]
    private int startingPlayerBananaCount;
    private bool dir = true;
    private bool[][] playerActions;
    public enum RoundType { Setup, Reveal, Place, End }
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
        startingPlayer = 0;
        //Debug.Log((int)Actions.Reveal);
        playerActions[0][(int)Actions.Reveal] = true;
        NotifyManager(0);
        for (int i = 0; i < numPlayers; i++)
        {
            NotifyManager(i);
        }
    }

    public bool[] GetPlayerAuthority(int playerId)
    {
        //Translate from netcode ID(starting at 1) to internal ID(starting at 0)
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
            //Debug.Log(currentPlayer);
            if (currentPlayer == startingPlayer)
            {
                AdvanceRound();
                startingPlayerBananaCount = 0;
                AuthorityOn(currentPlayer, Actions.Place);
            }
            else
            {
                AuthorityOn(currentPlayer, Actions.Reveal);
            }
        }
        else if (round == RoundType.Place)
        {
            //Debug.Log(round);
            ResetAuthority(currentPlayer);
            NextPlayerBanana();
            if (currentPlayer < 0)
            {
                AdvanceRound();
                currentPlayer = 0;
                AdvanceTurn();
            }
            else
            {
                AuthorityOn(currentPlayer, Actions.Place);
            }
        }
        else if (round == RoundType.End)
        {
            ResetAuthority(currentPlayer);
            listener.CalculatePoints();
            NewGame();
            /*// Debug Only
            for (int i = 0; i < numPlayers; i++)
            {
                Debug.Log($"Player {i+1}: {NetworkManager.instance.playerPoints[i+1]}");
            }
            */


        }

        return playerActions[currentPlayer];
    }

    public RoundType GetRound()
    {
        return round;
    }
    private void ResetAuthority(int playerIndex)
    {
        Debug.Log($"resetting authority for player {playerIndex+1}");
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
        currentPlayer = cyclicAdd(currentPlayer, numPlayers - 1);
    }

    private void NextPlayerBanana()
    {


        if (currentPlayer == startingPlayer)
        {
            if(startingPlayerBananaCount == 2)
            {
                currentPlayer = -1;
                return;
            }
            startingPlayerBananaCount++;
            dir = !dir;
            currentPlayer = cyclicSubtract(currentPlayer, numPlayers - 1);
        }
        else if (dir)
        {
            currentPlayer = cyclicAdd(currentPlayer, numPlayers - 1);
        }
        else
        {
            currentPlayer = cyclicSubtract(currentPlayer, numPlayers - 1);
        }
    }

    private void NewGame()
    {
        startingPlayer++;
        if (startingPlayer == numPlayers)
        {
            //End the game entirely
        }
        else
        {
            currentPlayer = startingPlayer;
            round = RoundType.Reveal;
            for (int i = 0; i < numPlayers; i++)
            {
                if (i == startingPlayer)
                {
                    AuthorityOn(i, Actions.Reveal);
                }
                else
                {
                    ResetAuthority(i);
                }
            }
        }
    }

    private void NotifyManager(int playerIndex)
    {
        //Translate from internal ID (starting at 0) to netcode ID (starting at 1)
        playerIndex++;
        listener.PlayerAuthUpdate(playerIndex, playerActions[playerIndex - 1]);
    }

    private void NotifyManager(int[] points)
    {

    }

    private int cyclicAdd(int number, int max)
    {
        number++;
        return cyclicNumber(number, max);
    }

    private int cyclicSubtract(int number, int max)
    {
        number--;
        return cyclicNumber(number, max);
    }

    private int cyclicNumber(int number, int max)
    {
        if (number < 0)
        {
            number = max;
        }
        else if (number > max)
        {
            number = 0;
        }
        return number;
    }


}
