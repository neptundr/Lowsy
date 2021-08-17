using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager This;

    public Text levelName;
    public Text levelDifficulty;

    private float _writingFrequency = 0.025f;
    private string _levelNameToSet;
    private string _levelDifficultyToSet;
    private UnityEngine.Color _colorToSet = UnityEngine.Color.white;
    private LevelIcon _lastLevelIcon;
    private TextData _nameTextData = new TextData();
    private TextData _difficultyTextData = new TextData();
    
    public void SetLevelInfo(string name, string difficulty, UnityEngine.Color color, LevelIcon levelIcon)
    {
        if (_lastLevelIcon == null)
        {
            _levelNameToSet = levelName.text;
            _levelDifficultyToSet = levelDifficulty.text;
            _colorToSet = levelDifficulty.color;

            StartCoroutine(Setter());
        }

        if (levelIcon != _lastLevelIcon)
        {
            _levelNameToSet = name;
            _levelDifficultyToSet = difficulty;
            _colorToSet = color;
            _lastLevelIcon = levelIcon;
            _nameTextData.Clear();
            _difficultyTextData.Clear();
        }
    }

    private IEnumerator Setter()
    {
        while (true)
        {
            UpdateText(levelName, _levelNameToSet, _nameTextData);
            UpdateText(levelDifficulty, _levelDifficultyToSet, _difficultyTextData);

            if (levelDifficulty.text == "") levelDifficulty.color = _colorToSet;
            
            yield return new WaitForSeconds(_writingFrequency);
        }
    }

    private void UpdateText(Text levelInfoText, string comparable, TextData textData)
    {
        if (levelInfoText.text != comparable)
        {
            if (!textData.deleted)
            {
                if (levelInfoText.text != "")
                {
                    levelInfoText.text = levelInfoText.text.Remove(levelInfoText.text.Length - 1);
                }
                else
                {
                    textData.deleted = true;
                    textData.fillingIndex = 0;
                }
            }
            else
            {
                levelInfoText.text += comparable[textData.fillingIndex];
                textData.fillingIndex += 1;
            }
        }
    }
    
    private void Start()
    {
        This = this;
    }
}

class TextData
{
    public bool deleted;
    public int fillingIndex;

    public void Clear()
    {
        deleted = false;
        fillingIndex = 0;
    }
}