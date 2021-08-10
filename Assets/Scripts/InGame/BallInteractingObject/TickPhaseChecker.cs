using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickPhaseChecker : BallInteractingObject
{
    public int tickPhaseBeLike = 2; 

    private Ball _lastBall;
    
    void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    if (ball.GetTickPhase() != tickPhaseBeLike)
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
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}
