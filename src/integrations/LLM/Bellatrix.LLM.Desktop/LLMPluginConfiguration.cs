using Bellatrix.Desktop.LLM.Plugins;
using Bellatrix.LLM.Plugins;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Bellatrix.Web.LLM.Skills;
using Microsoft.SemanticKernel;

namespace Bellatrix.LLM.Desktop;

public static class LLMPluginConfiguration
{
    public static void ConfigureLLM()
    {
        ServicesCollection.Main.RegisterInstance<IViewSnapshotProvider>(new ViewSnapshotProvider());

        if (ConfigurationService.GetSection<LargeLanguageModelsSettings>() == null)
        {
            Logger.LogError("Could not load LargeLanguageModelsSettings section from testFrameworkSettings.json");
            return;
        }

        try
        {
            var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();
            if (!settings.EnableSelfHealing && !settings.EnableSmartFailureAnalysis)
            {
                Logger.LogError("LLM Features are disabled.");
                return;
            }
            SemanticKernelService.Kernel.ImportPluginFromObject(new LocatorSkill(), nameof(LocatorSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new AssertionSkill(), nameof(AssertionSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new PageObjectSummarizerSkill(), nameof(PageObjectSummarizerSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new LocatorMapperSkill(), nameof(LocatorMapperSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new FailureAnalyzerSkill(), nameof(FailureAnalyzerSkill));

            // index all page objects:
            if (settings.ShouldIndexPageObjects)
            {
                PageObjectsIndexer.IndexAllPageObjects(settings.PageObjectFilesPath, settings.MemoryIndex, settings.ResetIndexEverytime);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex.ToString());
        }
    }
}