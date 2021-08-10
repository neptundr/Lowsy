using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Bug : BallInteractingObject
{
    public int bugSpikesCountStart = 2;
    public Direction startDirection;
    public bool turningRight;
    public GameObject bugSpike;
    public TMP_Text spikesCountText;

    private bool _isDead;
    private int _bugSpikesCount;
    private float _rotationSpeed = 0.1f;
    private float _movingSpeed = 0.1f;
    private List<GameObject> _bugSpikes = new List<GameObject>(1);
    private Quaternion _toRotation;
    private Vector2 _toPosition;
    private Vector2Int _startPosition;
    private Direction _direction;

    public override void Rotate()
    {
        startDirection = DifferentAdditions.NextDirection(startDirection);

        _toRotation = DifferentAdditions.DirectionToRotation(startDirection);
    }

    private void UpdateSpikesCountText()
    {
        spikesCountText.text = _bugSpikesCount.ToString();
    }
    
    private void Start()
    {
        GameManager.Tick1 += Move;
        GameManager.Restart += OnRestart;
        _bugSpikesCount = bugSpikesCountStart;
        UpdateSpikesCountText();
    }

    private void OnRestart()
    {
        _isDead = false;
        _direction = startDirection;
        _startPosition = DifferentAdditions.Vector2ToVector2Int(transform.position);
        _toPosition = _startPosition;
        _toRotation = DifferentAdditions.DirectionToRotation(startDirection);
    }

    public override void ResetToStart()
    {
        _isDead = false;
        _direction = startDirection;
        _toPosition = _startPosition;
        transform.position = _toPosition;
        _toRotation = DifferentAdditions.DirectionToRotation(startDirection);
        
        ClearSpikes();
        _bugSpikesCount = bugSpikesCountStart;
    }

    private void ClearSpikes()
    {
        for (int i = 0; i < _bugSpikes.Count; i++)
        {
            Destroy(_bugSpikes[i]);
        }
        _bugSpikes.Clear();
    }
    
    private void OnDestroy()
    {
        ClearSpikes();
        
        GameManager.ResetToStart -= ResetToStart;
        GameManager.Restart -= OnRestart;
        GameManager.Tick1 -= Move;
    }

    private void Update()
    {
        if (GameManager.Started && !_isDead)
        {
            if (Physics2D.OverlapPoint(_toPosition + DifferentAdditions.DirectionToVector2Int(_direction), GameManager.GroundLayer))
            {
                if (!(Physics2D.OverlapPoint(_toPosition + new Vector2(1, 0), GameManager.GroundLayer) && 
                    Physics2D.OverlapPoint(_toPosition + new Vector2(-1, 0), GameManager.GroundLayer) &&
                    Physics2D.OverlapPoint(_toPosition + new Vector2(0, 1), GameManager.GroundLayer) &&
                    Physics2D.OverlapPoint(_toPosition + new Vector2(0, -1), GameManager.GroundLayer)))
                {
                    if (Physics2D.OverlapPoint(_toPosition + DifferentAdditions.DirectionToVector2Int(_direction), GameManager.GroundLayer)
                        .gameObject != gameObject)
                    {
                        RotateBug();
                    }
                }
            }

            transform.position = Vector3.MoveTowards(transform.position, _toPosition, _movingSpeed);
        }
        
        spikesCountText.transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, _rotationSpeed);
    }

    private void RotateBug()
    {
        _direction = turningRight ? DifferentAdditions.NextDirection(_direction) : DifferentAdditions.PreviousDirection(_direction);

        _toRotation = DifferentAdditions.DirectionToRotation(_direction);
    }

    private void Move()
    {
        if (!_isDead)
        {
            if (_bugSpikes.Count > _bugSpikesCount - 1)
            {
                Destroy(_bugSpikes[0]);
                _bugSpikes.Remove(_bugSpikes[0]);
            }

            _bugSpikes.Add(Instantiate(bugSpike, transform.position, Quaternion.identity));

            Vector2 to = DifferentAdditions.DirectionToVector2Int(_direction);
            _toPosition = new Vector2(_toPosition.x + to.x, _toPosition.y + to.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (GameManager.Started && !_bugSpikes.Contains(other.gameObject))
        {
            _isDead = true;
            ClearSpikes();
        }
    }
}