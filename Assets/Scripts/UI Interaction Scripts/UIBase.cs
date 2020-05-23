using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public abstract class UIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header ("General Settings")]
    public float delay = 0.1f;
    public float speed = 0.1f;
    [HideInInspector] public bool beenClicked = false;

    [Header ("On Hover Settings")]
    public Vector3 deltaScale = Vector3.zero;
    [HideInInspector] public Vector3 onHoverScale = Vector3.one;
    public LeanTweenType onHoverType = LeanTweenType.easeInElastic;

    [Header ("Off Hover Settings")]
    [HideInInspector] public Vector3 offHoverScale = Vector3.one;
    public LeanTweenType offHoverType = LeanTweenType.easeOutElastic;

    [Header ("On Click Settings")]
    public LeanTweenType onClickType = LeanTweenType.easeOutElastic;
    public LeanTweenType onClickMoveType = LeanTweenType.easeOutElastic;

    public void OnPointerEnter(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, onHoverScale, speed).setDelay(delay).setEase(onHoverType);
    }

    public void OnPointerExit(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, offHoverScale, speed).setDelay(delay).setEase(offHoverType);
    }

    public abstract void OnClick();
}