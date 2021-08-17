using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstLaunch : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("FirstLaunch") != 1);
    }

    public void Yes()
    {
        PlayerPrefs.SetInt("FirstLaunch", 1);
        Loader.LoadScene("BasicTutorial");
    }

    public void No()
    {
        PlayerPrefs.SetInt("FirstLaunch", 1);
        
        Loader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
