using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCup : MonoBehaviour
{
    public int id;
    public int username;
    public Stack<Pill> pillStack;

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
