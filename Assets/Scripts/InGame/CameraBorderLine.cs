using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(LineRenderer))]
public class CameraBorderLine : MonoBehaviour
{
    public Transform leftUpPoint;
    public Transform rightDownPoint;

    private float _opacity = 0.1f;
    private float _lineLength = 0.2f;
    private LineRenderer _line;

    void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 4;
        _line.sortingOrder = 10;
        _line.endWidth = _lineLength;
        _line.startWidth = _lineLength;
        _line.loop = true;
        _line.startColor = new UnityEngine.Color(_line.startColor.r, _line.startColor.g, _line.startColor.b, _opacity);
        _line.endColor = new UnityEngine.Color(_line.endColor.r, _line.endColor.g, _line.endColor.b, _opacity);

        _line.SetPosition(0, leftUpPoint.position);
        _line.SetPosition(1, new Vector3(rightDownPoint.position.x, leftUpPoint.position.y));
        _line.SetPosition(2, rightDownPoint.position);
        _line.SetPosition(3, new Vector3(leftUpPoint.position.x, rightDownPoint.position.y));
    }
}
