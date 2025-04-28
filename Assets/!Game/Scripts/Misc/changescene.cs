using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changescene : MonoBehaviour
{
    public void changeTheScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void ExitTheGame()
    {
        Application.Quit();
    }
}
