// <copyright file="ImageButton.cs" company="Automate The Planet Ltd.">
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
    public class ImageButton : Element, IElementDisabled, IElementText
    {
        public static Action<ImageButton> OverrideClickGlobally;
        public static Func<ImageButton, string> OverrideGetTextGlobally;
        public static Func<ImageButton, bool> OverrideIsDisabledGlobally;

        public static Action<ImageButton> OverrideClickLocally;
        public static Func<ImageButton, string> OverrideGetTextLocally;
        public static Func<ImageButton, bool> OverrideIsDisabledLocally;

        public static event EventHandler<ElementActionEventArgs<IOSElement>> Clicking;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> Clicked;

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

        protected virtual void DefaultClick(ImageButton button) => DefaultClick(button, Clicking, Clicked);

        protected virtual string DefaultText(ImageButton button) => DefaultGetText(button);

        protected virtual bool DefaultIsDisabled(ImageButton button) => base.DefaultIsDisabled(button);
    }
}