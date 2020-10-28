// <copyright file="Number.cs" company="Automate The Planet Ltd.">
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
    public class Number : Element, IElementDisabled, IElementValue, IElementNumber, IElementAutoComplete, IElementRequired, IElementReadonly, IElementPlaceholder, IElementMax, IElementMin, IElementStep
    {
        public static Action<Number> OverrideHoverGlobally;
        public static Func<Number, bool> OverrideIsDisabledGlobally;
        public static Func<Number, string> OverrideValueGlobally;
        public static Func<Number, double> OverrideGetNumberGlobally;
        public static Action<Number, double> OverrideSetNumberGlobally;
        public static Func<Number, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Number, bool> OverrideIsReadonlyGlobally;
        public static Func<Number, bool> OverrideIsRequiredGlobally;
        public static Func<Number, int?> OverrideMaxGlobally;
        public static Func<Number, int?> OverrideMinGlobally;
        public static Func<Number, int?> OverrideStepGlobally;
        public static Func<Number, string> OverridePlaceholderGlobally;

        public static Action<Number> OverrideHoverLocally;
        public static Func<Number, bool> OverrideIsDisabledLocally;
        public static Func<Number, string> OverrideValueLocally;
        public static Func<Number, double> OverrideGetNumberLocally;
        public static Action<Number, double> OverrideSetNumberLocally;
        public static Func<Number, bool> OverrideIsAutoCompleteLocally;
        public static Func<Number, bool> OverrideIsReadonlyLocally;
        public static Func<Number, bool> OverrideIsRequiredLocally;
        public static Func<Number, int?> OverrideMaxLocally;
        public static Func<Number, int?> OverrideMinLocally;
        public static Func<Number, int?> OverrideStepLocally;
        public static Func<Number, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingNumber;
        public static event EventHandler<ElementActionEventArgs> NumberSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetNumberLocally = null;
            OverrideSetNumberLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public double GetNumber()
        {
            var action = InitializeAction(this, OverrideGetNumberGlobally, OverrideGetNumberLocally, DefaultGetNumber);
            return action();
        }

        public void SetNumber(double value)
        {
            var action = InitializeAction(this, OverrideSetNumberGlobally, OverrideSetNumberLocally, DefaultSetNumber);
            action(value);
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
        public string Placeholder
        {
            get
            {
                var action = InitializeAction(this, OverridePlaceholderGlobally, OverridePlaceholderLocally, DefaultGetPlaceholder);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Max
        {
            get
            {
                var action = InitializeAction(this, OverrideMaxGlobally, OverrideMaxLocally, DefaultGetMax);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Min
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

        protected virtual bool DefaultGetAutoComplete(Number number) => base.DefaultGetAutoComplete(number);

        protected virtual bool DefaultGetReadonly(Number number) => base.DefaultGetReadonly(number);

        protected virtual bool DefaultGetRequired(Number number) => base.DefaultGetRequired(number);

        protected virtual int? DefaultGetMaxLength(Number number) => DefaultGetMax(number);

        protected virtual int? DefaultGetMin(Number number) => base.DefaultGetMin(number);

        protected virtual int? DefaultGetStep(Number number) => base.DefaultGetStep(number);

        protected virtual string DefaultGetPlaceholder(Number number) => base.DefaultGetPlaceholder(number);

        protected virtual void DefaultHover(Number number) => DefaultHover(number, Hovering, Hovered);

        protected virtual string DefaultGetValue(Number number) => base.DefaultGetValue(number);

        protected virtual bool DefaultIsDisabled(Number number) => base.DefaultIsDisabled(number);

        protected virtual double DefaultGetNumber(Number number) => double.Parse(DefaultGetValue(number));

        protected virtual void DefaultSetNumber(Number number, double value) => DefaultSetText(number, SettingNumber, NumberSet, value.ToString());
    }
}