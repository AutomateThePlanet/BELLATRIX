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
using Bellatrix.Application;
using Bellatrix.Assertions;
using Bellatrix.SpecFlow.NUnit;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;

namespace Bellatrix.Web.SpecFlow
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseNUnitSettings(this BaseApp baseApp)
        {
            if (ServicesCollection.Current == null)
            {
                throw new DefaultContainerNotConfiguredException("The default container for the App is not configured.\n The first method you need to call is 'App.Use{IoCFramework}Container();'\nFor example, if you have installed Unity IoC projects call 'App.UseUnityContainer();'.");
            }

            ServicesCollection.Current.RegisterType<IAssert, NUnitAssert>();
            ServicesCollection.Current.RegisterType<ICollectionAssert, NUnitCollectionAssert>();
            return baseApp;
        }

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
    }
}