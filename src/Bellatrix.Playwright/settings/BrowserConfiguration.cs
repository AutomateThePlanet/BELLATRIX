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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Playwright.Enums;
using System.Drawing;

namespace Bellatrix.Playwright.Settings;

public class BrowserConfiguration
{
    public BrowserConfiguration()
    {
    }

    public BrowserConfiguration(ExecutionType executionType, Lifecycle browserBehavior, BrowserTypes browserType, Size size, string classFullName, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible, Dictionary<string, object> gridOptions = null)
        : this(browserBehavior, browserType, size, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        ExecutionType = executionType;
        ClassFullName = classFullName;
        GridOptions = gridOptions != null ? gridOptions : new Dictionary<string, object>();
    }

    public BrowserConfiguration(Lifecycle browserBehavior, BrowserTypes browserType, Size size, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible, Dictionary<string, object> gridOptions = null)
        : this(browserType, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        BrowserBehavior = browserBehavior;
        Size = size;
        GridOptions = gridOptions != null ? gridOptions : new Dictionary<string, object>();
    }

    public BrowserConfiguration(BrowserTypes browserType, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible, Dictionary<string, object> gridOptions = null)
    {
        BrowserType = browserType;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible;
        GridOptions = gridOptions != null ? gridOptions : new Dictionary<string, object>();
    }

    public BrowserTypes BrowserType { get; set; } = BrowserTypes.Chromium;
    public Lifecycle BrowserBehavior { get; set; } = Lifecycle.RestartEveryTime;
    public Size Size { get; set; }
    public bool ShouldCaptureHttpTraffic { get; set; }
    public bool ShouldDisableJavaScript { get; set; }
    public string ClassFullName { get; set; }
    public dynamic PlaywrightOptions { get; set; }
    public dynamic ContextOptions { get; set; }
    public ExecutionType ExecutionType { get; set; } = ExecutionType.Regular;
    public Dictionary<string, object> GridOptions { get; set; }
    public bool ShouldAutomaticallyScrollToVisible { get; set; }
    public bool IsLighthouseEnabled { get; set; }

    public bool Equals(BrowserConfiguration other) => ExecutionType.Equals(other?.ExecutionType) &&
                                                      BrowserType.Equals(other?.BrowserType) &&
                                                      ShouldCaptureHttpTraffic.Equals(other?.ShouldCaptureHttpTraffic) &&
                                                      ShouldDisableJavaScript.Equals(other?.ShouldDisableJavaScript) &&
                                                      Size.Equals(other?.Size) &&
                                                      GridOptions.SequenceEqual(other?.GridOptions) &&
                                                      IsLighthouseEnabled.Equals(other?.IsLighthouseEnabled) &&
                                                      ShouldAutomaticallyScrollToVisible.Equals(other?.ShouldAutomaticallyScrollToVisible);

    public override bool Equals(object obj)
    {
        if (obj is not BrowserConfiguration) return false;

        var browserConfiguration = (BrowserConfiguration)obj;

        return ExecutionType.Equals(browserConfiguration?.ExecutionType) &&
            BrowserType.Equals(browserConfiguration?.BrowserType) &&
            ShouldCaptureHttpTraffic.Equals(browserConfiguration?.ShouldCaptureHttpTraffic) &&
            ShouldDisableJavaScript.Equals(browserConfiguration?.ShouldDisableJavaScript) &&
            Size.Equals(browserConfiguration?.Size) &&
            GridOptions.Equals(browserConfiguration?.GridOptions) &&
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
                GridOptions.GetHashCode() +
                IsLighthouseEnabled.GetHashCode() +
                ShouldAutomaticallyScrollToVisible.GetHashCode();
    }
}
