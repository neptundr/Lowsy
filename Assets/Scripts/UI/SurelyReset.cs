using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurelyReset : MonoBehaviour
{
    public GameObject choosePanel;

    public void Show()
    {
        choosePanel.SetActive(true);
    }

    public void Yes()
    {
        AudioManager.OnMouseClick();
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void No()
    {
        AudioManager.OnMouseClick();
        Loader.LoadScene(0);
    }
}
