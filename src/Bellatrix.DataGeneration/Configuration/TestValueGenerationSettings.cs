namespace Bellatrix.DataGeneration.Configuration;
public class TestValueGenerationSettings
{
    public bool IncludeBoundaryValues { get; set; } = true;
    public bool AllowValidEquivalenceClasses { get; set; } = false;
    public bool AllowInvalidEquivalenceClasses { get; set; } = false;
    public Dictionary<string, InputTypeSettings> InputTypeSettings { get; set; } = new();
}