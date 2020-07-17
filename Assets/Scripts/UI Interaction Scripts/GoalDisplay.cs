using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> Visual Representation of a Player Goal. </summary>
public class GoalDisplay : MonoBehaviour {
    public Text goalText; // Text Component of the Goal
    public Goal goal; // Goal Data

    // Start is called before the first frame update
    public void Initialize() {
        // Assigns goal text based on Goal data
        if (goal.goalState == Goal.GoalState.Kill) { goalText.text = "Kill "; }
        else if (goal.goalState == Goal.GoalState.Save) { goalText.text = "Save "; }
        goalText.text += GameManager.cursors[goal.id].username;
    }
}
