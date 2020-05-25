using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Representation of a Player Goal. </summary>
public class Goal {
    public int myId; //Owning player ID
    public int id; // Target Player ID
    public enum GoalState { die, live } // What goals you can be given
    public GoalState goalState; // Players' goal

    /// <summary> Representation of a Player Goal. </summary>
    /// <param name="id"> Target Player ID. </param>
    /// <param name="goal"> Current Player Goal. </param>
    public Goal (int myId, int id, GoalState goal) {
        this.myId = myId;
        this.id = id;
        this.goalState = goal;
    }

    public override string ToString()
    {
        return ($"Goal: Player: {myId}, Target: {id}, goal: {goalState.ToString()}");
    }
}
