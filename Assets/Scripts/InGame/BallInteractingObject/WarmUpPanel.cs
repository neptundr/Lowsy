using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarmUpPanel : MonoBehaviour
{
    public LayerMask whatIsBallon;
    
    private Ball _lastBall;
    
    void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, whatIsBallon))
        {
            if (Physics2D.OverlapPoint(transform.position, whatIsBallon).transform.parent.TryGetComponent(out Ballon ballon))
            {
                ballon.WarmUp();
            }
        }
        
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