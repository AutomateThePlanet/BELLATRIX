using System.Diagnostics;

namespace Bellatrix.Desktop.GettingStarted;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}