using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCursor : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 cursorColor;
    public Color32 pill1Color;
    public Color32 pill2Color;

    public Vector3 cursorPos = Vector2.zero;

    public void Initialize(int _id, string _username, Color32 _cursorColor, Color32 _pill1Color, Color32 _pill2Color)
    {
        id = _id;
        username = _username;
        cursorColor = _cursorColor;
        pill1Color = _pill1Color;
        pill2Color = _pill2Color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ServerSend.CursorPosition(this);
    }

    public void SetPos(Vector3 _cursorPos)
    {
        cursorPos = _cursorPos;
    }
}
