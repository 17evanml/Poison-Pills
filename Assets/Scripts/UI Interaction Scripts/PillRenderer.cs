using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Visual Representation of a Pill. </summary>
public class PillRenderer : MonoBehaviour {
    [Header ("Pill Settings")]
    public Image bottomCapsule, topCapsule;
    [HideInInspector] public Pill pill; // Pill Data
    

    /// <summary> Initialize Pill Renderer GameObject </summary>
    void Initialize() {

        // Debug.Log(topCapsule.color);
        bottomCapsule.color = pill.playerColor;
        topCapsule.color = pill.pillColor;
        //Debug.Log("BOTTOM COLOR: " + pill.playerColor);
        //Debug.Log("TOP COLOR: " + pill.pillColor);


        //Debug.Log("TOP COLOR 2: " + topCapsule.color);
        //Debug.Log("BOTTOM COLOR 2: " + bottomCapsule.color);
    }

    /// <summary> Debug Test Pill Renderer GameObject </summary>
    void DebugInitialize() {
        pill = new Pill((Color32) Color.red, (Color32) Color.yellow, true);
    }

    // Start is called before the first frame update
    void Start() {
        // DebugInitialize();
        Initialize();
    }

    void Update() {
    }

    public void BarMaximize() {

    }

    public void BarMinimize() {

    }

    public void PillMaximize() {

    }

    public void PillMinimize() {

    }


}
