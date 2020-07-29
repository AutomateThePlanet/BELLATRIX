// <copyright file="Email.cs" company="Automate The Planet Ltd.">
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
    public class Email : Element, IElementDisabled, IElementValue, IElementEmail, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<Email> OverrideFocusGlobally;
        public static Action<Email> OverrideHoverGlobally;
        public static Func<Email, bool> OverrideIsDisabledGlobally;
        public static Func<Email, string> OverrideValueGlobally;
        public static Func<Email, string> OverrideGetEmailGlobally;
        public static Action<Email, string> OverrideSetEmailGlobally;
        public static Func<Email, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Email, bool> OverrideIsReadonlyGlobally;
        public static Func<Email, bool> OverrideIsRequiredGlobally;
        public static Func<Email, int?> OverrideMaxLengthGlobally;
        public static Func<Email, int?> OverrideMinLengthGlobally;
        public static Func<Email, int?> OverrideSizeGlobally;
        public static Func<Email, string> OverridePlaceholderGlobally;

        public static Action<Email> OverrideFocusLocally;
        public static Action<Email> OverrideHoverLocally;
        public static Func<Email, bool> OverrideIsDisabledLocally;
        public static Func<Email, string> OverrideValueLocally;
        public static Func<Email, string> OverrideGetEmailLocally;
        public static Action<Email, string> OverrideSetEmailLocally;
        public static Func<Email, bool> OverrideIsAutoCompleteLocally;
        public static Func<Email, bool> OverrideIsReadonlyLocally;
        public static Func<Email, bool> OverrideIsRequiredLocally;
        public static Func<Email, int?> OverrideMaxLengthLocally;
        public static Func<Email, int?> OverrideMinLengthLocally;
        public static Func<Email, int?> OverrideSizeLocally;
        public static Func<Email, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingEmail;
        public static event EventHandler<ElementActionEventArgs> EmailSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetEmailLocally = null;
            OverrideSetEmailLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetEmail()
        {
            var action = InitializeAction(this, OverrideGetEmailGlobally, OverrideGetEmailLocally, DefaultGetEmail);
            return action();
        }

        public void SetEmail(string email)
        {
            var action = InitializeAction(this, OverrideSetEmailGlobally, OverrideSetEmailLocally, DefaultSetEmail);
            action(email);
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

        protected virtual bool DefaultGetAutoComplete(Email email) => base.DefaultGetAutoComplete(email);

        protected virtual bool DefaultGetReadonly(Email email) => base.DefaultGetReadonly(email);

        protected virtual bool DefaultGetRequired(Email email) => base.DefaultGetRequired(email);

        protected virtual int? DefaultGetMaxLength(Email email) => base.DefaultGetMaxLength(email);

        protected virtual int? DefaultGetMinLength(Email email) => base.DefaultGetMinLength(email);

        protected virtual int? DefaultGetSize(Email email) => base.DefaultGetSize(email);

        protected virtual string DefaultGetPlaceholder(Email email) => base.DefaultGetPlaceholder(email);

        protected virtual void DefaultFocus(Email email) => DefaultFocus(email, Focusing, Focused);

        protected virtual void DefaultHover(Email email) => DefaultHover(email, Hovering, Hovered);

        protected virtual string DefaultGetValue(Email email) => base.DefaultGetValue(email);

        protected virtual bool DefaultIsDisabled(Email email) => base.DefaultIsDisabled(email);

        protected virtual string DefaultGetEmail(Email email) => base.DefaultGetValue(email);

        protected virtual void DefaultSetEmail(Email email, string value) => DefaultSetValue(email, SettingEmail, EmailSet, value);
    }
}