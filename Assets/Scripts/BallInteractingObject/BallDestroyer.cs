using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDestroyer : BallInteractingObject
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                _anim.SetTrigger("Destroy");
                Destroy(ball.gameObject);
            }
        }
    }
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
    }
}
