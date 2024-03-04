using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCreditsButtons : MonoBehaviour
{
    public int scene;
    public bool exit;
    public Vector3 hoverScale;
    private Vector3 nonHoverScale = Vector3.one;

    private void Start()
    {
        nonHoverScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (exit)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadScene(scene);
        }
    }

    private void OnMouseEnter()
    {
        gameObject.transform.localScale = hoverScale;
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = nonHoverScale;
    }
}
