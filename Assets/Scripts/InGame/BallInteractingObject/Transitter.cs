using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitter : BallInteractingObject
{
    public Transform firstPoint;
    public Transform secondPoint;
    public Transform out1;
    public Transform out2;

    private int _rotateStep;
    private Ball _lastBall;
    
    public override void Rotate()
    {
        switch (_rotateStep)
        {
            case 0:
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _rotateStep += 1;
                break;
            case 1:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                _rotateStep += 1;
                break;
            case 2:
                transform.rotation = Quaternion.Euler(180, 0, -90);
                _rotateStep += 1; 
                break;
            case 3:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _rotateStep += 1;
                break;
        }

        if (_rotateStep > 3) _rotateStep = 0;
    }
    
    private void Update()
    {
        if (_lastBall != null)
        {
            if (_lastBall.GetVector2IntPosition() == DifferentAdditions.Vector2ToVector2Int(out1.position) || 
                _lastBall.GetVector2IntPosition() == DifferentAdditions.Vector2ToVector2Int(out2.position))
            {
                _lastBall = null;
            }
        }

        if (Physics2D.OverlapPoint(firstPoint.position, GameManager.GroundLayer) && 
            Physics2D.OverlapPoint(firstPoint.position, GameManager.GroundLayer).GetComponent<Ball>())
        {
            if (Physics2D.OverlapPoint(firstPoint.position, GameManager.GroundLayer).GetComponent<Ball>() != _lastBall)
            {
                _lastBall = Physics2D.OverlapPoint(firstPoint.position, GameManager.GroundLayer).GetComponent<Ball>();
                Physics2D.OverlapPoint(firstPoint.position, GameManager.GroundLayer).GetComponent<Ball>()
                    .SetToPosition(DifferentAdditions.Vector2ToVector2Int(secondPoint.position));
            }
        }
        else if (Physics2D.OverlapPoint(secondPoint.position, GameManager.GroundLayer) &&
                 Physics2D.OverlapPoint(secondPoint.position, GameManager.GroundLayer).GetComponent<Ball>())
        {
            if (Physics2D.OverlapPoint(secondPoint.position, GameManager.GroundLayer).GetComponent<Ball>() != _lastBall)
            {
                _lastBall = Physics2D.OverlapPoint(secondPoint.position).GetComponent<Ball>();
                Physics2D.OverlapPoint(secondPoint.position, GameManager.GroundLayer).GetComponent<Ball>()
                    .SetToPosition(DifferentAdditions.Vector2ToVector2Int(firstPoint.position));
            }
        }
    }
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}