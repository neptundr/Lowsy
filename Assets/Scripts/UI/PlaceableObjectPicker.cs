using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceableObjectPicker : MonoBehaviour
{
    public Image image;
    public PlaceableObject placeableObject;
    public Text countText;

    private int _count;
    private int _startCount;

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }
    
    public void SetPlaceable()
    {
        if (_count > 0 && GameManager.GetIsCompletelyStopped()) Placer.This.SetPlaceable(placeableObject, this);
    }

    public void SetCount(int count)
    {
        _count = count;
        UpdateText();
    }
    
    public void AddCount()
    {
        _count += 1;
        UpdateText();
    }

    public void SubCount()
    {
        _count -= 1;
        UpdateText();
    }

    private void UpdateText()
    {
        countText.text = _count.ToString();
    }

    private void Start()
    {
        _startCount = _count;
    }
}