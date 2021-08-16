using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicTutorial : MonoBehaviour
{
    public GameObject[] showers;
    public int[] showersActive;
    public string[] tipsEng;
    public string[] tipsRus;
    public bool[] setTransitters;
    public bool[] play;
    public Transitter transitter;
    public Text tipText;
    public Text choosingVariantText;

    private int _nowTip;
    private bool _canBeSkipped;
    private bool _previousLosed;
    private bool _previousWined;

    private void Start()
    {
        for (int i = 0; i < showers.Length; i++)
        {
            showers[i].SetActive(false);
        }
    }

    public void NextTip()
    {
        if (!play[_nowTip] || _canBeSkipped)
        {
            Tip();
        }
    }

    private void Tip()
    {
        tipText.text = Settings.ProjectLanguage == Language.Eng ? tipsEng[_nowTip] : tipsRus[_nowTip];
        if (_nowTip < tipsEng.Length - 1) _nowTip += 1;
        TransitterSetActive();
        
        if (play[_nowTip] && GameManager.GetIsCompletelyStopped())
        {
            GameManager.ThisManager.StartPause();
            _canBeSkipped = false;
        }
        if (!play[_nowTip] && !GameManager.GetIsCompletelyStopped())
        {
            GameManager.ThisManager.CompleteStop();
            _canBeSkipped = false;
        }
        
        for (int i = 0; i < showers.Length; i++)
        {
            showers[i].SetActive(false);
        }
        if (showersActive[_nowTip] != -1) showers[showersActive[_nowTip]].SetActive(true);
    }

    private void Update()
    {
        if (GameManager.ThisManager.winned != _previousWined && GameManager.ThisManager.winned) _canBeSkipped = true;
        _previousWined = GameManager.ThisManager.winned;

        if (GameManager.ThisManager.losed != _previousLosed && GameManager.ThisManager.losed) _canBeSkipped = true;
        _previousLosed = GameManager.ThisManager.losed;
    }

    private void TransitterSetActive()
    {
        transitter.gameObject.SetActive(setTransitters[_nowTip]);
        choosingVariantText.text = setTransitters[_nowTip] ? "0" : "1";
    }
}
