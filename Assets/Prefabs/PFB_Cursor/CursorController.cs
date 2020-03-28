using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 cursorSizes = new Vector2(64, 128);
    private int cursorSize = 64;
    public Vector2 cursorOffset = new Vector2(1, 1);
    private Vector2 mousePosition;
    public Color32 cursorColor;

    public CupManager cupManager;
    public GameObject cup;
    private CupInfo oldCup;

    void Start()
    {
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        UpdateCursorPosition();

        MouseRaycast();
    }

    private void StartTurn()
    {
        SetCursorLarge();
        EnableSpotlight();
    }

    private void EndTurn()
    {
        SetCursorSmall();
        DisableSpotlight();
    }

    private void MouseRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log($"HIT: {hit.transform.gameObject.name}");
            CupInfo cupInfo = hit.transform.gameObject.GetComponent<CupInfo>();
            
            //If the gameobject has a cupInfo component
            if (cupInfo)
            {
                if(oldCup != cupInfo)
                {
                    cupInfo.OffHover();
                }
                oldCup = cupInfo;
                cupInfo.OnHover();
            }
        }
    }

    private void EnableSpotlight()
    {

    }

    private void DisableSpotlight()
    {

    }

    private void SetCursorLarge()
    {
        cursorSize = (int)cursorSizes.y;
    }

    private void SetCursorSmall()
    {
        cursorSize = (int)cursorSizes.x;
    }

    void UpdateCursorPosition()
    {
        mousePosition.x = Input.mousePosition.x;
        mousePosition.y = Screen.height - Input.mousePosition.y;
        mousePosition += cursorOffset;
    }

    void OnGUI()
    {
        GUI.color = cursorColor;
        GUI.DrawTexture(new Rect(mousePosition.x - (cursorSize / 2), mousePosition.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
    }
}
