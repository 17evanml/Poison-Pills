using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill
{
    public Color32 playerColor;
    public Color32 pillColor;
    public bool poison;
    public PillRenderer pillRenderer;

    public Pill(Color32 playerColor, Color32 pillColor, bool poison) {
        this.playerColor = playerColor;
        this.pillColor = pillColor;
        this.poison = poison;
        pillRenderer = null;
    }
}