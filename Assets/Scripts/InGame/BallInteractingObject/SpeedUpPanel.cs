using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpPanel : BallInteractingObject
{
    public Transform toSpeed;
    public Transform toSlow;

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
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    if (ball.GetPreviousPosition() == DifferentAdditions.Vector2ToVector2Int(toSlow.position)) ball.SetTickPhase(ball.GetTickPhase() - 1);
                    if (ball.GetPreviousPosition() == DifferentAdditions.Vector2ToVector2Int(toSpeed.position)) ball.SetTickPhase(ball.GetTickPhase() + 1);

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