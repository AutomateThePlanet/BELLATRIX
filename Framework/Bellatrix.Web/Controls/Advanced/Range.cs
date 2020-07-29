// <copyright file="Range.cs" company="Automate The Planet Ltd.">
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
    public class Range : Element, IElementDisabled, IElementValue, IElementRange, IElementList, IElementAutoComplete, IElementRequired, IElementMax, IElementMin, IElementStep
    {
        public static Action<Range> OverrideFocusGlobally;
        public static Action<Range> OverrideHoverGlobally;
        public static Func<Range, bool> OverrideIsDisabledGlobally;
        public static Func<Range, string> OverrideValueGlobally;
        public static Func<Range, int> OverrideGetRangeGlobally;
        public static Func<Range, string> OverrideListGlobally;
        public static Action<Range, int> OverrideSetRangeGlobally;
        public static Func<Range, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Range, bool> OverrideIsRequiredGlobally;
        public static Func<Range, int?> OverrideMaxGlobally;
        public static Func<Range, int?> OverrideMinGlobally;
        public static Func<Range, int?> OverrideStepGlobally;

        public static Action<Range> OverrideFocusLocally;
        public static Action<Range> OverrideHoverLocally;
        public static Func<Range, bool> OverrideIsDisabledLocally;
        public static Func<Range, string> OverrideValueLocally;
        public static Func<Range, int> OverrideGetRangeLocally;
        public static Func<Range, string> OverrideListLocally;
        public static Action<Range, int> OverrideSetRangeLocally;
        public static Func<Range, bool> OverrideIsAutoCompleteLocally;
        public static Func<Range, bool> OverrideIsRequiredLocally;
        public static Func<Range, int?> OverrideMaxLocally;
        public static Func<Range, int?> OverrideMinLocally;
        public static Func<Range, int?> OverrideStepLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingRange;
        public static event EventHandler<ElementActionEventArgs> RangeSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetRangeLocally = null;
            OverrideListLocally = null;
            OverrideSetRangeLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLocally = null;
            OverrideMinLocally = null;
            OverrideStepLocally = null;
        }

        public override Type ElementType => GetType();

        public int GetRange()
        {
            var action = InitializeAction(this, OverrideGetRangeGlobally, OverrideGetRangeLocally, DefaultGetRange);
            return action();
        }

        public void SetRange(int value)
        {
            var action = InitializeAction(this, OverrideSetRangeGlobally, OverrideSetRangeLocally, DefaultSetRange);
            action(value);
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
        public string List
        {
            get
            {
                var action = InitializeAction(this, OverrideListGlobally, OverrideListLocally, DefaultGetList);
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

        protected virtual bool DefaultGetAutoComplete(Range range) => base.DefaultGetAutoComplete(range);

        protected virtual bool DefaultGetRequired(Range range) => base.DefaultGetRequired(range);

        protected virtual int? DefaultGetMax(Range range) => base.DefaultGetMax(range);

        protected virtual int? DefaultGetMin(Range range) => base.DefaultGetMin(range);

        protected virtual string DefaultGetList(Range range) => base.DefaultGetList(range);

        protected virtual int? DefaultGetStep(Range range) => base.DefaultGetStep(range);

        protected virtual void DefaultFocus(Range range) => DefaultFocusJavaScript(range, Focusing, Focused);

        protected virtual void DefaultHover(Range range) => DefaultHover(range, Hovering, Hovered);

        protected virtual string DefaultGetValue(Range range) => base.DefaultGetValue(range);

        protected virtual bool DefaultIsDisabled(Range range) => base.DefaultIsDisabled(range);

        protected virtual int DefaultGetRange(Range range) => int.Parse(base.DefaultGetValue(range));

        protected virtual void DefaultSetRange(Range range, int valueToBeSet) => DefaultSetValue(range, SettingRange, RangeSet, valueToBeSet.ToString());
    }
}