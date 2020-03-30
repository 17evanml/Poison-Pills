using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupManager : MonoBehaviour
{
    public static CupManager Instance { get; private set; }

    //Positioning of all cups
    [SerializeField]
    private Vector3 origin;
    [SerializeField]
    private float radius = 5f;

    //Linked list of all cups
    private LinkedList<CupInfo> cupInfos = new LinkedList<CupInfo>();

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

    public void AddCup(CupInfo cup)
    {
        if(cup == null)
        {
            Debug.LogError("null parameter in AddCup");
            return;
        }

        cupInfos.AddLast(cup);
        RepositionCups();
    }

    public void RemoveCup(CupInfo cup)
    {
        if (cup == null)
        {
            Debug.LogError("null parameter in RemoveCup");
            return;
        }

        cupInfos.Remove(cup);
        Destroy(cup.transform.gameObject);
        RepositionCups();
    }

    private void RepositionCups()
    {
        if(cupInfos.Count == 0)
        {
            return;
        }

        //Calculate the position for all the cups
        float cupAngle = 180f / (cupInfos.Count + 1f);
        int index = 1;
        float d2r = Mathf.PI / 180f;

        foreach(CupInfo c in cupInfos)
        {
            c.transform.position = origin + new Vector3(Mathf.Cos(cupAngle * index * d2r), 0, Mathf.Sin(cupAngle * index * d2r)) * radius;
            index++;
        }
    }

    public CupInfo GetFirst()
    {
        return cupInfos.First.Value;
    }

    public int Count()
    {
        return cupInfos.Count;
    }
}
