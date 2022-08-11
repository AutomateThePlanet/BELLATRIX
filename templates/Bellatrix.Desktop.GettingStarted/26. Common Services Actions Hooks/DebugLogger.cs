using System.Diagnostics;

namespace Bellatrix.Web.Desktop.CommonServicesActionHooks;

public static class DebugLogger
{
    public static void LogInfo(string message)
    {
        Debug.WriteLine(message);
    }
}