using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGraphActiveSwitch : MonoBehaviour
{
    public GameObject active;
    public GameObject disactive;
    
    private void Start()
    {
        if (Settings.GraphActive)
        {
            active.SetActive(true);
            disactive.SetActive(false);
        }
        else
        {
            active.SetActive(false);
            disactive.SetActive(true);
        }
    }
    
    public void GraphActiveSwitch()
    {
        Settings.ChangeBackgroundGraphActive();
    }
}
