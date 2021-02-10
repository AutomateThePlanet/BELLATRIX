using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix
{
    public static class Logger
    {
        public static void LogInformation(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public static void LogError(string message, params object[] args)
        {
            Console.Error.WriteLine(message, args);
        }
    }
}
