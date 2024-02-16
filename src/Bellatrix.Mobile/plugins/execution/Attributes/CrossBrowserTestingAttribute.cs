// <copyright file="CrossBrowserTestingAttribute.cs" company="Automate The Planet Ltd.">
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
public abstract class CrossBrowserTestingAttribute : AppAttribute, IAppiumOptionsFactory
{
    protected CrossBrowserTestingAttribute(
        string appPath,
        string appId,
        string platformVersion,
        string deviceName,
        Lifecycle behavior = Lifecycle.NotSet,
        bool recordVideo = false,
        bool recordNetwork = false,
        string build = null)
        : base(appPath, appId, platformVersion, deviceName, behavior)
    {
        Build = build;
        RecordVideo = recordVideo;
        RecordNetwork = recordNetwork;
        AppConfiguration.ExecutionType = ExecutionType.BrowserStack;
    }

    public string Build { get; }

    public bool RecordVideo { get; }

    public bool RecordNetwork { get; }

    public AppiumOptions CreateAppiumOptions(MemberInfo memberInfo, Type testClassType)
    {
        var appiumOptions = new AppiumOptions();
        AddAdditionalCapabilities(testClassType, appiumOptions);

        if (!string.IsNullOrEmpty(Build))
        {
            appiumOptions.AddAdditionalAppiumOption("build", Build);
        }

        appiumOptions.AddAdditionalAppiumOption("device", AppConfiguration.DeviceName);
        appiumOptions.AddAdditionalAppiumOption("app", AppConfiguration.AppPath);
        appiumOptions.AddAdditionalAppiumOption("record_video", RecordVideo);
        appiumOptions.AddAdditionalAppiumOption("record_network", RecordNetwork);

        var credentials = CloudProviderCredentialsResolver.GetCredentials();
        appiumOptions.AddAdditionalAppiumOption("username", credentials.Item1);
        appiumOptions.AddAdditionalAppiumOption("password", credentials.Item2);

        appiumOptions.AddAdditionalAppiumOption("name", memberInfo.Name);

        return appiumOptions;
    }
}