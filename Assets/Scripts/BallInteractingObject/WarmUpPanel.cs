using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUpPanel : MonoBehaviour
{
    private Ball _lastBall;
    
    void Update()
    {
        if (Physics2D.OverlapPoint(transform.position))
        {
            if (Physics2D.OverlapPoint(transform.position).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    _lastBall = ball;
                    ball.WarmUp();
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
