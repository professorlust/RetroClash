using System;
using System.Collections.Generic;

namespace RetroRoyale.Protocol
{
    public class LogicCommandManager
    {
        public static Dictionary<int, Type> Commands;

        static LogicCommandManager()
        {
            Commands = new Dictionary<int, Type>();
        }
    }
}