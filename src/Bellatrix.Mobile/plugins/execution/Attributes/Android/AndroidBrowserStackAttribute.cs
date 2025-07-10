﻿// <copyright file="AndroidBrowserStackAttribute.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Reflection;
using Bellatrix.Mobile.Plugins.Attributes;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;

namespace Bellatrix.Mobile;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AndroidBrowserStackAttribute : BrowserStackAttribute, IAppiumOptionsFactory
{
    public AndroidBrowserStackAttribute(
        string appPath,
        string appId,
        string platformVersion,
        string deviceName,
        string appActivity,
        Lifecycle behavior = Lifecycle.NotSet,
        bool captureVideo = false,
        bool captureNetworkLogs = false,
        BrowserStackConsoleLogType consoleLogType = BrowserStackConsoleLogType.Disable,
        bool debug = false,
        string build = null)
        : base(appPath, appId, platformVersion, deviceName, behavior, captureVideo, captureNetworkLogs, consoleLogType, debug, build)
    {
        AppConfiguration.MobileOSType = MobileOSType.Android;
        AppConfiguration.PlatformName = "Android";
        AppConfiguration.AppActivity = appActivity;
    }

    public new AppiumOptions CreateAppiumOptions(MemberInfo memberInfo, Type testClassType)
    {
        var appiumOptions = base.CreateAppiumOptions(memberInfo, testClassType);
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppActivity, AppConfiguration.AppActivity);
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppWaitActivity, "*");
        appiumOptions.AddAdditionalAppiumOption(MobileCapabilityType.PlatformName, AppConfiguration.PlatformName);
        appiumOptions.AddAdditionalAppiumOption(AndroidMobileCapabilityType.AppPackage, AppConfiguration.AppId);

        return appiumOptions;
    }
}