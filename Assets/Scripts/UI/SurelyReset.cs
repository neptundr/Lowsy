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
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }

    public void No()
    {
        Loader.LoadScene(0);
    }
}
