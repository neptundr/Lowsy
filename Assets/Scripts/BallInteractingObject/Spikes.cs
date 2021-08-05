using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public BoxCollider2D col;

    private void Start()
    {
        GameManager.Restart += OnRestart;
        GameManager.ResetToStart += ResetToStart;
        ResetToStart();
    }

    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Restart -= OnRestart;
    }

    public void ResetToStart()
    {
        col.enabled = false;
    }

    private void OnRestart()
    {
        col.enabled = true;
    }
}
