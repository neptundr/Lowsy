using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class LevelIcon : MonoBehaviour
{
    public string nameEng;
    public string difficultyEng;
    public string nameRus;
    public string difficultyRus;
    public UnityEngine.Color difficultyColor;
    public int levelIndex;
    public Sprite unsolved;
    public Sprite[] solved;

    private float _iconChangeFrequency = 1;
    private float _iconChangeRange = 0.5f;
    private Image _image;

    private void OnMouseOver()
    {
        UIManager.This.levelName.text = Settings.ProjectLanguage == Language.Eng ? nameEng : nameRus;
        UIManager.This.levelDifficulty.text = Settings.ProjectLanguage == Language.Eng ? difficultyEng : difficultyRus;
        UIManager.This.levelDifficulty.color = difficultyColor;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }
    
    private void Start()
    {
        _image = GetComponent<Image>();
        
        if (PlayerPrefs.GetInt("Level" + levelIndex) == 0)
        {
            _image.sprite = unsolved;
        }
        else
        {
            ChangeSolvedIcon();
        }
    }

    private void ChangeSolvedIcon()
    {
        _image.sprite = solved[Random.Range(0, solved.Length)]; 
        Invoke(nameof(ChangeSolvedIcon),
            Random.Range(_iconChangeFrequency - _iconChangeRange, _iconChangeFrequency + _iconChangeRange));
    }
}
