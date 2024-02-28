// <copyright file="AppConfiguration.cs" company="Automate The Planet Ltd.">
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
using System.Runtime.InteropServices;
using Bellatrix.Mobile.Enums;
using Bellatrix.Utilities;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Configuration;

public class AppConfiguration : IEquatable<AppConfiguration>
{
    private string _appPath;

    public AppConfiguration()
    {
    }

    public AppConfiguration(OS os) => OSPlatform = os;

    public AppConfiguration(string appPath, string appId, Lifecycle lifecycle, string classFullName, AppiumOptions appiumOptions = null)
    {
        AppPath = appPath;
        AppId = appId;
        Lifecycle = lifecycle;
        ClassFullName = classFullName;
        AppiumOptions = appiumOptions;
    }

    public Lifecycle Lifecycle { get; set; } = Lifecycle.RestartEveryTime;

    public OS OSPlatform { get; set; }

    public string ClassFullName { get; set; }

    public string DeviceName { get; set; }

    public string PlatformName { get; set; }

    public string PlatformVersion { get; set; }

    public string AppActivity { get; set; }

    public string BrowserName { get; set; }

    public MobileOSType MobileOSType { get; set; }

    public ExecutionType ExecutionType { get; set; }

    public string AppPath { get => NormalizeAppPath(); set => _appPath = value; }

    public string AppId { get; set; }

    public AppiumOptions AppiumOptions { get; set; }

    public bool Equals(AppConfiguration other)
    {
        return AppPath == other.AppPath
                   && Lifecycle == other.Lifecycle
                   && DeviceName == other.DeviceName
                   && AppId == other.AppId
                   && PlatformName == other.PlatformName
                   && PlatformVersion == other.PlatformVersion
                   && AppActivity == other.AppActivity
                   && MobileOSType == other.MobileOSType;
    }

    public override bool Equals(object obj) => Equals(obj as AppConfiguration);

    protected AppiumOptions AddAdditionalAppiumOption(string classFullName, AppiumOptions appiumOptions)
    {
        var additionalCaps = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{classFullName}");
        if (additionalCaps != null)
        {
            foreach (var key in additionalCaps.Keys)
            {
                appiumOptions.AddAdditionalAppiumOption(key, additionalCaps[key]);
            }
        }

        return appiumOptions;
    }

    private string NormalizeAppPath()
    {
        if (string.IsNullOrEmpty(_appPath))
        {
            return _appPath;
        }
        else if (_appPath.StartsWith("AssemblyFolder", StringComparison.Ordinal))
        {
            var executionFolder = ExecutionDirectoryResolver.GetDriverExecutablePath();
            _appPath = _appPath.Replace("AssemblyFolder", executionFolder);

            if (RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
            {
                _appPath = _appPath.Replace('\\', '/');
            }
        }

        return _appPath;
    }

    public override int GetHashCode()
    {
        return AppPath.GetHashCode() +
               Lifecycle.GetHashCode() +
               DeviceName.GetHashCode() +
               AppId.GetHashCode() +
               PlatformName.GetHashCode() +
               PlatformVersion.GetHashCode() +
               AppActivity.GetHashCode() +
               MobileOSType.GetHashCode();
    }
}