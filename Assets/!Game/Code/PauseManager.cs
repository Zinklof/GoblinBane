using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject consoleMenu;

    public void resume()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
            }
        }

        if (consoleMenu.activeInHierarchy)
        {
            Time.timeScale = 0f;
        }
        else if (Input.GetKey(KeyCode.Backslash) && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 4.5f;
        }
        else if (Input.GetKey(KeyCode.RightBracket) && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 2.5f;
        }
        else if (Input.GetKey(KeyCode.P) && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0.05f;
        }
        else if (Input.GetKey(KeyCode.LeftBracket) && !pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 0.25f;
        }
        else if (!pauseMenu.activeInHierarchy)
        {
            Time.timeScale = 1f;
        }
    }
}
