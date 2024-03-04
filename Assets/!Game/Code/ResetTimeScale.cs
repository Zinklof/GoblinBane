using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimeScale : MonoBehaviour
{
    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
}
