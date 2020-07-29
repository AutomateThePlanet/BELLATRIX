// <copyright file="DisposeDriverService.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Services
{
    public static class DisposeDriverService
    {
        public static void DisposeAndroid(IServicesCollection childContainer)
        {
            var androidDriver = childContainer.Resolve<AndroidDriver<AndroidElement>>();
            androidDriver?.CloseApp();
            androidDriver?.Quit();
            androidDriver?.Dispose();
            childContainer.UnregisterSingleInstance<AndroidDriver<AndroidElement>>();
        }

        public static void DisposeAllAndroid()
        {
            foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
            {
                try
                {
                    var driver = childContainer.Resolve<AndroidDriver<AndroidElement>>();
                    driver?.CloseApp();
                    driver?.Quit();
                    driver?.Dispose();
                    childContainer?.UnregisterSingleInstance<AndroidDriver<AndroidElement>>();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            var webDriver = ServicesCollection.Main.Resolve<AndroidDriver<AndroidElement>>();
            webDriver?.Quit();
            webDriver?.Dispose();
            ServicesCollection.Main.UnregisterSingleInstance<AndroidDriver<AndroidElement>>();
        }

        public static void CloseAndroidApp(IServicesCollection childContainer)
        {
            var androidDriver = childContainer.Resolve<AndroidDriver<AndroidElement>>();
            androidDriver?.ResetApp();
            androidDriver?.CloseApp();
        }

        public static void DisposeAllIOS()
        {
            foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
            {
                try
                {
                    var driver = childContainer.Resolve<IOSDriver<IOSElement>>();
                    driver?.CloseApp();
                    driver?.Quit();
                    driver?.Dispose();
                    childContainer?.UnregisterSingleInstance<IOSDriver<IOSElement>>();
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            }

            var webDriver = ServicesCollection.Main.Resolve<IOSDriver<IOSElement>>();
            webDriver?.Quit();
            webDriver?.Dispose();
            ServicesCollection.Main.UnregisterSingleInstance<IOSDriver<IOSElement>>();
        }

        public static void DisposeIOS(IServicesCollection childContainer)
        {
            var iosDriver = childContainer.Resolve<IOSDriver<IOSElement>>();
            iosDriver?.CloseApp();
            iosDriver?.Quit();
            childContainer.UnregisterSingleInstance<IOSDriver<IOSElement>>();
        }

        public static void CloseIOSApp(IServicesCollection childContainer)
        {
            var iosDriver = childContainer.Resolve<IOSDriver<IOSElement>>();
            iosDriver?.CloseApp();
        }
    }
}