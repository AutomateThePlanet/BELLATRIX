// <copyright file="AppService.cs" company="Automate The Planet Ltd.">
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
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services;

public class AppService<TDriver, TComponent> : MobileService<TDriver, TComponent>
    where TDriver : AppiumDriver
    where TComponent : AppiumElement
{
    public AppService(TDriver wrappedDriver)
        : base(wrappedDriver)
    {
    }

    public string Context { get => WrappedAppiumDriver.Context; set => WrappedAppiumDriver.Context = value; }

    public void BackgroundApp(int seconds) => WrappedAppiumDriver.BackgroundApp(TimeSpan.FromSeconds(seconds));

    public void CloseApp() => WrappedAppiumDriver.TerminateApp(AppConfiguration.AppId);

    public void LaunchApp() => WrappedAppiumDriver.ActivateApp(AppConfiguration.AppId);

    // TODO: ResetApp() method
    public void ResetApp() => throw new NotImplementedException("Reset? No.");

    public void InstallApp(string appPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            appPath = appPath.Replace('\\', '/');
        }

        WrappedAppiumDriver.InstallApp(appPath);
    }

    public void RemoveApp(string appId) => WrappedAppiumDriver.RemoveApp(appId);

    public bool IsAppInstalled(string bundleId)
    {
        try
        {
            return WrappedAppiumDriver.IsAppInstalled(bundleId);
        }
        catch (FormatException)
        {
            return false;
        }
    }
}