﻿// <copyright file="AppRegistrationExtensions.cs" company="Automate The Planet Ltd.">
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

        public static BaseApp UseValidateExtensionsBddLogging(this BaseApp baseApp)
        {
            var bddLoggingValidateExtensions = new BDDLoggingValidateExtensionsService();
            bddLoggingValidateExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseValidateExtensionsDynamicTestCases(this BaseApp baseApp)
        {
            var dynamicTestCasesValidateExtensions = new DynamicTestCasesValidateExtensionsEventHandlers();
            dynamicTestCasesValidateExtensions.SubscribeToAll();

            return baseApp;
        }

        public static BaseApp UseValidateExtensionsBugReporting(this BaseApp baseApp)
        {
            var bugReportingValidateExtensions = new BugReportingValidateExtensionsEventHandlers();
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

        public static BaseApp UseElementsBddLogging(this BaseApp baseApp)
        {
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BDDLoggingTextFieldEventHandlers(),
                                           new BDDLoggingDateEventHandlers(),
                                           new BDDLoggingColorEventHandlers(),
                                           new BDDLoggingCheckboxEventHandlers(),
                                           new BDDLoggingDateTimeLocalEventHandlers(),
                                           new BDDLoggingElementEventHandlers(),
                                           new BDDLoggingEmailEventHandlers(),
                                           new BDDLoggingInputFileEventHandlers(),
                                           new BDDLoggingMonthEventHandlers(),
                                           new BDDLoggingNumberEventHandlers(),
                                           new BDDLoggingPasswordEventHandlers(),
                                           new BDDLoggingPhoneEventHandlers(),
                                           new BDDLoggingRangeEventHandlers(),
                                           new BDDLoggingSearchEventHandlers(),
                                           new BDDLoggingSelectEventHandlers(),
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
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new DynamicTestCasesTextFieldEventHandlers(),
                                           new DynamicTestCasesDateEventHandlers(),
                                           new DynamicTestCasesColorEventHandlers(),
                                           new DynamicTestCasesCheckboxEventHandlers(),
                                           new DynamicTestCasesDateTimeLocalEventHandlers(),
                                           new DynamicTestCasesElementEventHandlers(),
                                           new DynamicTestCasesEmailEventHandlers(),
                                           new DynamicTestCasesInputFileEventHandlers(),
                                           new DynamicTestCasesMonthEventHandlers(),
                                           new DynamicTestCasesNumberEventHandlers(),
                                           new DynamicTestCasesPasswordEventHandlers(),
                                           new DynamicTestCasesPhoneEventHandlers(),
                                           new DynamicTestCasesRangeEventHandlers(),
                                           new DynamicTestCasesSearchEventHandlers(),
                                           new DynamicTestCasesSelectEventHandlers(),
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
            var elementEventHandlers = new List<ElementEventHandlers>()
                                       {
                                           new BugReportingTextFieldEventHandlers(),
                                           new BugReportingDateEventHandlers(),
                                           new BugReportingColorEventHandlers(),
                                           new BugReportingCheckboxEventHandlers(),
                                           new BugReportingDateTimeLocalEventHandlers(),
                                           new BugReportingElementEventHandlers(),
                                           new BugReportingEmailEventHandlers(),
                                           new BugReportingInputFileEventHandlers(),
                                           new BugReportingNumberEventHandlers(),
                                           new BugReportingPasswordEventHandlers(),
                                           new BugReportingPhoneEventHandlers(),
                                           new BugReportingRangeEventHandlers(),
                                           new BugReportingSearchEventHandlers(),
                                           new BugReportingSelectEventHandlers(),
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
            if (ConfigurationService.GetSection<WebSettings>() == null)
            {
                throw new ArgumentException("Could not load web settings section from testFrameworkSettings.json");
            }

            if (ConfigurationService.GetSection<WebSettings>().ShouldHighlightElements)
            {
                var highlightElementEventHandler = new HighlightElementEventHandlers();
                highlightElementEventHandler.SubscribeToAll();
            }

            return baseApp;
        }
    }
}