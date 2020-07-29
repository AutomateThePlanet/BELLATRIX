// <copyright file="DateTimeLocal.cs" company="Automate The Planet Ltd.">
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
    public class DateTimeLocal : Element, IElementDisabled, IElementValue, IElementTime, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxText, IElementMinText, IElementStep
    {
        public static Action<DateTimeLocal> OverrideFocusGlobally;
        public static Action<DateTimeLocal> OverrideHoverGlobally;
        public static Func<DateTimeLocal, bool> OverrideIsDisabledGlobally;
        public static Func<DateTimeLocal, string> OverrideValueGlobally;
        public static Func<DateTimeLocal, string> OverrideGetTimeGlobally;
        public static Action<DateTimeLocal, DateTime> OverrideSetTimeGlobally;
        public static Func<DateTimeLocal, bool> OverrideIsAutoCompleteGlobally;
        public static Func<DateTimeLocal, bool> OverrideIsReadonlyGlobally;
        public static Func<DateTimeLocal, bool> OverrideIsRequiredGlobally;
        public static Func<DateTimeLocal, string> OverrideMaxGlobally;
        public static Func<DateTimeLocal, string> OverrideMinGlobally;
        public static Func<DateTimeLocal, int?> OverrideStepGlobally;

        public static Action<DateTimeLocal> OverrideFocusLocally;
        public static Action<DateTimeLocal> OverrideHoverLocally;
        public static Func<DateTimeLocal, bool> OverrideIsDisabledLocally;
        public static Func<DateTimeLocal, string> OverrideValueLocally;
        public static Func<DateTimeLocal, string> OverrideGetTimeLocally;
        public static Action<DateTimeLocal, DateTime> OverrideSetTimeLocally;
        public static Func<DateTimeLocal, bool> OverrideIsAutoCompleteLocally;
        public static Func<DateTimeLocal, bool> OverrideIsReadonlyLocally;
        public static Func<DateTimeLocal, bool> OverrideIsRequiredLocally;
        public static Func<DateTimeLocal, string> OverrideMaxLocally;
        public static Func<DateTimeLocal, string> OverrideMinLocally;
        public static Func<DateTimeLocal, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingTime;
        public static event EventHandler<ElementActionEventArgs> TimeSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetTimeLocally = null;
            OverrideSetTimeLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetTime()
        {
            var action = InitializeAction(this, OverrideGetTimeGlobally, OverrideGetTimeLocally, DefaultGetTime);
            return action();
        }

        public void SetTime(DateTime time)
        {
            var action = InitializeAction(this, OverrideSetTimeGlobally, OverrideSetTimeLocally, DefaultSetTime);
            action(time);
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
        public bool IsAutoComplete
        {
            get
            {
                var action = InitializeAction(this, OverrideIsAutoCompleteGlobally, OverrideIsAutoCompleteLocally, DefaultGetAutoComplete);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsReadonly
        {
            get
            {
                var action = InitializeAction(this, OverrideIsReadonlyGlobally, OverrideIsReadonlyLocally, DefaultGetReadonly);
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
        public string Max
        {
            get
            {
                var action = InitializeAction(this, OverrideMaxGlobally, OverrideMaxLocally, DefaultGetMax);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Min
        {
            get
            {
                var action = InitializeAction(this, OverrideMinGlobally, OverrideMinLocally, DefaultGetMin);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Step
        {
            get
            {
                var action = InitializeAction(this, OverrideStepGlobally, OverrideStepLocally, DefaultGetStep);
                return action();
            }
        }

        protected virtual bool DefaultGetAutoComplete(DateTimeLocal time) => base.DefaultGetAutoComplete(time);

        protected virtual bool DefaultGetReadonly(DateTimeLocal time) => base.DefaultGetReadonly(time);

        protected virtual bool DefaultGetRequired(DateTimeLocal time) => base.DefaultGetRequired(time);

        protected virtual string DefaultGetMax(DateTimeLocal time) => DefaultGetMaxAsString(time);

        protected virtual string DefaultGetMin(DateTimeLocal time) => DefaultGetMinAsString(time);

        protected virtual int? DefaultGetStep(DateTimeLocal time) => base.DefaultGetStep(time);

        protected virtual void DefaultFocus(DateTimeLocal time) => DefaultFocus(time, Focusing, Focused);

        protected virtual void DefaultHover(DateTimeLocal time) => DefaultHover(time, Hovering, Hovered);

        protected virtual string DefaultGetValue(DateTimeLocal time) => base.DefaultGetValue(time);

        protected virtual bool DefaultIsDisabled(DateTimeLocal time) => base.DefaultIsDisabled(time);

        protected virtual string DefaultGetTime(DateTimeLocal time) => base.DefaultGetValue(time);

        protected virtual void DefaultSetTime(DateTimeLocal time, DateTime dateTime) => DefaultSetValue(time, SettingTime, TimeSet, $"{dateTime.Year}-{dateTime.Month}-{dateTime.Day}T{dateTime.Hour}:{dateTime.Minute}");
    }
}