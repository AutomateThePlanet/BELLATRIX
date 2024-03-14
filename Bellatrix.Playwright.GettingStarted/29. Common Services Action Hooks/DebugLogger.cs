using System.Diagnostics;

namespace Bellatrix.Playwright.GettingStarted.Advanced._28._Common_Services_Actions_Hooks;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}