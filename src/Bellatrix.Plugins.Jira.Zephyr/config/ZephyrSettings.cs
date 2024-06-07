namespace Bellatrix.Plugins.Jira.Zephyr;

public sealed class ZephyrSettings
{
    public bool IsEnabled { get; set; }
    public string? ApiUrl { get; set; }
    public string? Token { get; set; }
    public string? DefaultProjectKey { get; set; }
    public string? TestCycleName { get; set; } = "BELLATRIX TEST RUN";
    public string? CycleFinalStatus { get; set; } = "Done";
    public bool IsExistingCycle { get; set; } = false;
}
