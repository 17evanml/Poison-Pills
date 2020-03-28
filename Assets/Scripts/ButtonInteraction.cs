using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ButtonInteraction : MonoBehaviour {
    public bool buttonHover = false;
    public Vector3 originalScale;

    // Start is called before the first frame update
    void Start() {
        originalScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        // if (hover) { OnHover(); Debug.Log("ON HOVER"); }
        // else { OffHover(); }
    }

    public abstract void OnHover();

    public abstract void OffHover();

    void OnMouseEnter() { buttonHover = true; OnHover(); }

    void OnMouseExit() { buttonHover = false; OffHover(); }

    public abstract void OnClick();

    void OnMouseDown() { OnClick(); }
}
