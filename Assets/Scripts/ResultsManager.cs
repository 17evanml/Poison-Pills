using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary> ResultsManager is attached to the ResultsCanvas. It is used to display results after a round. </summary>
public class ResultsManager : MonoBehaviour {
    public Image IMG_playerSprite;
    public TMP_Text TMP_playerName;
    public TMP_Text TMP_resultHeader;
    public TMP_Text TMP_pillHeader;
    public TMP_Text TMP_playerState;
    public TMP_Text TMP_points;
    public LeanTweenType tweenType;
    public GameObject skull;

    public PillDisplay UI_pillDisplay;




    // Start is called before the first frame update
    void Start() {


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene("SCN_Arjun");
        }
    }

    ///<summary> Sets the size of all UI elements. </summary>
    ///<param name="scaleVector"> The Local Scale that all UI Elements should be set to </param>
    private void SetUISize(CupInfo cupInfo, Vector3 scaleVector) {
        UI_pillDisplay = GameManager.instance.displayManager.pillDisplays[cupInfo.id - 1]; // Sets the Correct Cup Display based on CupInfo
        IMG_playerSprite.rectTransform.localScale = scaleVector; // Sets the Player Sprite's Scale
        TMP_playerName.rectTransform.localScale = scaleVector; // Sets the Player Name's Scale
        TMP_resultHeader.rectTransform.localScale = scaleVector; // Sets the Result Header's Scale
        TMP_pillHeader.rectTransform.localScale = scaleVector; // Sets the Pill Header's Scale
        TMP_playerState.rectTransform.localScale = scaleVector; // Sets the Player State's Scale
        TMP_points.rectTransform.localScale = scaleVector; // Sets the Point's Scale
        UI_pillDisplay.transform.position = scaleVector; // Sets tge Pill Display's Scale
    }

    ///<summary> Format all UI elements in accordance to the details within the CupInfo </summary>
    ///<param name="cupInfo"> The CupInfo for the corresponding Player </param>
    private void FormatUI(CupInfo cupInfo) {
        IMG_playerSprite.color = cupInfo.color; // Sets the Player Sprite to the Correct Color
        TMP_playerName.text = cupInfo.username; // Sets the PlayerName to the Correct Username
        UI_pillDisplay.GetComponent<RectTransform>().position = TMP_pillHeader.rectTransform.position - (Vector3.down * 4); // Sets the PillHeaders Local Position
    }

    ///<summary> Begins the Animation for Results Reveal </summary>
    ///<param name="cupInfo"> The CupInfo for the corresponding Player </param>
    public IEnumerator cutscene(CupInfo cupInfo) {
        SetUISize(cupInfo, Vector3.zero); // Sets the Scale of All Relevant UI Elements to Zero
        FormatUI(cupInfo); // Formats the UI to the details within the CupInfo

        // Animation Beat 1: Spawns PlayerSprite, PlayerName, ResultsHeader and PillHeader
        LeanTween.scale(IMG_playerSprite.gameObject, Vector3.one, 0.5f).setEase(tweenType);
        LeanTween.scale(TMP_playerName.gameObject, Vector3.one, 0.5f).setEase(tweenType);
        LeanTween.scale(TMP_resultHeader.gameObject, Vector3.one, 0.5f).setEase(tweenType);
        LeanTween.scale(TMP_pillHeader.gameObject, Vector3.one, 0.5f).setEase(tweenType);

        yield return new WaitForSeconds(1f); // Waits 1 Second between Animation Beats

        LeanTween.scale(UI_pillDisplay.gameObject, Vector3.one, 0.5f).setEase(tweenType); // Animation Beat 2: Spawns PillDisplay

        yield return new WaitForSeconds(1f); // Waits 1 Second between Animation Beats

        // Animation Beat 3: Spawns a Skull for each Poison Pill
        for (int i = 0; i < UI_pillDisplay.list_pills.Count; i++) {
            if (UI_pillDisplay.list_pills[i].GetComponent<PillRenderer>().pill.poison == true) {
                GameObject temp_marker = Instantiate(skull, UI_pillDisplay.list_pills[i].transform.position + (Vector3.up * 2), Quaternion.identity);
                temp_marker.GetComponent<RectTransform>().localScale = Vector3.zero;
                LeanTween.scale(temp_marker, Vector3.one, 0.25f).setEase(tweenType);
            }
            yield return new WaitForSeconds(0.25f);
        }

        yield return new WaitForSeconds(1f); // Waits 1 Second Between Animation beats

        // Set Dead State and Points (ASK EVAN FOR CALCULATING DEAD STATE AND POINTS)

        // Animation Beat 4: Spawns DeadState and Points
        LeanTween.scale(TMP_playerState.gameObject, Vector3.one, 0.5f).setEase(tweenType);
        LeanTween.scale(TMP_points.gameObject, Vector3.one, 0.5f).setEase(tweenType);

        // ASK EVAN HOW WE SHOULD TRIGGER THE NEXT CUTSCENE
        //Some Trigger
        // cutscene();
    }
}
