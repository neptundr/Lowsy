using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonTouchableIfRotated : BallInteractingObject
{
    public Orientation orientation;
    
    private float _rotationSpeed = 0.1f;
    private Orientation _orientation;
    private Quaternion _toRotation;
    private Ball _lastBall;

    public override void Rotate()
    {
        ChangeRotation();
        orientation = _orientation;
    }

    public override void ResetToStart()
    {
        _orientation = orientation;
        transform.rotation = _orientation == Orientation.Horizontal
            ? Quaternion.Euler(0, 0, 0)
            : Quaternion.Euler(0, 0, 90);
    }

    private void Start()
    {
        GameManager.Tick1 += ChangeRotation;
        ResetToStart();
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, _rotationSpeed);

        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (_lastBall != ball)
                {
                    if (ball.GetPreviousPosition().x == transform.position.x && _orientation == Orientation.Vertical)
                    {
                        GameManager.Lose();
                        ball.Die();
                    }
                    if (ball.GetPreviousPosition().y == transform.position.y && _orientation == Orientation.Horizontal)
                    {
                        GameManager.Lose();
                        ball.Die();
                    }

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
    
    private void ChangeRotation()
    {
        _orientation = _orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;
        transform.rotation = _orientation == Orientation.Vertical
            ? Quaternion.Euler(0, 0, 90)
            : Quaternion.Euler(0, 0, 0);
    }
    
    private void OnDestroy()
    {
        GameManager.Tick1 -= ChangeRotation;
        GameManager.ResetToStart -= ResetToStart;
    }
}
