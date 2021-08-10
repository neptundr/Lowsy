using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartLanguage : MonoBehaviour
{
    public string eng;
    public string rus;
    
    void Start()
    {
        GetComponent<Text>().text = Settings.ProjectLanguage == Language.Eng ? eng : rus;
    }
}
