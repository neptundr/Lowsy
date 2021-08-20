using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackButton : MonoBehaviour
{
    public void BackToMenu()
    {
        AudioManager.BackButton();
        Loader.LoadScene(0);
    }
}
