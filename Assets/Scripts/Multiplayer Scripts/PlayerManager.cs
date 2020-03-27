using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;

    private void FixedUpdate()
    {
        SendInputToServer();
    }

    private void SendInputToServer()
    {
        bool[] _inputs = new bool[] {
        Input.GetKey(KeyCode.W),
        Input.GetKey(KeyCode.S),
        Input.GetKey(KeyCode.A),
        Input.GetKey(KeyCode.D)
        };
        ClientSend.PlayerMovement(_inputs);

    }

}
