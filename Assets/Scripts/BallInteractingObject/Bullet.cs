using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BallInteractingObject
{
    private int _lifeTicks = 15;
    private float _speed = 0.1f;
    private Vector2Int _movingTo;
    private Vector2Int _toPosition;
    private Rifle _myRifle;
    
    public void SetBullet(Direction direction, Rifle myRifle)
    {
        transform.rotation = DifferentAdditions.DirectionToRotation(direction);
        _toPosition = DifferentAdditions.Vector2ToVector2Int(transform.position);
        _movingTo = DifferentAdditions.DirectionToVector2Int(direction);
        _myRifle = myRifle;
        GameManager.Tick1 += Move;
    }

    public override void ResetToStart()
    {
        Destroy(gameObject, 0.1f);
        GameManager.Tick1 -= Move;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, (Vector2) _toPosition, _speed);
    }

    private void Move()
    {
        _toPosition = new Vector2Int(_toPosition.x + _movingTo.x, _toPosition.y + _movingTo.y);
        _lifeTicks -= 1;

        if (_lifeTicks <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Ball>()) Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.Tick1 -= Move;
        GameManager.ResetToStart -= ResetToStart;
    }
}