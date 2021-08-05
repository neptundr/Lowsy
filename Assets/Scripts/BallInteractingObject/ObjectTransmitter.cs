using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectTransmitter : BallInteractingObject
{
    // private static List<ObjectTransmitter> _objectTransmitters = new List<ObjectTransmitter>();

    public Orientation orientation;
    public Transform point1;
    public Transform point2;
    public LayerMask whatToCatch;

    private bool _isGoingToPoint2 = true;
    private int _stepsDone;
    private int _stepsNeeded;
    private int _rotatePhase;
    private float _lineLength = 0.05f;
    private float _pointsMovingSpeed = 0.1f;
    private List<Transform> _catchedObjects = new List<Transform>();
    private Transform _catchedInPoint1;
    private Transform _catchedInPoint2;
    private Vector2 _point1ToPosition;
    private Vector2 _point2ToPosition;
    private Vector2 _point1StartPosition;
    private Vector2 _point2StartPosition;
    private LineRenderer _line;

    public override void Rotate()
    {
        orientation = DifferentAdditions.AnotherOrientation(orientation);
        if (_rotatePhase == 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
            _rotatePhase += 1;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
            _rotatePhase = 0;
        }
    }
    
    private void Start()
    {
        GameManager.PreRestart += Catch;
        GameManager.Tick1 += MovePoints;
        GameManager.Restart += OnRestart;
        _stepsNeeded = Convert.ToInt32(Vector3.Distance(point1.position, point2.position));
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 2;
        _line.sortingOrder = 10;
        _line.endWidth = _lineLength;
        _line.startWidth = _lineLength;

        _line.enabled = false;
    }

    private void Update()
    {
        if (GameManager.Started)
        {
            point1.position = Vector3.MoveTowards(point1.position, _point1ToPosition, _pointsMovingSpeed);
            point2.position = Vector3.MoveTowards(point2.position, _point2ToPosition, _pointsMovingSpeed);
        }
    }

    public override void ResetToStart()
    {
        _point1ToPosition = _point1StartPosition;
        _point2ToPosition = _point2StartPosition;
        point1.position = _point1StartPosition;
        point2.position = _point2StartPosition;
        _stepsDone = 0;
        _isGoingToPoint2 = true;
        _catchedObjects.Clear();
        
        _line.enabled = false;
    }

    private void OnRestart()
    {
        _point1ToPosition = point1.position;
        _point2ToPosition = point2.position;
        
        _line.enabled = true;
        
        _line.SetPosition(0, _point1ToPosition);
        _line.SetPosition(1, _point2ToPosition);
    }

    private void MovePoints()
    {
        if (orientation == Orientation.Horizontal)
        {
            if (_isGoingToPoint2)
            {
                _stepsDone += 1;
                _point1ToPosition = new Vector3(_point1StartPosition.x + _stepsDone, _point1StartPosition.y);
                _point2ToPosition = new Vector3(_point2StartPosition.x - _stepsDone, _point2StartPosition.y);
                
                if (_stepsDone >= _stepsNeeded) _isGoingToPoint2 = false;
            }
            else
            {
                _stepsDone -= 1;
                _point1ToPosition = new Vector3(_point1StartPosition.x + _stepsDone, _point1StartPosition.y);
                _point2ToPosition = new Vector3(_point2StartPosition.x - _stepsDone, _point2StartPosition.y);

                if (_stepsDone <= 0) _isGoingToPoint2 = true;
            }
        }
        else
        {
            if (_isGoingToPoint2)
            {
                _stepsDone += 1;
                _point1ToPosition = new Vector3(_point1StartPosition.x, _point1StartPosition.y - _stepsDone);
                _point2ToPosition = new Vector3(_point2StartPosition.x, _point2StartPosition.y + _stepsDone);
                
                if (_stepsDone >= _stepsNeeded) _isGoingToPoint2 = false;
            }
            else
            {
                _stepsDone -= 1;
                _point1ToPosition = new Vector3(_point1StartPosition.x, _point1StartPosition.y - _stepsDone);
                _point2ToPosition = new Vector3(_point2StartPosition.x, _point2StartPosition.y + _stepsDone);

                if (_stepsDone <= 0) _isGoingToPoint2 = true;
            }
        }
    }
    
    private void OnDestroy()
    {
        GameManager.PreRestart -= Catch;
        GameManager.Tick1 -= MovePoints;
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Restart -= OnRestart;
    }

    private void Catch()
    {
        _point1StartPosition = point1.position;
        _point2StartPosition = point2.position;
        
        // if (point1.position.x > point2.position.x || point1.position.y < point2.position.y) throw new InvalidCastException();

        TryCatchInPoint(point1);
        TryCatchInPoint(point2);
    }

    private void TryCatchInPoint(Transform point)
    {
        if (Physics2D.OverlapPoint(point.position, whatToCatch))
        {
            if (Physics2D.OverlapPoint(point.position, whatToCatch).TryGetComponent(out BallInteractingObject bio))
            {
                if (bio.GetComponent<PlaceableObject>().canBeCatched)
                {
                    bio.GetComponent<PlaceableObject>().AddCatchedByCount();

                    if (bio.GetComponent<PlaceableObject>().GetCatchedByCount() <= 1)
                    {
                        _catchedObjects.Add(bio.transform);
                        bio.transform.parent = point;
                    }
                }
            }
        }
    }
}
