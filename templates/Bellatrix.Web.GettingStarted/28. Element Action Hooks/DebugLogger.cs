using System.Diagnostics;

namespace Bellatrix.Web.GettingStarted.Advanced._27._Element_Actions_Hooks;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}