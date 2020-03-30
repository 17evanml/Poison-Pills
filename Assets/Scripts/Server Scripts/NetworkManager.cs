﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    public GameObject cursorPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            InstantiatePlayer();
        }
    }


    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;

        Server.Start(50, 6942);
    }

    public ServerCursor InstantiatePlayer()
    {
        return Instantiate(cursorPrefab, Vector3.zero, Quaternion.identity).GetComponent<ServerCursor>();
    }
}
