// <copyright file="TextArea.cs" company="Automate The Planet Ltd.">
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
    public class TextArea : Element, IElementInnerText, IElementText, IElementDisabled
    {
        public static Action<TextArea> OverrideHoverGlobally;
        public static Func<TextArea, string> OverrideInnerTextGlobally;
        public static Func<TextArea, string> OverrideGetTextGlobally;
        public static Action<TextArea, string> OverrideSetTextGlobally;
        public static Func<TextArea, bool> OverrideIsDisabledGlobally;

        public static Action<TextArea> OverrideHoverLocally;
        public static Func<TextArea, string> OverrideInnerTextLocally;
        public static Func<TextArea, string> OverrideGetTextLocally;
        public static Action<TextArea, string> OverrideSetTextLocally;
        public static Func<TextArea, bool> OverrideIsDisabledLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingText;
        public static event EventHandler<ElementActionEventArgs> TextSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideInnerTextLocally = null;
            OverrideGetTextLocally = null;
            OverrideSetTextLocally = null;
            OverrideIsDisabledLocally = null;
        }

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultGetText);
            return action();
        }

        public void SetText(string value)
        {
            var action = InitializeAction(this, OverrideSetTextGlobally, OverrideSetTextLocally, DefaultSetText);
            action(value);
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
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
                return action();
            }
        }

        protected virtual void DefaultHover(TextArea textArea) => DefaultHover(textArea, Hovering, Hovered);

        protected virtual string DefaultInnerText(TextArea textArea) => base.DefaultInnerText(textArea);

        protected virtual string DefaultGetText(TextArea textArea) => base.DefaultGetText(textArea);

        protected virtual void DefaultSetText(TextArea textArea, string value) => DefaultSetText(textArea, SettingText, TextSet, value);

        protected virtual bool DefaultIsDisabled(TextArea textArea) => base.DefaultIsDisabled(textArea);
    }
}