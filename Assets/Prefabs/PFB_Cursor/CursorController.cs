using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Vector2 cursorSizes = new Vector2(64, 128);
    private int cursorSize = 64;
    public Vector2 cursorOffset = new Vector2(1, 1);
    private Vector2 mousePosition; //SERVER NEEDS TO KNOW

   
    private int id; //ASSIGNED IN LOGIN
    private string playerName; //ASSIGNED IN LOGIN
    public Color32 playerColor; //SERVER NEEDS TO ASSIGN

    public Pill pillPoison, pillFake;

    public CupManager cupManager;
    public GameObject cup;
    private CupInfo oldCup;

    public void Initialize(int id, Color32 playerColor, string playerName)
    {
        this.id = id;
        this.playerColor = playerColor;
        this.playerName = playerName;

        Cursor.visible = false;

        Color32 poisonColor = GetPoisonColor(id);
        Color32 fakeColor = GetFakeColor(id);
        pillPoison = new Pill(playerColor, poisonColor, true);
        pillFake = new Pill(playerColor, fakeColor, false);
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

    private void Start()
    {
        Initialize(1, new Color32(255, 0, 0, 255), "bill");
    }

    private void LateUpdate()
    {
        UpdateCursorPosition();

        //MouseRaycast();

        if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
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

        //SEND INFO TO SERVER
    }

    void OnGUI()
    {
        GUI.color = playerColor;
        GUI.DrawTexture(new Rect(mousePosition.x - (cursorSize / 2), mousePosition.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
    }
}
