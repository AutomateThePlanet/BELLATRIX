// <copyright file="Search.cs" company="Automate The Planet Ltd.">
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
    public class Search : Element, IElementDisabled, IElementValue, IElementSearch, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<Search> OverrideFocusGlobally;
        public static Action<Search> OverrideHoverGlobally;
        public static Func<Search, bool> OverrideIsDisabledGlobally;
        public static Func<Search, string> OverrideValueGlobally;
        public static Func<Search, string> OverrideGetSearchGlobally;
        public static Action<Search, string> OverrideSetSearchGlobally;
        public static Func<Search, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Search, bool> OverrideIsReadonlyGlobally;
        public static Func<Search, bool> OverrideIsRequiredGlobally;
        public static Func<Search, int?> OverrideMaxLengthGlobally;
        public static Func<Search, int?> OverrideMinLengthGlobally;
        public static Func<Search, int?> OverrideSizeGlobally;
        public static Func<Search, string> OverridePlaceholderGlobally;

        public static Action<Search> OverrideFocusLocally;
        public static Action<Search> OverrideHoverLocally;
        public static Func<Search, bool> OverrideIsDisabledLocally;
        public static Func<Search, string> OverrideValueLocally;
        public static Func<Search, string> OverrideGetSearchLocally;
        public static Action<Search, string> OverrideSetSearchLocally;
        public static Func<Search, bool> OverrideIsAutoCompleteLocally;
        public static Func<Search, bool> OverrideIsReadonlyLocally;
        public static Func<Search, bool> OverrideIsRequiredLocally;
        public static Func<Search, int?> OverrideMaxLengthLocally;
        public static Func<Search, int?> OverrideMinLengthLocally;
        public static Func<Search, int?> OverrideSizeLocally;
        public static Func<Search, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingSearch;
        public static event EventHandler<ElementActionEventArgs> SearchSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetSearchLocally = null;
            OverrideSetSearchLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetSearch()
        {
            var action = InitializeAction(this, OverrideGetSearchGlobally, OverrideGetSearchLocally, DefaultGetSearch);
            return action();
        }

        public void SetSearch(string search)
        {
            var action = InitializeAction(this, OverrideSetSearchGlobally, OverrideSetSearchLocally, DefaultSetSearch);
            action(search);
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
        public string Placeholder
        {
            get
            {
                var action = InitializeAction(this, OverridePlaceholderGlobally, OverridePlaceholderLocally, DefaultGetPlaceholder);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? MaxLength
        {
            get
            {
                var action = InitializeAction(this, OverrideMaxLengthGlobally, OverrideMaxLengthLocally, DefaultGetMaxLength);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? MinLength
        {
            get
            {
                var action = InitializeAction(this, OverrideMinLengthGlobally, OverrideMinLengthLocally, DefaultGetMinLength);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public new int? Size
        {
            get
            {
                var action = InitializeAction(this, OverrideSizeGlobally, OverrideSizeLocally, DefaultGetSize);
                return action();
            }
        }

        protected virtual bool DefaultGetAutoComplete(Search search) => base.DefaultGetAutoComplete(search);

        protected virtual bool DefaultGetReadonly(Search search) => base.DefaultGetReadonly(search);

        protected virtual bool DefaultGetRequired(Search search) => base.DefaultGetRequired(search);

        protected virtual int? DefaultGetMaxLength(Search search) => base.DefaultGetMaxLength(search);

        protected virtual int? DefaultGetMinLength(Search search) => base.DefaultGetMinLength(search);

        protected virtual int? DefaultGetSize(Search search) => base.DefaultGetSize(search);

        protected virtual string DefaultGetPlaceholder(Search search) => base.DefaultGetPlaceholder(search);

        protected virtual void DefaultFocus(Search search) => DefaultFocus(search, Focusing, Focused);

        protected virtual void DefaultHover(Search search) => DefaultHover(search, Hovering, Hovered);

        protected virtual string DefaultGetValue(Search search) => base.DefaultGetValue(search);

        protected virtual bool DefaultIsDisabled(Search search) => base.DefaultIsDisabled(search);

        protected virtual string DefaultGetSearch(Search search) => base.DefaultGetValue(search);

        protected virtual void DefaultSetSearch(Search search, string value) => DefaultSetValue(search, SettingSearch, SearchSet, value);
    }
}