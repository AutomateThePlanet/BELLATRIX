// <copyright file="ApplicationSettings.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using System.Linq;
using Bellatrix.Desktop.Configuration;

namespace Bellatrix.Desktop;

public class ApplicationSettings
{
    public string AppPath { get; set; }

    // If appNameOrPath is a key in desktopSettings.applicationsSettings (case-insensitive),
    // sets AppName to that key and AppPath to its appPath. Otherwise leaves the configuration unchanged.
    public static void ResolveAppName(AppInitializationInfo appConfiguration, string appNameOrPath)
    {
        var applicationsSettings = ConfigurationService.GetSection<DesktopSettings>()?.ApplicationsSettings;
        if (applicationsSettings == null || string.IsNullOrEmpty(appNameOrPath))
        {
            return;
        }

        var entry = applicationsSettings.FirstOrDefault(a => string.Equals(a.Key, appNameOrPath, StringComparison.OrdinalIgnoreCase));
        if (entry.Value != null)
        {
            appConfiguration.AppName = entry.Key;
            appConfiguration.AppPath = entry.Value.AppPath;
        }
    }
}
