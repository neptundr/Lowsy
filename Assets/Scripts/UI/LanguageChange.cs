using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageChange : MonoBehaviour
{
    public GameObject eng;
    public GameObject rus;
    
    private void Start()
    {
        if (Settings.ProjectLanguage == Language.Eng)
        {
            rus.SetActive(true);
            eng.SetActive(false);
        }
        else
        {
            rus.SetActive(false);
            eng.SetActive(true);
        }
    }
    
    public void ChangeLanguage()
    {
        Settings.ChangeLanguage();
    }
}
