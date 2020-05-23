using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiOnClickChange : UIBase
{
    private bool state = false;
    public Vector3 position1 = Vector3.zero;
    public Vector3 position2 = Vector3.zero;
    public Vector3 scale1 = Vector3.zero;
    public Vector3 scale2 = Vector3.zero; 
        

    void Start()
    {
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
    }

    /// <summary>
    /// For UI that is supposed to change after clicking. Scale to custom set.
    /// </summary>
    public override void OnClick()
    {
        beenClicked = true;
        if (state)
        {
            LeanTween.move(gameObject, position1, 1f).setEase(onClickMoveType);
            LeanTween.scale(gameObject, scale1, speed).setDelay(delay).setEase(onClickType);
        }
        else
        {
            LeanTween.move(gameObject, position2, 1f).setEase(onClickMoveType);
            LeanTween.scale(gameObject, scale2, speed).setDelay(delay).setEase(onClickType);
        }
        state = !state; 
    }

}
