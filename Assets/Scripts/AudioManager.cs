using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager This;

    public AudioSource backButton;
    public AudioSource onMouseClick;
    public AudioSource objectPlace;
    public AudioSource wrongPlacement;
    public AudioSource sceneLoading;
    public AudioSource tick;
    public AudioSource lose;
    public AudioSource win;

    private bool _isFirst;

    public static void BackButton() { This.backButton.Play(); }
    public static void OnMouseClick() { This.onMouseClick.Play(); }
    public static void ObjectPlace() { This.objectPlace.Play(); }
    public static void WrongPlacement() { This.wrongPlacement.Play(); }
    public static void SceneLoading() { This.sceneLoading.Play(); }
    public static void Tick() { This.tick.Play(); }
    public static void Lose() { This.lose.Play(); }
    public static void Win() { This.win.Play(); }

    private void Start()
    {
        if (FindObjectsOfType<AudioManager>().Length == 1) _isFirst = true;
        if (FindObjectsOfType<AudioManager>().Length > 1 || !_isFirst) Destroy(gameObject);
        
        This = this;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(backButton);
        DontDestroyOnLoad(onMouseClick);
        DontDestroyOnLoad(objectPlace);
        DontDestroyOnLoad(wrongPlacement);
        DontDestroyOnLoad(sceneLoading);
        DontDestroyOnLoad(tick);
        DontDestroyOnLoad(lose);
        DontDestroyOnLoad(win);
    }
}