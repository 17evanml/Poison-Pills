using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITween : MonoBehaviour
{
    [SerializeField] private LeanTweenType type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) {
            LeanTween.scale(gameObject, new Vector3(0,0,0), 0.5f).setDelay(0.5f).setEase(type);
        } else if (Input.GetKeyDown(KeyCode.W)) {
            LeanTween.scale(gameObject, new Vector3(1,1,1), 0.5f).setDelay(0.5f).setEase(type);
        }
    }
}
