// <copyright file="AppService.cs" company="Automate The Planet Ltd.">
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
using System.Runtime.InteropServices;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile.Services
{
    public class AppService<TDriver, TElement> : MobileService<TDriver, TElement>
        where TDriver : AppiumDriver<TElement>
        where TElement : AppiumWebElement
    {
        public AppService(TDriver wrappedDriver)
            : base(wrappedDriver)
        {
        }

        public string Context { get => WrappedAppiumDriver.Context; set => WrappedAppiumDriver.Context = value; }

        public void BackgroundApp(int seconds) => WrappedAppiumDriver.BackgroundApp(seconds);

        public void CloseApp() => WrappedAppiumDriver.CloseApp();

        public void LaunchApp() => WrappedAppiumDriver.LaunchApp();

        public void ResetApp() => WrappedAppiumDriver.ResetApp();

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
}