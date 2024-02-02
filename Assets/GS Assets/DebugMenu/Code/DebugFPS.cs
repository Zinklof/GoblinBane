using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugFPS : MonoBehaviour
{
    public TMP_Text Text;
    private float timesinceupdate;

    private void Update()
    {
        timesinceupdate += Time.deltaTime;

        if (timesinceupdate > .5f)
        {
            int temp = Mathf.FloorToInt(1 / Time.deltaTime);
            Text.text = temp.ToString();
            timesinceupdate = 0;
        }

    }
}