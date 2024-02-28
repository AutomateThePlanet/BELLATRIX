// <copyright file="WrappedAppiumCreateService.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>AndroidDriver
// <site>https://bellatrix.solutions/</site>
using System;
using System.Runtime.InteropServices;
using Bellatrix.Mobile.Configuration;
using Bellatrix.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;

namespace Bellatrix.Mobile.Services;

public static class WrappedAppiumCreateService
{
    private static readonly bool _shouldStartAppiumLocalService;
    private static readonly string _appiumServiceUrl;

    static WrappedAppiumCreateService()
    {
        _shouldStartAppiumLocalService = ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.ShouldStartLocalService;
        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            _shouldStartAppiumLocalService = false;
        }

        _appiumServiceUrl = ConfigurationService.GetSection<MobileSettings>().ExecutionSettings.Url;
    }

    public static AppiumLocalService AppiumLocalService { get; set; }

    public static AndroidDriver CreateAndroidDriver(AppConfiguration appConfiguration, ServicesCollection childContainer)
    {
        var driverOptions = childContainer.Resolve<AppiumOptions>(appConfiguration.ClassFullName) ?? appConfiguration.AppiumOptions;
        driverOptions ??= new AppiumOptions();
        driverOptions.App = appConfiguration.AppPath ?? appConfiguration.AppId;
        driverOptions.DeviceName = appConfiguration.DeviceName;
        driverOptions.PlatformName = appConfiguration.PlatformName;
        driverOptions.PlatformVersion = appConfiguration.PlatformVersion;
        driverOptions.AutomationName = "UiAutomator2";

        if (string.IsNullOrEmpty(appConfiguration.BrowserName))
        {
            AddAdditionalOptions(driverOptions, AndroidMobileCapabilityType.AppPackage, appConfiguration.AppId);
            AddAdditionalOptions(driverOptions, AndroidMobileCapabilityType.AppActivity, appConfiguration.AppActivity);
        }
        else
        {
            AddAdditionalOptions(driverOptions, MobileCapabilityType.BrowserName, appConfiguration.BrowserName);
        }

        var wrappedWebDriver = default(AndroidDriver);

        if (_shouldStartAppiumLocalService)
        {
            if (AppiumLocalService == null)
            {
                throw new ArgumentException("The Appium local service is not started. You need to call App?.StartAppiumLocalService() in AssemblyInitialize method.");
            }

            wrappedWebDriver = new AndroidDriver(AppiumLocalService, driverOptions);
        }
        else
        {
            wrappedWebDriver = new AndroidDriver(new Uri(_appiumServiceUrl), driverOptions);
        }

        wrappedWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigurationService.GetSection<MobileSettings>().TimeoutSettings.ImplicitWaitTimeout);

        childContainer.RegisterInstance<IWebDriver>(wrappedWebDriver);
        childContainer.RegisterInstance(childContainer.Resolve<BrowserService>());
        childContainer.RegisterInstance(childContainer.Resolve<CookiesService>());
        childContainer.RegisterInstance(childContainer.Resolve<DialogService>());
        childContainer.RegisterInstance(childContainer.Resolve<JavaScriptService>());
        childContainer.RegisterInstance(childContainer.Resolve<NavigationService>());
        childContainer.RegisterInstance(childContainer.Resolve<ComponentCreateService>());
        var webDriver = childContainer.Resolve<IWebDriver>();
        childContainer.RegisterInstance<IWebDriverElementFinderService>(new NativeElementFinderService(webDriver));
        childContainer.RegisterNull<int?>();
        childContainer.RegisterNull<IWebElement>();

        return wrappedWebDriver;
    }

    public static IOSDriver CreateIOSDriver(AppConfiguration appConfiguration, ServicesCollection childContainer)
    {
        var driverOptions = childContainer.Resolve<AppiumOptions>(appConfiguration.ClassFullName) ?? appConfiguration.AppiumOptions;
        driverOptions.App = appConfiguration.AppPath;
        driverOptions.DeviceName = appConfiguration.DeviceName;
        driverOptions.PlatformName = appConfiguration.PlatformName;
        driverOptions.PlatformVersion = appConfiguration.PlatformVersion;
        driverOptions.AutomationName = "XCUITest";

        IOSDriver wrappedWebDriver;
        if (_shouldStartAppiumLocalService)
        {
            if (AppiumLocalService == null)
            {
                throw new ArgumentException("The Appium local service is not started. You need to call App?.StartAppiumLocalService() in AssemblyInitialize method.");
            }

            wrappedWebDriver = new IOSDriver(AppiumLocalService, driverOptions);
        }
        else
        {
            wrappedWebDriver = new IOSDriver(new Uri(_appiumServiceUrl), driverOptions);
        }

        wrappedWebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(ConfigurationService.GetSection<MobileSettings>().TimeoutSettings.ImplicitWaitTimeout);

        childContainer.RegisterInstance(wrappedWebDriver);
        childContainer.RegisterInstance<IWebDriver>(wrappedWebDriver);
        childContainer.RegisterInstance(childContainer.Resolve<BrowserService>());
        childContainer.RegisterInstance(childContainer.Resolve<CookiesService>());
        childContainer.RegisterInstance(childContainer.Resolve<DialogService>());
        childContainer.RegisterInstance(childContainer.Resolve<JavaScriptService>());
        childContainer.RegisterInstance(childContainer.Resolve<NavigationService>());
        childContainer.RegisterInstance(childContainer.Resolve<ComponentCreateService>());
        var webDriver = childContainer.Resolve<IWebDriver>();
        childContainer.RegisterInstance<IWebDriverElementFinderService>(new NativeElementFinderService(webDriver));
        childContainer.RegisterNull<int?>();
        childContainer.RegisterNull<IWebElement>();

        return wrappedWebDriver;
    }

    private static AppiumOptions AddAdditionalOptions(AppiumOptions options, string key, string value)
    {
        if (!options.ToDictionary().ContainsKey(key))
        {
            options.AddAdditionalAppiumOption(key, value);
        }

        return options;
    }
}