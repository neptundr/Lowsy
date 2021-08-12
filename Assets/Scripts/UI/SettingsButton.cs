using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SettingsButton : MonoBehaviour
{
    public GameObject[] settingsIcons;

    private bool _activated = true;
    
    private void Start()
    {
        foreach (GameObject settingsIcon in settingsIcons) settingsIcon.SetActive(true);
        
        Invoke(nameof(SettingsIconsActiveChange), 0.005f);
    }

    public void SettingsIconsActiveChange()
    {
        _activated = !_activated;
        foreach (GameObject settingsIcon in settingsIcons) settingsIcon.SetActive(_activated);
    }
}
