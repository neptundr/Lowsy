using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : BallInteractingObject
{
    public Transform entry1;
    public Transform entry2;

    private int _rotatingPhase;
    private float _portFrequency = 0.05f;
    private Ball _lastBall1;
    private Ball _lastBall2;

    public override void Rotate()
    {
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
    }

    private void Start()
    {
        Invoke(nameof(Port), _portFrequency);
        // GameManager.Tick3 += Port;
        // GameManager.AlternativeTick3 += Port;
    }

    private void Update()
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
        else
        {
            _lastBall2 = null;
        }
        
        if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(entry2.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall1)
                {
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
        else
        {
            _lastBall1 = null;
        }
        
        Invoke(nameof(Port), _portFrequency);
    }
    
    private void OnDestroy()
    {
        // GameManager.Tick3 -= Port;
        // GameManager.AlternativeTick3 -= Port;
        GameManager.ResetToStart -= ResetToStart;
    }
}
