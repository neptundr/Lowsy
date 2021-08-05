using System;
using UnityEngine;

// [RequireComponent(typeof(PlaceableObject))]
public abstract class BallInteractingObject : MonoBehaviour
{
    public virtual void Rotate() {}

    public virtual void ResetToStart(){}


    private void Awake()
    {
        GameManager.ResetToStart += ResetToStart;
    }

    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}