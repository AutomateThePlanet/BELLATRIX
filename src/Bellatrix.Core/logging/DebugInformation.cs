using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bellatrix.Core.logging;

namespace Bellatrix
{
    public static class DebugInformation
    {
        private static readonly object _lockObject = new object();

        public static void PrintStackTrace<TException>(this TException ex)
            where TException : Exception
        {
            if (ConfigurationService.GetSection<TroubleshootingSettings>().DebugInformationEnabled)
            {
                lock (_lockObject)
                {
                    try
                    {
                        Console.Error.WriteLine();
                        Console.Error.WriteLine(new string('*', 10));
                        Console.Error.WriteLine(ex.ToString());
                        Console.Error.WriteLine(new string('*', 10));
                        Console.Error.WriteLine();

                        Debug.WriteLine(string.Empty);
                        Debug.WriteLine(new string('*', 10));
                        Debug.WriteLine(ex.ToString());
                        Debug.WriteLine(new string('*', 10));
                        Debug.WriteLine(string.Empty);
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
        }
    }
}
