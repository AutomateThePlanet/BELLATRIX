// <copyright file="Url.cs" company="Automate The Planet Ltd.">
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
    public class Url : Element, IElementDisabled, IElementValue, IElementUrl, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<Url> OverrideHoverGlobally;
        public static Func<Url, bool> OverrideIsDisabledGlobally;
        public static Func<Url, string> OverrideValueGlobally;
        public static Func<Url, string> OverrideGetUrlGlobally;
        public static Action<Url, string> OverrideSetUrlGlobally;
        public static Func<Url, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Url, bool> OverrideIsReadonlyGlobally;
        public static Func<Url, bool> OverrideIsRequiredGlobally;
        public static Func<Url, int?> OverrideMaxLengthGlobally;
        public static Func<Url, int?> OverrideMinLengthGlobally;
        public static Func<Url, int?> OverrideSizeGlobally;
        public static Func<Url, string> OverridePlaceholderGlobally;

        public static Action<Url> OverrideHoverLocally;
        public static Func<Url, bool> OverrideIsDisabledLocally;
        public static Func<Url, string> OverrideValueLocally;
        public static Func<Url, string> OverrideGetUrlLocally;
        public static Action<Url, string> OverrideSetUrlLocally;
        public static Func<Url, bool> OverrideIsAutoCompleteLocally;
        public static Func<Url, bool> OverrideIsReadonlyLocally;
        public static Func<Url, bool> OverrideIsRequiredLocally;
        public static Func<Url, int?> OverrideMaxLengthLocally;
        public static Func<Url, int?> OverrideMinLengthLocally;
        public static Func<Url, int?> OverrideSizeLocally;
        public static Func<Url, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingUrl;
        public static event EventHandler<ElementActionEventArgs> UrlSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetUrlLocally = null;
            OverrideSetUrlLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetUrl()
        {
            var action = InitializeAction(this, OverrideGetUrlGlobally, OverrideGetUrlLocally, DefaultGetUrl);
            return action();
        }

        public void SetUrl(string url)
        {
            var action = InitializeAction(this, OverrideSetUrlGlobally, OverrideSetUrlLocally, DefaultSetUrl);
            action(url);
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

        protected virtual bool DefaultGetAutoComplete(Url url) => base.DefaultGetAutoComplete(url);

        protected virtual bool DefaultGetReadonly(Url url) => base.DefaultGetReadonly(url);

        protected virtual bool DefaultGetRequired(Url url) => base.DefaultGetRequired(url);

        protected virtual int? DefaultGetMaxLength(Url url) => base.DefaultGetMaxLength(url);

        protected virtual int? DefaultGetMinLength(Url url) => base.DefaultGetMinLength(url);

        protected virtual int? DefaultGetSize(Url url) => base.DefaultGetSize(url);

        protected virtual string DefaultGetPlaceholder(Url url) => base.DefaultGetPlaceholder(url);

        protected virtual void DefaultHover(Url url) => DefaultHover(url, Hovering, Hovered);

        protected virtual string DefaultGetValue(Url url) => base.DefaultGetValue(url);

        protected virtual bool DefaultIsDisabled(Url url) => base.DefaultIsDisabled(url);

        protected virtual string DefaultGetUrl(Url url) => base.DefaultGetValue(url);

        protected virtual void DefaultSetUrl(Url url, string value) => DefaultSetValue(url, SettingUrl, UrlSet, value);
    }
}