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
using Bellatrix.SpecFlow.MSTest;
using Bellatrix.Web.Controls.Advanced.ControlDataHandlers;

namespace Bellatrix.Web.SpecFlow
{
    public static class AppRegistrationExtensions
    {
        public static BaseApp UseMsTestSettings(this BaseApp baseApp)
        {
            ServicesCollection.Current.RegisterType<IAssert, MsTestAssert>();
            ServicesCollection.Current.RegisterType<ICollectionAssert, MsTestCollectionAssert>();
            return baseApp;
        }

        public static BaseApp UseControlDataHandlers(this BaseApp baseApp)
        {
            // Editable Control DataHandlers - need to be registered both as readonly and editable
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Date>, DateControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<DateTimeLocal>, DateTimeLocalControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Email>, EmailControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Month>, MonthControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Password>, PasswordControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Phone>, PhoneControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Range>, RangeControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Search>, SearchControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Time>, TimeControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Url>, UrlControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Week>, WeekControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<CheckBox>, CheckBoxControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<RadioButton>, RadioButtonControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<Select>, SelectControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<TextArea>, TextAreaControlDataHandler>();
            ServicesCollection.Current.RegisterType<IEditableControlDataHandler<TextField>, TextFieldControlDataHandler>();

            // Readonly Control DataHandlers - need to be registered only as readonly
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Number>, NumberControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Output>, OutputControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Anchor>, AnchorControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Button>, ButtonControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Div>, DivControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Image>, ImageControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Label>, LabelControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Reset>, ResetControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Span>, SpanControlDataHandler>();
            ServicesCollection.Current.RegisterType<IReadonlyControlDataHandler<Color>, ColorControlDataHandler>();
            return baseApp;
        }
    }
}
