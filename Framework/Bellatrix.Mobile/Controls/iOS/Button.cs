// <copyright file="Button.cs" company="Automate The Planet Ltd.">
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

namespace Bellatrix.Mobile.IOS
{
    public class Button : Element, IElementDisabled, IElementText
    {
        public static Action<Button> OverrideClickGlobally;
        public static Func<Button, string> OverrideGetTextGlobally;
        public static Func<Button, bool> OverrideIsDisabledGlobally;

        public static Action<Button> OverrideClickLocally;
        public static Func<Button, string> OverrideGetTextLocally;
        public static Func<Button, bool> OverrideIsDisabledLocally;

        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.iOS.IOSElement>> Clicking;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.iOS.IOSElement>> Clicked;

        public static new void ClearLocalOverrides()
        {
            OverrideClickLocally = null;
            OverrideGetTextLocally = null;
            OverrideIsDisabledLocally = null;
        }

        public void Click()
        {
            var action = InitializeAction(this, OverrideClickGlobally, OverrideClickLocally, DefaultClick);
            action();
        }

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultText);
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

        protected virtual void DefaultClick(Button button) => DefaultClick(button, Clicking, Clicked);

        protected virtual string DefaultText(Button button) => DefaultGetText(button);

        protected virtual bool DefaultIsDisabled(Button button) => base.DefaultIsDisabled(button);
    }
}