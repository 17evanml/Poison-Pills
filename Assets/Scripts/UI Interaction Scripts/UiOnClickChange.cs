using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> UI Script used only for Pill Info Buttons. </summary>
public class UiOnClickChange : UIBase {
    private bool state = false;
    public Vector3 position1 = Vector3.zero;
    public Vector3 position2 = Vector3.zero;
    public Vector3 scale1 = Vector3.zero;
    public Vector3 scale2 = Vector3.zero; 
    private Vector3 currentLocation = Vector3.zero;
    public Vector3 pillRendererScale = Vector3.one;
    public List<GameObject> pillRenderers = new List<GameObject>();
        

    void Start() {
        currentLocation = transform.position;
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
    }

    /// <summary>
    /// For UI that is supposed to change after clicking. Scale to custom set.
    /// </summary>
    public override void OnClick() {
        beenClicked = true;
        if (state)
        {
            LeanTween.moveLocal(gameObject, position1, 1f).setEase(onClickMoveType);
            LeanTween.scale(gameObject, scale1, speed).setDelay(delay).setEase(onClickType);
            for (int i = 0; i < pillRenderers.Count; i++) {
                LeanTween.scale(pillRenderers[i], pillRendererScale, speed).setDelay(delay).setEase(onClickType);
            }
        }
        else
        {
            LeanTween.moveLocal(gameObject, position2, 1f).setEase(onClickMoveType);
            LeanTween.scale(gameObject, scale2, speed).setDelay(delay).setEase(onClickType);
            for (int i = 0; i < pillRenderers.Count; i++) {
                LeanTween.scale(pillRenderers[i], Vector3.zero, speed).setDelay(delay).setEase(onClickType);
            }
        }
        state = !state; 
    }
}
