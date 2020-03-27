using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientServerTestScript : MonoBehaviour
{

    public Server server;
    // Start is called before the first frame update
    void Start()
    {
        print("hi");
        server = new Server();
        server.Start(10, 6942);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
