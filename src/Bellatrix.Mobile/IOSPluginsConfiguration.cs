// <copyright file="IOSAppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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
using Bellatrix.Mobile.BddLogging.IOS;
using Bellatrix.Mobile.BugReporting.IOS;
using Bellatrix.Mobile.DynamicTestCases.IOS;
using Bellatrix.Mobile.EventHandlers.IOS;
using Bellatrix.Mobile.Plugins;
using Bellatrix.Mobile.Screenshots;
using Bellatrix.Plugins;
using Bellatrix.Plugins.Screenshots;
using Bellatrix.Plugins.Screenshots.Contracts;

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

    public static void AddDynamicTestCases()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>
                                   {
                                       new DynamicTestCasesButtonEventHandlers(),
                                       new DynamicTestCasesRadioButtonEventHandlers(),
                                       new DynamicTestCasesCheckboxEventHandlers(),
                                       new DynamicTestCasesToggleButtonEventHandlers(),
                                       new DynamicTestCasesTextFieldEventHandlers(),
                                       new DynamicTestCasesComboBoxEventHandlers(),
                                       new DynamicTestCasesPasswordEventHandlers(),
                                       new DynamicTestCasesImageButtonEventHandlers(),
                                       new DynamicTestCasesNumberEventHandlers(),
                                       new DynamicTestCasesSeekBarEventHandlers(),
                                   };
        foreach (var elementEventHandler in elementEventHandlers)
        {
            elementEventHandler.SubscribeToAll();
        }
    }

    public static void AddBugReporting()
    {
        var elementEventHandlers = new List<ComponentEventHandlers>
                                   {
                                       new BugReportingButtonEventHandlers(),
                                       new BugReportingRadioButtonEventHandlers(),
                                       new BugReportingCheckboxEventHandlers(),
                                       new BugReportingToggleButtonEventHandlers(),
                                       new BugReportingTextFieldEventHandlers(),
                                       new BugReportingComboBoxEventHandlers(),
                                       new BugReportingPasswordEventHandlers(),
                                       new BugReportingImageButtonEventHandlers(),
                                       new BugReportingNumberEventHandlers(),
                                       new BugReportingSeekBarEventHandlers(),
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
        var bugReprtingValidateExtensions = new BugReportingValidateExtensions();
        bugReprtingValidateExtensions.SubscribeToAll();
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

    public static void AddLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, AppWorkflowPlugin>(Guid.NewGuid().ToString());
    }

    public static void AddLogExecutionLifecycle()
    {
        ServicesCollection.Current.RegisterType<Plugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());
    }
}
