using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGraphActiveSwitch : MonoBehaviour
{
    public GameObject active;
    public GameObject disactive;
    
    private void Start()
    {
        if (!Settings.GraphActive)
        {
            disactive.SetActive(true);
            active.SetActive(false);
        }
        else
        {
            disactive.SetActive(false);
            active.SetActive(true);
        }
    }
    
    public void GraphActiveSwitch()
    {
        Settings.ChangeBackgroundGraphActive();
    }
}
