using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : ButtonInteraction {
    public override void OnHover() {
        transform.localScale = /*Vector3.Lerp(transform.localScale, originalScale * 1.2f, Time.deltaTime);*/ new Vector3(0.6f, 0.6f, 0.6f);
    }

    public override void OffHover() {
        transform.localScale = /*Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime);*/ Vector3.zero;
    }

    public override void OnClick() {
        // Call Philippes Function
        Debug.Log("Did the Thing");
    }
}
