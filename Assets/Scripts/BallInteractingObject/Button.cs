using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Button : BallInteractingObject
{
    public Color color;
    public SpriteRenderer[] activateable;
    public bool[] actives;

    private bool _onStartBool;
    private float _opacityLevel = 0.65f;
    private float _lineLength = 0.05f;
    private UnityEngine.Color _lightGreen = new UnityEngine.Color(0.55f, 0.7f, 0.3f);
    private UnityEngine.Color _darkRed = new UnityEngine.Color(0.5f, 0.05f, 0.05f);
    private Ball _lastBall;
    private Animator _anim;
    private LineRenderer _line;

    public override void ResetToStart()
    {
        if (_onStartBool)
        {
            _onStartBool = false;
            ChangeActive();
        }
        
        for (int i = 0; i < activateable.Length; i++)
        {
            activateable[i].gameObject.SetActive(true);
        }
    }
    
    private void OnDestroy()
    {
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Restart -= SetActivateables;
        GameManager.Restart -= SetFullOpacity;
        GameManager.ResetToStart -= SetHalfOpacity;
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _line = GetComponent<LineRenderer>();
        _line.positionCount = activateable.Length * 2 + 1;
        _line.sortingOrder = 10;
        _line.endWidth = _lineLength;
        _line.startWidth = _lineLength;
        
        GameManager.Restart += SetActivateables;
        GameManager.Restart += SetFullOpacity;
        GameManager.ResetToStart += SetHalfOpacity;
        
        SetActivateablesTrue();
        SetHalfOpacity();
    }

    private void SetActivateablesTrue()
    {
        for (int i = 0; i < activateable.Length; i++)
        {
            activateable[i].gameObject.SetActive(true);
        }
    }

    private void SetHalfOpacity()
    {
        _line.enabled = true;
        for (int i = 0; i < activateable.Length; i++)
        {
            if (actives[i]) activateable[i].color = _darkRed;
            else activateable[i].color = _lightGreen;
            
            activateable[i].color = new UnityEngine.Color(activateable[i].color.r,
                activateable[i].color.g, activateable[i].color.b, _opacityLevel);
            
            _line.SetPosition(i * 2, transform.position);
            _line.SetPosition(i * 2 + 1, activateable[i].transform.position);
        }
        _line.SetPosition(activateable.Length * 2, transform.position);
    }
    
    private void SetFullOpacity()
    {
        _line.enabled = false;
        for (int i = 0; i < activateable.Length; i++)
        {
            activateable[i].color = new UnityEngine.Color(1, 1, 1, 1);
        }
    }
    
    private void UpdateAnims()
    {
        _anim.SetBool("Pressed", _onStartBool);
    }

    private void Update()
    {
        UpdateAnims();
        
        if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer))
        {
            if (Physics2D.OverlapPoint(transform.position, GameManager.GroundLayer).TryGetComponent(out Ball ball))
            {
                if (ball != _lastBall)
                {
                    if (ball.GetColor() == color)
                    {
                        ChangeActive();

                        _onStartBool = !_onStartBool;

                        _lastBall = ball;
                    }
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

    private void ChangeActive()
    {
        for (int i = 0; i < activateable.Length; i++)
        {
            actives[i] = !actives[i];
        }
        SetActivateables();
    }

    private void SetActivateables()
    {
        for (int i = 0; i < activateable.Length; i++)
        {
            activateable[i].gameObject.SetActive(actives[i]);
        }
    }
}
