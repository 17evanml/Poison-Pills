using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Visual Representation of a Pill. </summary>
public class PillRenderer : MonoBehaviour {
    public Pill pill; // Pill Data
    public SpriteRenderer bottomCapsule, topCapsule; // Seperate Sprite Renderers for Bottom and Top Capsules

    // Start is called before the first frame update
    void Start() {
        bottomCapsule.color = pill.pillColor;
        topCapsule.color = pill.playerColor;
    }
}
