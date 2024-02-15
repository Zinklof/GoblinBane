using CommandTerminal;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZinlofDev.Console
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        private CommandInfo commandInfo;

        public CommandAttribute(CommandInfo commandInfo)
        {
            this.commandInfo = commandInfo;
        }
    }

    public static class ConsoleTypes
    {
        public struct commandInfo
        {
            int minArguments;
            int maxArguments;
            string name;
            string hint;
        }

        public enum GSLogType
        {
            Error,
            Warning,
            Command,
            Response,
            Misc,
        }

        public struct GSLog
        {
            public GSLogType type;
            public string log;
            public string locale;
        }
    }
}
