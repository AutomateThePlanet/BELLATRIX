// <copyright file="ProcessCleanupService.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

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
