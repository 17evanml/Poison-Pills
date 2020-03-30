using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoOptionsHover : CupInteraction {
    public override void OnHover() {
        button1.transform.localScale = Vector3.one; /*Vector3.Lerp(button1.transform.localScale, originalScale, Time.deltaTime);*/
        button2.transform.localScale = Vector3.one; /*Vector3.Lerp(button2.transform.localScale, originalScale, Time.deltaTime);*/
    }

    public override void OffHover() {
        button1.transform.localScale = Vector3.zero; /*Vector3.Lerp(button1.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);*/
        button2.transform.localScale = Vector3.zero; /*Vector3.Lerp(button2.transform.localScale, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);*/
    }
}
