// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using System.IO;
using Bellatrix.Desktop.Configuration;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop.Services;

public class WrappedWebDriverCreateService
{
    private static readonly string _serviceUrl;

    static WrappedWebDriverCreateService()
    {
        _serviceUrl = ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Url;
    }

    public static WindowsDriver<WindowsElement> Create(AppInitializationInfo appConfiguration, ServicesCollection childContainer)
    {
        var driverOptions = childContainer.Resolve<DesiredCapabilities>(appConfiguration.ClassFullName) ?? childContainer.Resolve<DesiredCapabilities>() ?? appConfiguration.AppiumOptions;
        driverOptions.SetCapability("app", appConfiguration.AppPath);
        driverOptions.SetCapability("deviceName", "WindowsPC");
        driverOptions.SetCapability("platformName", "Windows");
        string workingDir = Path.GetDirectoryName(appConfiguration.AppPath);
        driverOptions.SetCapability("appWorkingDir", workingDir);
        driverOptions.SetCapability("createSessionTimeout", ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.CreateSessionTimeout);
        driverOptions.SetCapability("ms:waitForAppLaunch", ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.WaitForAppLaunchTimeout);

        var additionalCapabilities = ServicesCollection.Main.Resolve<Dictionary<string, object>>($"caps-{appConfiguration.ClassFullName}") ?? new Dictionary<string, object>();
        foreach (var additionalCapability in additionalCapabilities)
        {
            driverOptions.SetCapability(additionalCapability.Key, additionalCapability.Value);
        }

        var wrappedWebDriver = new WindowsDriver<WindowsElement>(new Uri(_serviceUrl), driverOptions);

        wrappedWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ImplicitWaitTimeout);

        ChangeWindowSize(appConfiguration.Size, wrappedWebDriver);
        wrappedWebDriver.SwitchTo().Window(wrappedWebDriver.CurrentWindowHandle);
        try
        {
            var closeButton = wrappedWebDriver.FindElementByAccessibilityId("Close");
            wrappedWebDriver.Mouse.MouseMove(closeButton.Coordinates);
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
        }

        return wrappedWebDriver;
    }

    private static void ChangeWindowSize(Size windowSize, WindowsDriver<WindowsElement> wrappedWebDriver)
    {
        try
        {
            if (windowSize != default)
            {
                wrappedWebDriver.Manage().Window.Size = windowSize;
            }
            else
            {
                wrappedWebDriver.Manage().Window.Maximize();
            }
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
            throw;
        }
    }
}
