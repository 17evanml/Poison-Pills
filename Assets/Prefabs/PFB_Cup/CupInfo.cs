using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupInfo : MonoBehaviour
{
    private int id;
    private string username;
    private Color32 color;

    private Pill tempFakePill;
    private Pill tempPoisonPill;

    private Stack<Pill> pillStack;

    public bool hover = false;

    CupInfo(int id, string name, Color32 color)
    {
        this.id = id;
        this.username = name;
        //this.color = color;
    }

    public void Initialize(int id, string name, Color32 color)
    {
        this.id = id;
        this.username = name;
        this.color = color;
        pillStack = new Stack<Pill>();
    }

    public void SetValues(int id, string name, Color32 color)
    {
        this.id = id;
        //this.username = name;
        this.color = color;
    }

    public void OnHover()
    {
        //Outline cup?
        //Set cup bool onHover to be true
    }

    public void OffHover()
    {
        //Set cup bool onHover to be false
    }

    public void OnClick(CursorController player)
    {
        //Notify server that you clicked a cup
        //Call the button display script
        //TODO: Moves buttons to right place (based on offset)
        //SETS background button to be OFFCLICK for this CUPINFO class
        CursorGameManager.Instance.offClick.onClick.AddListener(OffClick);
        //SET RAND LEFT OR RIGHT BUTTON TO BE AddPill(player.poisonPill)
        tempFakePill = player.pillFake;
        tempPoisonPill = player.pillPoison;
        CursorGameManager.Instance.poisonClick.onClick.AddListener(AddPoisonPill);
        CursorGameManager.Instance.fakeClick.onClick.AddListener(AddFakePill);
        //Enables UI
        CursorGameManager.Instance.canvas.SetActive(true);
    }

    public void OffClick()
    {
        //Notify Server that you clicked off 

        tempFakePill = null;
        tempPoisonPill = null;

        CursorGameManager.Instance.offClick.onClick.RemoveListener(OffClick);
        CursorGameManager.Instance.poisonClick.onClick.RemoveListener(AddPoisonPill);
        CursorGameManager.Instance.fakeClick.onClick.RemoveListener(AddFakePill);
        //DISABLES UI
        CursorGameManager.Instance.canvas.SetActive(false);
        
    }

    public void AddPoisonPill()
    {
        if (tempPoisonPill != null)
        {
            AddPill(tempPoisonPill);
        }
        else
        {
            Debug.LogError("TempPoisonPill in CupInfo is null");
        }
    }

    public void AddFakePill()
    {
        if (tempFakePill != null)
        {
            AddPill(tempFakePill);
        }
        else
        {
            Debug.LogError("TempFakePill in CupInfo is null");
        }
    }

    public void AddPill(Pill pill)
    {
        pillStack.Push(pill);
        OffClick();
        CursorGameManager.Instance.NextTurn();
    }

    //OPERATOR OVERLOADING
    public override bool Equals(object obj)
    {
        return this.Equals(obj as CupInfo);
    }

    public bool Equals(CupInfo c)
    {
        // If parameter is null, return false.
        if (Object.ReferenceEquals(c, null))
        {
            return false;
        }

        // Optimization for a common success case.
        if (Object.ReferenceEquals(this, c))
        {
            return true;
        }

        // If run-time types are not exactly the same, return false.
        if (this.GetType() != c.GetType())
        {
            return false;
        }

        // Return true if the fields match.
        // Note that the base class is not invoked because it is
        // System.Object, which defines Equals as reference equality.
        return (id == c.id);
    }

    public override int GetHashCode()
    {
        return id * 0x00010000;
    }

    public static bool operator ==(CupInfo lhs, CupInfo rhs)
    {
        // Check for null on left side.
        if (Object.ReferenceEquals(lhs, null))
        {
            if (Object.ReferenceEquals(rhs, null))
            {
                // null == null = true.
                return true;
            }

            // Only the left side is null.
            return false;
        }
        // Equals handles case of null on right side.
        return lhs.Equals(rhs);
    }

    public static bool operator !=(CupInfo lhs, CupInfo rhs)
    {
        return !(lhs == rhs);
    }
}
