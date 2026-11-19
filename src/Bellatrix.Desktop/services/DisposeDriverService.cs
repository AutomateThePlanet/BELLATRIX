// <copyright file="DisposeDriverService.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Services;

public static class DisposeDriverService
{
    public static void Dispose(WindowsDriver driver, ServicesCollection childContainer)
    {
        driver?.Quit();
        driver?.Dispose();
        childContainer.UnregisterSingleInstance<WindowsDriver>();
    }

    public static void Dispose(ServicesCollection childContainer)
    {
        WindowsDriver webDriver = null;
        try
        {
            if (childContainer.IsRegistered<WindowsDriver>())
            {
                webDriver = childContainer.Resolve<WindowsDriver>();
                try
                {
                    webDriver?.Quit();
                    webDriver?.Dispose();
                }
                catch (System.Exception ex)
                {
                    // The driver may already be quit by App.CloseOrKill or AppSessionService.
                    System.Diagnostics.Debug.WriteLine(ex);
                }

                childContainer.UnregisterSingleInstance<WindowsDriver>();
                ServicesCollection.Main.UnregisterSingleInstance<WindowsDriver>();
            }
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }

        DisposeAppSessions(childContainer, webDriver);
    }

    private static void DisposeAppSessions(ServicesCollection childContainer, WindowsDriver alreadyQuitDriver)
    {
        try
        {
            if (childContainer != null && childContainer.IsRegistered<AppSessionService>())
            {
                childContainer.Resolve<AppSessionService>()?.DisposeAll(alreadyQuitDriver);
            }
        }
        catch (System.Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(ex);
        }
    }

    public static void DisposeAll()
    {
        foreach (var childContainer in ServicesCollection.Main.GetChildServicesCollections())
        {
            WindowsDriver driver = null;
            try
            {
                driver = childContainer.Resolve<WindowsDriver>();
                driver?.Quit();
                driver?.Dispose();
                childContainer?.UnregisterSingleInstance<WindowsDriver>();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            DisposeAppSessions(childContainer, driver);
        }

        ServicesCollection.Main.UnregisterSingleInstance<WindowsDriver>();
    }
}