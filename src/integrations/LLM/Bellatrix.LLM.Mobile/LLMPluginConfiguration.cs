using Bellatrix.LLM;
using Bellatrix.LLM.Mobile;
using Bellatrix.LLM.Plugins;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Bellatrix.Mobile.LLM.Skills;
using Bellatrix.Mobile.LLM.Skills.Android;
using Microsoft.SemanticKernel;

namespace Bellatrix.Mobile.LLM;

public class LLMPluginConfiguration
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

            SemanticKernelService.Kernel.ImportPluginFromObject(new AndroidLocatorSkill(), nameof(AndroidLocatorSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new AssertionSkill(), nameof(AssertionSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new AndroidPageObjectSummarizerSkill(), nameof(AndroidPageObjectSummarizerSkill));
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