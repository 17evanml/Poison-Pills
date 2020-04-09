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

    }

}
