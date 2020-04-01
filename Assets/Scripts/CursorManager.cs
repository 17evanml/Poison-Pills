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
    private Vector2 tempPos = new Vector2();
    public int cursorSize = 64;
    public Texture2D cursorTexture;


    void OnGUI()
    {
        GUI.color = color;
        tempPos = mousePosition;
        tempPos.x += Screen.width / 2;
        tempPos.y += Screen.height / 2;
        GUI.DrawTexture(new Rect(tempPos.x - (cursorSize / 2), tempPos.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
    }

    public void SetMousePosition(Vector2 _mousePosition)
    {
        mousePosition = _mousePosition;
    }
}
