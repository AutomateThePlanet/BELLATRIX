namespace Bellatrix.DataGeneration.Configuration;
public class InputTypeSettings
{
    public string PrecisionStep { get; set; }
    public string PrecisionStepUnit { get; set; } = null;
    public List<string> ValidEquivalenceClasses { get; set; } = new();
    public List<string> InvalidEquivalenceClasses { get; set; } = new();
}
