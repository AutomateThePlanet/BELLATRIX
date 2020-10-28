// <copyright file="Phone.cs" company="Automate The Planet Ltd.">
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
    public class Phone : Element, IElementDisabled, IElementValue, IElementPhone, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<Phone> OverrideHoverGlobally;
        public static Func<Phone, bool> OverrideIsDisabledGlobally;
        public static Func<Phone, string> OverrideValueGlobally;
        public static Func<Phone, string> OverrideGetPhoneGlobally;
        public static Action<Phone, string> OverrideSetPhoneGlobally;
        public static Func<Phone, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Phone, bool> OverrideIsReadonlyGlobally;
        public static Func<Phone, bool> OverrideIsRequiredGlobally;
        public static Func<Phone, int?> OverrideMaxLengthGlobally;
        public static Func<Phone, int?> OverrideMinLengthGlobally;
        public static Func<Phone, int?> OverrideSizeGlobally;
        public static Func<Phone, string> OverridePlaceholderGlobally;

        public static Action<Phone> OverrideHoverLocally;
        public static Func<Phone, bool> OverrideIsDisabledLocally;
        public static Func<Phone, string> OverrideValueLocally;
        public static Func<Phone, string> OverrideGetPhoneLocally;
        public static Action<Phone, string> OverrideSetPhoneLocally;
        public static Func<Phone, bool> OverrideIsAutoCompleteLocally;
        public static Func<Phone, bool> OverrideIsReadonlyLocally;
        public static Func<Phone, bool> OverrideIsRequiredLocally;
        public static Func<Phone, int?> OverrideMaxLengthLocally;
        public static Func<Phone, int?> OverrideMinLengthLocally;
        public static Func<Phone, int?> OverrideSizeLocally;
        public static Func<Phone, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingPhone;
        public static event EventHandler<ElementActionEventArgs> PhoneSet;

        public static new void ClearLocalOverrides()
        {
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetPhoneLocally = null;
            OverrideSetPhoneLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetPhone()
        {
            var action = InitializeAction(this, OverrideGetPhoneGlobally, OverrideGetPhoneLocally, DefaultGetPhone);
            return action();
        }

        public void SetPhone(string phone)
        {
            var action = InitializeAction(this, OverrideSetPhoneGlobally, OverrideSetPhoneLocally, DefaultSetPhone);
            action(phone);
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

        protected virtual bool DefaultGetAutoComplete(Phone phone) => base.DefaultGetAutoComplete(phone);

        protected virtual bool DefaultGetReadonly(Phone phone) => base.DefaultGetReadonly(phone);

        protected virtual bool DefaultGetRequired(Phone phone) => base.DefaultGetRequired(phone);

        protected virtual int? DefaultGetMaxLength(Phone phone) => base.DefaultGetMaxLength(phone);

        protected virtual int? DefaultGetMinLength(Phone phone) => base.DefaultGetMinLength(phone);

        protected virtual int? DefaultGetSize(Phone phone) => base.DefaultGetSize(phone);

        protected virtual string DefaultGetPlaceholder(Phone phone) => base.DefaultGetPlaceholder(phone);

        protected virtual void DefaultHover(Phone phone) => DefaultHover(phone, Hovering, Hovered);

        protected virtual string DefaultGetValue(Phone phone) => base.DefaultGetValue(phone);

        protected virtual bool DefaultIsDisabled(Phone phone) => base.DefaultIsDisabled(phone);

        protected virtual string DefaultGetPhone(Phone phone) => base.DefaultGetValue(phone);

        protected virtual void DefaultSetPhone(Phone phone, string value) => DefaultSetValue(phone, SettingPhone, PhoneSet, value);
    }
}