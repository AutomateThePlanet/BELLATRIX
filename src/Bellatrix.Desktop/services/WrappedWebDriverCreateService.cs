// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Services;

public class WrappedWebDriverCreateService
{
    private static readonly string ServiceUrl;

    private const string CloseButtonXPath = "//Button[contains(translate(@AutomationId, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'close') or contains(translate(@Name, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz'), 'close')]";

    static WrappedWebDriverCreateService()
    {
        ServiceUrl = ConfigurationService.GetSection<DesktopSettings>().ExecutionSettings.Url;
    }

    public static WindowsDriver<WindowsElement> Create(AppInitializationInfo appConfiguration, ServicesCollection childContainer)
    {
        var driverOptions = childContainer.Resolve<AppiumOptions>(appConfiguration.ClassFullName) ?? childContainer.Resolve<AppiumOptions>() ?? appConfiguration.AppiumOptions;

        var appiumOptions = new Dictionary<string, object>
        {
            { "automationName", "Windows" },
            { "appWorkingDir", Path.GetDirectoryName(appConfiguration.AppPath) },
            { "createSessionTimeout", ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.CreateSessionTimeout },
            { "ms:waitForAppLaunch", ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.WaitForAppLaunchTimeout },
            { "ms:experimental-webdriver", true }
        };

        if (appConfiguration.AppPath == "Root")
        {
            if (appConfiguration.WindowHandle == null)
            {
                appiumOptions.Add("app", appConfiguration.AppPath);
            }
            else
            {
                appiumOptions.Add("appTopLevelWindow", appConfiguration.WindowHandle);
            }
        }
        else
        {
            appiumOptions.Add("app", appConfiguration.AppPath);
        }

        var appiumCapabilities = ServicesCollection.Main.Resolve<Dictionary<string, object>>($"caps-{appConfiguration.ClassFullName}");

        driverOptions.PlatformName = "Windows";
        driverOptions.AddAdditionalCapability("appium:options", appiumOptions);
        var additionalCapabilities = appiumCapabilities ?? new Dictionary<string, object>();
        foreach (var additionalCapability in additionalCapabilities)
        {
            driverOptions.AddAdditionalCapability(additionalCapability.Key, additionalCapability.Value);
        }

        var wrappedWebDriver = new WindowsDriver<WindowsElement>(new Uri(ServiceUrl), driverOptions);

        wrappedWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigurationService.GetSection<DesktopSettings>().TimeoutSettings.ImplicitWaitTimeout);

        if (!appiumOptions.TryGetValue("app", out var app) || app as string == "Root")
        {
            return wrappedWebDriver;
        }

        try
        {
            var closeButton = wrappedWebDriver.FindElementByXPath(CloseButtonXPath);

            wrappedWebDriver.ExecuteScript("windows: hover", new Dictionary<string, object>
            {
                { "startElementId", closeButton.Id },
                { "endElementId", closeButton.Id },
                { "durationMs", 0 }
            });
            
            wrappedWebDriver.SwitchTo().Window(wrappedWebDriver.CurrentWindowHandle);
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
