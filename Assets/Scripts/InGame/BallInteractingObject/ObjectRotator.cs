using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class ObjectRotator : BallInteractingObject
{
    private static List<ObjectRotator> _objectRotators = new List<ObjectRotator>();

    public int startSetIndex;
    public Transform upObl;
    public Transform downObl;
    public Transform rightObl;
    public Transform leftObl;
    public bool goingLeft;
    public TMP_Text setIndexText;

    private bool _insertedIntoList;
    private float _rotationSpeed = 0.1f;
    private List<Transform> _catchedObjects;
    private Direction _direction = Direction.Up;
    private Quaternion _toRotation;
    private ObjectRotator _catchedBy;

    public static void ToStart()
    {
        for (int i = 0; i < _objectRotators.Count; i++)
        {
            _objectRotators[i]._direction = Direction.Up;
            _objectRotators[i]._toRotation = Quaternion.Euler(0, 0, 0);
            _objectRotators[i].transform.rotation = _objectRotators[i]._toRotation;
            _objectRotators[i].StabilizeObls();
            
            foreach (Transform catchedObject in _objectRotators[i]._catchedObjects)
            {
                catchedObject.parent = null;
            }
            _objectRotators[i]._catchedObjects.Clear();
            
            _objectRotators[i]._catchedBy = null;
        }
    }

    private void Start()
    {
        _catchedObjects = new List<Transform>();
        _toRotation = quaternion.Euler(0, 0, 0);
        transform.rotation = _toRotation;
        // CatchObject(new Vector2Int(0, 1));
        // CatchObject(new Vector2Int(0, -1));
        // CatchObject(new Vector2Int(1, 0));
        // CatchObject(new Vector2Int(-1, 0));

        GameManager.AlternativeTick1 += RotateDirection;
        GameManager.PreRestart += Catch;

        if (startSetIndex <= 0)
        {
            _objectRotators.Add(this);
            _insertedIntoList = true;
        }

        GameManager.ResetToStart += ToStart;
    }

    private void RotateDirection()
    {
        if (goingLeft) _direction = DifferentAdditions.PreviousDirection(_direction);
        else _direction = DifferentAdditions.NextDirection(_direction);
        _toRotation = DifferentAdditions.DirectionToRotation(_direction);
    }

    private void Update()
    {
        if (!_insertedIntoList)
        {
            if (_objectRotators.Count + 1 == startSetIndex)
            {
                _objectRotators.Add(this);
                _insertedIntoList = true;
            }
        }
        else
        {
            setIndexText.gameObject.SetActive(GameManager.GetIsCompletelyStopped());
            setIndexText.text = (_objectRotators.IndexOf(this) + 1).ToString();
        }

        if (GameManager.Started)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _toRotation, _rotationSpeed);

            StabilizeObls();
        }
    }

    private void StabilizeObls()
    {
        rightObl.rotation = Quaternion.Euler(0, 0, 0);
        leftObl.rotation = Quaternion.Euler(0, 0, 0);
        upObl.rotation = Quaternion.Euler(0, 0, 0);
        downObl.rotation = Quaternion.Euler(0, 0, 0);
    }
    
    private void OnDestroy()
    {
        _objectRotators.Remove(this);
        GameManager.ResetToStart -= ResetToStart;
        GameManager.PreRestart -= Catch;
        GameManager.AlternativeTick1 -= RotateDirection;
    }

    private void Catch()
    {
        CatchObject(new Vector2Int(0, 1), upObl);
        CatchObject(new Vector2Int(0, -1), downObl);
        CatchObject(new Vector2Int(1, 0), rightObl);
        CatchObject(new Vector2Int(-1, 0), leftObl);
    }
    
    private void CatchObject(Vector2Int position, Transform futureParent)
    {
        if (Physics2D.OverlapPoint(new Vector2(transform.position.x + position.x, transform.position.y + position.y), GameManager.GroundLayer))
        {
            if (Physics2D
                .OverlapPoint(new Vector2(transform.position.x + position.x, transform.position.y + position.y), GameManager.GroundLayer)
                .TryGetComponent(out BallInteractingObject bio))
            {
                if (bio.GetComponent<PlaceableObject>().canBeCatched)
                {
                    if (bio.TryGetComponent(out ObjectRotator or))
                    {
                        if (_objectRotators.IndexOf(or) < _objectRotators.IndexOf(this))
                        {
                            return;
                        }
                        else
                        {
                            if (or._catchedBy == null)
                            {
                                or._catchedBy = this;
                            }
                            else
                            {
                                if (_objectRotators.IndexOf(or._catchedBy) < _objectRotators.IndexOf(this))
                                {
                                    _objectRotators[_objectRotators.IndexOf(or._catchedBy)]._catchedObjects
                                        .Remove(or.transform);
                                    or._catchedBy = this;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        bio.GetComponent<PlaceableObject>().AddCatchedByCount();
                    }

                    if (bio.GetComponent<PlaceableObject>().GetCatchedByCount() <= 1)
                    {
                        _catchedObjects.Add(bio.transform);
                        bio.transform.parent = futureParent;
                    }
                }
            }
        }
    }
}