// <copyright file="TextField.cs" company="Automate The Planet Ltd.">
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
    public class TextField : Element, IElementDisabled, IElementInnerText, IElementInnerHtml, IElementValue, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementSize, IElementPlaceholder
    {
        public static Action<TextField> OverrideFocusGlobally;
        public static Action<TextField> OverrideHoverGlobally;
        public static Func<TextField, string> OverrideInnerTextGlobally;
        public static Func<TextField, string> OverrideInnerHtmlGlobally;
        public static Func<TextField, bool> OverrideIsDisabledGlobally;
        public static Func<TextField, string> OverrideValueGlobally;
        public static Action<TextField, string> OverrideSetTextGlobally;
        public static Func<TextField, bool> OverrideIsAutoCompleteGlobally;
        public static Func<TextField, bool> OverrideIsReadonlyGlobally;
        public static Func<TextField, bool> OverrideIsRequiredGlobally;
        public static Func<TextField, int?> OverrideMaxLengthGlobally;
        public static Func<TextField, int?> OverrideMinLengthGlobally;
        public static Func<TextField, int?> OverrideSizeGlobally;
        public static Func<TextField, string> OverridePlaceholderGlobally;

        public static Action<TextField> OverrideFocusLocally;
        public static Action<TextField> OverrideHoverLocally;
        public static Func<TextField, string> OverrideInnerTextLocally;
        public static Func<TextField, string> OverrideInnerHtmlLocally;
        public static Func<TextField, bool> OverrideIsDisabledLocally;
        public static Func<TextField, string> OverrideValueLocally;
        public static Action<TextField, string> OverrideSetTextLocally;
        public static Func<TextField, bool> OverrideIsAutoCompleteLocally;
        public static Func<TextField, bool> OverrideIsReadonlyLocally;
        public static Func<TextField, bool> OverrideIsRequiredLocally;
        public static Func<TextField, int?> OverrideMaxLengthLocally;
        public static Func<TextField, int?> OverrideMinLengthLocally;
        public static Func<TextField, int?> OverrideSizeLocally;
        public static Func<TextField, string> OverridePlaceholderLocally;

        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> Focusing;
        public static event EventHandler<ElementActionEventArgs> Focused;
        public static event EventHandler<ElementActionEventArgs> SettingText;
        public static event EventHandler<ElementActionEventArgs> TextSet;

        public static new void ClearLocalOverrides()
        {
            OverrideFocusLocally = null;
            OverrideHoverLocally = null;
            OverrideInnerTextLocally = null;
            OverrideInnerHtmlLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideValueLocally = null;
            OverrideSetTextLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideSizeLocally = null;
            OverridePlaceholderLocally = null;
        }

        public override Type ElementType => GetType();

        public void SetText(string value)
        {
            var action = InitializeAction(this, OverrideSetTextGlobally, OverrideSetTextLocally, DefaultSetText);
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
        public string InnerText
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerTextGlobally, OverrideInnerTextLocally, DefaultInnerText);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerHtml
        {
            get
            {
                var action = InitializeAction(this, OverrideInnerHtmlGlobally, OverrideInnerHtmlLocally, DefaultInnerHtml);
                return action();
            }
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

        protected virtual bool DefaultGetAutoComplete(TextField textField) => base.DefaultGetAutoComplete(textField);

        protected virtual bool DefaultGetReadonly(TextField textField) => base.DefaultGetReadonly(textField);

        protected virtual bool DefaultGetRequired(TextField textField) => base.DefaultGetRequired(textField);

        protected virtual int? DefaultGetMaxLength(TextField textField) => base.DefaultGetMaxLength(textField);

        protected virtual int? DefaultGetMinLength(TextField textField) => base.DefaultGetMinLength(textField);

        protected virtual int? DefaultGetSize(TextField textField) => base.DefaultGetSize(textField);

        protected virtual string DefaultGetPlaceholder(TextField textField) => base.DefaultGetPlaceholder(textField);

        protected virtual void DefaultFocus(TextField textField) => DefaultFocus(textField, Focusing, Focused);

        protected virtual void DefaultHover(TextField textField) => DefaultHover(textField, Hovering, Hovered);

        protected virtual string DefaultInnerText(TextField textField) => base.DefaultInnerText(textField);

        protected virtual string DefaultInnerHtml(TextField textField) => base.DefaultInnerHtml(textField);

        protected virtual string DefaultGetValue(TextField textField) => base.DefaultGetValue(textField);

        protected virtual bool DefaultIsDisabled(TextField textField) => base.DefaultIsDisabled(textField);

        protected virtual void DefaultSetText(TextField textField, string value) => DefaultSetText(textField, SettingText, TextSet, value);
    }
}