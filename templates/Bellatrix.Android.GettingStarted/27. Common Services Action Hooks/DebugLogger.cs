using System.Diagnostics;

namespace Bellatrix.Mobile.Android.GettingStarted.CommonServices;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}