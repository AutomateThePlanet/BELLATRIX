// <copyright file="AppAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using Bellatrix.Mobile.Configuration;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public abstract class AppAttribute : Attribute
{
    protected AppAttribute(string appPath, string appId, string platformVersion, string deviceName, Lifecycle lifecycle = Lifecycle.NotSet)
        => AppConfiguration = new AppConfiguration
        {
            AppPath = appPath,
            AppId = appId,
            Lifecycle = lifecycle,
            PlatformVersion = platformVersion,
            DeviceName = deviceName,
            AppiumOptions = new AppiumOptions(),
            OSPlatform = OS.Windows,
        };

    protected AppAttribute(OS osPlatform, string appPath, string appId, string platformVersion, string deviceName, Lifecycle behavior = Lifecycle.NotSet)
        => AppConfiguration = new AppConfiguration
                              {
                                  AppPath = appPath,
                                  AppId = appId,
                                  Lifecycle = behavior,
                                  PlatformVersion = platformVersion,
                                  DeviceName = deviceName,
                                  AppiumOptions = new AppiumOptions(),
                                  OSPlatform = osPlatform,
                              };

    public AppConfiguration AppConfiguration { get; }

    protected AppiumOptions AddAdditionalCapabilities(Type type, AppiumOptions appiumOptions)
    {
        var additionalCaps = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{type.FullName}");
        if (additionalCaps != null)
        {
            foreach (var key in additionalCaps.Keys)
            {
                appiumOptions.AddAdditionalAppiumOption(key, additionalCaps[key]);
            }
        }

        return appiumOptions;
    }
}
