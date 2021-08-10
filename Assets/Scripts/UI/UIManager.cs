using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager This;

    public Text levelName;
    public Text levelDifficulty;

    private void Start()
    {
        This = this;
    }
}
