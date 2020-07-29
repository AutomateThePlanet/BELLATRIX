// <copyright file="RadioButton.cs" company="Automate The Planet Ltd.">
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
  public class RadioButton : Element, IElementDisabled, IElementChecked, IElementText
    {
        public static Action<RadioButton> OverrideClickGlobally;
        public static Func<RadioButton, bool> OverrideIsDisabledGlobally;
        public static Func<RadioButton, bool> OverrideIsCheckedGlobally;
        public static Func<RadioButton, string> OverrideGetTextGlobally;

        public static Action<RadioButton> OverrideClickLocally;
        public static Func<RadioButton, bool> OverrideIsDisabledLocally;
        public static Func<RadioButton, bool> OverrideIsCheckedLocally;
        public static Func<RadioButton, string> OverrideGetTextLocally;

        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Clicking;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Clicked;

        public static new void ClearLocalOverrides()
        {
            OverrideClickLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideIsCheckedLocally = null;
            OverrideGetTextLocally = null;
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

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsChecked
        {
            get
            {
                var action = InitializeAction(this, OverrideIsCheckedGlobally, OverrideIsCheckedLocally, DefaultIsChecked);
                return action();
            }
        }

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultText);
            return action();
        }

        public void Click()
        {
            var action = InitializeAction(this, OverrideClickGlobally, OverrideClickLocally, DefaultClick);
            action();
        }

        protected virtual void DefaultClick(RadioButton button) => DefaultClick(button, Clicking, Clicked);

        protected virtual bool DefaultIsDisabled(RadioButton button) => base.DefaultIsDisabled(button);

        protected virtual bool DefaultIsChecked(RadioButton button) => base.DefaultIsChecked(button);

        protected virtual string DefaultText(RadioButton button) => DefaultGetText(button);
    }
}