using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlaceableObject : MonoBehaviour
{
    public bool prePlaced;
    public bool canBeCatched;

    [SerializeField] private int _catchedByCount;
    private bool _canBePlaced = true;
    private float _lastCollisionRegisterTime;
    private PlaceableObjectPicker _minePicker;
    private BoxCollider2D _bc;
    private BallInteractingObject _bio;

    public void AddCatchedByCount()
    {
        _catchedByCount += 1;
    }
    
    public int GetCatchedByCount()
    {
        return _catchedByCount;
    }
    
    public PlaceableObjectPicker GetPicker()
    {
        return _minePicker;
    }
    
    public void SetPicker(PlaceableObjectPicker picker)
    {
        _minePicker = picker;
    }

    public void Rotate()
    {
        if (_bio != null) _bio.Rotate();
    }

    private void OnRestart()
    {
        if (_catchedByCount > 1) transform.parent = null;
    }
    
    private void Start()
    {
        if (TryGetComponent(out Rigidbody2D rb))
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }

        _bc = GetComponent<BoxCollider2D>();
        _bc.isTrigger = true;
        _bio = GetComponent<BallInteractingObject>();

        GameManager.Restart += TurnOffBC;
        GameManager.ResetToStart += TurnOnBC;

        if(prePlaced) Place();

        GameManager.Restart += OnRestart;
        GameManager.ResetToStart += () => _catchedByCount = 0;
    }

    private void OnDestroy()
    {
        GameManager.Restart -= OnRestart;
        GameManager.Restart -= TurnOffBC;
        GameManager.ResetToStart -= TurnOnBC;
    }

    private void TurnOffBC()
    {
        _bc.enabled = false;
    }
    private void TurnOnBC()
    {
        _bc.enabled = true;
    }

    private void Update()
    {
        if (_lastCollisionRegisterTime + 0.1f < Time.time) _canBePlaced = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _canBePlaced = false;
        _lastCollisionRegisterTime = Time.time;
    }

    public void Place()
    {
        _bc.isTrigger = false;
    }

    public bool GetCanBePlaced()
    {
        return _canBePlaced;
    }
}