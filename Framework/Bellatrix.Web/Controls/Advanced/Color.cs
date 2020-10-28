// <copyright file="Color.cs" company="Automate The Planet Ltd.">
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
    public class Color : Element, IElementDisabled, IElementValue, IElementColor, IElementList, IElementAutoComplete, IElementRequired
    {
        public static Action<Color> OverrideHoverGlobally;
        public static Func<Color, bool> OverrideIsDisabledGlobally;
        public static Func<Color, string> OverrideValueGlobally;
        public static Func<Color, string> OverrideGetColorGlobally;
        public static Func<Color, string> OverrideListGlobally;
        public static Action<Color, string> OverrideSetColorGlobally;
        public static Func<Color, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Color, bool> OverrideIsRequiredGlobally;

        public static Action<Color> OverrideHoverLocally;
        public static Func<Color, bool> OverrideIsDisabledLocally;
        public static Func<Color, string> OverrideValueLocally;
        public static Func<Color, string> OverrideGetColorLocally;
        public static Func<Color, string> OverrideListLocally;
        public static Action<Color, string> OverrideSetColorLocally;
        public static Func<Color, bool> OverrideIsAutoCompleteLocally;
        public static Func<Color, bool> OverrideIsRequiredLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingColor;
        public static event EventHandler<ElementActionEventArgs> ColorSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetColorLocally = null;
            OverrideListLocally = null;
            OverrideSetColorLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsRequiredLocally = null;
        }

        public override Type ElementType => GetType();

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
            action();
        }

        public string GetColor()
        {
            var action = InitializeAction(this, OverrideGetColorGlobally, OverrideGetColorLocally, DefaultGetColor);
            return action();
        }

        public void SetColor(string value)
        {
            var action = InitializeAction(this, OverrideSetColorGlobally, OverrideSetColorLocally, DefaultSetColor);
            action(value);
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
        public bool IsAutoComplete
        {
            get
            {
                var action = InitializeAction(this, OverrideIsAutoCompleteGlobally, OverrideIsAutoCompleteLocally, DefaultGetAutoComplete);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsRequired
        {
            get
            {
                var action = InitializeAction(this, OverrideIsRequiredGlobally, OverrideIsRequiredLocally, DefaultGetRequired);
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
        public string List
        {
            get
            {
                var action = InitializeAction(this, OverrideListGlobally, OverrideListLocally, DefaultGetList);
                return action();
            }
        }

        protected virtual bool DefaultGetAutoComplete(Color color) => base.DefaultGetAutoComplete(color);

        protected virtual bool DefaultGetRequired(Color color) => base.DefaultGetRequired(color);

        protected virtual string DefaultGetList(Color color) => base.DefaultGetList(color);

        protected virtual void DefaultHover(Color color) => DefaultHover(color, Hovering, Hovered);

        protected virtual string DefaultGetValue(Color color) => base.DefaultGetValue(color);

        protected virtual bool DefaultIsDisabled(Color color) => base.DefaultIsDisabled(color);

        protected virtual string DefaultGetColor(Color color) => base.DefaultGetValue(color);

        protected virtual void DefaultSetColor(Color color, string valueToBeSet) => DefaultSetValue(color, SettingColor, ColorSet, valueToBeSet);
    }
}