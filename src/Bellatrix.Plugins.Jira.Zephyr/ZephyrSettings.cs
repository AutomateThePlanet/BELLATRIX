namespace Bellatrix.Plugins.Jira.Zephyr;

public class ZephyrSettings
{
    public bool IsEnabled { get; set; }
    public string? ApiUrl { get; set; }
    public string? Token { get; set; }
    public string? DefaultProjectKey { get; set; }
    public string TestCycleName { get; set; } = "BELLATRIX TEST RUN";
    public string? CycleFinalStatus { get; set; } = "Done";
}
