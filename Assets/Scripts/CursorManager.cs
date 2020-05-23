using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 color = new Color32(255, 255, 255, 255);

    public Vector3 mousePosition;
    public Vector2 cursorSizes = new Vector2(64, 128);
    public Vector2 lerpPosition = new Vector2(0, 0);
    private Vector2 tempPos = new Vector2(0, 0);
    private Camera cam;
    
    public int cursorSize = 64;
    public Texture2D cursorTexture;

    private void Awake()
    {
        cam = Camera.main;
    }

    void OnGUI()
    {
        if(Client.instance.myId == id)
        {
            GUI.color = color;
            tempPos = cam.WorldToScreenPoint(mousePosition);
            tempPos += Vector2.one * 16;
            GUI.DrawTexture(new Rect(tempPos.x - (cursorSize / 2), tempPos.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
        }
        else
        {
            GUI.color = color;
            tempPos = cam.WorldToScreenPoint(mousePosition);
            tempPos += Vector2.one * 16;
            GUI.DrawTexture(new Rect(tempPos.x - (cursorSize / 2), tempPos.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
            //lerpPosition = tempPos;
            //lerpPosition /= 2;
            //lerpPosition += Vector2.one * 16;
            //GUI.DrawTexture(new Rect(lerpPosition.x - (cursorSize / 2), lerpPosition.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
        }
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
