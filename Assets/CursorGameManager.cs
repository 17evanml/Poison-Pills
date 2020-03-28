using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorGameManager : MonoBehaviour
{
    public static CursorGameManager Instance { get; private set; }
    public GameObject testCup;
    Dictionary<int, Color32> dictionary;

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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateCup();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RemoveCup();
        }
    }


    private void CreateCup()
    {
        //GameObject g = new GameObject("Generated");
        //CupInfo c = g.AddComponent<CupInfo>();
        GameObject g = Instantiate(testCup);
        CupInfo c = g.GetComponent<CupInfo>();
        if (c)
        {
            c.SetValues(1, "Bill", Random.ColorHSV());
        }
        CupManager.Instance.AddCup(c);
    }

    private void RemoveCup()
    {
        if (CupManager.Instance.Count() > 0)
        {
            CupManager.Instance.RemoveCup(CupManager.Instance.GetFirst());
        }
    }

    public void NextTurn()
    {

    }
}
