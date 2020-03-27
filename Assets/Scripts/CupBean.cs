using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Pills { Red, Black };
public class CupBean
{
    private int playerID;
    private int redPillCount;
    private int blackPillCount;

    public CupBean(int playerIDInit)
    {
        playerID = playerIDInit;
        redPillCount = 0;
        blackPillCount = 0;
    }


    public bool addPill(Pills pillColor)
    {
        if(pillColor == Pills.Red)
        {
            redPillCount++;
            return true;
        } else if (pillColor == Pills.Black)
        {
            blackPillCount++;
            return true;
        }
        else
        {
            return false;

        }
    }

    public bool resetPills()
    {
        redPillCount = 0;
        blackPillCount = 0;
        return true;
    }
}
