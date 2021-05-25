using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallMovement : MonoBehaviour
{
    public float speed = 0;

    private Rigidbody rb;
    private float movementX, movementY;
    // Start is called before the first frame update
    Rigidbody playerRigidBody;
    GameObject cameraObj;
    Tracker cameraTracker;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraObj = GameObject.FindWithTag("MainCamera");
        cameraTracker = cameraObj.GetComponent<Tracker>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        Quaternion lookRotation = Quaternion.LookRotation(Camera.main.transform.forward);
        movement = lookRotation * movement;
        rb.AddForce(movement * speed);
    }

    void Update() {
    }
}
