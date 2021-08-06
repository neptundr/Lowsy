using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    [Header("Sorting order depends too")]
    public string skyLayerName;
    public string groundLayerName;
    [Header("")]
    public GameObject shadow;
    public Tier tier;

    private bool _catched;
    private bool _isDead;
    private int _movingPhase;
    private float _speed = 0.1f;
    private float _tierSpawnDelay = 0.75f;
    private Bird _catchedBy;
    private Vector2Int _toPosition;
    private Vector2Int _previousPosition;
    private Direction _movingDirection;
    private Color _color;
    private Animator _anim;
    private SpriteRenderer _sr;
    private VerticalDirection _verticalPosition = VerticalDirection.Down;

    public bool GetIsCatched()
    {
        return _catched;
    }
    public Bird GetByCatched()
    {
        return _catchedBy;
    }
    
    public void BeCatched(Bird by)
    {
        _catchedBy = by;
        _catched = true;
    }

    public void UnCatched()
    {
        _catched = false;
    }
    
    public void Die()
    {
        _isDead = true;
        Invoke(nameof(SpawnTier), _tierSpawnDelay);
    }
    
    public void SetColor(Color color)
    {
        _color = color;
    }

    public Color GetColor()
    {
        return _color;
    }
    
    public void SetToPosition(Vector2Int to)
    {
        _toPosition = to;
    }

    public VerticalDirection GetVerticalPosition()
    {
        return _verticalPosition;
    }
    
    public void GoDown()
    {
        _verticalPosition = VerticalDirection.Down;
        gameObject.layer = LayerMask.NameToLayer(groundLayerName);
        _sr.sortingLayerName = groundLayerName;
        shadow.SetActive(false);
    }

    public void GoUp()
    {
        _verticalPosition = VerticalDirection.Up;
        gameObject.layer = LayerMask.NameToLayer(skyLayerName);
        _sr.sortingLayerName = skyLayerName;
        shadow.SetActive(true);
    }
    
    public Vector2Int GetPreviousPosition()
    {
        return _previousPosition;
    }
    
    public Vector2Int GetVector2IntPosition()
    {
        return new Vector2Int(Convert.ToInt32(transform.position.x), Convert.ToInt32(transform.position.y));
    }
    
    public void SetBallDirection(Direction direction)
    {
        _movingDirection = direction;
    }
    
    public Direction GetBallDirection()
    {
        return _movingDirection;
    }

    public int GetTickPhase()
    {
        return _movingPhase;
    }
    
    public void SetTickPhase(int phase)
    {
        _movingPhase = phase;

        GameManager.Tick1 -= Move;
        GameManager.Tick2 -= Move;
        GameManager.Tick3 -= Move;

        if (_movingPhase < 1) _movingPhase = 1;
        if (_movingPhase > 3) _movingPhase = 3;
        
        switch (_movingPhase)
        {
            case 1:
                GameManager.Tick1 += Move;
                break;
            case 2:
                GameManager.Tick2 += Move;
                break;
            case 3:
                GameManager.Tick3 += Move;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpdateAnims()
    {
        _anim.SetFloat("IsOrange", _color == Color.Color2 ? 0 : 1);
        _anim.SetInteger("TickPhase", _movingPhase);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Die();
        GameManager.Lose();
    }

    public void AddTickPhase(int addition)
    {
        if (addition < 1) throw new ArgumentOutOfRangeException();
        
        if(_movingPhase >= GameManager.TickPhaseMax) return;
        
        SetTickPhase(_movingPhase + addition);
    }

    public void SubTickPhase(int subtrahend)
    {
        if (subtrahend < 1) throw new ArgumentOutOfRangeException();
        
        if(_movingPhase <= 1) return;
        
        SetTickPhase(_movingPhase - subtrahend);
    }

    public void Move()
    { 
        Move(DifferentAdditions.DirectionToVector2Int(_movingDirection));
    }

    private void SpawnTier()
    {
        Instantiate(tier, transform.position, Quaternion.identity);
        Invoke(nameof(SpawnTier), _tierSpawnDelay * Random.Range(0.5f, 1.5f));
    }

    private void DestroyBall()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.ResetToStart -= DestroyBall;
        GameManager.Tick1 -= Move;
        GameManager.Tick2 -= Move;
        GameManager.Tick3 -= Move;
    }

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
        _toPosition = new Vector2Int(Convert.ToInt32(transform.position.x), Convert.ToInt32(transform.position.y));
        GameManager.ResetToStart += DestroyBall;
        SetTickPhase(1);
    }

    private void Move(Vector2Int to)
    {
        if (!_catched)
        {
            _previousPosition = _toPosition;
            _toPosition = new Vector2Int(_toPosition.x + to.x, _toPosition.y + to.y);
        }
    }

    private void Update()
    {
        if (!_isDead)
        {
            UpdateAnims();
            transform.position = Vector3.MoveTowards(transform.position, (Vector2) _toPosition, _speed);
        }
    }
}