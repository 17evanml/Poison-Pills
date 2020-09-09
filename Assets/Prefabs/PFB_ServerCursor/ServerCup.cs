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
        //Debug.Log($"Added a: {pill}");
        pillStack.Push(pill);
    }

    public void ResetPills()
    {
        pillStack.Clear();
    }

    public bool isAlive()
    {
        int poison = 0;
        foreach (Pill p in pillStack)
        {
            if (p.poison)
            {
                poison++;
            }
        }

        if (poison % 2 == 1)
        {
            return false;
        }
        return true;
    }
}
