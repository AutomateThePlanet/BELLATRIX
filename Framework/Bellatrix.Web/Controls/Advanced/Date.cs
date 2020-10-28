// <copyright file="Date.cs" company="Automate The Planet Ltd.">
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
    public class Date : Element, IElementDisabled, IElementValue, IElementDate, IElementAutoComplete, IElementRequired, IElementReadonly, IElementMaxText, IElementMinText, IElementStep
    {
        public static Action<Date> OverrideHoverGlobally;
        public static Func<Date, bool> OverrideIsDisabledGlobally;
        public static Func<Date, string> OverrideValueGlobally;
        public static Func<Date, string> OverrideGetDateGlobally;
        public static Action<Date, int, int, int> OverrideSetDateGlobally;
        public static Func<Date, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Date, bool> OverrideIsReadonlyGlobally;
        public static Func<Date, bool> OverrideIsRequiredGlobally;
        public static Func<Date, string> OverrideMaxGlobally;
        public static Func<Date, string> OverrideMinGlobally;
        public static Func<Date, int?> OverrideStepGlobally;

        public static Action<Date> OverrideHoverLocally;
        public static Func<Date, bool> OverrideIsDisabledLocally;
        public static Func<Date, string> OverrideValueLocally;
        public static Func<Date, string> OverrideGetDateLocally;
        public static Action<Date, int, int, int> OverrideSetDateLocally;
        public static Func<Date, bool> OverrideIsAutoCompleteLocally;
        public static Func<Date, bool> OverrideIsReadonlyLocally;
        public static Func<Date, bool> OverrideIsRequiredLocally;
        public static Func<Date, string> OverrideMaxLocally;
        public static Func<Date, string> OverrideMinLocally;
        public static Func<Date, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingDate;
        public static event EventHandler<ElementActionEventArgs> DateSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetDateLocally = null;
            OverrideSetDateLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetDate()
        {
            var action = InitializeAction(this, OverrideGetDateGlobally, OverrideGetDateLocally, DefaultGetDate);
            return action();
        }

        public void SetDate(int year, int month, int day)
        {
            var action = InitializeAction(this, OverrideSetDateGlobally, OverrideSetDateLocally, DefaultSetDate);
            action(year, month, day);
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

        protected virtual bool DefaultGetAutoComplete(Date date) => base.DefaultGetAutoComplete(date);

        protected virtual bool DefaultGetReadonly(Date date) => base.DefaultGetReadonly(date);

        protected virtual bool DefaultGetRequired(Date date) => base.DefaultGetRequired(date);

        protected virtual string DefaultGetMax(Date date) => DefaultGetMaxAsString(date);

        protected virtual string DefaultGetMin(Date date) => DefaultGetMinAsString(date);

        protected virtual int? DefaultGetStep(Date date) => base.DefaultGetStep(date);

        protected virtual void DefaultHover(Date date) => DefaultHover(date, Hovering, Hovered);

        protected virtual string DefaultGetValue(Date date) => base.DefaultGetValue(date);

        protected virtual bool DefaultIsDisabled(Date date) => base.DefaultIsDisabled(date);

        protected virtual string DefaultGetDate(Date date) => base.DefaultGetValue(date);

        protected virtual void DefaultSetDate(Date date, int year, int month, int day)
        {
            if (year <= 0)
            {
                throw new ArgumentException($"The year should be a positive number but you specified: {year}");
            }

            if (month <= 0 || month > 12)
            {
                throw new ArgumentException($"The month should be between 0 and 12 but you specified: {month}");
            }

            if (day <= 0 || day > 31)
            {
                throw new ArgumentException($"The day should be between 0 and 31 but you specified: {day}");
            }

            string valueToBeSet = month < 10 ? $"{year}-0{month}" : $"{year}-{month}";
            valueToBeSet = day < 10 ? $"{valueToBeSet}-0{day}" : $"{valueToBeSet}-{day}";
            DefaultSetValue(date, SettingDate, DateSet, valueToBeSet);
        }
    }
}