using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Unity.VisualScripting;

namespace ZinklofDev.Console
{
    public class ConsoleLog : MonoBehaviour
    {
        [SerializeField] private TMP_Text Console;
        List<string> lines = new List<string>();

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
            string transformedLog = string.Empty;

            if (type == LogType.Error)
            {
                transformedLog = "<color=#FF0000>" + logString + "</color>";
            }
            if (type == LogType.Warning)
            {
                transformedLog = "<color=#FFEB04>" + logString + "</color>";
            }
            else
            {
                transformedLog = "<color=#FFFFFF>" + logString + "</color>";
            }
        }

        void TransformLog()
        {
            
        }

        private void Update()
        {
            while (lines.Count > 100)
                lines.Remove(lines[0]);

            Console.text = null;
            foreach (string line in lines)
            {
                string temp = line;
                Console.text += "<br>" + line;
            }
        }
    }

}
