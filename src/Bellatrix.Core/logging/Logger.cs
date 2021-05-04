using System;
using System.Collections.Generic;
using System.Text;

namespace Bellatrix
{
    public static class Logger
    {
        private static readonly object _lockObject = new object();

        public static void LogInformation(string message, params object[] args)
        {
            lock (_lockObject)
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
        }

        public static void LogError(string message, params object[] args)
        {
            lock (_lockObject)
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
}
