using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCursor : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        Cursor.visible = false;
        _mainCamera = Camera.main;
    }

    void Update()
    {
        transform.position = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }
}
