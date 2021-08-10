using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleRotate : BallInteractingObject
{
    public int tickPhase;
    public Direction startDirection;
    
    private float _rotationSpeed = 0.1f;
    private Direction _direction;
    private Quaternion _toRotation;
    private Ball _lastBall;

    // public override void Rotate()
    // {
    //     ChangeDirection();
    //     startDirection = _direction;
    // }

    public override void ResetToStart()
    {
        _direction = startDirection;
        
        SetToRotationWithDirection();
    }

    private void Start()
    {
        switch (tickPhase)
        {
            case 1:
                GameManager.Tick1 += ChangeDirection;
                break;
            case 2:
                GameManager.Tick2 += ChangeDirection;
                break;
            case 3:
                GameManager.Tick3 += ChangeDirection;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        ResetToStart();
    }

    private void ChangeDirection()
    {
        _direction = DifferentAdditions.NextDirection(_direction);

        SetToRotationWithDirection();
    }

    private void SetToRotationWithDirection()
    {
        _toRotation = DifferentAdditions.DirectionToRotation(_direction);
    }
    
    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, _rotationSpeed);
        
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    ball.SetBallDirection(_direction);
                    ball.SetToPosition(DifferentAdditions.Vector2ToVector2Int(transform.position));
                    _lastBall = ball;
                }
            }
            else
            {
                _lastBall = null;
            }
        }
        else
        {
            _lastBall = null;
        }
    }
    
    private void OnDestroy()
    {
        GameManager.Tick1 -= ChangeDirection;
        GameManager.Tick2 -= ChangeDirection;
        GameManager.Tick3 -= ChangeDirection;
        GameManager.ResetToStart -= ResetToStart;
    }
}