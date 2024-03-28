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
        public static void RegisterCommand(Command<int> command)
        {
            commandList.Add(command);
        }
        public static void RegisterCommand(Command<byte> command)
        {
            commandList.Add(command);
        }
        public static void RegisterCommand(Command<bool> command)
        {
            commandList.Add(command);
        }
        public static void RegisterCommand(Command<float> command)
        {
            commandList.Add(command);
        }

        public static void HandleCommand(string consoleInput)
        { 
            string[] alteredInput = consoleInput.Split(' ');

            for(int i = 0; i < commandList.Count; i++)
            {
                CommandBasic commandBasic = commandList[i] as CommandBasic;
                if (commandBasic.commandCheat == true && CheatsOn == false)
                {
                    if (alteredInput[0] == commandBasic.Format)
                    {
                        Log.LogWarning("Cheats are currently disabled, use gs_cheats 1 to enable.");
                        return;
                    }
                }

                if (alteredInput[0] == commandBasic.Format)
                {
                    if (commandList[i] as Command != null)
                    {
                        (commandList[i] as Command).Invoke();
                        return;
                    }
                    else if (commandList[i] as Command<byte> != null)
                    {
                        (commandList[i] as Command<byte>).invoke(byte.Parse(alteredInput[1]));
                        return;
                    }
                    else if (commandList[i] as Command<bool> != null)
                    {
                        (commandList[i] as Command<bool>).invoke(bool.Parse(alteredInput[1]));
                        return;
                    }
                    else if (commandList[i] as Command<int> != null)
                    {
                        (commandList[i] as Command<int>).invoke(int.Parse(alteredInput[1]));
                        return;
                    }
                    else if (commandList[i] as Command<float> != null)
                    {
                        (commandList[i] as Command<float>).invoke(float.Parse(alteredInput[1]));
                        return;
                    }
                    else
                    {
                        Log.LogError(alteredInput[0] += " is not a poperly registered command");
                        return;
                    }
                }
            }
            Log.LogError(alteredInput[0] += " is not a valid command");
            return;
        }
    }
}
