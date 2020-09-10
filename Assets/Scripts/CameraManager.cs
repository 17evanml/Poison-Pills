using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class CameraManager : MonoBehaviour {
    public bool gameStarted = false;
    private bool isMoving = false;
    public Vector3 position;
    public Vector3 rotation;
    public float focus;
    private Vector3 offPosition;
    public Vector3 onPosition;
    private Vector3 offRotation;
    public Vector3 onRotation;
    private float offFocus = 5;
    private float onFocus = 10;
    public float speedMod = 70f;


    // Start is called before the first frame update
    void Start() {
        offPosition = transform.position;
        offRotation = transform.rotation.eulerAngles;
        //onPosition = offPosition + new Vector3(0f, -3.5f, 4.5f);
        //onRotation = offRotation + new Vector3(15, 0, 0);
        position = offPosition;
        rotation = offRotation;
        focus = offFocus;
    }

    // Update is called once per frame
    void Update() {
 
    }

    public void SwitchCameraPosition()
    {
        gameStarted = !gameStarted;
        if (!gameStarted)
        {
            position = offPosition;
            rotation = offRotation;
            focus = offFocus;
        }
        else
        {
            position = onPosition;
            rotation = onRotation;
            focus = onFocus;
        }
        StartCoroutine(MoveCameraPosition(Time.deltaTime));
    }

    IEnumerator MoveCameraPosition(float deltaTime)
    {
        if (transform.position != position)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(rotation), Time.deltaTime * speedMod);
            if ((transform.position - position).magnitude < 0.75f)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, Time.deltaTime * speedMod * 0.75f);
            } else
            {
                transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime * speedMod);
            }
            DepthOfField depth = (DepthOfField)gameObject.GetComponent<Volume>().profile.components[0];
            depth.focusDistance.value = Mathf.Lerp(depth.focusDistance.value, focus, Time.deltaTime * speedMod);
            yield return new WaitForSeconds(deltaTime);
            StartCoroutine(MoveCameraPosition(Time.deltaTime));
        } else
        {
            UIManager.instance.InitializePillDisplays();
            yield return 0;
        }
    }
}