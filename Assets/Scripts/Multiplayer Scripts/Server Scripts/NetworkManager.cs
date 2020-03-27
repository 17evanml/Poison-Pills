using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ServerScripts
{
    public class NetworkManager : MonoBehaviour
    {
        public static NetworkManager instance;

        public GameObject playerPrefab;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            Server.Start(10, 6942);
            //Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Server"), LayerMask.NameToLayer("Default"));
        }

        public Player InstantiatePlayer()
        {
            return Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();

        }
    }
}