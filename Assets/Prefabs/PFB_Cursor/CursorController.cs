using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Vector2 cursorOffset = new Vector2(16, 16);

    public Pill pillPoison, pillFake;

    public CursorManager cursorManager;

    public CupManager cupManager;
    public GameObject cup;
    private CupInfo oldCup;

    public void Awake()
    {
        Cursor.visible = false;

        Color32 poisonColor = GetPoisonColor(cursorManager.id);
        Color32 fakeColor = GetFakeColor(cursorManager.id);
        pillPoison = new Pill(cursorManager.color, poisonColor, true);
        pillFake = new Pill(cursorManager.color, fakeColor, false);
    }

    public Color32 GetPoisonColor(int id)
    {
        //EVAN DO SERVER STUFF HERE
        return new Color32();
    }

    public Color32 GetFakeColor(int id)
    {
        //EVAN DO SERVER STUFF HERE
        return new Color32();
    }

    private void LateUpdate()
    {
        UpdateCursorPosition();

        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnClick();
        }
    }

    //Server manager needs to call functions
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
                if (oldCup != newCup)
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
        cursorManager.cursorSize = (int)cursorManager.cursorSizes.y;
    }

    private void SetCursorSmall()
    {
        cursorManager.cursorSize = (int)cursorManager.cursorSizes.x;
    }

    void UpdateCursorPosition()
    {
        //Get mouse position
        cursorManager.mousePosition.x = Input.mousePosition.x;
        cursorManager.mousePosition.y = Screen.height - Input.mousePosition.y;

        //Make it relative to center of the screen
        cursorManager.mousePosition.x -= Screen.width / 2;
        cursorManager.mousePosition.y -= Screen.height / 2;
        cursorManager.mousePosition += cursorOffset;

        //Send information to server
        ClientSend.CursorMovement(cursorManager.mousePosition);
    }
}
