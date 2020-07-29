// <copyright file="TextField.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public class TextField : Element, IElementDisabled, IElementText
    {
        public static Func<TextField, string> OverrideGetTextGlobally;
        public static Func<TextField, bool> OverrideIsDisabledGlobally;
        public static Action<TextField, string> OverrideSetTextGlobally;

        public static Func<TextField, string> OverrideGetTextLocally;
        public static Func<TextField, bool> OverrideIsDisabledLocally;
        public static Action<TextField, string> OverrideSetTextLocally;

        public static event EventHandler<ElementActionEventArgs<IOSElement>> SettingText;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> TextSet;

        public static new void ClearLocalOverrides()
        {
            OverrideGetTextLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideSetTextLocally = null;
        }

        public void SetText(string value)
        {
            var action = InitializeAction(this, OverrideSetTextGlobally, OverrideSetTextLocally, DefaultSetText);
            action(value);
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

        protected virtual string DefaultGetText(TextField textField)
        {
            string textValue = DefaultGetValue(textField);

            return textValue ?? string.Empty;
        }

        protected virtual bool DefaultIsDisabled(TextField textField) => base.DefaultIsDisabled(textField);

        protected virtual void DefaultSetText(TextField textField, string value) => DefaultSetValue(textField, SettingText, TextSet, value);
    }
}