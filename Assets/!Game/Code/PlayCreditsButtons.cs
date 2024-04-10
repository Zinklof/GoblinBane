using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayCreditsButtons : MonoBehaviour
{
    public int scene;
    public bool exit;
    public Vector3 hoverScale;
    public SettingsMainMenu settings;
    private Vector3 nonHoverScale = Vector3.one;

    private void Start()
    {
        nonHoverScale = transform.localScale;
    }

    private void OnMouseDown()
    {
        if (settings.open)
        {
            return;
        }
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
        if (settings.open)
        {
            return;
        }
        gameObject.transform.localScale = hoverScale;
    }

    private void OnMouseExit()
    {
        gameObject.transform.localScale = nonHoverScale;
    }
}
