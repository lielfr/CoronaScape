using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tracker : MonoBehaviour
{
    public GameObject toTrack;
    public float sensitivity = 10f;

    public Vector3 cameraLookAt;
    public Vector3 cameraPosition;
    Vector3 delta;
    // Start is called before the first frame update
    void Start()
    {
        delta = transform.position - toTrack.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position  = toTrack.transform.position + delta;
        cameraLookAt = transform.forward;
        cameraPosition = transform.position;
    }

    public void OnMouseX(InputValue value) {
        var deltax = value.Get<float>();
        
        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x,
            transform.localEulerAngles.y + deltax * 0.1f,
            transform.localEulerAngles.z
        );
    }

    public void OnMouseY(InputValue value)
    {
        var deltay = value.Get<float>();

        transform.localEulerAngles = new Vector3(
            transform.localEulerAngles.x - deltay * 0.1f,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z
        );
    }
}
