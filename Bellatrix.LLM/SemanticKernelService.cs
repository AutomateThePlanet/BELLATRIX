// <copyright file="SemanticKernelService.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
// <note>This file is part of an academic research project exploring autonomous test agents using LLMs and Semantic Kernel.
// The architecture and agent logic are original contributions by Anton Angelov, forming the foundation for a PhD dissertation.
// Please cite or credit appropriately if reusing in academic or commercial work.</note>
using Bellatrix.KeyVault;
using Bellatrix.LLM.settings;
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