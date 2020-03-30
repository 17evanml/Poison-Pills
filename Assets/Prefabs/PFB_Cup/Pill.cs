using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Representation of a Pill. </summary>
public class Pill {
    public Color32 playerColor;
    public Color32 pillColor;
    public bool poison;
    public PillRenderer pillRenderer;

    /// <summary> Representation of a Pill. </summary>
    /// <param name="playerColor"> Color of the Player. </param>
    /// <param name="pillColor"> Color of this specific pill. </param>
    /// <param name="poison"> If the pill is poisoned or not. </param>
    public Pill(Color32 playerColor, Color32 pillColor, bool poison) {
        this.playerColor = playerColor;
        this.pillColor = pillColor;
        this.poison = poison;
        pillRenderer = null;
    }

    /// <summary> Returns a string representation of a Pill. </summary>
    /// <returns> String representation of a Pill. </returns>
    public override string ToString() {
        return ($"Pill: playerColor: {playerColor}, pillColor: {pillColor}, poison: {poison}");
    }
}