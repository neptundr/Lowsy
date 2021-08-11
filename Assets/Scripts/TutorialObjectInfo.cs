using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialObjectInfo : MonoBehaviour
{
    public string objectNameEng;
    public string objectDescriptionEng;
    public string objectNameRus;
    public string objectDescriptionRus;
    public GameObject showerLevel;

    public void SetInfo()
    {
        TutorialTrey.This.SetInfo(Settings.ProjectLanguage == Language.Eng ? objectNameEng : objectNameRus,
            Settings.ProjectLanguage == Language.Eng ? objectDescriptionEng : objectDescriptionRus, showerLevel);
    }
}
