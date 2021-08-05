using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : BallInteractingObject
{
    public Color color;

    private Ball _lastBall;

    void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    ball.SetColor(color);

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
