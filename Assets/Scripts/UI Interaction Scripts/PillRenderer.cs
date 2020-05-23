using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Visual Representation of a Pill. </summary>
public class PillRenderer : MonoBehaviour {
    [Header ("Pill Settings")]
    [HideInInspector] public Pill pill; // Pill Data
    public Image bottomCapsule, topCapsule;
    // [Header ("Pilsl Settings")]
    // public Material mat;

    /// <summary> Initialize Pill Renderer GameObject </summary>
    void Initialize() {
        bottomCapsule.color = pill.pillColor;
        topCapsule.color = pill.playerColor;
    }

    /// <summary> Debug Test Pill Renderer GameObject </summary>
    void DebugInitialize() {
        pill = new Pill((Color32) Color.red, (Color32) Color.yellow, true);
    }


    // NEW STUFF
    // public Vector3 


    // private Material bottom, top; // Seperate Sprite Renderers for Bottom and Top Capsules

    // Start is called before the first frame update
    void Start() {
        DebugInitialize();
        Initialize();
    }

    void Update() {
        // Debug.Log(topCapsule.material.name);
        // Debug.Log(topCapsule.material.color);
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
