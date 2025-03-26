namespace Bellatrix.DataGeneration.Configuration;
public class InputTypeSettings
{
    public string PrecisionStep { get; set; }
    public List<string> ValidEquivalenceClasses { get; set; } = new();
    public List<string> InvalidEquivalenceClasses { get; set; } = new();
}
