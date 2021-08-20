using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static Language ProjectLanguage = Language.Eng;
    public static bool GraphActive = true;
    public static bool Windowed;
    public static Settings This;

    public AudioMixerGroup mixer;
    
    private bool _isFirst;
    
    public void Start()
    {
        if (FindObjectsOfType(typeof(Settings)).Length == 1) _isFirst = true;
        else Destroy(gameObject);

        if (_isFirst)
        {
            DontDestroyOnLoad(gameObject);

            This = this;

            ProjectLanguage = PlayerPrefs.GetString("Language") == "Eng" ? Language.Rus : Language.Eng;
            GraphActive = PlayerPrefs.GetInt("GraphActive") != 1;
            Windowed = PlayerPrefs.GetInt("ScreenMode") != 0;
            
            Screen.fullScreenMode = Windowed ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
            UpdateWindowResolution();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void SetMusicVolume(float value)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0,value));
    }
    
    public void SetEffectsVolume(float value)
    {
        mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0,value));
    }

    public static void ChangeScreenMode()
    {
        Windowed = !Windowed;
        PlayerPrefs.SetInt("ScreenMode", Windowed ? 0 : 1);
        Screen.fullScreenMode = Windowed ? FullScreenMode.Windowed : FullScreenMode.FullScreenWindow;
        UpdateWindowResolution();
        
        Loader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static void UpdateWindowResolution()
    {
        Screen.SetResolution(Convert.ToInt32(Screen.currentResolution.width * 0.75f), Convert.ToInt32(Screen.currentResolution.height * 0.75f), !Windowed);
    }

    public static void ChangeBackgroundGraphActive()
    {
        GraphActive = !GraphActive;
        
        PlayerPrefs.SetInt("GraphActive", GraphActive ? 1 : 0);
        Loader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static void ChangeLanguage()
    {
        ProjectLanguage = ProjectLanguage == Language.Eng ? Language.Rus : Language.Eng;
        
        PlayerPrefs.SetString("Language", ProjectLanguage == Language.Eng ? "Eng" : "Rus");
        Loader.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

public enum Language
{
    Eng,
    Rus
}