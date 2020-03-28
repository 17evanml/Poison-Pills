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
    public Pill pillPoison, pillFake;

    public CupManager cupManager;
    public GameObject cup;
    private CupInfo oldCup;

    void Awake()
    {
        Cursor.visible = false;
        Color32 c = Random.ColorHSV();
        
        pillPoison = new Pill(cursorColor, c, true);
        //fix this later for complementary colors
        pillFake = new Pill(cursorColor, new Color32(c.g, c.r, c.b, 1), false);
    }

    private void LateUpdate()
    {
        UpdateCursorPosition();

        MouseRaycast();

        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnClick();
        }
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
            Debug.Log($"HOVERING: {hit.transform.gameObject.name}");
            CupInfo newCup = hit.transform.gameObject.GetComponent<CupInfo>();
            

            //If the gameobject has a cupInfo component
            if (newCup)
            {
                if(oldCup != newCup)
                {
                    newCup.OffHover();
                }
                oldCup = newCup;
                newCup.OnHover();
            }
        }
    }

    private void OnClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log($"CLICKED ON: {hit.transform.gameObject.name}");
            CupInfo cup = hit.transform.gameObject.GetComponent<CupInfo>();

            //If the gameobject has a cupInfo component
            if (cup)
            {
                cup.OnClick(this);
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
