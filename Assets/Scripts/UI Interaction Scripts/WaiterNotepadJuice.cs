using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Manages all Visual Effects in the Waiter Notepad Canvas. </summary>
public class WaiterNotepadJuice : MonoBehaviour
{
    public Transform revealPosition; // Transform of Canvas when in Reveal Position 
    public Transform hiddenPosition; // Transform of Canvas when in Hidden Position
    public bool isRunning; // Is True if a Coroutine is running

    /// <summary> Eases the Waiter Notepad Canvas into the Reveal Position. </summary>
    /// <param name="time"> Total time required to ease the Waiter Notepad Canvas into the Reveal Position. </param>
    IEnumerator ShowWaiterNotepad(float time) 
    {
        isRunning = true;

        float currentTime = 0;
        while (currentTime < time) 
        {
            gameObject.GetComponent<RectTransform>().position = new Vector3(EasingFunction.EaseInSine(hiddenPosition.position.x, revealPosition.position.x, currentTime/time), EasingFunction.EaseInSine(hiddenPosition.position.y, revealPosition.position.y, currentTime/time), EasingFunction.EaseInSine(hiddenPosition.position.z, revealPosition.position.z, currentTime/time));
            yield return null;
        }
        isRunning = false;
    }

    /// <summary> Eases the Waiter Notepad Canvas into the Hidden Position. </summary>
    /// <param name="time"> Total time required to ease the Waiter Notepad Canvas into the Hidden Position. </param>
    IEnumerator HideWaiterNotepad(float time) 
    {
        isRunning = true;

        float currentTime = 0;
        while (currentTime < time) 
        {
            gameObject.GetComponent<RectTransform>().position = new Vector3(EasingFunction.EaseInSine(revealPosition.position.x, hiddenPosition.position.x, currentTime/time), EasingFunction.EaseInSine(revealPosition.position.y, hiddenPosition.position.y, currentTime/time), EasingFunction.EaseInSine(revealPosition.position.z, hiddenPosition.position.z, currentTime/time));
            yield return null;
        }
        isRunning = false;
    }

    /// <summary> Eases the Waiter Notepad Canvas into the Position of the next revealed player and updates the revealedPosition. </summary>
    /// <param name="positionSpacing"> Spacing between goal entries in the y-axis. </param>
    /// <param name="time"> Total time required to ease the Waiter Notepad Canvas into the new Revealed Position. </param>
    public IEnumerator ScrollWaiterNotepad(float positionSpacing, float time) 
    {
        isRunning = true;

        float currentTime = 0;
        while (currentTime < time) 
        {
            gameObject.GetComponent<RectTransform>().position = new Vector3(revealPosition.position.x, EasingFunction.EaseInSine(revealPosition.position.y, revealPosition.position.y + positionSpacing, currentTime/time), revealPosition.position.z);
            yield return null;
        }
        revealPosition.position = revealPosition.position + (Vector3.up * positionSpacing);

        isRunning = false;
    }
}