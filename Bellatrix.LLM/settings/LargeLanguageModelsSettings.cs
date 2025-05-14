using SemanticKernelWebDriverPoC;

namespace Bellatrix.LLM.settings;
public class LargeLanguageModelsSettings
{
    public List<ModelSettings> ModelSettings { get; set; }
    // Postgres connection string
    public string LocalCacheConnectionString { get; set; }
    public string LocalCacheProjectName { get; set; }
    public string QdrantMemoryDbEndpoint { get; set; } = "http://localhost:6333";
    public bool ShouldIndexPageObjects { get; set; }
    public string PageObjectFilesPath { get; set; }
    public string MemoryIndex { get; set; }
    public bool ResetIndexEverytime { get; set; }
    public int LocatorRetryAttempts { get; set; }
}
