using System.Diagnostics;

namespace Bellatrix.Api;

public static class DebugLogger
{
    public static void LogInformation(string message)
    {
        Debug.WriteLine(message);
    }
}