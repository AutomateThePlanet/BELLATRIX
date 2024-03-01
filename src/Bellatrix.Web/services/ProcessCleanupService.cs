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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Bellatrix.Web;

namespace Bellatrix.Utilities;

public static class ProcessCleanupService
{
    private static readonly WebSettings ProcessCleanupSettings = ConfigurationService.GetSection<WebSettings>();
    private static readonly bool IsParallelExecutionEnabled = ProcessCleanupSettings?.IsParallelExecutionEnabled ?? false;

    public static void KillPreviousDriversAndBrowsersOsAgnostic(DateTime? executionStartDate)
    {
        // TODO: Anton(16.12.2019): This should be moved to configuration and the service should be made more generic for other processes as well.
        var browsersToCheck = new List<string>
        {
            "opera",
            "chrome",
            "firefox",
            "edge",
            "iexplore",
            "safari",
        };
        var driversToCheck = new List<string>
        {
            "operadriver",
            "chromedriver",
            "iedriverserver",
            "geckodriver",
            "msedgedriver",
        };

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            KillAllProcesses(driversToCheck);
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            var driversAndBrowsersТoCheck = driversToCheck.Union(browsersToCheck).ToList();

            KillProcessStartedAfterTime(driversAndBrowsersТoCheck, executionStartDate);
        }
    }

    public static void KillAllDriversAndChildProcessesWindows()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return;
        }

        if (IsParallelExecutionEnabled)
        {
            return;
        }

        var driversToCheck = new List<string>
        {
            "operadriver",
            "chromedriver",
            "iedriverserver",
            "geckodriver",
            "msedgedriver",
        };

        KillAllProcesses(driversToCheck);
    }

    private static void KillAllProcesses(List<string> processesToKill)
    {
        var processes = Process.GetProcesses().Where(p => processesToKill.Any(x => p.ProcessName.ToLower().Contains(x)));

        foreach (var process in processes)
        {
            try
            {
                var children = process.GetChildProcesses();
                foreach (var child in children)
                {
                    child.Kill();
                }

                process.Kill();
            }
            catch (Exception e)
            {
                e.PrintStackTrace();
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
            catch (Exception e)
            {
                e.PrintStackTrace();
            }
        }
    }
}