using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ZinklofDev.Console;

namespace ZinklofDev.Console
{
    public class ConsoleLog : MonoBehaviour
    {
        [SerializeField] private TMP_Text Console;
        List<string> lines = new List<string>();

        void Start()
        {
            Application.logMessageReceived += HandleUnityLog;
        }

        void HandleUnityLog(string logString, string stackTrace, LogType type)
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

            AddLog(transformedLog);
        }

        void HandleGSLog(ConsoleTypes.GSLog log)
        {
            string transformedLog = string.Empty;

            if (log.type == ConsoleTypes.GSLogType.Error)
            {
                transformedLog = "<color=#FF0000>" + log.log + "</color>";
            }
            if (log.type == ConsoleTypes.GSLogType.Warning)
            {
                transformedLog = "<color=#FFEB04>" + log.log + "</color>";
            }
            if (log.type == ConsoleTypes.GSLogType.Command)
            {
                transformedLog = "<color=#0FFFFF>" + log.log + "</color>";
            }
            if (log.type == ConsoleTypes.GSLogType.Response)
            {
                transformedLog = "<color=#0f0f0f>" + log.log + "</color>";
            }
            else
            {
                transformedLog = "<color=#FFFFFF>" + log.log + "</color>";
            }

            AddLog(transformedLog);
        }

        public void ClearLog()
        {
            lines.clear();
        }

        void AddLog(string logString)
        {
            lines.Add(logString);

            if (lines.Count > 100)
            {
                lines.RemoveAt(0);
            }

            Console.text = null;
            foreach (string line in lines)
            {
                string temp = line;
                Console.text += "<br>" + line;
            }
        }
    }
}
