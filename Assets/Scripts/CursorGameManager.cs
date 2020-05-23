using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorGameManager : MonoBehaviour
{
    public static CursorGameManager Instance { get; private set; }
    public GameObject testCup;

    //Client side buttons
    public GameObject canvas;
    public RectTransform canvasRectTransform;
    public RectTransform buttonParent;
    public Button offClick;
    public Button poisonClick;
    public Button fakeClick;
    public Vector2 offset;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }

    public void SwapPillPosition()
    {
        int r = Random.Range(0, 2);
        if (r == 1) {
            Debug.Log("Swapping");
            Vector3 temp = poisonClick.transform.position;
            poisonClick.transform.position = fakeClick.transform.position;
            fakeClick.transform.position = temp;
        }
    }

    private void CreateCup(CursorManager cm)
    {
        GameObject g = Instantiate(testCup);
        CupInfo c = g.GetComponent<CupInfo>();
        if (c)
        {
            c.Initialize(cm.id, cm.name, cm.color);
        }
        CupManager.Instance.AddCup(c);
    }

    public void CreateAllCups()
    {
        foreach (CursorManager cm in GameManager.cursors.Values)
        {
            CreateCup(cm);
        }

    }

    public void NextTurn()
    {
        //this needs to be called for all players
    }
}
