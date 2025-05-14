namespace Bellatrix.LLM.cache;
public class SelfHealingLocatorEntry
{
    public int Id { get; set; }
    public string Project { get; set; } = string.Empty;
    public string AppLocation { get; set; } = string.Empty;
    public string ValidLocator { get; set; } = string.Empty;
    public string ViewSummary { get; set; } = string.Empty;
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;
}