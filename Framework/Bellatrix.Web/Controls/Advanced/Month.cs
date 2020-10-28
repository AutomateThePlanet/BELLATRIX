// <copyright file="Month.cs" company="Automate The Planet Ltd.">
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
    public class Month : Element, IElementDisabled, IElementValue, IElementMonth, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxText, IElementMinText, IElementStep
    {
        public static Action<Month> OverrideHoverGlobally;
        public static Func<Month, bool> OverrideIsDisabledGlobally;
        public static Func<Month, string> OverrideValueGlobally;
        public static Func<Month, string> OverrideGetMonthGlobally;
        public static Action<Month, int, int> OverrideSetMonthGlobally;
        public static Func<Month, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Month, bool> OverrideIsReadonlyGlobally;
        public static Func<Month, bool> OverrideIsRequiredGlobally;
        public static Func<Month, string> OverrideMaxGlobally;
        public static Func<Month, string> OverrideMinGlobally;
        public static Func<Month, int?> OverrideStepGlobally;

        public static Action<Month> OverrideHoverLocally;
        public static Func<Month, bool> OverrideIsDisabledLocally;
        public static Func<Month, string> OverrideValueLocally;
        public static Func<Month, string> OverrideGetMonthLocally;
        public static Action<Month, int, int> OverrideSetMonthLocally;
        public static Func<Month, bool> OverrideIsAutoCompleteLocally;
        public static Func<Month, bool> OverrideIsReadonlyLocally;
        public static Func<Month, bool> OverrideIsRequiredLocally;
        public static Func<Month, string> OverrideMaxLocally;
        public static Func<Month, string> OverrideMinLocally;
        public static Func<Month, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingMonth;
        public static event EventHandler<ElementActionEventArgs> MonthSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetMonthLocally = null;
            OverrideSetMonthLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetMonth()
        {
            var action = InitializeAction(this, OverrideGetMonthGlobally, OverrideGetMonthLocally, DefaultGetMonth);
            return action();
        }

        public void SetMonth(int year, int monthNumber)
        {
            var action = InitializeAction(this, OverrideSetMonthGlobally, OverrideSetMonthLocally, DefaultSetMonth);
            action(year, monthNumber);
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

        protected virtual bool DefaultGetAutoComplete(Month month) => base.DefaultGetAutoComplete(month);

        protected virtual bool DefaultGetReadonly(Month month) => base.DefaultGetReadonly(month);

        protected virtual bool DefaultGetRequired(Month month) => base.DefaultGetRequired(month);

        protected virtual string DefaultGetMax(Month month) => DefaultGetMaxAsString(month);

        protected virtual string DefaultGetMin(Month month) => DefaultGetMinAsString(month);

        protected virtual int? DefaultGetStep(Month month) => base.DefaultGetStep(month);

        protected virtual void DefaultHover(Month month) => DefaultHover(month, Hovering, Hovered);

        protected virtual string DefaultGetValue(Month month) => base.DefaultGetValue(month);

        protected virtual bool DefaultIsDisabled(Month month) => base.DefaultIsDisabled(month);

        protected virtual string DefaultGetMonth(Month month) => base.DefaultGetValue(month);

        protected virtual void DefaultSetMonth(Month month, int year, int monthNumber)
        {
            if (monthNumber <= 0 || monthNumber > 12)
            {
                throw new ArgumentException($"The month number should be between 0 and 12 but you specified: {monthNumber}");
            }

            if (year <= 0)
            {
                throw new ArgumentException($"The year should be a positive number but you specified: {year}");
            }

            string valueToBeSet = monthNumber < 10 ? $"{year}-0{monthNumber}" : $"{year}-{monthNumber}";
            DefaultSetValue(month, SettingMonth, MonthSet, valueToBeSet);
        }
    }
}