using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static Language ProjectLanguage = Language.Eng;
    public static bool GraphActive = true;
    
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        
        // if (PlayerPrefs.HasKey("Language"))
        // {
            ProjectLanguage = PlayerPrefs.GetString("Language") == "Eng" ? Language.Eng : Language.Rus;
        // }
        // else
        // {
        //     PlayerPrefs.SetString("Language", "Eng");
        //     ProjectLanguage = Language.Eng;
        // }

        // if (PlayerPrefs.HasKey("GraphActive"))
        // {
            GraphActive = PlayerPrefs.GetInt("GraphActive") == 1;
        // }
        // else
        // {
        //     PlayerPrefs.SetInt("GraphActive", 1);
        //     GraphActive = true;
        // }
    }

    public static void ChangeBackgroundGraphActive()
    {
        GraphActive = !GraphActive;
        
        PlayerPrefs.SetInt("GraphActive", GraphActive ? 1 : 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void ChangeLanguage()
    {
        ProjectLanguage = ProjectLanguage == Language.Eng ? Language.Rus : Language.Eng;
        
        PlayerPrefs.SetString("Language", ProjectLanguage == Language.Eng ? "Eng" : "Rus");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum Language
{
    Eng,
    Rus
}