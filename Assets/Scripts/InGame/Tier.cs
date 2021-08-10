using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tier : MonoBehaviour
{
    private float _fallingSpeed = 0.075f;

    private void Start()
    {
        Destroy(gameObject, 2.5f);
    }

    private void Update()
    {
        transform.Translate(new Vector3(0, -_fallingSpeed * Time.timeScale));
    }
}
