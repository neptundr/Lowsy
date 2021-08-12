using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLaunch : MonoBehaviour
{
    public void Yes()
    {
        Debug.Log("Yes");
    }

    public void No()
    {
        gameObject.SetActive(false);
    }
}
