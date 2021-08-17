﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SettingsButton : MonoBehaviour
{
    public Animator settingsIcons;

    private bool _activated = true;
    
    private void Start()
    {
        settingsIcons.SetTrigger("On");
        Invoke(nameof(SettingsIconsActiveChange), 0.005f);
    }

    public void SettingsIconsActiveChange()
    {
        _activated = !_activated;
        if (_activated) settingsIcons.SetTrigger("On");
        else settingsIcons.SetTrigger("Off");
    }
}
