using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 cursorColor = new Color32();
    public Color32 pill1Color = new Color32();
    public Color32 pill2Color = new Color32();
    public Vector3 mousePosition;
    public Vector2 cursorSizes = new Vector2(64, 128);
    public Vector2 lerpPosition = new Vector2(0, 0);
    private Vector2 tempPos = new Vector2(0, 0);
    private Camera cam;
    
    public int cursorSize = 64;
    public Vector2 cursorOffset = new Vector2(20, 26);
    public Texture2D cursorTexture;

    private void Awake()
    {
        cam = Camera.main;
    }

    void OnGUI()
    {
        GUI.color = cursorColor;
        tempPos = cam.WorldToScreenPoint(mousePosition);
        tempPos += cursorOffset * (cursorSize/64);
        GUI.DrawTexture(new Rect(tempPos.x - (cursorSize / 2), tempPos.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);

        if (true) //change this to debug manager later
        {
            GUI.color = Color.black;
            GUI.Label(new Rect(tempPos.x - (cursorSize / 2), tempPos.y + (cursorSize / 2), cursorSize, cursorSize), username, CursorGameManager.Instance.textStyle);
        }
        

        /*
        if (Client.instance.myId == id)
        {

        }
        */
    }


    public void StartTurn()
    {
        SetCursorLarge();
        EnableSpotlight();
    }

    public void EndTurn()
    {
        SetCursorSmall();
        DisableSpotlight();
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

    public void SetMousePosition(Vector3 _mousePosition)
    {
        mousePosition = _mousePosition;
    }
}
