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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop
{
    public class ComboBox : Element, IElementDisabled, IElementInnerText
    {
        public static Action<ComboBox> OverrideHoverGlobally;
        public static Action<ComboBox, string> OverrideSelectByTextGlobally;
        public static Func<ComboBox, bool> OverrideIsDisabledGlobally;
        public static Func<ComboBox, string> OverrideInnerTextGlobally;

        public static Action<ComboBox> OverrideHoverLocally;
        public static Action<ComboBox, string> OverrideSelectByTextLocally;
        public static Func<ComboBox, bool> OverrideIsDisabledLocally;
        public static Func<ComboBox, string> OverrideInnerTextLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Selecting;
        public static event EventHandler<ElementActionEventArgs> Selected;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideSelectByTextLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideInnerTextLocally = null;
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        public void SelectByText(string text)
        {
            var action = InitializeAction(this, OverrideSelectByTextGlobally, OverrideSelectByTextLocally, DefaultSelectByText);
            action(text);
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
            Selecting?.Invoke(this, new ElementActionEventArgs(comboBox, value));

            if (WrappedElement.Text != value)
            {
                WrappedElement.SendKeys(value);
            }

            Selected?.Invoke(this, new ElementActionEventArgs(comboBox, value));
        }

        protected virtual string DefaultInnerText(TextField textField) => base.DefaultInnerText(textField);

        protected virtual void DefaultHover(ComboBox comboBox) => DefaultHover(comboBox, Hovering, Hovered);
    }
}