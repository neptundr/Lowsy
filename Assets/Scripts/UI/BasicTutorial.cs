using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicTutorial : MonoBehaviour
{
    public string[] tipsEng;
    public string[] tipsRus;
    public Text tipText;

    private int _nowTip;

    public void NextTip()
    {
        tipText.text = Settings.ProjectLanguage == Language.Eng ? tipsEng[_nowTip] : tipsRus[_nowTip];
        _nowTip += 1;
    }
}
