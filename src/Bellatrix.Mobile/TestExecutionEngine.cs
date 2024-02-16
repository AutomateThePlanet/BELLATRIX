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
using Bellatrix.Mobile.Configuration;
using Bellatrix.Mobile.Services;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public class TestExecutionEngine
{
    public void StartApp(AppConfiguration appConfiguration, ServicesCollection childContainer)
    {
        try
        {
            if (appConfiguration.MobileOSType.Equals(MobileOSType.Android))
            {
                var wrappedWebDriver = WrappedAppiumCreateService.CreateAndroidDriver(appConfiguration, childContainer);
                childContainer.RegisterInstance(wrappedWebDriver);
                try
                {
                    wrappedWebDriver.HideKeyboard();
                }
                catch
                {
                    // ignore
                }
            }
            else
            {
                WrappedAppiumCreateService.CreateIOSDriver(appConfiguration, childContainer);
            }

            childContainer.RegisterInstance(childContainer.Resolve<ComponentCreateService>());
            childContainer.RegisterNull<int?>();
            childContainer.RegisterNull<IWebElement>();
            childContainer.RegisterNull<AppiumElement>();
            childContainer.RegisterNull<AppiumElement>();
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

    public void CloseApp(ServicesCollection childContainer)
    {
        DisposeDriverService.CloseAndroidApp(childContainer);
        DisposeDriverService.CloseIOSApp(childContainer);
        DisposeDriverService.CloseAndroidApp(ServicesCollection.Main);
        DisposeDriverService.CloseIOSApp(ServicesCollection.Main);
    }

    public void Dispose(ServicesCollection childContainer)
    {
        DisposeDriverService.DisposeAndroid(childContainer);
        DisposeDriverService.DisposeIOS(childContainer);
        DisposeDriverService.DisposeAndroid(ServicesCollection.Main);
        DisposeDriverService.DisposeIOS(ServicesCollection.Main);
    }

    public void DisposeAll()
    {
        foreach (var childContainer in ServicesCollection.Current.GetChildServicesCollections())
        {
            DisposeDriverService.DisposeAndroid(childContainer);
            DisposeDriverService.DisposeIOS(childContainer);
        }

        DisposeDriverService.DisposeAndroid(ServicesCollection.Main);
        DisposeDriverService.DisposeIOS(ServicesCollection.Main);
    }
}
