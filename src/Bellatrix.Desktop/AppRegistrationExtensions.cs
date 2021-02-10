// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
using Bellatrix.Application;
using Bellatrix.Desktop;
using Bellatrix.Desktop.BddLogging;
using Bellatrix.Desktop.BugReporting;
using Bellatrix.Desktop.DynamicTestCases;
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Desktop.TestExecutionExtensions;
using Bellatrix.Layout;
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseAppBehavior(this BaseApp baseApp)
        {
            baseApp.RegisterType<TestWorkflowPlugin, AppWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseLogExecutionBehavior(this BaseApp baseApp)
        {
            baseApp.RegisterType<TestWorkflowPlugin, LogWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseVanillaWebDriverScreenshotsOnFail(this BaseApp baseApp)
        {
            baseApp.RegisterType<IScreenshotEngine, VanillaWebDriverScreenshotEngine>();
            baseApp.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
            baseApp.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
            baseApp.RegisterType<TestWorkflowPlugin, ScreenshotWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseElementsBddLogging(this BaseApp baseApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BDDLoggingCheckboxEventHandlers(),
                                           new BDDLoggingComboBoxEventHandlers(),
                                           new BDDLoggingDateEventHandlers(),
                                           new BDDLoggingElementEventHandlers(),
                                           new BDDLoggingPasswordEventHandlers(),
                                           new BDDLoggingTextAreaEventHandlers(),
                                           new BDDLoggingTextFieldEventHandlers(),
                                           new BDDLoggingTimeEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }

        public static BaseApp UseDynamicTestCases(this BaseApp baseApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new DynamicTestCasesCheckboxEventHandlers(),
                                           new DynamicTestCasesComboBoxEventHandlers(),
                                           new DynamicTestCasesDateEventHandlers(),
                                           new DynamicTestCasesElementEventHandlers(),
                                           new DynamicTestCasesPasswordEventHandlers(),
                                           new DynamicTestCasesTextAreaEventHandlers(),
                                           new DynamicTestCasesTextFieldEventHandlers(),
                                           new DynamicTestCasesTimeEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }

        public static BaseApp UseBugReporting(this BaseApp baseApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BugReportingCheckboxEventHandlers(),
                                           new BugReportingComboBoxEventHandlers(),
                                           new BugReportingDateEventHandlers(),
                                           new BugReportingElementEventHandlers(),
                                           new BugReportingPasswordEventHandlers(),
                                           new BugReportingTextAreaEventHandlers(),
                                           new BugReportingTextFieldEventHandlers(),
                                           new BugReportingTimeEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }

        public static BaseApp UseValidateExtensionsBddLogging(this BaseApp baseApp)
        {
            var bddLoggingValidateExtensions = new BDDLoggingValidateExtensionsService();
            bddLoggingValidateExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseValidateExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            var dynamicTestCasesValidateExtensions = new DynamicTestCasesValidateExtensions();
            dynamicTestCasesValidateExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseValidateExtensionsBugReporting(this BaseApp baseApp)
        {
            var bugReportingValidateExtensions = new BugReportingValidateExtensions();
            bugReportingValidateExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsBddLogging(this BaseApp baseApp)
        {
            var bddLoggingLayoutAssertionsExtensions = new BDDLoggingAssertionExtensionsService();
            bddLoggingLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            var dynamicTestCasesLayoutAssertionsExtensions = new DynamicTestCasesAssertionExtensions();
            dynamicTestCasesLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsBugReporting(this BaseApp baseApp)
        {
            var bugReportingLayoutAssertionsExtensions = new BugReportingAssertionExtensions();
            bugReportingLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }
    }
}
