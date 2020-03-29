using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillRenderer : MonoBehaviour {
    public Pill pill;
    public SpriteRenderer bottomCapsule, topCapsule;

    // Start is called before the first frame update
    void Start() {


        bottomCapsule.color = pill.pillColor;
        topCapsule.color = pill.playerColor;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
