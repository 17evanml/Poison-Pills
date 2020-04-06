using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Representation of the All Pills in a Cup. </summary>
public class PillDisplay : MonoBehaviour {
    public Stack<Pill> pills = new Stack<Pill>(); // Stack of Pills in the Display
    public int maxColumn = 3; // Max Columns of the Display
    private Vector3 currentPos = Vector3.zero; // Current position of the Most Recent Pill
    public Vector2 spacing; // Spacing between pills
    public GameObject pillPrefab; // Reference to Pill Prefab

    /// <summary> Adds a Pill to the Stack. Then Displays the new Pill. </summary>
    /// <param name="pill"> A new Pill. </param>
    void AddPill(Pill pill) {
        pills.Push(pill);
        DisplayNewPill(pill);
    }

    /// <summary> Displays the PillRenderer of the new Pill. </summary>
    /// <param name="pill"> A new Pill. </param>
    void DisplayNewPill(Pill pill) {
        // Instantiates a new Pill, and add references to each other.
        GameObject newPill = Instantiate(pillPrefab, transform.position + currentPos, Quaternion.identity);
        newPill.GetComponent<PillRenderer>().pill = pill;
        pill.pillRenderer = newPill.GetComponent<PillRenderer>();
        
        // Increments Current Position
        if (currentPos.x < (maxColumn - 1) * spacing.x) {
            currentPos.x += spacing.x;
        } else {
            currentPos.x = 0;
            currentPos.y -= spacing.y;
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            AddPill(new Pill((Color32) Color.red, (Color32) Color.blue, false));
        }
    }
}
