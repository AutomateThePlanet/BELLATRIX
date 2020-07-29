// <copyright file="CheckBox.cs" company="Automate The Planet Ltd.">
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
    public class CheckBox : Element, IElementDisabled, IElementChecked, IElementValue
    {
        public static Action<CheckBox> OverrideFocusGlobally;
        public static Action<CheckBox> OverrideHoverGlobally;
        public static Func<CheckBox, bool> OverrideIsDisabledGlobally;
        public static Func<CheckBox, bool> OverrideIsCheckedGlobally;
        public static Action<CheckBox, bool> OverrideCheckGlobally;
        public static Action<CheckBox> OverrideUncheckGlobally;
        public static Func<CheckBox, string> OverrideValueGlobally;

        public static Action<CheckBox> OverrideFocusLocally;
        public static Action<CheckBox> OverrideHoverLocally;
        public static Func<CheckBox, bool> OverrideIsDisabledLocally;
        public static Func<CheckBox, bool> OverrideIsCheckedLocally;
        public static Action<CheckBox, bool> OverrideCheckLocally;
        public static Action<CheckBox> OverrideUncheckLocally;
        public static Func<CheckBox, string> OverrideValueLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> Checking;
        public static event EventHandler<ElementActionEventArgs> Checked;
        public static event EventHandler<ElementActionEventArgs> Unchecking;
        public static event EventHandler<ElementActionEventArgs> Unchecked;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideIsCheckedLocally = null;
            OverrideCheckLocally = null;
            OverrideUncheckLocally = null;
            OverrideValueLocally = null;
        }

        public override Type ElementType => GetType();

        public void Check(bool isChecked = true)
        {
            var action = InitializeAction(this, OverrideCheckGlobally, OverrideCheckLocally, DefaultCheck);
            action(isChecked);
        }

        public void Uncheck()
        {
            var action = InitializeAction(this, OverrideUncheckGlobally, OverrideUncheckLocally, DefaultUncheck);
            action();
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        public void Focus()
        {
            var action = InitializeAction(this, OverrideFocusGlobally, OverrideFocusLocally, DefaultFocus);
            action();
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
        public string Value
        {
            get
            {
                var action = InitializeAction(this, OverrideValueGlobally, OverrideValueLocally, DefaultGetValue);
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

        protected virtual void DefaultCheck(CheckBox checkBox, bool isChecked = true)
        {
            if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
            {
                DefaultClick(checkBox, Checking, Checked);
            }
        }

        protected virtual void DefaultUncheck(CheckBox checkBox)
        {
            if (WrappedElement.Selected)
            {
                DefaultClick(checkBox, Unchecking, Unchecked);
            }
        }

        protected virtual void DefaultFocus(CheckBox checkBox) => DefaultFocus(checkBox, Focusing, Focused);

        protected virtual void DefaultHover(CheckBox checkBox) => DefaultHover(checkBox, Hovering, Hovered);

        protected virtual string DefaultGetValue(CheckBox checkBox) => base.DefaultGetValue(checkBox);

        protected virtual bool DefaultIsDisabled(CheckBox checkBox) => !WrappedElement.Enabled;

        protected virtual bool DefaultIsChecked(CheckBox checkBox) => WrappedElement.Selected;
    }
}