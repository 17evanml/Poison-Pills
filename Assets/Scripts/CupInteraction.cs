using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CupInteraction : MonoBehaviour {
    public Vector3 button1Offset;
    public Vector3 button2Offset;
    public GameObject buttonPrefab;
    public GameObject button1;
    public GameObject button2;
    public Vector3 originalScale;
    public BoxCollider2D boxCollider;
    public bool hover = false;


    // Start is called before the first frame update
    void Start() {
        button1 = Instantiate(buttonPrefab, this.transform.position + button1Offset, Quaternion.identity);
        button2 = Instantiate(buttonPrefab, this.transform.position + button2Offset, Quaternion.identity);
        originalScale = button1.transform.localScale;
        button1.transform.localScale = Vector3.zero;
        button2.transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        // if (hover) { OnHover(); }
        // else { OffHover(); }
    }

    public abstract void OnHover();

    public abstract void OffHover();

    void OnMouseEnter() { hover = true; OnHover(); boxCollider.size *= 5; }

    void OnMouseExit() { hover = false; OffHover(); boxCollider.size /= 5; }
}
