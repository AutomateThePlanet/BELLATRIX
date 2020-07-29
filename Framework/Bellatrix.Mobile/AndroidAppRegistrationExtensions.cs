// <copyright file="AndroidAppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.BddLogging.Android;
using Bellatrix.Mobile.BugReporting.Android;
using Bellatrix.Mobile.DynamicTestCases.Android;
using Bellatrix.Mobile.EventHandlers.Android;
using Bellatrix.Mobile.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots;
using Bellatrix.TestExecutionExtensions.Screenshots.Contracts;
using Bellatrix.TestWorkflowPlugins;

namespace Bellatrix.Mobile.Android
{
    public static class AndroidAppRegistrationExtensions
    {
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
                                           new BDDLoggingSwitchEventHandlers(),
                                           new BDDLoggingNumberEventHandlers(),
                                           new BDDLoggingSeekBarEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return androidApp;
        }

        public static BaseApp UseDynamicTestCases(this BaseApp androidApp)
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
                                           new DynamicTestCasesSwitchEventHandlers(),
                                           new DynamicTestCasesNumberEventHandlers(),
                                           new DynamicTestCasesSeekBarEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return androidApp;
        }

        public static BaseApp UseBugReporting(this BaseApp androidApp)
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
                                           new BugReportingSwitchEventHandlers(),
                                           new BugReportingNumberEventHandlers(),
                                           new BugReportingSeekBarEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return androidApp;
        }

        public static BaseApp UseAndroidDriverScreenshotsOnFail(this BaseApp baseApp)
        {
            baseApp.RegisterType<IScreenshotEngine, AndroidDriverScreenshotEngine>();
            baseApp.RegisterType<IScreenshotOutputProvider, ScreenshotOutputProvider>();
            baseApp.RegisterType<IScreenshotPluginProvider, ScreenshotPluginProvider>();
            baseApp.RegisterType<TestWorkflowPlugin, ScreenshotWorkflowPlugin>(Guid.NewGuid().ToString());

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

            var bddLoggingEnsureExtensions = new DynamicTestCasesEnsureExtensions();
            bddLoggingEnsureExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseEnsureExtensionsBugReporting(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bddLoggingEnsureExtensions = new BugReportingEnsureExtensions();
            bddLoggingEnsureExtensions.SubscribeToAll();

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
