using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupInfo : MonoBehaviour
{
    public int id;
    public string username;
    public Color32 color;

    private Pill tempFakePill;
    private Pill tempPoisonPill;

    private Stack<Pill> pillStack;

    public bool hover = false;

    public static bool clickable = true;

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
        Debug.Log($"{player.cursorManager.username} entered OnClick for {username}'s cup");

        //Forces player to click off the cup before clicking on another cup
        if (!clickable)
        {
            return;
        }
        clickable = false;

        Debug.Log($"{player.cursorManager.username} continued OnClick for {username}'s cup");

        //Add offclick to background
        CursorGameManager.Instance.offClick.onClick.AddListener(OffClick);

        //Set the pills 
        tempFakePill = player.GetPillFake();
        tempPoisonPill = player.GetPillPoison();
        //Debug.Log($"Fake Pill Colors: Pill: {tempFakePill.pillColor}, Player {tempFakePill.playerColor} ");
        //Debug.Log($"Real Pill Colors: Pill: {tempPoisonPill.pillColor}, Player {tempPoisonPill.playerColor} ");

        //Checks that pills are valid
        if (tempFakePill == null || tempPoisonPill == null)
        {
            Debug.Log($"One of {player.cursorManager.username}'s pills are invalid");
        }

        //Swap button position randomly
        CursorGameManager.Instance.SwapPillPosition();

        //Center the buttons
        Vector2 objPos = Camera.main.WorldToScreenPoint(transform.position);

        //Enable the canvas
        CursorGameManager.Instance.canvas.SetActive(true);

        //Add listeners
        CursorGameManager.Instance.poisonClick.onClick.AddListener(AddPoisonPill);
        CursorGameManager.Instance.fakeClick.onClick.AddListener(AddFakePill);
        CursorGameManager.Instance.buttonParent.anchoredPosition = objPos - CursorGameManager.Instance.canvasRectTransform.anchoredPosition + CursorGameManager.Instance.offset;
    }

    public void OffClick()
    {
        //Debug.Log($"OFFCLICK: {tempPoisonPill}");
        //DISABLES UI
        CursorGameManager.Instance.canvas.SetActive(false);

        tempFakePill = null;
        tempPoisonPill = null;

        CursorGameManager.Instance.offClick.onClick.RemoveListener(OffClick);
        CursorGameManager.Instance.poisonClick.onClick.RemoveListener(AddPoisonPill);
        CursorGameManager.Instance.fakeClick.onClick.RemoveListener(AddFakePill);

        clickable = true;
    }

    public void AddPoisonPill()
    {
        Debug.Log($"AddPoisonPill: {tempPoisonPill}");
        if (tempPoisonPill != null)
        {
            AddPill(tempPoisonPill);
        }
        else
        {
            Debug.Log("TempPoisonPill in CupInfo is null");
        }
    }

    public void AddFakePill()
    {
        //Debug.Log($"AddFakePill: {tempFakePill}");
        if (tempFakePill != null)
        {
            Debug.Log("fakepill is not null");
            AddPill(tempFakePill);
        }
        else
        {
            Debug.Log("TempFakePill in CupInfo is null");
        }
    }

    public void AddPill(Pill pill)
    {
        Debug.Log("addpill cupinfo");
        //Debug.Log($"Pill Colors: Pill: {pill.pillColor}, Player {pill.playerColor} ");
        ClientSend.PlacePill(pill, this);
        //somehow in server do ur magic
        OffClick();
        CursorGameManager.Instance.NextTurn();
    }

    public void ReceivePill(Pill pill)
    {
        pillStack.Push(pill);

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
