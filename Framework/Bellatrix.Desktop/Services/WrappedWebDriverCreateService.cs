﻿// <copyright file="WrappedWebDriverCreateService.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using System.Drawing;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Trace;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop.Services
{
    public class WrappedWebDriverCreateService
    {
        private static readonly string _serviceUrl;

        static WrappedWebDriverCreateService() => _serviceUrl = ConfigurationService.GetSection<DesktopSettings>().ServiceUrl;

        public static WindowsDriver<WindowsElement> Create(AppConfiguration appConfiguration, ServicesCollection childContainer)
        {
            var driverOptions = childContainer.Resolve<DesiredCapabilities>(appConfiguration.ClassFullName) ?? childContainer.Resolve<DesiredCapabilities>() ?? appConfiguration.DesiredCapabilities;
            driverOptions.SetCapability("app", appConfiguration.AppPath);
            driverOptions.SetCapability("deviceName", "WindowsPC");

            var wrappedWebDriver = new WindowsDriver<WindowsElement>(new Uri(_serviceUrl), driverOptions);

            ChangeWindowSize(appConfiguration.Size, wrappedWebDriver);
            wrappedWebDriver.SwitchTo().Window(wrappedWebDriver.CurrentWindowHandle);
            try
            {
                var closeButton = wrappedWebDriver.FindElementByAccessibilityId("Close");
                wrappedWebDriver.Mouse.MouseMove(closeButton.Coordinates);
            }
            catch
            {
                // ignore
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
                throw;
            }
        }
    }
}
