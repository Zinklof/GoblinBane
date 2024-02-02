using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCreditsButtons : MonoBehaviour
{
    public bool isPlay = false;
    public bool isReg = false;
    public bool isOptimized = false;
    public bool isPlayOptimized = false;
    public bool isExit = false;
    public float scaleAmount = 1f;

    private void OnMouseDown()
    {
        if (isReg == true)
        {
            SceneManager.LoadScene(4);
        }

        if (isOptimized  == true)
        {
            SceneManager.LoadScene(1);
        }

        if (isPlay == true)
        {
            SceneManager.LoadScene(1);
        }

        if (isPlayOptimized == true)
        {
            SceneManager.LoadScene(3);
        }

        if (isExit) 
        { 
            Application.Quit();
        }
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
        Debug.Log("mouse begins to hover");
    }

    private void OnMouseExit()
    {
        if (isOptimized || isReg)
        {
            gameObject.transform.localScale = new Vector3(0.0214f, 0.0214f, 0.0214f);
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0.0163f, 0.0163f, 0.0163f);
        }

        Debug.Log("mouse exits hover");
    }
}
