// <copyright file="ComboBox.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Events;

namespace Bellatrix.Mobile.Android
{
    public class ComboBox : Element, IElementDisabled, IElementText
    {
        public static Action<ComboBox, string> OverrideSelectByTextGlobally;
        public static Func<ComboBox, bool> OverrideIsDisabledGlobally;
        public static Func<ComboBox, string> OverrideGetTextGlobally;

        public static Action<ComboBox, string> OverrideSelectByTextLocally;
        public static Func<ComboBox, bool> OverrideIsDisabledLocally;
        public static Func<ComboBox, string> OverrideGetTextLocally;

        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Selecting;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Selected;

        public static new void ClearLocalOverrides()
        {
            OverrideSelectByTextLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideGetTextLocally = null;
        }

        public void SelectByText(string text)
        {
            var action = InitializeAction(this, OverrideSelectByTextGlobally, OverrideSelectByTextLocally, DefaultSelectByText);
            action(text);
        }

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultGetText);
            return action();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
                return action();
            }
        }

        protected virtual void DefaultSelectByText(ComboBox comboBox, string value)
        {
            Selecting?.Invoke(this, new ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>(comboBox, value));

            if (WrappedElement.Text != value)
            {
                WrappedElement.Click();
                var elementCreateService = ServicesCollection.Current.Resolve<ElementCreateService>();
                var innerElementToClick = elementCreateService.CreateByTextContaining<RadioButton>(value);
                innerElementToClick.Click();
            }

            Selected?.Invoke(this, new ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>(comboBox, value));
        }

        protected virtual string DefaultGetText(ComboBox comboBox)
        {
            var result = base.DefaultGetText(comboBox);
            if (string.IsNullOrEmpty(result))
            {
                var textField = comboBox.CreateByClass<TextField>("android.widget.TextView");
                result = textField.GetText();
            }

            return result;
        }
    }
}