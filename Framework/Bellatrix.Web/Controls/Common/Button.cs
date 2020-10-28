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
using Bellatrix.Web.Contracts;
using Bellatrix.Web.Events;

namespace Bellatrix.Web
{
    public class Button : Element, IElementValue, IElementDisabled, IElementInnerText
    {
        public static Action<Button> OverrideClickGlobally;
        public static Action<Button> OverrideHoverGlobally;
        public static Func<Button, string> OverrideInnerTextGlobally;
        public static Func<Button, bool> OverrideIsDisabledGlobally;
        public static Func<Button, string> OverrideValueGlobally;

        public static Action<Button> OverrideClickLocally;
        public static Action<Button> OverrideHoverLocally;
        public static Func<Button, string> OverrideInnerTextLocally;
        public static Func<Button, bool> OverrideIsDisabledLocally;
        public static Func<Button, string> OverrideValueLocally;

        public static event EventHandler<ElementActionEventArgs> Clicking;
        public static event EventHandler<ElementActionEventArgs> Clicked;
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;

        public static new void ClearLocalOverrides()
        {
            OverrideClickLocally = null;
            OverrideHoverLocally = null;
            OverrideInnerTextLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
        }

        public override Type ElementType => GetType();

        public void Click()
        {
            var action = InitializeAction(this, OverrideClickGlobally, OverrideClickLocally, DefaultClick);
            action();
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerText
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerTextGlobally, OverrideInnerTextLocally, DefaultInnerText);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Value
        {
            get
            {
                var action = InitializeAction(this, OverrideValueGlobally, OverrideValueLocally, DefaultGetValue);
                return action();
            }
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

        protected virtual void DefaultHover(Button button) => DefaultHover(button, Hovering, Hovered);

        protected virtual string DefaultInnerText(Button button) => base.DefaultInnerText(button);

        protected virtual bool DefaultIsDisabled(Button button) => base.DefaultIsDisabled(button);

        protected virtual string DefaultGetValue(Button button) => base.DefaultGetValue(button);
    }
}