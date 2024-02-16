// <copyright file="SauceLabsAttribute.cs" company="Automate The Planet Ltd.">
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
using System.Reflection;
using Bellatrix.Mobile.Enums;
using Bellatrix.Mobile.Plugins;
using Bellatrix.Mobile.Plugins.Attributes;
using OpenQA.Selenium.Appium;

namespace Bellatrix.Mobile;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public abstract class SauceLabsAttribute : AppAttribute, IAppiumOptionsFactory
{
    protected SauceLabsAttribute(
        string appPath,
        string appId,
        string platformVersion,
        string deviceName,
        Lifecycle behavior = Lifecycle.NotSet,
        bool recordVideo = false,
        bool recordScreenshots = false)
        : base(appPath, appId, platformVersion, deviceName, behavior)
    {
        RecordVideo = recordVideo;
        RecordScreenshots = recordScreenshots;
        AppConfiguration.ExecutionType = ExecutionType.SauceLabs;
    }

    public bool RecordVideo { get; }

    public bool RecordScreenshots { get; }

    public AppiumOptions CreateAppiumOptions(MemberInfo memberInfo, Type testClassType)
    {
        var appiumOptions = new AppiumOptions();
        AddAdditionalCapabilities(testClassType, appiumOptions);

        appiumOptions.AddAdditionalAppiumOption("browserName", string.Empty);
        appiumOptions.AddAdditionalAppiumOption("deviceName", AppConfiguration.DeviceName);
        appiumOptions.AddAdditionalAppiumOption("app", AppConfiguration.AppPath);
        appiumOptions.AddAdditionalAppiumOption("platformVersion", AppConfiguration.PlatformVersion);
        appiumOptions.AddAdditionalAppiumOption("recordVideo", RecordVideo);
        appiumOptions.AddAdditionalAppiumOption("recordScreenshots", RecordScreenshots);
        appiumOptions.AddAdditionalAppiumOption("appiumVersion", "1.8.1");

        var credentials = CloudProviderCredentialsResolver.GetCredentials();
        appiumOptions.AddAdditionalAppiumOption("username", credentials.Item1);
        appiumOptions.AddAdditionalAppiumOption("accessKey", credentials.Item2);
        appiumOptions.AddAdditionalAppiumOption("name", testClassType.FullName);

        return appiumOptions;
    }
}