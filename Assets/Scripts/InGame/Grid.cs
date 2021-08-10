using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

public class Grid : MonoBehaviour
{
    private int _size = 100; // must be even
    private float _opacity = 0.05f;
    private float _width = 0.25f;
    private Vector2 _offset = new Vector2(0.5f, 0.5f);
    private LineRenderer _line;
    
    private void Start()
    {
        if (_size % 2 != 0) throw new InvalidCastException();
        
        _line = GetComponent<LineRenderer>();
        _line.positionCount = _size * 4;
        _line.startColor = new UnityEngine.Color(1, 1, 1, _opacity);
        _line.endColor = new UnityEngine.Color(1, 1, 1, _opacity);
        _line.startWidth = _width;
        _line.endWidth = _width;

        Vector2 downLeftCorner =
            new Vector2(transform.position.x - _size / 2, transform.position.y - _size / 2);
        
        for (int i = 0; i < _size * 2; i += 2)
        {
            _line.SetPosition(i, new Vector2(downLeftCorner.x + ((i / 2) % 2 == 0 ? 0 : _size), downLeftCorner.y + (i / 2)) + _offset);
            _line.SetPosition(i + 1, new Vector2(downLeftCorner.x + ((i / 2) % 2 == 0 ? _size : 0), downLeftCorner.y + (i / 2)) + _offset);
        }
        for (int i = 0; i < _size * 2; i += 2)
        {
            _line.SetPosition(i + _size * 2, new Vector2(downLeftCorner.x + (i / 2), downLeftCorner.y + ((i / 2) % 2 == 0 ? 0 : _size)) + _offset);
            _line.SetPosition(i + 1 + _size * 2, new Vector2(downLeftCorner.x + (i / 2), downLeftCorner.y + ((i / 2) % 2 == 0 ? _size : 0)) + _offset);
        }
    }
}
