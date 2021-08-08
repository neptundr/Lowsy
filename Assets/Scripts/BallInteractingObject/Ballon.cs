using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballon : MonoBehaviour
{
    public GameObject shadow;
    
    private bool _warmed;
    private Ball _lastBall;
    private LayerMask _whatCol;
    
    private void Start()
    {
        UpdateLayer();
        GameManager.Tick1 += CoolUp;
    }

    private void Update()
    {
        if (Physics2D.OverlapPoint(transform.position, _whatCol))
        {
            if (Physics2D.OverlapPoint(transform.position, _whatCol).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    _lastBall = ball;
                    GameManager.Lose();
                    ball.Die();
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
        GameManager.Tick1 -= CoolUp;
    }

    public void WarmUp()
    {
        _warmed = true;
        UpdateLayer();
    }

    private void CoolUp()
    {
        _warmed = false;
        UpdateLayer();
    }

    private void UpdateLayer()
    {
        _whatCol = _warmed ? GameManager.SkyLayer : GameManager.GroundLayer;
        shadow.SetActive(_warmed);
    }
}
