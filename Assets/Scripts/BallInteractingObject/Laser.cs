using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BallInteractingObject
{
    public Sprite orange;
    public Sprite blue;
    
    private Color _color;
    private Ball _lastBall;

    public override void ResetToStart()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    if (ball.GetColor() != _color)
                    {
                        Debug.Log("asd");
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
    
    public void SetColor(Color color)
    {
        _color = color;
        if (color == Color.Color1) GetComponent<SpriteRenderer>().sprite = orange;
        else GetComponent<SpriteRenderer>().sprite = blue;
    }
}
