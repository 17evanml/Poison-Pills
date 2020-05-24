using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayManager : MonoBehaviour {
    public enum GoalState {kill, save};

    [Header("Goal References")]
    public TMP_Text text_goal_1;
    public Goal goal_1;
    // public string player_1;
    // public GoalState goalState_1;
    public TMP_Text text_goal_2;
    public Goal goal_2;
    public string player_2;
    public GoalState goalState_2;

    [Header("Score References")]
    public TMP_Text score;
    // Sort Score

    [Header("Pill Display References")]
    public List<PillDisplay> pillDisplays = new List<PillDisplay>();

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize() {
        goal_1.text = FormatGoal(goalState_1, player_1);
        goal_2.text = FormatGoal(goalState_2, player_2);
    }

    string FormatGoal(GoalState goalState, string player) {
        string tempGoal;
        if (goalState == GoalState.kill) { tempGoal = "Kill "; }
        else { tempGoal = "Save "; } 
        tempGoal += player;
        return tempGoal;
    }
}
