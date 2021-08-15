using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Placer : MonoBehaviour
{
    public static Placer This;

    public GameObject shineUpOn;
    public GameObject shineUpOff;
    
    private bool _isShined;
    private Vector2Int _placerPosition;
    private Vector2 _cursorPosition;
    private Camera _mainCamera;
    private PlaceableObject _nowPlaceable;
    private PlaceableObjectPicker _lastPicker;

    public void ShineUpPlacedObjects()
    {
        _isShined = !_isShined;
        UnityEngine.Color color = _isShined ? new UnityEngine.Color(GameManager.PrePlacedObjectsOpacity,
            GameManager.PrePlacedObjectsOpacity, GameManager.PrePlacedObjectsOpacity) : new UnityEngine.Color(1, 1, 1);
        
        foreach (SpriteRenderer sr in GameManager.OnStartSpriteRenderers)
        {
            sr.color = color;
        }

        if (!_isShined)
        {
            shineUpOn.SetActive(true);
            shineUpOff.SetActive(false);
        }
        else
        {
            shineUpOn.SetActive(false);
            shineUpOff.SetActive(true);
        }
    }
    
    public void DestroyPlacedObjects()
    {
        if (GameManager.GetIsCompletelyStopped())
        {
            foreach (PlaceableObject placeableObject in FindObjectsOfType<PlaceableObject>().Where(po => po.prePlaced == false))
            {
                placeableObject.GetPicker().AddCount();
                Destroy(placeableObject.gameObject);
            }
        }
    }

    public PlaceableObject GetNowPlaceable()
    {
        return _nowPlaceable;
    }
    
    public void SetPlaceable(PlaceableObject original, PlaceableObjectPicker nowVariant)
    {
        _lastPicker = nowVariant;
        
        if(_nowPlaceable != null) Destroy(_nowPlaceable.gameObject);
        _nowPlaceable = Instantiate(original.gameObject, (Vector2) _placerPosition,
            original.transform.rotation).GetComponent<PlaceableObject>();
        _nowPlaceable.SetPicker(nowVariant);
    }
    
    private void Start()
    {
        This = this;
        _mainCamera = Camera.main;

        GameManager.PreRestart += DestroyNowPlaceable;
    }

    private void DestroyNowPlaceable()
    {
        if (_nowPlaceable != null) Destroy(_nowPlaceable.gameObject);
    }
    
    private void Update()
    {
        _cursorPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(_nowPlaceable != null) _nowPlaceable.transform.position = (Vector2) _placerPosition;

        CheckMove();
        MovePosition();
        CheckInput();
    }

    private void CheckInput()
    {
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z)) && _nowPlaceable != null && GameManager.GetIsCompletelyStopped()) Place();
        if ((Input.GetMouseButtonUp(1) || Input.GetKeyUp(KeyCode.C)) && _nowPlaceable != null) _nowPlaceable.Rotate();
        if ((Input.GetMouseButton(2) || Input.GetKey(KeyCode.X)) && GameManager.GetIsCompletelyStopped())
        {
            if (_nowPlaceable == null)
            {
                if (Physics2D.OverlapPoint(_placerPosition))
                {
                    if (Physics2D.OverlapPoint(_placerPosition).TryGetComponent(out PlaceableObject pc))
                    {
                        if (!pc.prePlaced)
                        {
                            pc.GetPicker().AddCount();
                            Destroy(pc.gameObject);
                        }
                    }
                }
            }
            else
            {
                DestroyNowPlaceable();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl)) DestroyPlacedObjects();
        if (Input.GetKeyDown(KeyCode.RightControl)) ShineUpPlacedObjects();
    }

    private void MovePosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, (Vector2) _placerPosition, 100);
    }
    
    private void CheckMove()
    {
        if (_cursorPosition.x + 0.5f < _placerPosition.x) MoveTo(new Vector2Int(-1, 0));
        else if (_cursorPosition.x - 0.5f > _placerPosition.x) MoveTo(new Vector2Int(1, 0));
        else if (_cursorPosition.y + 0.5f < _placerPosition.y) MoveTo(new Vector2Int(0, -1));
        else if (_cursorPosition.y - 0.5f > _placerPosition.y) MoveTo(new Vector2Int(0, 1));
    }

    private void MoveTo(Vector2Int to)
    {
        _placerPosition = new Vector2Int(_placerPosition.x + to.x, _placerPosition.y + to.y);
    }

    private void Place()
    {
        if (_nowPlaceable.GetCanBePlaced())
        {
            _nowPlaceable.transform.SetParent(null);
            _nowPlaceable.Place();
            _nowPlaceable = null;
            _lastPicker.SubCount();
        }
    }
}