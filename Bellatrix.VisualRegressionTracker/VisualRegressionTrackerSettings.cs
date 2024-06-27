namespace Bellatrix.VRT;

public class VisualRegressionTrackerSettings
{
    public string? ApiUrl { get; set; }
    public string? ApiKey { get; set; }
    public string? Project {  get; set; }
    public string? Branch { get; set; }
    public bool EnableSoftAssert { get; set; } = false;
    public string? CiBuildId { get; set; }
}
