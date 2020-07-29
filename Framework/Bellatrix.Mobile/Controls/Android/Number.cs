// <copyright file="Number.cs" company="Automate The Planet Ltd.">
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
    public class Number : Element, IElementDisabled, IElementNumber
    {
        public static Func<Number, int> OverrideGetNumberGlobally;
        public static Func<Number, bool> OverrideIsDisabledGlobally;
        public static Action<Number, int> OverrideSetNumberGlobally;

        public static Func<Number, int> OverrideGetNumberLocally;
        public static Func<Number, bool> OverrideIsDisabledLocally;
        public static Action<Number, int> OverrideSetNumberLocally;

        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> SettingNumber;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> NumberSet;

        public static new void ClearLocalOverrides()
        {
            OverrideGetNumberLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideSetNumberLocally = null;
        }

        public void SetNumber(int value)
        {
            var action = InitializeAction(this, OverrideSetNumberGlobally, OverrideSetNumberLocally, DefaultSetText);
            action(value);
        }

        public int GetNumber()
        {
            var action = InitializeAction(this, OverrideGetNumberGlobally, OverrideGetNumberLocally, DefaultGetNumber);
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

        protected virtual int DefaultGetNumber(Number number)
        {
            var resultText = DefaultGetText(number);
            if (string.IsNullOrEmpty(resultText))
            {
                var textField = number.CreateByClass<TextField>("android.widget.EditText");
                resultText = textField.GetText();
            }

            int.TryParse(resultText, out var result);

            return result;
        }

        protected virtual bool DefaultIsDisabled(Number number) => base.DefaultIsDisabled(number);

        protected virtual void DefaultSetText(Number number, int value) => DefaultSetText(number, SettingNumber, NumberSet, value.ToString());
    }
}