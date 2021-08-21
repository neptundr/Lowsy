using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReportBug : MonoBehaviour
{
    public GameObject bugReportText;

    private float _waitTime = 5;
    
    public void Click()
    {
        bugReportText.SetActive(!bugReportText.activeSelf);
        Invoke(nameof(SetActiveFalse), _waitTime);
    }

    private void SetActiveFalse()
    {
        bugReportText.SetActive(false);
    }
}
