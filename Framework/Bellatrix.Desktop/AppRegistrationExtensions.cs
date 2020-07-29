// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop;
using Bellatrix.Desktop.BddLogging;
using Bellatrix.Desktop.EventHandlers;
using Bellatrix.Layout;
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix
{
    public static class AppRegistrationExtensions
    {
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
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BDDLoggingButtonEventHandlers(),
                                           new BDDLoggingCalendarEventHandlers(),
                                           new BDDLoggingCheckboxEventHandlers(),
                                           new BDDLoggingComboBoxEventHandlers(),
                                           new BDDLoggingDateEventHandlers(),
                                           new BDDLoggingElementEventHandlers(),
                                           new BDDLoggingExpanderEventHandlers(),
                                           new BDDLoggingImageEventHandlers(),
                                           new BDDLoggingLabelEventHandlers(),
                                           new BDDLoggingListBoxEventHandlers(),
                                           new BDDLoggingMenuEventHandlers(),
                                           new BDDLoggingPasswordEventHandlers(),
                                           new BDDLoggingProgressEventHandlers(),
                                           new BDDLoggingRadioButtonEventHandlers(),
                                           new BDDLoggingTabsEventHandlers(),
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
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new DynamicTestCasesButtonEventHandlers(),
                                           new DynamicTestCasesCalendarEventHandlers(),
                                           new DynamicTestCasesCheckboxEventHandlers(),
                                           new DynamicTestCasesComboBoxEventHandlers(),
                                           new DynamicTestCasesDateEventHandlers(),
                                           new DynamicTestCasesElementEventHandlers(),
                                           new DynamicTestCasesExpanderEventHandlers(),
                                           new DynamicTestCasesImageEventHandlers(),
                                           new DynamicTestCasesLabelEventHandlers(),
                                           new DynamicTestCasesListBoxEventHandlers(),
                                           new DynamicTestCasesMenuEventHandlers(),
                                           new DynamicTestCasesPasswordEventHandlers(),
                                           new DynamicTestCasesProgressEventHandlers(),
                                           new DynamicTestCasesRadioButtonEventHandlers(),
                                           new DynamicTestCasesTabsEventHandlers(),
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
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BugReportingButtonEventHandlers(),
                                           new BugReportingCalendarEventHandlers(),
                                           new BugReportingCheckboxEventHandlers(),
                                           new BugReportingComboBoxEventHandlers(),
                                           new BugReportingDateEventHandlers(),
                                           new BugReportingElementEventHandlers(),
                                           new BugReportingExpanderEventHandlers(),
                                           new BugReportingImageEventHandlers(),
                                           new BugReportingLabelEventHandlers(),
                                           new BugReportingListBoxEventHandlers(),
                                           new BugReportingMenuEventHandlers(),
                                           new BugReportingPasswordEventHandlers(),
                                           new BugReportingProgressEventHandlers(),
                                           new BugReportingRadioButtonEventHandlers(),
                                           new BugReportingTabsEventHandlers(),
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

            var bugReportingEnsureExtensions = new BugReportingEnsureExtensions();
            bugReportingEnsureExtensions.SubscribeToAll();

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
