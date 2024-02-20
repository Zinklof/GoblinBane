using System;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZinklofDev.Console
{
    public static class Shell
    {
        static List<object> commandList = new List<object>();
        public static bool CheatsOn = false;

        public static void RegisterCommand(Command command)
        {
            commandList.Add(command);
        }

        public static void HandleCommand(string consoleInput)
        { 
            string[] alteredInput = consoleInput.Split(' ');

            string enteredCommand = alteredInput[0];


            for(int i = 0; i < commandList.Count; i++)
            {
                CommandBasic commandBasic = commandList[i] as CommandBasic;

                if(enteredCommand == commandBasic.Format)
                {
                    if (commandList[i] as Command != null)
                    {
                        (commandList[i] as Command).Invoke();
                        return;
                    }
                    else
                    {
                        Log.LogError(enteredCommand += " is not a poperly registered command");
                    }
                }
            }
            Log.LogError(enteredCommand += " is not a valid command");
        }
    }
}
