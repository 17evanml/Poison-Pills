using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour {
    public static PauseMenuManager instance { get; private set; }
    public GameObject background;
    public GameObject options;
    private bool inGame = false;
    private bool optionOpen = false;

    // Start is called before the first frame update
    void Start() {
        if (instance != null && instance != this) { 
            Destroy(gameObject);
            return;
        } else { 
            instance = this; 
        }

        DontDestroyOnLoad(gameObject); // AudioManager transfers between Scenes

        if (SceneManager.GetActiveScene().name == "SCN_StartMenu") {
            background.SetActive(false);
        } else {
            background.SetActive(true);
            inGame = true;
            options.SetActive(true);
            options.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update() {
        if (inGame) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (!optionOpen) {
                    LeanTween.scale(options, new Vector3(0.5f, 0.5f, 0.5f), 1f).setEase(LeanTweenType.easeInOutElastic);
                    optionOpen = !optionOpen;
                } else {
                    LeanTween.scale(options, Vector3.zero, 1f).setEase(LeanTweenType.easeInOutElastic);
                    optionOpen = !optionOpen;
                }
            }
        }
    }
}
