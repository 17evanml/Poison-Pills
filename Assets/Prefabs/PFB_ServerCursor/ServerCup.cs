using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerCup : MonoBehaviour
{
    public int id;
    public int username;
    public Stack<Pill> pillStack;

    public void AddPill(Pill pill)
    {
        pillStack.Push(pill);

    }
}
