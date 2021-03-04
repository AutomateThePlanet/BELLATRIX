using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix
{
    public static class Logger
    {
        public static void LogInformation(string message, params object[] args)
        {
            try
            {
                Console.WriteLine(message, args);
            }
            catch
            {
                // ignore
            }
        }

        public static void LogWarning(string message, params object[] args)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message, args);
                Console.ResetColor();
            }
            catch
            {
                // ignore
            }
        }

        public static void LogError(string message, params object[] args)
        {
            try
            {
                Console.Error.WriteLine(message, args);
            }
            catch
            {
                // ignore
            }
        }
    }
}
