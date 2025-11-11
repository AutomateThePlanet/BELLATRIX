// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop;
using Bellatrix.Desktop.BddLogging;
using Bellatrix.Desktop.BugReporting;
using Bellatrix.Desktop.DynamicTestCases;
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.Plugins;
using Bellatrix.Layout;
using Bellatrix.LLM.Plugins;
using Bellatrix.LLM.Settings;
using Bellatrix.LLM.Skills;
using Bellatrix.LLM;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Contracts;
using Microsoft.SemanticKernel;
using Bellatrix.Desktop.LLM.Plugins;
using Bellatrix.Web.LLM.Skills;

namespace Bellatrix;

public static class DesktopPluginsConfiguration
{
    public static void AddLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, AppLifecyclePlugin>(Guid.NewGuid().ToString());
    }

    public static void AddLogExecutionLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, LogLifecyclePlugin>(Guid.NewGuid().ToString());
    }

    public static void AddVanillaWebDriverScreenshotsOnFail()
    {
        ServicesCollection.Current.RegisterType<IScreenshotEngine, VanillaWebDriverScreenshotEngine>();
        ServicesCollection.Current.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
        ServicesCollection.Current.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
        ServicesCollection.Current.RegisterType<Plugin, ScreenshotPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddElementsBddLogging()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
                                   {
                                       new BDDLoggingCheckboxEventHandlers(),
                                       new BDDLoggingComboBoxEventHandlers(),
                                       new BDDLoggingDateEventHandlers(),
                                       new BDDLoggingComponentEventHandlers(),
                                       new BDDLoggingPasswordEventHandlers(),
                                       new BDDLoggingTextAreaEventHandlers(),
                                       new BDDLoggingTextFieldEventHandlers(),
                                       new BDDLoggingTimeEventHandlers(),
                                   };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }

    public static void AddDynamicTestCases()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
                                   {
                                       new DynamicTestCasesCheckboxEventHandlers(),
                                       new DynamicTestCasesComboBoxEventHandlers(),
                                       new DynamicTestCasesDateEventHandlers(),
                                       new DynamicTestCasesComponentEventHandlers(),
                                       new DynamicTestCasesPasswordEventHandlers(),
                                       new DynamicTestCasesTextAreaEventHandlers(),
                                       new DynamicTestCasesTextFieldEventHandlers(),
                                       new DynamicTestCasesTimeEventHandlers(),
                                   };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }

    public static void AddBugReporting()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>()
                                   {
                                       new BugReportingCheckboxEventHandlers(),
                                       new BugReportingComboBoxEventHandlers(),
                                       new BugReportingDateEventHandlers(),
                                       new BugReportingComponentEventHandlers(),
                                       new BugReportingPasswordEventHandlers(),
                                       new BugReportingTextAreaEventHandlers(),
                                       new BugReportingTextFieldEventHandlers(),
                                       new BugReportingTimeEventHandlers(),
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

    public static void AddValidateExtensionsDynamicTestCases()
    {
        var dynamicTestCasesValidateExtensions = new DynamicTestCasesValidateExtensions();
        dynamicTestCasesValidateExtensions.SubscribeToAll();
    }

    public static void AddValidateExtensionsBugReporting()
    {
        var bugReportingValidateExtensions = new BugReportingValidateExtensions();
        bugReportingValidateExtensions.SubscribeToAll();
    }

    public static void AddLayoutAssertionExtensionsBddLogging()
    {
        var bddLoggingLayoutAssertionsExtensions = new BDDLoggingAssertionExtensionsService();
        bddLoggingLayoutAssertionsExtensions.SubscribeToAll();
    }

    public static void AddLayoutAssertionExtensionsDynamicTestCases()
    {
        var dynamicTestCasesLayoutAssertionsExtensions = new DynamicTestCasesAssertionExtensions();
        dynamicTestCasesLayoutAssertionsExtensions.SubscribeToAll();
    }

    public static void AddLayoutAssertionExtensionsBugReporting()
    {
        var bugReportingLayoutAssertionsExtensions = new BugReportingAssertionExtensions();
        bugReportingLayoutAssertionsExtensions.SubscribeToAll();
    }

    public static void ConfigureLLM()
    {
        if (ConfigurationService.GetSection<LargeLanguageModelsSettings>() == null)
        {
            throw new ArgumentException("Could not load LargeLanguageModelsSettings section from testFrameworkSettings.json");
        }

        try
        {
            var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

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
