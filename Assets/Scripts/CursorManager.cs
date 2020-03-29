using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 color;

    public Vector2 mousePosition; //SERVER NEEDS TO KNOW
    public Vector2 cursorSizes = new Vector2(64, 128);
    public int cursorSize = 64;
    public Texture2D cursorTexture;


    void OnGUI()
    {
        Debug.Log("Called onGUI");
        GUI.color = color;
        GUI.DrawTexture(new Rect(mousePosition.x - (cursorSize / 2), mousePosition.y - (cursorSize / 2), cursorSize, cursorSize), cursorTexture);
    }
}
