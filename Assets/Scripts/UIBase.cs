using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public abstract class UIBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [Header ("General Settings")]
    public float delay = 0.5f;
    public float speed = 0.5f;
    [HideInInspector] public bool beenClicked = false;

    [Header ("On Hover Settings")]
    public Vector3 deltaScale = Vector3.zero;
    [HideInInspector] public Vector3 onHoverScale = Vector3.one;
    public LeanTweenType onHover = LeanTweenType.easeInElastic;

    [Header ("Off Hover Settings")]
    [HideInInspector] public Vector3 offHoverScale = Vector3.one;
    public LeanTweenType offHover = LeanTweenType.easeOutElastic;

    [Header ("On Click Settings")]
    public Vector3 onClickScale = Vector3.zero;
    public Vector3 deltaPosition = Vector3.zero;
    [HideInInspector] public Vector3 onClickPosition = Vector3.zero;
    public LeanTweenType onClick = LeanTweenType.easeOutElastic;
    public LeanTweenType onClickMove = LeanTweenType.easeOutElastic;

    public void OnPointerEnter(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, onHoverScale, speed).setDelay(delay).setEase(onHover);
    }

    public void OnPointerExit(PointerEventData data) {
        if (!beenClicked)
            LeanTween.scale(gameObject, offHoverScale, speed).setDelay(delay).setEase(offHover);
    }

    public abstract void OnClick();
}