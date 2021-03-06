using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraController : MonoBehaviour
{
    public Transform leftUpPoint;
    public Transform rightDownPoint;

    private float _toRotation;
    private float _rotationRange = 2.5f;
    private float _rotationSpeed = 0.025f;
    private float _lastMovingTime; 
    private float _movingSpeed = 0.1f;
    private float _movingFrequency = 0.1f;
    private Vector2Int _toPosition;
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    public void Rotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, _rotationRange * (Random.Range(0, 2) == 1 ? -1 : 1));
    } 
    
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), _rotationSpeed);
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_toPosition.x, _toPosition.y, -10), _movingSpeed);

        if (_lastMovingTime + _movingFrequency < Time.time)
        {
            _lastMovingTime = Time.time;
            if (Input.GetKey(KeyCode.A) && _toPosition.x > leftUpPoint.position.x) _toPosition = new Vector2Int(_toPosition.x - 1, _toPosition.y);
            else if (Input.GetKey(KeyCode.D) && _toPosition.x < rightDownPoint.position.x) _toPosition = new Vector2Int(_toPosition.x + 1, _toPosition.y);
            else if (Input.GetKey(KeyCode.W) && _toPosition.y < leftUpPoint.position.y) _toPosition = new Vector2Int(_toPosition.x, _toPosition.y + 1);
            else if (Input.GetKey(KeyCode.S) && _toPosition.y > rightDownPoint.position.y) _toPosition = new Vector2Int(_toPosition.x, _toPosition.y - 1);
            else _lastMovingTime = 0;
        }

        if (Input.GetMouseButtonUp(1) && Placer.This.GetNowPlaceable() == null)
        {
            Vector2 mp = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            
            if (mp.x > leftUpPoint.position.x && mp.x < rightDownPoint.position.x &&
                mp.y < leftUpPoint.position.y && mp.y > rightDownPoint.position.y)
            {
                _toPosition = DifferentAdditions.Vector2ToVector2Int(mp);
            }
        }
    }
}
