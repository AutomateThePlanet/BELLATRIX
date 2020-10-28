// <copyright file="Week.cs" company="Automate The Planet Ltd.">
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
    public class Week : Element, IElementDisabled, IElementValue, IElementWeek, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxText, IElementMinText, IElementStep
    {
        public static Action<Week> OverrideHoverGlobally;
        public static Func<Week, bool> OverrideIsDisabledGlobally;
        public static Func<Week, string> OverrideValueGlobally;
        public static Func<Week, string> OverrideGetWeekGlobally;
        public static Action<Week, int, int> OverrideSetWeekGlobally;
        public static Func<Week, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Week, bool> OverrideIsReadonlyGlobally;
        public static Func<Week, bool> OverrideIsRequiredGlobally;
        public static Func<Week, string> OverrideMaxGlobally;
        public static Func<Week, string> OverrideMinGlobally;
        public static Func<Week, int?> OverrideStepGlobally;

        public static Action<Week> OverrideHoverLocally;
        public static Func<Week, bool> OverrideIsDisabledLocally;
        public static Func<Week, string> OverrideValueLocally;
        public static Func<Week, string> OverrideGetWeekLocally;
        public static Action<Week, int, int> OverrideSetWeekLocally;
        public static Func<Week, bool> OverrideIsAutoCompleteLocally;
        public static Func<Week, bool> OverrideIsReadonlyLocally;
        public static Func<Week, bool> OverrideIsRequiredLocally;
        public static Func<Week, string> OverrideMaxLocally;
        public static Func<Week, string> OverrideMinLocally;
        public static Func<Week, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingWeek;
        public static event EventHandler<ElementActionEventArgs> WeekSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetWeekLocally = null;
            OverrideSetWeekLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public override Type ElementType => GetType();

        public string GetWeek()
        {
            var action = InitializeAction(this, OverrideGetWeekGlobally, OverrideGetWeekLocally, DefaultGetWeek);
            return action();
        }

        public void SetWeek(int year, int weekNumber)
        {
            var action = InitializeAction(this, OverrideSetWeekGlobally, OverrideSetWeekLocally, DefaultSetWeek);
            action(year, weekNumber);
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

        protected virtual bool DefaultGetAutoComplete(Week week) => base.DefaultGetAutoComplete(week);

        protected virtual bool DefaultGetReadonly(Week week) => base.DefaultGetReadonly(week);

        protected virtual bool DefaultGetRequired(Week week) => base.DefaultGetRequired(week);

        protected virtual string DefaultGetMax(Week week) => DefaultGetMaxAsString(week);

        protected virtual string DefaultGetMin(Week week) => DefaultGetMinAsString(week);

        protected virtual int? DefaultGetStep(Week week) => base.DefaultGetStep(week);

        protected virtual void DefaultHover(Week week) => DefaultHover(week, Hovering, Hovered);

        protected virtual string DefaultGetValue(Week week) => base.DefaultGetValue(week);

        protected virtual bool DefaultIsDisabled(Week week) => base.DefaultIsDisabled(week);

        protected virtual string DefaultGetWeek(Week week) => base.DefaultGetValue(week);

        protected virtual void DefaultSetWeek(Week week, int year, int weekNumber)
        {
            if (weekNumber <= 0 || weekNumber > 52)
            {
                throw new ArgumentException($"The week number should be between 0 and 53 but you specified: {weekNumber}");
            }

            if (year <= 0)
            {
                throw new ArgumentException($"The year should be a positive number but you specified: {year}");
            }

            string valueToBeSet = weekNumber < 10 ? $"{year}-W0{weekNumber}" : $"{year}-W{weekNumber}";
            DefaultSetValue(week, SettingWeek, WeekSet, valueToBeSet);
        }
    }
}