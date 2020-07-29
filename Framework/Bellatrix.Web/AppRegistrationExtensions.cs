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
using Bellatrix.Layout;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;
using Bellatrix.Web.Controls.EventHandlers;
using Bellatrix.Web.EventHandlers.DynamicTestCases;
using Bellatrix.Web.Extensions.Controls.Controls.EventHandlers;

namespace Bellatrix.Web
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseControlDataHandlers(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            // Editable Control DataHandlers - need to be registered both as readonly and editable
            ServicesCollection.Current.RegisterType<IControlDataHandler<Date>, DateControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<DateTimeLocal>, DateTimeLocalControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Email>, EmailControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Month>, MonthControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Password>, PasswordControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Phone>, PhoneControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Range>, RangeControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Search>, SearchControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Time>, TimeControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Url>, UrlControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Week>, WeekControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<CheckBox>, CheckBoxControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<RadioButton>, RadioButtonControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Select>, SelectControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<TextArea>, TextAreaControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<TextField>, TextFieldControlDataHandler>();

            // Readonly Control DataHandlers - need to be registered only as readonly
            ServicesCollection.Current.RegisterType<IControlDataHandler<Number>, NumberControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Output>, OutputControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Anchor>, AnchorControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Button>, ButtonControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Div>, DivControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Image>, ImageControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Label>, LabelControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Reset>, ResetControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Span>, SpanControlDataHandler>();
            ServicesCollection.Current.RegisterType<IControlDataHandler<Color>, ColorControlDataHandler>();
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

            var dynamicTestCasesEnsureExtensions = new DynamicTestCasesEnsureExtensionsEventHandlers();
            dynamicTestCasesEnsureExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseEnsureExtensionsBugReporting(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var bugReportingEnsureExtensions = new BugReportingEnsureExtensionsEventHandlers();
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

        public static BaseApp UseElementsBddLogging(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException();
            }

            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BDDLoggingButtonEventHandlers(),
                                           new BDDLoggingAnchorEventHandlers(),
                                           new BDDLoggingTextFieldEventHandlers(),
                                           new BDDLoggingDateEventHandlers(),
                                           new BDDLoggingColorEventHandlers(),
                                           new BDDLoggingCheckboxEventHandlers(),
                                           new BDDLoggingDateTimeLocalEventHandlers(),
                                           new BDDLoggingDivEventHandlers(),
                                           new BDDLoggingElementEventHandlers(),
                                           new BDDLoggingEmailEventHandlers(),
                                           new BDDLoggingHeadingEventHandlers(),
                                           new BDDLoggingImageEventHandlers(),
                                           new BDDLoggingInputFileEventHandlers(),
                                           new BDDLoggingLabelEventHandlers(),
                                           new BDDLoggingMonthEventHandlers(),
                                           new BDDLoggingNumberEventHandlers(),
                                           new BDDLoggingOutputEventHandlers(),
                                           new BDDLoggingPasswordEventHandlers(),
                                           new BDDLoggingPhoneEventHandlers(),
                                           new BDDLoggingRadioButtonEventHandlers(),
                                           new BDDLoggingRangeEventHandlers(),
                                           new BDDLoggingResetEventHandlers(),
                                           new BDDLoggingSearchEventHandlers(),
                                           new BDDLoggingSelectEventHandlers(),
                                           new BDDLoggingSpanEventHandlers(),
                                           new BDDLoggingTextAreaEventHandlers(),
                                           new BDDLoggingTimeEventHandlers(),
                                           new BDDLoggingUrlEventHandlers(),
                                           new BDDLoggingWeekEventHandlers(),
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
                                           new DynamicTestCasesAnchorEventHandlers(),
                                           new DynamicTestCasesTextFieldEventHandlers(),
                                           new DynamicTestCasesDateEventHandlers(),
                                           new DynamicTestCasesColorEventHandlers(),
                                           new DynamicTestCasesCheckboxEventHandlers(),
                                           new DynamicTestCasesDateTimeLocalEventHandlers(),
                                           new DynamicTestCasesDivEventHandlers(),
                                           new DynamicTestCasesElementEventHandlers(),
                                           new DynamicTestCasesEmailEventHandlers(),
                                           new DynamicTestCasesHeadingEventHandlers(),
                                           new DynamicTestCasesImageEventHandlers(),
                                           new DynamicTestCasesInputFileEventHandlers(),
                                           new DynamicTestCasesLabelEventHandlers(),
                                           new DynamicTestCasesMonthEventHandlers(),
                                           new DynamicTestCasesNumberEventHandlers(),
                                           new DynamicTestCasesOutputEventHandlers(),
                                           new DynamicTestCasesPasswordEventHandlers(),
                                           new DynamicTestCasesPhoneEventHandlers(),
                                           new DynamicTestCasesRadioButtonEventHandlers(),
                                           new DynamicTestCasesRangeEventHandlers(),
                                           new DynamicTestCasesResetEventHandlers(),
                                           new DynamicTestCasesSearchEventHandlers(),
                                           new DynamicTestCasesSelectEventHandlers(),
                                           new DynamicTestCasesSpanEventHandlers(),
                                           new DynamicTestCasesTextAreaEventHandlers(),
                                           new DynamicTestCasesTimeEventHandlers(),
                                           new DynamicTestCasesUrlEventHandlers(),
                                           new DynamicTestCasesWeekEventHandlers(),
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
                                           new BugReportingAnchorEventHandlers(),
                                           new BugReportingTextFieldEventHandlers(),
                                           new BugReportingDateEventHandlers(),
                                           new BugReportingColorEventHandlers(),
                                           new BugReportingCheckboxEventHandlers(),
                                           new BugReportingDateTimeLocalEventHandlers(),
                                           new BugReportingDivEventHandlers(),
                                           new BugReportingElementEventHandlers(),
                                           new BugReportingEmailEventHandlers(),
                                           new BugReportingHeadingEventHandlers(),
                                           new BugReportingImageEventHandlers(),
                                           new BugReportingInputFileEventHandlers(),
                                           new BugReportingLabelEventHandlers(),
                                           new BugReportingMonthEventHandlers(),
                                           new BugReportingNumberEventHandlers(),
                                           new BugReportingOutputEventHandlers(),
                                           new BugReportingPasswordEventHandlers(),
                                           new BugReportingPhoneEventHandlers(),
                                           new BugReportingRadioButtonEventHandlers(),
                                           new BugReportingRangeEventHandlers(),
                                           new BugReportingResetEventHandlers(),
                                           new BugReportingSearchEventHandlers(),
                                           new BugReportingSelectEventHandlers(),
                                           new BugReportingSpanEventHandlers(),
                                           new BugReportingTextAreaEventHandlers(),
                                           new BugReportingTimeEventHandlers(),
                                           new BugReportingUrlEventHandlers(),
                                           new BugReportingWeekEventHandlers(),
                                       };
            foreach (var elementEventHandler in elementEventHandlers)
            {
                elementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }

        public static BaseApp UseHighlightElements(this BaseApp baseApp)
        {
            if (ConfigurationService.Instance.GetWebSettings() == null)
            {
                throw new ArgumentException("Could not load web settings section from testFrameworkSettings.json");
            }

            if (ConfigurationService.Instance.GetWebSettings().ShouldHighlightElements)
            {
                if (ServicesCollection.Current == null)
                {
                    throw new DefaultContainerNotConfiguredException();
                }

                var highlightElementEventHandler = new HighlightElementEventHandlers();
                highlightElementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }
    }
}