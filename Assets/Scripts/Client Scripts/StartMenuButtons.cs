using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class StartMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;  

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontStyle = TMPro.FontStyles.Bold;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontStyle = TMPro.FontStyles.Normal;
    }
}
