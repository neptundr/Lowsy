using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeChanger : MonoBehaviour
{
    public GameObject windowed;
    public GameObject fullscreen;

    private void Start()
    {
        windowed.SetActive(Settings.Windowed);
        fullscreen.SetActive(!Settings.Windowed);
    }

    public void ChangeScreenMode()
    {
        Settings.ChangeScreenMode();
    }
}
