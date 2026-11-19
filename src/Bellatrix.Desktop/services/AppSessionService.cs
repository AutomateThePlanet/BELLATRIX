// <copyright file="AppSessionService.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bellatrix.Desktop.Configuration;
using Bellatrix.LLM;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Services;

// One instance per test-class container. Holds the app sessions opened by the test class:
// the primary app (started by AppLifecyclePlugin) and the apps opened with App.SwitchApp.
public class AppSessionService
{
    private readonly Dictionary<string, (WindowsDriver Driver, AppInitializationInfo AppConfiguration)> _sessions = new(StringComparer.OrdinalIgnoreCase);

    public string PrimaryAppName { get; private set; }

    public string ActiveAppName { get; private set; }

    public bool Contains(string appName) => _sessions.ContainsKey(appName);

    public void Register(string appName, WindowsDriver driver, AppInitializationInfo appConfiguration, bool isPrimary)
    {
        if (_sessions.TryGetValue(appName, out var existing) && existing.Driver != driver)
        {
            QuitSafely(existing.Driver);
        }

        _sessions[appName] = (driver, appConfiguration);
        if (isPrimary)
        {
            PrimaryAppName = appName;
            ActiveAppName = appName;
        }
    }

    // Makes the session the one that all services resolve: re-registers its WindowsDriver in the test-class container.
    public void Activate(string appName, ServicesCollection container)
    {
        var driver = _sessions[appName].Driver;
        container.RegisterInstance(driver);
        ServicesCollection.Main.RegisterInstance<IViewSnapshotProvider>(container.Resolve<AppService>());
        ActiveAppName = appName;
    }

    // Waits for the app process to exit and kills it after the timeout. Needs the Appium power_shell insecure feature.
    // Returns true if the process had to be killed.
    public bool EnsureExited(string appName, TimeSpan timeout)
    {
        if (!_sessions.TryGetValue(appName, out var session))
        {
            return false;
        }

        var processName = Path.GetFileNameWithoutExtension(session.AppConfiguration.AppPath);
        var seconds = (int)Math.Ceiling(timeout.TotalSeconds);
        var command = $"Wait-Process -Name '{processName}' -Timeout {seconds} -ErrorAction SilentlyContinue; if (Get-Process -Name '{processName}' -ErrorAction SilentlyContinue) {{ Stop-Process -Name '{processName}' -Force; 'killed' }} else {{ 'exited' }}";
        var result = session.Driver.ExecuteScript("powerShell", new Dictionary<string, object> { { "command", command } });
        var killed = string.Equals(result?.ToString()?.Trim(), "killed", StringComparison.OrdinalIgnoreCase);
        if (killed)
        {
            Logger.LogWarning("App '{0}' did not exit within {1}s and was killed.", appName, seconds);
        }

        return killed;
    }

    public void Remove(string appName)
    {
        if (_sessions.TryGetValue(appName, out var session))
        {
            QuitSafely(session.Driver);
            _sessions.Remove(appName);
        }

        if (string.Equals(ActiveAppName, appName, StringComparison.OrdinalIgnoreCase))
        {
            ActiveAppName = null;
        }
    }

    public void DisposeSecondary()
    {
        foreach (var appName in _sessions.Keys.Where(k => !string.Equals(k, PrimaryAppName, StringComparison.OrdinalIgnoreCase)).ToList())
        {
            Remove(appName);
        }
    }

    // alreadyQuitDriver: a driver the caller has already quit, so it is not quit twice.
    public void DisposeAll(WindowsDriver alreadyQuitDriver = null)
    {
        foreach (var session in _sessions.Values.Where(s => s.Driver != alreadyQuitDriver).ToList())
        {
            QuitSafely(session.Driver);
        }

        _sessions.Clear();
        PrimaryAppName = null;
        ActiveAppName = null;
    }

    private static void QuitSafely(WindowsDriver driver)
    {
        try
        {
            driver?.Quit();
            driver?.Dispose();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }
}
