// <copyright file="IOSAppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Layout;
using Bellatrix.Mobile.BddLogging.IOS;
using Bellatrix.Mobile.BugReporting.IOS;
using Bellatrix.Mobile.DynamicTestCases.IOS;
using Bellatrix.Mobile.EventHandlers.IOS;
using Bellatrix.Mobile.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix.Mobile.IOS
{
    public static class IOSAppRegistrationExtensions
    {
        public static BaseApp UseIOSDriverScreenshotsOnFail(this BaseApp baseApp)
        {
            baseApp.RegisterType<IScreenshotEngine, IOSDriverScreenshotEngine>();
            baseApp.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
            baseApp.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
            baseApp.RegisterType<TestWorkflowPlugin, ScreenshotWorkflowPlugin>(Guid.NewGuid().ToString());

            return baseApp;
        }

        public static BaseApp UseElementsBddLogging(this BaseApp iosApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>
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

            return iosApp;
        }

        public static BaseApp UseDynamicTestCases(this BaseApp iosApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>
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

            return iosApp;
        }

        public static BaseApp UseBugReporting(this BaseApp iosApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>
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

            return iosApp;
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
            var bugReprtingValidateExtensions = new BugReportingValidateExtensions();
            bugReprtingValidateExtensions.SubscribeToAll();

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
