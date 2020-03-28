﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupInfo : MonoBehaviour
{
    private int id;
    private string playerName;
    private Color32 color;

    CupInfo(int id, string name, Color32 color)
    {
        this.id = id;
        this.playerName = name;
        this.color = color;
    }

    public void SetValues(int id, string name, Color32 color)
    {
        this.id = id;
        this.playerName = name;
        this.color = color;
    }

    public void OnHover()
    {

    }

    public void OffHover()
    {

    }

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