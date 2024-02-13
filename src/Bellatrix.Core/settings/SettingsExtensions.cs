// <copyright file="SettingsExtensions.cs" company="Automate The Planet Ltd.">
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
using System.IO;
using System.Reflection;
using System.Text;
using Bellatrix.Utilities;

namespace Bellatrix.Settings;

public static class SettingsExtensions
{
    public static string NormalizeAppPath(this string appPath)
    {
        if (string.IsNullOrEmpty(appPath))
        {
            return appPath;
        }
        else if (appPath.StartsWith("AssemblyFolder", StringComparison.Ordinal))
        {
            var executionFolder = ExecutionDirectoryResolver.GetDriverExecutablePath();
            appPath = appPath.Replace("AssemblyFolder", executionFolder);
        }
        else if (appPath.StartsWith("RootFolder", StringComparison.Ordinal))
        {
            var executionFolder = ExecutionDirectoryResolver.GetRootPath();
            appPath = appPath.Replace("RootFolder", executionFolder);
        }

        return appPath;
    }
}
