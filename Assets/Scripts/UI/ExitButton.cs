using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void Exit()
    {
        StartCoroutine(Quit());
    }

    private IEnumerator Quit()
    {
        FindObjectOfType<Loader>().anim.SetTrigger("Start");
        
        yield return new WaitForSeconds(0.75f);
        
        Application.Quit();
    }
}
