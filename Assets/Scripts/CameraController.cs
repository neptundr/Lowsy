using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool canMove;
    public float movementSpeed = 0.2f;

    private void Update()
    {
        if (canMove)
        {
            if (Input.GetKey(KeyCode.A)) transform.Translate(new Vector3(-movementSpeed, 0));
            if (Input.GetKey(KeyCode.D)) transform.Translate(new Vector3(movementSpeed, 0));
            if (Input.GetKey(KeyCode.W)) transform.Translate(new Vector3(0, movementSpeed));
            if (Input.GetKey(KeyCode.S)) transform.Translate(new Vector3(0, -movementSpeed));
        }
    }
}
