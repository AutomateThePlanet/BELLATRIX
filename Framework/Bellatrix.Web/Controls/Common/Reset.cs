// <copyright file="Reset.cs" company="Automate The Planet Ltd.">
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
    public class Reset : Element, IElementDisabled, IElementInnerText, IElementValue
    {
        public static Action<Reset> OverrideClickGlobally;
        public static Action<Reset> OverrideHoverGlobally;
        public static Func<Reset, string> OverrideInnerTextGlobally;
        public static Func<Reset, bool> OverrideIsDisabledGlobally;
        public static Func<Reset, string> OverrideValueGlobally;

        public static Action<Reset> OverrideClickLocally;
        public static Action<Reset> OverrideHoverLocally;
        public static Func<Reset, string> OverrideInnerTextLocally;
        public static Func<Reset, bool> OverrideIsDisabledLocally;
        public static Func<Reset, string> OverrideValueLocally;

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

        protected virtual void DefaultClick(Reset button) => DefaultClick(button, Clicking, Clicked);

        protected virtual void DefaultHover(Reset button) => DefaultHover(button, Hovering, Hovered);

        protected virtual string DefaultInnerText(Reset button) => base.DefaultInnerText(button);

        protected virtual bool DefaultIsDisabled(Reset button) => base.DefaultIsDisabled(button);

        protected virtual string DefaultGetValue(Reset button) => base.DefaultGetValue(button);
    }
}