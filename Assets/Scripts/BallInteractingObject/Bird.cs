using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : BallInteractingObject
{
    public Direction direction;

    private float _movingSpeed = 0.1f;
    private bool _grabbed;
    private Ball _catchedBall;
    private Vector2 _startPosition;
    private Vector2 _toDirection;
    private Vector2 _toPosition;

    public override void Rotate()
    {
        direction = DifferentAdditions.NextDirection(direction);
        transform.rotation = DifferentAdditions.DirectionToRotation(direction);
    }

    private void Start()
    {
        _toDirection = DifferentAdditions.DirectionToVector2Int(direction);
        transform.rotation = DifferentAdditions.DirectionToRotation(direction);
        GameManager.Tick1 += Move;
        GameManager.Restart += OnRestart;
    }

    private void OnRestart()
    {
        _startPosition = transform.position;
        _toPosition = transform.position;
    } 
    
    public override void ResetToStart()
    {
        _toPosition = _startPosition;
        transform.position = _startPosition;
        UnGrabBall();
    }

    private void Update()
    {
        if (!GameManager.GetIsCompletelyStopped())
        {
            transform.position = Vector3.MoveTowards(transform.position, _toPosition, _movingSpeed);

            if (!_grabbed)
            {
                if (Physics2D.OverlapPoint(transform.position, GameManager.SkyLayer))
                {
                    if (Physics2D.OverlapPoint(transform.position, GameManager.SkyLayer).TryGetComponent(out Ball ball))
                    {
                        if (!ball.GetIsCatched())
                        {
                            _grabbed = true;
                            _catchedBall = ball;
                            _catchedBall.BeCatched();
                        }
                    }
                }
            }
            else
            {
                if (_catchedBall.GetVerticalPosition() != VerticalDirection.Up)
                {
                    UnGrabBall();
                }
            }
        }
    }

    private void UnGrabBall()
    {
        if (_catchedBall != null) _catchedBall.UnCatched();
        _grabbed = false;
        _catchedBall = null;
    }

    private void Move()
    {
        if (_grabbed)
        {
            _toPosition += _toDirection;
            _catchedBall.SetToPosition(DifferentAdditions.Vector2ToVector2Int(_toPosition));
        }
    }

    private void OnDestroy()
    {
        GameManager.Tick1 -= Move;
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Restart -= OnRestart;
    }
}
