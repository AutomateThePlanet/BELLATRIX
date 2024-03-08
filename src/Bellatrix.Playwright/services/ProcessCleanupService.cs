using Bellatrix.Playwright.Settings;
using Bellatrix.Utilities;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Bellatrix.Playwright.Services;
public static class ProcessCleanupService
{
    private static readonly WebSettings ProcessCleanupSettings = ConfigurationService.GetSection<WebSettings>();
    private static readonly bool IsParallelExecutionEnabled = ProcessCleanupSettings?.IsParallelExecutionEnabled ?? false;

    public static void KillPreviousBrowsersOsAgnostic(DateTime? executionStartDate)
    {
        var browsersToCheck = new List<string>
        {
            "chrome",
            "chromium",
            "msedge",
            "firefox",
            // playwright stands for webkit
            "playwright",
        };

        KillProcessStartedAfterTime(browsersToCheck, executionStartDate);
    }

    public static void KillAllBrowsersAdnChildProcessesWindows()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var osxProcessesToCheck = new List<string>
            {
                "Chromium",
                "Google Chrome",
                "Microsoft Edge",
                "firefox",
                "Playwright"
            };

            KillProcessStartedAfterTime(osxProcessesToCheck, DisposeBrowserService.TestRunStartTime);
        }

        if (IsParallelExecutionEnabled)
        {
            return;
        }

        var browsersToCheck = new List<string>
        {
            "chrome",
            "chromium",
            "msedge",
            "firefox",
            // playwright stands for webkit
            "playwright",
        };

        KillAllProcessesStartedAfterTime(browsersToCheck, DisposeBrowserService.TestRunStartTime);
    }

    private static void KillAllProcessesStartedAfterTime(List<string> processesToKill, DateTime? executionStartDate)
    {
        var processes = Process.GetProcesses().Where(p => processesToKill.Any(x => p.ProcessName.ToLower().Contains(x)));

        foreach (var process in processes)
        {
            try
            {
                var children = process.GetChildProcesses();
                foreach (var child in children)
                {
                    if (child.StartTime > executionStartDate) child.Kill();
                }

                if(process.StartTime > executionStartDate) process.Kill();
            }
            catch (Exception ex)
            {
                ex.PrintStackTrace();
            }
        }
    }

    private static void KillProcessStartedAfterTime(List<string> processesToKill, DateTime? executionStartDate)
    {
        var processes = Process.GetProcesses().Where(p => processesToKill.Any(x => p.ProcessName.ToLower().Contains(x)));
        foreach (var process in processes)
        {
            try
            {
                Debug.WriteLine(process.ProcessName);
                if (process.StartTime > executionStartDate)
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {
                ex.PrintStackTrace();
            }
        }
    }
}
