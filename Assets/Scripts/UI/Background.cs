using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject graph;
    
    private void Start()
    {
        graph.SetActive(Settings.GraphActive);
    }
}
