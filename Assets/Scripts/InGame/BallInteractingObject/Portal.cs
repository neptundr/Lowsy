using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BallInteractingObject
{
    public bool isFiveLength;
    public Transform entry1;
    public Transform entry2;

    private int _rotatingPhase;
    private float _portFrequency = 0.025f;
    private Ball _lastBall;
    private Ball _lastBall1;
    private Ball _lastBall2;

    public override void Rotate()
    {
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 2) _rotatingPhase = 0;
    }

    private void Start()
    {
        if (!isFiveLength) Invoke(nameof(Port), _portFrequency);
    }

    private void Update()
    {
        if (!isFiveLength)
        {
            if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer))
            {
                if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
                {
                    ball.Port();
                }
            }

            if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer))
            {
                if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
                {
                    ball.Port();
                }
            }
        }
        else
        {
            if (Physics2D.OverlapPoint(entry1.position))
            {
                if (Physics2D.OverlapPoint(entry1.position).TryGetComponent(out Ball ball))
                {
                    if (ball != _lastBall)
                    {
                        ball.SetToPosition(DifferentAdditions.Vector2ToVector2Int(entry2.position));
                        ball.transform.position = entry2.position;
                        _lastBall = ball;
                    }
                }
                else
                {
                    _lastBall = null;
                }
            }
            else if (Physics2D.OverlapPoint(entry2.position))
            {
                if (Physics2D.OverlapPoint(entry2.position).TryGetComponent(out Ball ball))
                {
                    if (ball != _lastBall)
                    {
                        ball.SetToPosition(DifferentAdditions.Vector2ToVector2Int(entry1.position));
                        ball.transform.position = (Vector2) entry1.position;
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
    }

    private void Port()
    {
        if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall2)
                {
                    ball.SetToPosition(DifferentAdditions.Vector2ToVector2Int(entry2.position));
                    ball.transform.position = entry2.position;
                    _lastBall1 = ball;
                }
            }
            else
            {
                _lastBall2 = null;
            }
        }
        
        if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall1)
                {
                    Debug.Log("d");
                    ball.SetToPosition(DifferentAdditions.Vector2ToVector2Int(entry1.position));
                    ball.transform.position = (Vector2) entry1.position;
                    _lastBall2 = ball;
                }
            }
            else
            {
                _lastBall1 = null;
            }
        }

        Invoke(nameof(Port), _portFrequency);
    }

    // private void CheckLastBalls()
    // {
    //     if (!(IfBallInPoint(entry1.position) && IfBallInPoint(entry2.position)))
    //     {
    //         _lastBall1 = null;
    //         _lastBall2 = null;
    //     }
    // }
    //
    // private bool IfBallInPoint(Vector2 position)
    // {
    //     if (Physics2D.OverlapPoint(position, GameManager.GroundLayer))
    //     {
    //         if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer).GetComponent<Ball>())
    //         {
    //             return true;
    //         }
    //         return false;
    //     } 
    //     return false;
    // }
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}
