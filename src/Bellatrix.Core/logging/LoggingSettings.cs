namespace Bellatrix.Core.logging;
public class LoggingSettings
{
    public bool IsEnabled { get; set; } = true;
    public bool IsConsoleLoggingEnabled { get; set; } = true;
    public bool IsDebugLoggingEnabled { get; set; } = true;
    public bool IsFileLoggingEnabled { get; set; } = false;
    public bool IsReportPortalLoggingEnabled { get; set; } = true;
    public bool ShouldMaskSensitiveInfo { get; set; } = true;
    public string OutputTemplate { get; set; } = "{Message:lj}{NewLine}{Exception}";
}