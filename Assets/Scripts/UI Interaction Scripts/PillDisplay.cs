using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary> Representation of the All Pills in a Cup. </summary>
public class PillDisplay : UIBase {
    [Header ("Text References")]
    public TMP_Text text_pillCount; // Text for the number of pills within the game
    public TMP_Text text_pillSprite; // Text for the Title Text "Current Pills" 

    [Header ("Image References")]
    public Image image_outline; // Image for the Button Outline
    public Image image_fill; // Image for the Button Fill

    [Header ("Color References")]
    public Color color_outline; // Color for the Button Outline
    public Color color_fill; // Color for the Button Fill
    public Color color_text; // Color for text_pillCount and text_pillSprite

    [Header ("Vector References")]
    public Vector3 vector_outlineLarge; // Local Scale Value for Enlarged Outline Image
    private Vector3 vector_outlineSmall; // Local Scale Value for Reduced Outline Image
    public Vector3 vector_fillLarge; // Local Scale Value for Enlarged Fill Image
    private Vector3 vector_fillSmall; // Local Scale Value for Reduced Fill Image
    private Vector3 vector_pillCount; // Local Scale Value for Enlarged Pill Count Text
    private Vector3 vector_pillSpriteText; // Local Scale Value for Enlarged Pill Sprite Text
    public Vector3 vector_pillSprite = new Vector3(0.12f, 0.18f, 0.12f); // Local Scale Value for Enlarged Pill Sprite Image

    [Header ("Pill Spawning Settings")]
    public List<GameObject> list_pills = new List<GameObject>(); // List containing all Pills within the Display
    public Vector3 vector_spawn = new Vector3(-10f, 2f, 0f); // Local Position Vector for Initial Spawning Point
    public Vector2 vector_spacing = new Vector3(5,5,0); // Local Position Vector for spacing between Pills
    public Vector2 vector_maxGrid = new Vector2(4, 1); // Maximum rows and columns of the current Display
    public Vector2 vector_currentGrid = Vector3.zero; // Current Position in the grid of the latest index in the display
    public GameObject gameObject_pill; // Sprite used to represent a Pill
    private bool state = true;

    void Start() {
        // Set Image Colors
        image_outline.color = color_outline;
        image_fill.color = color_fill;

        // Set Text Colors
        text_pillCount.color = color_text;
        text_pillSprite.color = color_text;

        // Set Scale Vectors
        vector_outlineSmall = image_outline.rectTransform.localScale;
        vector_fillSmall = image_fill.rectTransform.localScale;
        vector_pillCount = text_pillCount.transform.localScale;
        vector_pillSpriteText = text_pillSprite.transform.localScale;
        text_pillSprite.transform.localScale = Vector3.zero;

        // Set UI Base Parameters
        offHoverScale = transform.localScale;
        onHoverScale = offHoverScale + deltaScale;
    }

    void Update() {
        text_pillCount.text = list_pills.Count.ToString(); // Set PillCount to List's Count
        // DebugTester(); // Only for Debugging Purposes (Uses Q)
    }

    /// <summary> The Function being called upon being Clicked </summary>
    public override void OnClick() {
        beenClicked = true;
        if (state) {
            // Sets the Scale of All GameObjects to display Pill Sprites
            LeanTween.scale(image_outline.gameObject, vector_outlineLarge, speed).setEase(onClickType);
            LeanTween.scale(image_fill.gameObject, vector_fillLarge, speed).setEase(onClickType);
            LeanTween.scale(text_pillCount.gameObject, Vector3.zero, speed).setEase(onClickType);
            LeanTween.scale(text_pillSprite.gameObject, vector_pillSpriteText, speed).setEase(onClickType);
            for(int i = 0; i < list_pills.Count; i++) {
                LeanTween.scale(list_pills[i], vector_pillSprite, speed).setEase(onClickType);
            }
        } else {
            // Sets the Scale of All GameObjects to display Pill Count
            LeanTween.scale(image_outline.gameObject, vector_outlineSmall, speed).setEase(onClickType);
            LeanTween.scale(image_fill.gameObject, vector_fillSmall, speed).setEase(onClickType);
            LeanTween.scale(text_pillCount.gameObject, vector_pillCount, speed).setEase(onClickType);
            LeanTween.scale(text_pillSprite.gameObject, Vector3.zero, speed).setEase(onClickType);
            for(int i = 0; i < list_pills.Count; i++) {
                LeanTween.scale(list_pills[i], Vector3.zero, speed).setEase(onClickType);
            }
        }
        state = !state; // Reverses State
    }

    /// <summary> Adds a new Pill to the Pill Display </summary>
    /// <param name="pillData"> A Pill Class Representation of the Pill being Added </param>
    public void AddPill(Pill pillData) {
        // Instantiate a new Pill at correct Location and Set Scale to Zero
        GameObject newPill = Instantiate(gameObject_pill, Vector3.zero, Quaternion.identity);
        newPill.GetComponent<PillRenderer>().pill = pillData;
        newPill.transform.parent = transform;
        newPill.transform.localPosition = vector_spawn;
        
        newPill.transform.localScale = Vector3.zero; 

        // Tween the Scale if Pills are currently being displayed
        if (!state) {
            LeanTween.scale(newPill, vector_pillSprite, 0.5f).setEase(onClickType);
        }
        
        list_pills.Add(newPill); // Add the new GameObject to list_pills
        
        if (vector_currentGrid.x < vector_maxGrid.x) { // Adds Pill to next Column
            vector_currentGrid.x++;
            vector_spawn.x += vector_spacing.x;
        } else if (vector_currentGrid.y < vector_maxGrid.y){ // Add Pill to Next Row
            vector_currentGrid.y++;
            vector_currentGrid.x = 0;
            vector_spawn.x = -10f;
            vector_spawn.y -= vector_spacing.y;
        } else { // Grows the Display
            Grow();
        }
    }

    public void ClearPills()
    {
        list_pills.Clear();
        vector_spawn = new Vector3(-10f, 2f, 0f);
        vector_currentGrid = Vector3.zero;
        vector_maxGrid = new Vector2(4, 1);
    }

    /// <summary> Debug Commands for the Pill Display Script </summary>
    public void DebugTester() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            AddPill(new Pill((Color32) Color.red, (Color32) Color.yellow, true));
        }
    }

    /// <summary> Grows the Entire Display by 2 Columns and 1 Row </summary>
    public void Grow() {
        // Rescales the new Grid
        vector_maxGrid += new Vector2(2,1);
        vector_spacing.x *= 0.7f;
        vector_spacing.y *= 0.75f;
        vector_currentGrid = Vector2.zero;
        vector_pillSprite *= 0.80f;
        vector_spawn = new Vector3(-10f, 2f, 0);

        // Reformats each existing pill's transform
        for (int i = 0; i < list_pills.Count; i++) {
            list_pills[i].transform.localPosition = vector_spawn;
            list_pills[i].transform.localScale = vector_pillSprite;

            if (vector_currentGrid.x < vector_maxGrid.x) {
                vector_currentGrid.x++;
                vector_spawn.x += vector_spacing.x;
            } else {
                vector_currentGrid.y++;
                vector_currentGrid.x = 0;
                vector_spawn.x = -10f;
                vector_spawn.y -= vector_spacing.y;
            }
        }
    }
}