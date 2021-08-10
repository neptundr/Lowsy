using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winpoint : BallInteractingObject
{
    private bool _used;
    private Animator _anim;
    private void Start()
    {
        _anim = GetComponent<Animator>();
        GameManager.Restart += PlusBallToRegister;
    }

    private void PlusBallToRegister()
    {
        GameManager.BallsToRegister += 1;
    }
    
    public override void ResetToStart()
    {
        _used = false;
    }

    private void UpdateAnims()
    {
        _anim.SetBool("Used", _used);
    }
    
    void Update()
    {
        UpdateAnims();
        
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (!_used)
                {
                    GameManager.RegisterBall();
                    _used = true;
                    Destroy(ball.gameObject);
                }
            }
        }
    }

    private void OnDestroy()
    {
        GameManager.Restart -= PlusBallToRegister;
        GameManager.ResetToStart -= ResetToStart;
    }
}
