// <copyright file="DisposeDriverService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Configuration;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Services;

public static class DisposeDriverService
{
    public static void DisposeAndroid(ServicesCollection childContainer)
    {
        var androidDriver = childContainer.Resolve<AndroidDriver>();
        androidDriver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
        androidDriver?.Quit();
        androidDriver?.Dispose();
        childContainer.UnregisterSingleInstance<AndroidDriver>();
    }

    public static void DisposeAllAndroid()
    {
        foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
        {
            try
            {
                var driver = childContainer.Resolve<AndroidDriver>();
                driver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
                driver?.Quit();
                driver?.Dispose();
                childContainer?.UnregisterSingleInstance<AndroidDriver>();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        var webDriver = ServicesCollection.Main.Resolve<AndroidDriver>();
        webDriver?.Quit();
        webDriver?.Dispose();
        ServicesCollection.Main.UnregisterSingleInstance<AndroidDriver>();
    }

    public static void CloseAndroidApp(ServicesCollection childContainer)
    {
        var androidDriver = childContainer.Resolve<AndroidDriver>();
        //androidDriver?.ResetApp();
        androidDriver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
    }

    public static void DisposeAllIOS()
    {
        foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
        {
            try
            {
                var driver = childContainer.Resolve<IOSDriver>();
                driver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
                driver?.Quit();
                driver?.Dispose();
                childContainer?.UnregisterSingleInstance<IOSDriver>();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        var webDriver = ServicesCollection.Main.Resolve<IOSDriver>();
        webDriver?.Quit();
        webDriver?.Dispose();
        ServicesCollection.Main.UnregisterSingleInstance<IOSDriver>();
    }

    public static void DisposeIOS(ServicesCollection childContainer)
    {
        var iosDriver = childContainer.Resolve<IOSDriver>();
        iosDriver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
        iosDriver?.Quit();
        childContainer.UnregisterSingleInstance<IOSDriver>();
    }

    public static void CloseIOSApp(ServicesCollection childContainer)
    {
        var iosDriver = childContainer.Resolve<IOSDriver>();
        iosDriver?.TerminateApp(childContainer.Resolve<AppConfiguration>("_currentAppConfiguration").AppId);
    }
}