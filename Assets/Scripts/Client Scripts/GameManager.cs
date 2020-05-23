using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static Dictionary<int, CursorManager> cursors = new Dictionary<int, CursorManager>();
    public static Dictionary<int, CupInfo> cups = new Dictionary<int, CupInfo>();

    public GameObject localCursorPrefab;
    public GameObject cursorPrefab;
    public GameObject cupPrefab;

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


    /// <summary>Spawns a player.</summary>
    /// <param name="_id">The player's ID.</param>
    /// <param name="_name">The player's name.</param>
    /// <param name="_position">The player's starting position.</param>
    /// <param name="_rotation">The player's starting rotation.</param>
    public void SpawnCursor(int _id, string _username, Vector3 _position, Quaternion _rotation, Color32 _color)
    {
        GameObject _cursor;
        if (_id == Client.instance.myId)
        {
            _cursor = Instantiate(localCursorPrefab, _position, _rotation);
        }
        else
        {
            _cursor = Instantiate(cursorPrefab, _position, _rotation);
        }

        _cursor.GetComponent<CursorManager>().id = _id;
        _cursor.GetComponent<CursorManager>().username = _username;
        _cursor.GetComponent<CursorManager>().color = _color;
        cursors.Add(_id, _cursor.GetComponent<CursorManager>());
    }

}
