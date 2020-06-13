using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RevealManager : MonoBehaviour {
    public TMP_Text currentGoals;
    public TMP_Text goal1;
    public TMP_Text goal2;

    // Start is called before the first frame update
    void Start() {
        
    }

    void Initialize() {
        goal1.text = GameManager.instance.displayManager.goal_1.ToString();
        goal2.text = GameManager.instance.displayManager.goal_2.ToString(); 
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            goal1.GetComponentInParent<Button>().gameObject.SetActive(true);
            goal2.GetComponentInParent<Button>().gameObject.SetActive(true);
        } else if (Input.GetKeyUp(KeyCode.Escape)) {
            goal1.GetComponentInParent<Button>().gameObject.SetActive(false);
            goal2.GetComponentInParent<Button>().gameObject.SetActive(false);
        }
    }

    void OnClick(bool right) {
        if (right) {
            currentGoals.text += GameManager.cups[GameManager.instance.displayManager.goal_2.myId] + ": ";
            currentGoals.text += goal2.text + "\n";
        } else {
            currentGoals.text += GameManager.cups[GameManager.instance.displayManager.goal_1.myId] + ": ";
            currentGoals.text += goal1.text + "\n";
        }
        goal1.GetComponentInParent<Button>().gameObject.SetActive(false);
        goal2.GetComponentInParent<Button>().gameObject.SetActive(false);
        // Initialize Next UI
        // Talk to Evan if we should seperate the 2 UI Canvas
    }
}
