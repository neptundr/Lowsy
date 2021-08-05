using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallStartPoint : BallInteractingObject
{
    public Ball ball;
    public Direction startDirection;
    public Color color;
    
    private int _rotatingPhase;
    private Animator _anim;
    
    public override void Rotate()
    {
        startDirection = DifferentAdditions.NextDirection(startDirection);
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
    }

    private void Start()
    {
        GameManager.Restart += OnStart;
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _anim.SetBool("CompletelyStopped", GameManager.GetIsCompletelyStopped());
    }

    private void OnDestroy()
    {
        GameManager.Restart -= OnStart;
        GameManager.ResetToStart -= ResetToStart;
    }

    private void OnStart()
    {
        Ball nowBall = Instantiate(ball, transform.position, Quaternion.identity).GetComponent<Ball>();
        nowBall.SetBallDirection(startDirection);
        nowBall.SetColor(color);
    }
}
