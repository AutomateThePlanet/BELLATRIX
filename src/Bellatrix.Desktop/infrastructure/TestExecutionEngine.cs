// <copyright file="TestExecutionEngine.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Windows;

namespace Bellatrix.Desktop;

public class TestExecutionEngine
{
    public void StartApp(AppInitializationInfo appConfiguration, ServicesCollection childContainer)
    {
        try
        {
            var wrappedWebDriver = WrappedWebDriverCreateService.Create(appConfiguration, childContainer);
            childContainer.RegisterInstance<WindowsDriver<WindowsElement>>(wrappedWebDriver);
            ////childContainer.RegisterInstance(new AppService(wrappedWebDriver));
            ////childContainer.RegisterInstance(new ComponentCreateService());
            childContainer.RegisterNull<int?>();
            childContainer.RegisterNull<IWebElement>();
            childContainer.RegisterNull<WindowsElement>();
            IsAppStartedCorrectly = true;
        }
        catch (Exception e)
        {
            e.PrintStackTrace();
            IsAppStartedCorrectly = false;
            throw;
        }
    }

    public bool IsAppStartedCorrectly { get; set; }

    public void Dispose(ServicesCollection childContainer) => DisposeDriverService.Dispose(childContainer);

    public void DisposeAll()
    {
        foreach (var childContainer in ServicesCollection.Current.GetChildServicesCollections())
        {
            var driver = childContainer.Resolve<WindowsDriver<WindowsElement>>();
            DisposeDriverService.Dispose(driver, childContainer);
        }
    }
}
