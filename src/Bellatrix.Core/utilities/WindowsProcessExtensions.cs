// <copyright file="WindowsProcessExtensions.cs" company="Automate The Planet Ltd.">
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
using System.Management;

namespace Bellatrix.Utilities;

public static class WindowsProcessExtensions
{
    public static IEnumerable<Process> GetChildProcesses(this Process process)
    {
        var children = new List<Process>();
        var mos = new ManagementObjectSearcher(string.Format("Select * From Win32_Process Where ParentProcessID={0}", process.Id));

        foreach (ManagementObject mo in mos.Get())
        {
            children.Add(Process.GetProcessById(Convert.ToInt32(mo["ProcessID"])));
        }

        return children;
    }

    public static Process GetParentProcess(this Process process)
    {
        int parentPid = 0;
        using (ManagementObject mo = new ManagementObject($"win32_process.handle='{process.Id}'"))
        {
            mo.Get();
            parentPid = Convert.ToInt32(mo["ParentProcessId"]);
        }

        return Process.GetProcessById(parentPid);
    }
}