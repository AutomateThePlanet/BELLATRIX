// <copyright file="IOSAppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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

        public static BaseApp UseElementsBddLogging(this BaseApp androidApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

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

            return androidApp;
        }

        public static BaseApp UseDynamicTestCases(this BaseApp iosApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

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
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

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

        public static BaseApp UseEnsureExtensionsBddLogging(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bddLoggingEnsureExtensions = new BDDLoggingEnsureExtensionsService();
            bddLoggingEnsureExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseEnsureExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var dynamicTestCasesEnsureExtensions = new DynamicTestCasesEnsureExtensions();
            dynamicTestCasesEnsureExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseEnsureExtensionsBugReporting(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bugReprtingEnsureExtensions = new BugReportingEnsureExtensions();
            bugReprtingEnsureExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsBddLogging(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bddLoggingLayoutAssertionsExtensions = new BDDLoggingAssertionExtensionsService();
            bddLoggingLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var dynamicTestCasesLayoutAssertionsExtensions = new DynamicTestCasesAssertionExtensions();
            dynamicTestCasesLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseLayoutAssertionExtensionsBugReporting(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bugReportingLayoutAssertionsExtensions = new BugReportingAssertionExtensions();
            bugReportingLayoutAssertionsExtensions.SubscribeToAll();

            return baseApp;
        }
    }
}
