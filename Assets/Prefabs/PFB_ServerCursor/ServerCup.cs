using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCup : MonoBehaviour
{
    public int id;
    public string username;
    public Stack<Pill> pillStack;

    public void Initialize(int _id, string _username)
    {
        id = _id;
        username = _username;

    }

    private void Awake()
    {
        pillStack = new Stack<Pill>();
    }

    public void AddPill(Pill pill)
    {
        Debug.Log($"Added a: {pill}");
        pillStack.Push(pill);

    }
}
