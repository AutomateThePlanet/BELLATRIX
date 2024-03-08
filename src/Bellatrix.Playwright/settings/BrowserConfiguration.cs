using Bellatrix.Playwright.Enums;
using System.Drawing;

namespace Bellatrix.Playwright.Settings;

public class BrowserConfiguration
{
    public BrowserConfiguration()
    {
    }

#pragma warning disable 618
    public BrowserConfiguration(ExecutionType executionType, Lifecycle browserBehavior, BrowserChoice browserType, Size size, string classFullName, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible)
#pragma warning restore 618
        : this(browserBehavior, browserType, size, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        ExecutionType = executionType;
        ClassFullName = classFullName;
    }

    public BrowserConfiguration(Lifecycle browserBehavior, BrowserChoice browserType, Size size, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible)
        : this(browserType, shouldCaptureHttpTraffic, shouldDisableJavaScript, shouldAutomaticallyScrollToVisible)
    {
        BrowserBehavior = browserBehavior;
        Size = size;
    }

    public BrowserConfiguration(BrowserChoice browserType, bool shouldCaptureHttpTraffic, bool shouldDisableJavaScript, bool shouldAutomaticallyScrollToVisible)
    {
        BrowserType = browserType;
        ShouldCaptureHttpTraffic = shouldCaptureHttpTraffic;
        ShouldDisableJavaScript = shouldDisableJavaScript;
        ShouldAutomaticallyScrollToVisible = shouldAutomaticallyScrollToVisible;
    }

    public BrowserChoice BrowserType { get; set; } = BrowserChoice.Chromium;
    public Lifecycle BrowserBehavior { get; set; } = Lifecycle.RestartEveryTime;
    public Size Size { get; set; }
    public bool ShouldCaptureHttpTraffic { get; set; }
    public bool ShouldDisableJavaScript { get; set; }
    public string ClassFullName { get; set; }
    public dynamic PlaywrightOptions { get; set; }
    public dynamic ContextOptions { get; set; }
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
