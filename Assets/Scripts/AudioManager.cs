using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager This;

    public AudioSource backButton;
    public AudioSource onMouseClick;
    public AudioSource objectPlace;
    public AudioSource wrongPlacement;
    public AudioSource sceneLoading;
    public AudioSource tick;
    public AudioSource lose;
    public AudioSource win;

    private bool _isFirst;
    private void Start()
    {
        if (GameObject.FindObjectsOfType<AudioManager>().Length > 1 || _isFirst) Destroy(gameObject);
        else _isFirst = true;

        This = this;
        
        DontDestroyOnLoad(gameObject);
    }
}