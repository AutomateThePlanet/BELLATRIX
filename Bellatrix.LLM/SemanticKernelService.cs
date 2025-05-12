using Bellatrix.KeyVault;
using Bellatrix.LLM.settings;
using Microsoft.Extensions.Configuration;
using Microsoft.KernelMemory;
using Microsoft.SemanticKernel;
using System.Collections.Concurrent;

namespace Bellatrix.LLM;

public class SemanticKernelService
{
    private static readonly ThreadLocal<Kernel> _kernel = new();
    private static readonly ThreadLocal<IKernelMemory> _memory = new();
    private static readonly ThreadLocal<ConcurrentDictionary<string, string>> _locatorCache = new(() => new ConcurrentDictionary<string, string>());

    public static Kernel Kernel
    {
        get
        {
            EnsureInitialized();
            return _kernel.Value!;
        }
    }

    public static IKernelMemory Memory
    {
        get
        {
            EnsureInitialized();
            return _memory.Value!;
        }
    }

    public static ConcurrentDictionary<string, string> LocatorCache => _locatorCache.Value!;

    private static void EnsureInitialized()
    {
        if (_kernel.Value != null && _memory.Value != null)
            return;

        var llmSettings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        var genSettings = llmSettings.ModelSettings[0];
        var embedSettings = llmSettings.ModelSettings[1];

        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(
            deploymentName: genSettings.Deployment,
            endpoint: SecretsResolver.GetSecret(() => genSettings.Endpoint),
            apiKey: SecretsResolver.GetSecret(() => genSettings.Key));

        var memory = new KernelMemoryBuilder()
            .WithQdrantMemoryDb(llmSettings.QdrantMemoryDbEndpoint)
            .WithAzureOpenAITextGeneration(new AzureOpenAIConfig
            {
                APIKey = SecretsResolver.GetSecret(() => genSettings.Key),
                Endpoint = SecretsResolver.GetSecret(() => genSettings.Endpoint),
                Deployment = genSettings.Deployment,
                Auth = AzureOpenAIConfig.AuthTypes.APIKey
            })
            .WithAzureOpenAITextEmbeddingGeneration(new AzureOpenAIConfig
            {
                APIKey = SecretsResolver.GetSecret(() => embedSettings.Key),
                Endpoint = SecretsResolver.GetSecret(() => embedSettings.Endpoint),
                Deployment = embedSettings.EmbeddingDeployment,
                Auth = AzureOpenAIConfig.AuthTypes.APIKey
            })
            .Build(new KernelMemoryBuilderBuildOptions
            {
                AllowMixingVolatileAndPersistentData = true
            });

        _kernel.Value = builder.Build();
        _memory.Value = memory;
    }
}