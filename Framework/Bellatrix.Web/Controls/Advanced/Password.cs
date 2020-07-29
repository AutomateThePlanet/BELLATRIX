// <copyright file="Password.cs" company="Automate The Planet Ltd.">
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
    public class Password : Element, IElementDisabled, IElementValue, IElementPassword, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<Password> OverrideFocusGlobally;
        public static Action<Password> OverrideHoverGlobally;
        public static Func<Password, bool> OverrideIsDisabledGlobally;
        public static Func<Password, string> OverrideValueGlobally;
        public static Func<Password, string> OverrideGetPasswordGlobally;
        public static Action<Password, string> OverrideSetPasswordGlobally;
        public static Func<Password, bool> OverrideIsAutoCompleteGlobally;
        public static Func<Password, bool> OverrideIsReadonlyGlobally;
        public static Func<Password, bool> OverrideIsRequiredGlobally;
        public static Func<Password, int?> OverrideMaxLengthGlobally;
        public static Func<Password, int?> OverrideMinLengthGlobally;
        public static Func<Password, int?> OverrideSizeGlobally;
        public static Func<Password, string> OverridePlaceholderGlobally;

        public static Action<Password> OverrideFocusLocally;
        public static Action<Password> OverrideHoverLocally;
        public static Func<Password, bool> OverrideIsDisabledLocally;
        public static Func<Password, string> OverrideValueLocally;
        public static Func<Password, string> OverrideGetPasswordLocally;
        public static Action<Password, string> OverrideSetPasswordLocally;
        public static Func<Password, bool> OverrideIsAutoCompleteLocally;
        public static Func<Password, bool> OverrideIsReadonlyLocally;
        public static Func<Password, bool> OverrideIsRequiredLocally;
        public static Func<Password, int?> OverrideMaxLengthLocally;
        public static Func<Password, int?> OverrideMinLengthLocally;
        public static Func<Password, int?> OverrideSizeLocally;
        public static Func<Password, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingPassword;
        public static event EventHandler<ElementActionEventArgs> PasswordSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideGetPasswordLocally = null;
            OverrideSetPasswordLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetPassword()
        {
            var action = InitializeAction(this, OverrideGetPasswordGlobally, OverrideGetPasswordLocally, DefaultGetPassword);
            return action();
        }

        public void SetPassword(string password)
        {
            var action = InitializeAction(this, OverrideSetPasswordGlobally, OverrideSetPasswordLocally, DefaultSetPassword);
            action(password);
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

        protected virtual bool DefaultGetAutoComplete(Password password) => base.DefaultGetAutoComplete(password);

        protected virtual bool DefaultGetReadonly(Password password) => base.DefaultGetReadonly(password);

        protected virtual bool DefaultGetRequired(Password password) => base.DefaultGetRequired(password);

        protected virtual int? DefaultGetMaxLength(Password password) => base.DefaultGetMaxLength(password);

        protected virtual int? DefaultGetMinLength(Password password) => base.DefaultGetMinLength(password);

        protected virtual int? DefaultGetSize(Password password) => base.DefaultGetSize(password);

        protected virtual string DefaultGetPlaceholder(Password password) => base.DefaultGetPlaceholder(password);

        protected virtual void DefaultFocus(Password password) => DefaultFocus(password, Focusing, Focused);

        protected virtual void DefaultHover(Password password) => DefaultHover(password, Hovering, Hovered);

        protected virtual string DefaultGetValue(Password password) => base.DefaultGetValue(password);

        protected virtual bool DefaultIsDisabled(Password password) => base.DefaultIsDisabled(password);

        protected virtual string DefaultGetPassword(Password password) => base.DefaultGetValue(password);

        protected virtual void DefaultSetPassword(Password password, string value) => DefaultSetText(password, SettingPassword, PasswordSet, value);
    }
}