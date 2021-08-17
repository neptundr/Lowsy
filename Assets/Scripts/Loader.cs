using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private static Loader This;
    
    public Animator anim;

    private float _transitionDuration = 0.65f;

    private void Start()
    {
        This = this;
    }

    public static void LoadScene(int buildIndex)
    {
        LoadScene(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(buildIndex)));
    }

    public static void LoadScene(string sceneName)
    {
        This.StartLoading(sceneName);
    }

    private void StartLoading(string sceneName)
    {
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        anim.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionDuration);

        SceneManager.LoadScene(sceneName);
    }
}