using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

public class ConsoleSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text Console;
    List<string> lines = new List<string>();

    private void Start()
    {
    }

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        lines.Add(logString);
    }

    private void Update()
    {
        while (lines.Count > 25)
        lines.Remove(lines[0]);

        Console.text = null;
        foreach (string line in lines)
        {
            string temp = line;
            Console.text += "<br>" + line;
        }
    }
}
