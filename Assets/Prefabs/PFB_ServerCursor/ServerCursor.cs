using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCursor : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 color;

    public Vector2 cursorPos = Vector2.zero;

    public void Initialize(int _id, string _username, Color32 _color)
    {
        id = _id;
        username = _username;
        color = _color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ServerSend.CursorPosition(this);
    }

    public void SetPos(Vector2 _cursorPos)
    {
        cursorPos = _cursorPos;
    }
}
