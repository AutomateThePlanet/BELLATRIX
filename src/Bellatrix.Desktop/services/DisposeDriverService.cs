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
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop.Services;

public static class DisposeDriverService
{
    public static void Dispose(WindowsDriver<WindowsElement> driver, ServicesCollection childContainer)
    {
        driver?.Quit();
        driver?.Dispose();
        childContainer.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
    }

    public static void Dispose(ServicesCollection childContainer)
    {
        try
        {
            if (childContainer.IsRegistered<WindowsDriver<WindowsElement>>())
            {
                var webDriver = childContainer.Resolve<WindowsDriver<WindowsElement>>();
                webDriver?.Quit();
                webDriver?.Dispose();
                childContainer.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
                ServicesCollection.Main.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
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
            try
            {
                var driver = childContainer.Resolve<WindowsDriver<WindowsElement>>();
                driver?.Quit();
                driver?.Dispose();
                childContainer?.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        ServicesCollection.Main.UnregisterSingleInstance<WindowsDriver<WindowsElement>>();
    }
}