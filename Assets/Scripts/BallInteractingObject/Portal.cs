using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BallInteractingObject
{
    public Transform entry1;
    public Transform entry2;

    private int _rotatingPhase;
    private Ball _lastBall;

    public override void Rotate()
    {
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
    }

    private void Update()
    {
        if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(entry1.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
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
        else if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
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
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}
