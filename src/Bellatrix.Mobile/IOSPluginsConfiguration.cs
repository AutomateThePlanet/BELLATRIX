// <copyright file="IOSAppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using System;
using System.Collections.Generic;
using Bellatrix.Layout;
using Bellatrix.LLM.Plugins;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Bellatrix.LLM;
using Bellatrix.Mobile.BddLogging.IOS;
using Bellatrix.Mobile.EventHandlers.IOS;
using Bellatrix.Mobile.Plugins;
using Bellatrix.Mobile.Screenshots;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Contracts;
using Bellatrix.Mobile.LLM.Skills.iOS;
using Microsoft.SemanticKernel;

namespace Bellatrix.Mobile.IOS;

public static class IOSPluginsConfiguration
{
    public static void AddIOSDriverScreenshotsOnFail()
    {
        ServicesCollection.Current.RegisterType<IScreenshotEngine, IOSDriverScreenshotEngine>();
        ServicesCollection.Current.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
        ServicesCollection.Current.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
        ServicesCollection.Current.RegisterType<Plugin, ScreenshotPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddElementsBddLogging()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>
                                   {
                                       new BDDLoggingButtonEventHandlers(),
                                       new BDDLoggingRadioButtonEventHandlers(),
                                       new BDDLoggingCheckboxEventHandlers(),
                                       new BDDLoggingToggleButtonEventHandlers(),
                                       new BDDLoggingTextFieldEventHandlers(),
                                       new BDDLoggingComboBoxEventHandlers(),
                                       new BDDLoggingPasswordEventHandlers(),
                                       new BDDLoggingImageButtonEventHandlers(),
                                       new BDDLoggingNumberEventHandlers(),
                                       new BDDLoggingSeekBarEventHandlers(),
                                   };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }

    public static void AddValidateExtensionsBddLogging()
    {
        var bddLoggingValidateExtensions = new BDDLoggingValidateExtensionsService();
        bddLoggingValidateExtensions.SubscribeToAll();
    }

    public static void AddLayoutAssertionExtensionsBddLogging()
    {
        var bddLoggingLayoutAssertionsExtensions = new BDDLoggingAssertionExtensionsService();
        bddLoggingLayoutAssertionsExtensions.SubscribeToAll();
    }

    public static void AddLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, AppWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddLogExecutionLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void ConfigureLLM()
    {
        if (ConfigurationService.GetSection<LargeLanguageModelsSettings>() == null)
        {
            Logger.LogError("Could not load LargeLanguageModelsSettings section from testFrameworkSettings.json");
            return;
        }

        try
        {
            var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

            SemanticKernelService.Kernel.ImportPluginFromObject(new IOSLocatorSkill(), nameof(IOSLocatorSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new AssertionSkill(), nameof(AssertionSkill));
            SemanticKernelService.Kernel.ImportPluginFromObject(new IOSPageObjectSummarizerSkill(), nameof(IOSPageObjectSummarizerSkill));
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