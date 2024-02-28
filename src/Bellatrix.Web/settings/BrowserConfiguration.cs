// <copyright file="BrowserConfiguration.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Web.Enums;
using OpenQA.Selenium;

namespace Bellatrix.Web;

public class BrowserConfiguration : IEquatable<BrowserConfiguration>
{
    public BrowserConfiguration()
    {
    }

#pragma warning disable 618
    public BrowserConfiguration(ExecutionType executionType, Lifecycle browserBehavior, BrowserType browserType, Size size, string classFullName, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible, DriverOptions driverOptions = null)
#pragma warning restore 618
        : this(browserBehavior, browserType, size, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        ExecutionType = executionType;
        ClassFullName = classFullName;
        DriverOptions = driverOptions;
    }

    public BrowserConfiguration(Lifecycle browserBehavior, BrowserType browserType, Size size, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible)
        : this(browserType, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        BrowserBehavior = browserBehavior;
        Size = size;
    }

    public BrowserConfiguration(BrowserType browserType, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible)
    {
        BrowserType = browserType;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible;
    }

    public BrowserType BrowserType { get; set; } = BrowserType.Chrome;

    public Lifecycle BrowserBehavior { get; set; } = Lifecycle.RestartEveryTime;

    public Size Size { get; set; }

    public bool ShouldCaptureHttpTraffic { get; set; }
    public bool ShouldDisableJavaScript { get; set; }

    public string ClassFullName { get; set; }

    public dynamic DriverOptions { get; set; }

    public ExecutionType ExecutionType { get; set; } = ExecutionType.Regular;

    public bool ShouldAutomaticallyScrollToVisible { get; set; }
    public bool IsLighthouseEnabled { get; set; }

    public bool Equals(BrowserConfiguration other) => ExecutionType.Equals(other?.ExecutionType) &&
                                                      BrowserType.Equals(other?.BrowserType) &&
                                                      ShouldCaptureHttpTraffic.Equals(other?.ShouldCaptureHttpTraffic) &&
                                                      ShouldDisableJavaScript.Equals(other?.ShouldDisableJavaScript) &&
                                                      Size.Equals(other?.Size) &&
                                                      IsLighthouseEnabled.Equals(other?.IsLighthouseEnabled) &&
                                                      ShouldAutomaticallyScrollToVisible.Equals(other?.ShouldAutomaticallyScrollToVisible);

    public override bool Equals(object obj)
    {
        var browserConfiguration = (BrowserConfiguration)obj;
        return ExecutionType.Equals(browserConfiguration?.ExecutionType) &&
        BrowserType.Equals(browserConfiguration?.BrowserType) &&
        ShouldCaptureHttpTraffic.Equals(browserConfiguration?.ShouldCaptureHttpTraffic) &&
        ShouldDisableJavaScript.Equals(browserConfiguration?.ShouldDisableJavaScript) &&
        Size.Equals(browserConfiguration?.Size) &&
        ShouldAutomaticallyScrollToVisible.Equals(browserConfiguration?.ShouldAutomaticallyScrollToVisible) &&
        IsLighthouseEnabled.Equals(browserConfiguration?.IsLighthouseEnabled);
    }

    public override int GetHashCode()
    {
        return ExecutionType.GetHashCode() +
                BrowserType.GetHashCode() +
                ShouldCaptureHttpTraffic.GetHashCode() +
                ShouldDisableJavaScript.GetHashCode() +
                Size.GetHashCode() +
                IsLighthouseEnabled.GetHashCode() +
                ShouldAutomaticallyScrollToVisible.GetHashCode();
    }
}