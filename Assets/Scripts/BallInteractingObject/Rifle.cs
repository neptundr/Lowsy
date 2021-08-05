using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : BallInteractingObject
{
    public int shootingFrequency = 2;
    public Bullet bullet;
    public Direction direction;

    private int _rotatingPhase;
    private int _frequencyStep;

    public override void Rotate()
    {
        direction = DifferentAdditions.NextDirection(direction);
        _rotatingPhase += 1;
        transform.rotation = DifferentAdditions.RotationPhaseToRotation(_rotatingPhase);
        if (_rotatingPhase >= 4) _rotatingPhase = 0;
    }

    public override void ResetToStart()
    {
        _frequencyStep = 0;
    }

    private void Start()
    {
        GameManager.Tick1 += Shoot;
    }

    private void OnDestroy()
    {
        GameManager.Tick1 -= Shoot;
        GameManager.ResetToStart -= ResetToStart;
    }
    
    private void Shoot()
    {
        if (_frequencyStep == 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity).GetComponent<Bullet>().SetBullet(direction, this);
        }

        _frequencyStep += 1;
        if (_frequencyStep >= shootingFrequency)
        {
            _frequencyStep = 0;
        }
    }
}
