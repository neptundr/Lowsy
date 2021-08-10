using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : BallInteractingObject
{
    public Transform point;
    
    private int _rotatingPhase;
    private float _rotationSpeed = 0.1f;
    private Quaternion _toRotation;
    private Quaternion _startToRotation;
    private Ball _lastBall;

    public override void Rotate()
    {
        _rotatingPhase += 1;
        _toRotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
        _startToRotation = _toRotation;
    }

    private void Start()
    {
        _toRotation = transform.rotation;
        _startToRotation = _toRotation;
        GameManager.Tick1 += CheckLastBall;
    }
    
    public override void ResetToStart()
    {
        _toRotation = _startToRotation;
    }

    private void CheckLastBall()
    {
        if (_lastBall != null)
        {
            if (_lastBall.GetVector2IntPosition() != DifferentAdditions.Vector2ToVector2Int(point.position))
            {
                _lastBall = null;
            }
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, _rotationSpeed);

        if (Physics2D.OverlapPoint(point.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(point.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    switch (ball.GetBallDirection())
                    {
                        case Direction.Up:
                            _toRotation = Quaternion.Euler(0, 0, 90);

                            ball.SetToPosition(new Vector2Int(Convert.ToInt32(transform.position.x),
                                Convert.ToInt32(transform.position.y) + 1));
                            if (ball.gameObject.transform.position.x > transform.position.x)
                                ball.SetBallDirection(Direction.Left);
                            else ball.SetBallDirection(Direction.Right);
                            break;
                        case Direction.Down:
                            _toRotation = Quaternion.Euler(0, 0, 270);

                            ball.SetToPosition(new Vector2Int(Convert.ToInt32(transform.position.x),
                                Convert.ToInt32(transform.position.y) - 1));
                            if (ball.gameObject.transform.position.x > transform.position.x)
                                ball.SetBallDirection(Direction.Left);
                            else ball.SetBallDirection(Direction.Right);
                            break;
                        case Direction.Right:
                            _toRotation = Quaternion.Euler(0, 0, 0);

                            ball.SetToPosition(new Vector2Int(Convert.ToInt32(transform.position.x) + 1,
                                Convert.ToInt32(transform.position.y)));
                            if (ball.gameObject.transform.position.y > transform.position.y)
                                ball.SetBallDirection(Direction.Down);
                            else ball.SetBallDirection(Direction.Up);
                            break;
                        case Direction.Left:
                            _toRotation = Quaternion.Euler(0, 0, 180);

                            ball.SetToPosition(new Vector2Int(Convert.ToInt32(transform.position.x) - 1,
                                Convert.ToInt32(transform.position.y)));
                            if (ball.gameObject.transform.position.y > transform.position.y)
                                ball.SetBallDirection(Direction.Down);
                            else ball.SetBallDirection(Direction.Up);
                            break;
                    }

                    _lastBall = ball;
                }
            }
            else
            {
                _lastBall = null;
            }
        }
    }
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Tick1 -= CheckLastBall;
    }
}