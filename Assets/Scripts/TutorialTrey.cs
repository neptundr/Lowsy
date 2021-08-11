using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialTrey : MonoBehaviour
{
    public static TutorialTrey This;
    
    public GameObject trey;
    public Text objectName;
    public Text objectDescription;
    
    private bool _opened;
    private GameObject _previousShower;
    
    public void SetInfo(string name, string description, GameObject shower)
    {
        if (_previousShower != null) _previousShower.SetActive(false);
        _previousShower = shower;
        shower.SetActive(true);

        GameManager.ThisManager.CompleteStop();
        
        _opened = false;
        trey.SetActive(_opened);
        objectName.gameObject.SetActive(!_opened);
        objectDescription.gameObject.SetActive(!_opened);
        
        objectName.text = name;
        objectDescription.text = description;
    }
    
    private void Start()
    {
        This = this;

        _opened = true;
        trey.SetActive(_opened);
        objectName.gameObject.SetActive(!_opened);
        objectDescription.gameObject.SetActive(!_opened);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(2))
        {
            Trey();
        }
    }

    public void Trey()
    {
        if (!_opened) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
