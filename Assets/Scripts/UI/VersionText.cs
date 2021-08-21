using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VersionText : MonoBehaviour
{
    private static string Version = "1.0";
    private void Start()
    {
        GetComponent<Text>().text = Version;
    }
}
