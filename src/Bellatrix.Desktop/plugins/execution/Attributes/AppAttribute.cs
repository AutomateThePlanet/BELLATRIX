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
using System.Drawing;
using Bellatrix.Desktop.Configuration;
using Bellatrix.Desktop.Services;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AppAttribute : Attribute
{
    public AppAttribute(string appPath, Lifecycle lifecycle = Lifecycle.NotSet)
    {
        AppConfiguration = new AppInitializationInfo();
        AppConfiguration.AppPath = appPath;
        AppConfiguration.Lifecycle = lifecycle;
        AppConfiguration.Size = default;
        AppConfiguration.AppiumOptions = new DesiredCapabilities();
    }

    public AppAttribute(string appPath, int width, int height, Lifecycle behavior = Lifecycle.NotSet)
        : this(appPath, behavior)
    {
        AppConfiguration.Size = new Size(width, height);
    }

    public AppAttribute(string appPath, MobileWindowSize mobileWindowSize, Lifecycle behavior = Lifecycle.NotSet)
    : this(appPath, behavior)
    {
        AppConfiguration.Size = WindowsSizeResolver.GetWindowSize(mobileWindowSize);
    }

    public AppAttribute(string appPath, TabletWindowSize tabletWindowSize, Lifecycle behavior = Lifecycle.NotSet)
    : this(appPath, behavior)
    {
        AppConfiguration.Size = WindowsSizeResolver.GetWindowSize(tabletWindowSize);
    }

    public AppAttribute(string appPath, DesktopWindowSize desktopWindowSize, Lifecycle behavior = Lifecycle.NotSet)
    : this(appPath, behavior)
    {
        AppConfiguration.Size = WindowsSizeResolver.GetWindowSize(desktopWindowSize);
    }

    public AppInitializationInfo AppConfiguration { get; }

    ////protected DesiredCapabilities AddAdditionalCapability(Type type, DesiredCapabilities desiredCapabilities)
    ////{
    ////    var additionalCaps = ServicesCollection.Current.Resolve<Dictionary<string, object>>($"caps-{type.FullName}");
    ////    if (additionalCaps != null)
    ////    {
    ////        foreach (var key in additionalCaps.Keys)
    ////        {
    ////            desiredCapabilities.SetCapability(key, additionalCaps[key]);
    ////        }
    ////    }

    ////    return desiredCapabilities;
    ////}
}
