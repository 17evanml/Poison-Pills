using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill
{
    Color32 playerColor;
    Color32 pillColor;
    bool poison;

    public Pill(Color32 playerColor, Color32 pillColor, bool poison)
    {
        this.playerColor = playerColor;
        this.pillColor = pillColor;
        this.poison = poison;
    }
}