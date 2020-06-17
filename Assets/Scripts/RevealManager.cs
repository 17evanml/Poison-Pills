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

    public void Initialize() {
        goal1.text = GameManager.cursors[UIManager.instance.goal_1.id].username;
        goal2.text = GameManager.cursors[UIManager.instance.goal_2.id].username;
    }

    // Update is called once per frame
    void Update() {
        // if (Input.GetKeyDown(KeyCode.Escape)) {
        //     goal1.GetComponentInParent<Button>().gameObject.SetActive(true);
        //     goal2.GetComponentInParent<Button>().gameObject.SetActive(true);
        // } else if (Input.GetKeyUp(KeyCode.Escape)) {
        //     goal1.GetComponentInParent<Button>().gameObject.SetActive(false);
        //     goal2.GetComponentInParent<Button>().gameObject.SetActive(false);
        // }
    }

    public void OnClick(bool right) {
        currentGoals.text += GameManager.cursors[Client.instance.myId].username + ": ";
        if (right) {
            currentGoals.text += goal2.text + "\n";
        } else {
            currentGoals.text += goal1.text + "\n";
        }
        goal1.GetComponentInParent<Button>().gameObject.SetActive(false);
        goal2.GetComponentInParent<Button>().gameObject.SetActive(false);
        // Initialize Next UI
        // Talk to Evan if we should seperate the 2 UI Canvas
    }
}
