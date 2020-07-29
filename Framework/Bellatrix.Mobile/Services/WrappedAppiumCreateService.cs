// <copyright file="WrappedAppiumCreateService.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Configuration;
using Bellatrix.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Service;

namespace Bellatrix.Mobile.Services
{
    public static class WrappedAppiumCreateService
    {
        private static readonly bool _shouldStartAppiumLocalService;
        private static readonly string _appiumServiceUrl;

        static WrappedAppiumCreateService()
        {
            _shouldStartAppiumLocalService = ConfigurationService.Instance.GetMobileSettings().ShouldStartAppiumLocalService;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                _shouldStartAppiumLocalService = false;
            }

            _appiumServiceUrl = ConfigurationService.Instance.GetMobileSettings().AppiumServiceUrl;
        }

        public static AppiumLocalService AppiumLocalService { get; set; }

        public static AndroidDriver<AndroidElement> CreateAndroidDriver(AppConfiguration appConfiguration, IServicesCollection childContainer)
        {
            var driverOptions = childContainer.Resolve<AppiumOptions>(appConfiguration.ClassFullName) ?? appConfiguration.AppiumOptions;
            driverOptions = driverOptions ?? new AppiumOptions();
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, appConfiguration.AppPath);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, appConfiguration.DeviceName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, appConfiguration.PlatformName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, appConfiguration.PlatformVersion);

            if (string.IsNullOrEmpty(appConfiguration.BrowserName))
            {
                driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppPackage, appConfiguration.AppPackage);
                driverOptions.AddAdditionalCapability(AndroidMobileCapabilityType.AppActivity, appConfiguration.AppActivity);
            }
            else
            {
                driverOptions.AddAdditionalCapability(MobileCapabilityType.BrowserName, appConfiguration.BrowserName);
            }

            var wrappedWebDriver = default(AndroidDriver<AndroidElement>);

            if (appConfiguration.ExecutionType == Enums.ExecutionType.Regular)
            {
                if (_shouldStartAppiumLocalService)
                {
                    if (AppiumLocalService == null)
                    {
                        throw new ArgumentException("The Appium local service is not started. You need to call App?.StartAppiumLocalService() in AssemblyInitialize method.");
                    }

                    wrappedWebDriver = new AndroidDriver<AndroidElement>(AppiumLocalService, driverOptions);
                }
                else
                {
                    wrappedWebDriver = new AndroidDriver<AndroidElement>(new Uri(_appiumServiceUrl), driverOptions);
                }
            }
            else if (appConfiguration.ExecutionType == Enums.ExecutionType.SauceLabs)
            {
                wrappedWebDriver = new AndroidDriver<AndroidElement>(ConfigurationService.Instance.GetMobileSettings().SauceLabs.GridUri, appConfiguration.AppiumOptions, TimeSpan.FromSeconds(240));
            }
            else if (appConfiguration.ExecutionType == Enums.ExecutionType.BrowserStack)
            {
                wrappedWebDriver = new AndroidDriver<AndroidElement>(ConfigurationService.Instance.GetMobileSettings().BrowserStack.GridUri, appConfiguration.AppiumOptions);
            }
            else if (appConfiguration.ExecutionType == Enums.ExecutionType.CrossBrowserTesting)
            {
                wrappedWebDriver = new AndroidDriver<AndroidElement>(ConfigurationService.Instance.GetMobileSettings().CrossBrowserTesting.GridUri, appConfiguration.AppiumOptions);
            }

            return wrappedWebDriver;
        }

        public static IOSDriver<IOSElement> CreateIOSDriver(AppConfiguration appConfiguration, IServicesCollection childContainer)
        {
            var driverOptions = childContainer.Resolve<AppiumOptions>(appConfiguration.ClassFullName) ?? appConfiguration.AppiumOptions;
            driverOptions.AddAdditionalCapability(MobileCapabilityType.App, appConfiguration.AppPath);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.DeviceName, appConfiguration.DeviceName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, appConfiguration.PlatformName);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, appConfiguration.PlatformVersion);
            driverOptions.AddAdditionalCapability(MobileCapabilityType.AutomationName, "XCUITest");

            IOSDriver<IOSElement> wrappedWebDriver;
            if (_shouldStartAppiumLocalService)
            {
                if (AppiumLocalService == null)
                {
                    throw new ArgumentException("The Appium local service is not started. You need to call App?.StartAppiumLocalService() in AssemblyInitialize method.");
                }

                wrappedWebDriver = new IOSDriver<IOSElement>(AppiumLocalService, driverOptions);
            }
            else
            {
                wrappedWebDriver = new IOSDriver<IOSElement>(new Uri(_appiumServiceUrl), driverOptions);
            }

            wrappedWebDriver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(180);

            childContainer.RegisterInstance(wrappedWebDriver);
            childContainer.RegisterInstance<IWebDriver>(wrappedWebDriver);
            childContainer.RegisterInstance(childContainer.Resolve<BrowserService>());
            childContainer.RegisterInstance(childContainer.Resolve<CookiesService>());
            childContainer.RegisterInstance(childContainer.Resolve<DialogService>());
            childContainer.RegisterInstance(childContainer.Resolve<JavaScriptService>());
            childContainer.RegisterInstance(childContainer.Resolve<NavigationService>());
            childContainer.RegisterInstance(childContainer.Resolve<ElementCreateService>());
            var webDriver = childContainer.Resolve<IWebDriver>();
            childContainer.RegisterInstance<IWebDriverElementFinderService>(new NativeElementFinderService(webDriver));
            childContainer.RegisterNull<int?>();
            childContainer.RegisterNull<IWebElement>();

            return wrappedWebDriver;
        }
    }
}
