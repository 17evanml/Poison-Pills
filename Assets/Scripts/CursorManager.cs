using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 color = new Color32(255, 255, 255, 255);

    public Vector2 mousePosition; //SERVER NEEDS TO KNOW
    public Vector2 cursorSizes = new Vector2(64, 128);
    public Vector2 lerpPosition = new Vector2(0, 0);
<<<<<<< HEAD
    private Vector2 tempPos = new Vector2(0, 0);
=======
    private Vector2 tempPos = new Vector2(0,0);
>>>>>>> c0ff0329c0cd9cbb7a2873a7bbaf6497043aa25d
    public int cursorSize = 64;
    public Texture2D cursorTexture;

    void OnGUI()
    {
        if(Client.instance.myId == id)
        {
            GUI.color = color;
            tempPos = mousePosition;
            tempPos.x *= Screen.width / 2;
            tempPos.y *= Screen.height / 2;
            tempPos.x += Screen.width / 2;
            tempPos.y += Screen.height / 2;
            tempPos += Vector2.one * 16;
            GUI.DrawTexture(new Rect(tempPos.x - (cursorSize / 2), tempPos.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
        }
        else
        {
            GUI.color = color;
            tempPos = (mousePosition + tempPos)/2;
            lerpPosition = tempPos;
            lerpPosition.x *= Screen.width / 2;
            lerpPosition.y *= Screen.height / 2;
            lerpPosition.x += Screen.width / 2;
            lerpPosition.y += Screen.height / 2;
            lerpPosition += Vector2.one * 16;
            GUI.DrawTexture(new Rect(lerpPosition.x - (cursorSize / 2), lerpPosition.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
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

    public void SetMousePosition(Vector2 _mousePosition)
    {
        mousePosition = _mousePosition;
    }
}
