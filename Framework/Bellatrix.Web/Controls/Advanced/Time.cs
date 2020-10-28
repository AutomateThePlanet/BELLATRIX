// <copyright file="Time.cs" company="Automate The Planet Ltd.">
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
    public class Time : Element, IElementDisabled, IElementValue, IElementTime, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxText, IElementMinText, IElementStep
    {
        public static Action<Time> OverrideHoverGlobally;
        public static Func<Time, bool> OverrideIsDisabledGlobally;
        public static Func<Time, string> OverrideValueGlobally;
        public static Func<Time, string> OverrideGetTimeGlobally;
        public static Action<Time, int, int> OverrideSetTimeGlobally;
        public static Func<Time, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Time, bool> OverrideIsReadonlyGlobally;
        public static Func<Time, bool> OverrideIsRequiredGlobally;
        public static Func<Time, string> OverrideMaxGlobally;
        public static Func<Time, string> OverrideMinGlobally;
        public static Func<Time, int?> OverrideStepGlobally;

        public static Action<Time> OverrideHoverLocally;
        public static Func<Time, bool> OverrideIsDisabledLocally;
        public static Func<Time, string> OverrideValueLocally;
        public static Func<Time, string> OverrideGetTimeLocally;
        public static Action<Time, int, int> OverrideSetTimeLocally;
        public static Func<Time, bool> OverrideIsAutoCompleteLocally;
        public static Func<Time, bool> OverrideIsReadonlyLocally;
        public static Func<Time, bool> OverrideIsRequiredLocally;
        public static Func<Time, string> OverrideMaxLocally;
        public static Func<Time, string> OverrideMinLocally;
        public static Func<Time, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingTime;
        public static event EventHandler<ElementActionEventArgs> TimeSet;

        public static new void ClearLocalOverrides()
        {
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

        public void SetTime(int hours, int minutes)
        {
            var action = InitializeAction(this, OverrideSetTimeGlobally, OverrideSetTimeLocally, DefaultSetTime);
            action(hours, minutes);
        }

        public void Hover()
        {
            var action = InitializeAction(this, OverrideHoverGlobally, OverrideHoverLocally, DefaultHover);
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

        protected virtual bool DefaultGetAutoComplete(Time time) => base.DefaultGetAutoComplete(time);

        protected virtual bool DefaultGetReadonly(Time time) => base.DefaultGetReadonly(time);

        protected virtual bool DefaultGetRequired(Time time) => base.DefaultGetRequired(time);

        protected virtual string DefaultGetMax(Time time) => DefaultGetMaxAsString(time);

        protected virtual string DefaultGetMin(Time time) => DefaultGetMinAsString(time);

        protected virtual int? DefaultGetStep(Time time) => base.DefaultGetStep(time);

        protected virtual void DefaultHover(Time time) => DefaultHover(time, Hovering, Hovered);

        protected virtual string DefaultGetValue(Time time) => base.DefaultGetValue(time);

        protected virtual bool DefaultIsDisabled(Time time) => base.DefaultIsDisabled(time);

        protected virtual string DefaultGetTime(Time time) => base.DefaultGetValue(time);

        protected virtual void DefaultSetTime(Time time, int hours, int minutes) => DefaultSetValue(time, SettingTime, TimeSet, $"{hours}:{minutes}:00");
    }
}