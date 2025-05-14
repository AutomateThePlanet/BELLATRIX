namespace Bellatrix.LLM.Cache;
public class LocatorCacheEntry
{
    public int Id { get; set; }
    public string Project { get; set; } = string.Empty; // from config: localCacheProjectName
    public string Url { get; set; } = string.Empty;
    public string Instruction { get; set; } = string.Empty;
    public string XPath { get; set; } = string.Empty;
    public DateTime LastValidated { get; set; } = DateTime.UtcNow;
}