using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiOnClickDisappear : UIBase
{
    [HideInInspector] public Vector3 onClickPosition = Vector3.zero;
    public Vector3 onClickScale = Vector3.zero;
    public Vector3 deltaPosition = Vector3.zero;

    void Start()
    {
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
        onClickPosition = transform.position + deltaPosition;
    }

    /// <summary>
    /// For UI that is supposed to disappear after clicking. Scale down to 0.
    /// </summary>
    public override void OnClick()
    {
        beenClicked = true;
        LeanTween.move(gameObject, onClickPosition, 1f).setEase(onClickMoveType);
        LeanTween.scale(gameObject, onClickScale, speed).setDelay(delay).setEase(onClickType).setDestroyOnComplete(true);
    }

}
