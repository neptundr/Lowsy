using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePanel : MonoBehaviour
{
    public PlaceableObject[] objects;
    public int[] objectCounts;
    
    private PlaceableObjectPicker[] _pickers;

    private void Start()
    {
        _pickers = GetComponentsInChildren<PlaceableObjectPicker>();
        if (objects.Length != objectCounts.Length || _pickers.Length < objects.Length) throw new InvalidCastException();
        
        for (int i = 0; i < objects.Length; i++)
        {
            _pickers[i].placeableObject = objects[i];
            _pickers[i].SetCount(objectCounts[i]);
            _pickers[i].SetImage(objects[i].GetComponent<SpriteRenderer>().sprite);
        }

        for (int i = objects.Length; i < _pickers.Length; i++)
        {
            _pickers[i].gameObject.SetActive(false);
        }
    }
}