using System.Diagnostics;

namespace Bellatrix.Mobile.IOS.GettingStarted;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}