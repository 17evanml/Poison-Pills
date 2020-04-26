using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiOnClickChange : UIBase
{

    void Start()
    {
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
        onClickPosition = transform.position + deltaPosition;
    }

    /// <summary>
    /// For UI that is supposed to change after clicking. Scale to custom set.
    /// </summary>
    public override void OnClick()
    {
        var seq = LeanTween.sequence();
        seq.append(0.1f); // delay
        seq.append(() => { // fire an event before start
            Debug.Log("I have started");
        });
        //seq.append(LeanTween.move(gameObject, deltaPosition, 1f));
        seq.append(LeanTween.scale(gameObject, Vector3.zero, speed).setDelay(delay).setEase(onClick));
        seq.append(() => { // fire event after tween
            Debug.Log("We are done now");
        }); ;
    }

}
