using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emitter : BallInteractingObject
{
    [Header("Must be set!")] public Color color;
    public Direction direction;
    public Laser laser;

    private bool _isFunctioning;
    private int _rotatingPhase;
    private int _length = 50;
    private Transform[] _laserLine;
    private Quaternion _previousRotation;

    private void OnEnable()
    {
        _isFunctioning = true;
    }

    private void OnDisable()
    {
        _isFunctioning = false;
    }

    public override void Rotate()
    {
        direction = DifferentAdditions.NextDirection(direction);
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
    }

    private void Start()
    {
        _laserLine = new Transform[_length];
        GameManager.PreRestart += Emit;
        // GameManager.Restart += CheckLaserLine;
    }

    private void Update()
    {
        if (GameManager.Started) CheckLaserLine();
    }

    private void CheckLaserLine()
    {
        int laserDistance = _length;
        if (_isFunctioning && Physics2D.Raycast(transform.position, DifferentAdditions.DirectionToVector2Int(direction),
            _length, GameManager.GroundLayer))
        {
            if (Physics2D.Raycast(transform.position, DifferentAdditions.DirectionToVector2Int(direction), _length, GameManager.GroundLayer)
                .transform.TryGetComponent(out Ball ball))
            {
                if (ball.GetColor() != color)
                {
                    ball.Die();
                    GameManager.Lose();
                }
            }

            laserDistance = Convert.ToInt32(Vector2.Distance(transform.position,
                Physics2D.Raycast(transform.position, DifferentAdditions.DirectionToVector2Int(direction), _length, GameManager.GroundLayer)
                    .transform.position));
        }

        for (int i = 0; i < laserDistance; i++)
        {
            _laserLine[i].gameObject.SetActive(true);
        }

        for (int i = laserDistance; i < _length; i++)
        {
            _laserLine[i].gameObject.SetActive(false);
        }
    }

    private void Emit()
    {
        Vector2Int to = DifferentAdditions.DirectionToVector2Int(direction);

        _laserLine[0] = Instantiate(laser.gameObject, new Vector3(transform.position.x, transform.position.y),
            Quaternion.identity, transform).transform;
        _laserLine[0].transform.rotation = transform.rotation;
        _laserLine[0].GetComponent<Laser>().SetColor(color);

        for (int i = 1; i < _length; i++)
        {
            _laserLine[i] = Instantiate(laser.gameObject,
                new Vector3(_laserLine[i - 1].position.x + to.x, _laserLine[i - 1].position.y + to.y),
                Quaternion.identity, _laserLine[i - 1]).transform;
            _laserLine[i].rotation = transform.rotation;
            _laserLine[i].GetComponent<Laser>().SetColor(color);
        }
    }

    private void OnDestroy()
    {
        GameManager.PreRestart -= Emit;
        // GameManager.Tick3 -= CheckLaserLine;
        GameManager.ResetToStart -= ResetToStart;
    }
}