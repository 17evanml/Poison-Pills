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

    private Camera cam;

    public bool[] authorities = { false, false };
    public enum Actions { Reveal, Place }

    public void Awake()
    {
        cam = Camera.main;
        Cursor.visible = false;
        //Color32 poisonColor = GetPoisonColor(cursorManager.id);
        //Color32 fakeColor = GetFakeColor(cursorManager.id);
        pillPoison = new Pill(cursorManager.pill1Color, cursorManager.cursorColor, true);
        pillFake = new Pill(cursorManager.pill2Color, cursorManager.cursorColor, false);
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
                if (authorities[(int)Actions.Place])
                {
                    cup.OnClick(this);
                }
                else
                {
                    Debug.Log("Not your turn to place a pill");

                }
            }
            else
            {
                if (authorities[(int)Actions.Reveal])
                {
                    ClientSend.RevealTarget(1);
                }
                else
                {
                    Debug.Log("not your turn to reveal");
                }
            }
        }
    }

    void UpdateCursorPosition()
    {
        //Get mouse position
        float mouseX = Input.mousePosition.x;
        float mouseY = Screen.height - Input.mousePosition.y;

        //Get world coordinates of cursor
        cursorManager.mousePosition = cam.ScreenToWorldPoint(new Vector3(mouseX, mouseY, cam.nearClipPlane));

        //Send information to server
        ClientSend.CursorMovement(cursorManager.mousePosition);
    }

    public void SetAuthorities(bool[] _authorities)
    {
        authorities = _authorities;
    }
}