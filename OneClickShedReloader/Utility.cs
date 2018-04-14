using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitwiseJonMods
{
    static class Utility
    {
        private static IMonitor _monitor;

        public static void InitLogging(IMonitor monitor)
        {
            _monitor = monitor;
        }

        [ConditionalAttribute("DEBUG")]
        public static void Log(string msg, LogLevel level = LogLevel.Debug)
        {
            _monitor.Log(msg, level);
        }
    }
}
