using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiOnClickDisappear : UIBase
{
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
        
    }

}
