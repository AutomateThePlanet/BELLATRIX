// <copyright file="TextArea.cs" company="Automate The Planet Ltd.">
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
    public class TextArea : Element, IElementInnerText, IElementText, IElementDisabled, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxLength, IElementMinLength, IElementRows, IElementCols, IElementPlaceholder, IElementSpellCheck, IElementWrap
    {
        public static Action<TextArea> OverrideFocusGlobally;
        public static Action<TextArea> OverrideHoverGlobally;
        public static Func<TextArea, string> OverrideInnerTextGlobally;
        public static Func<TextArea, string> OverrideGetTextGlobally;
        public static Action<TextArea, string> OverrideSetTextGlobally;
        public static Func<TextArea, bool> OverrideIsDisabledGlobally;
        public static Func<TextArea, bool> OverrideIsAutoCompleteGlobally;
        public static Func<TextArea, bool> OverrideIsReadonlyGlobally;
        public static Func<TextArea, bool> OverrideIsRequiredGlobally;
        public static Func<TextArea, int?> OverrideMaxLengthGlobally;
        public static Func<TextArea, int?> OverrideMinLengthGlobally;
        public static Func<TextArea, int?> OverrideRowsGlobally;
        public static Func<TextArea, string> OverridePlaceholderGlobally;
        public static Func<TextArea, int?> OverrideColsGlobally;
        public static Func<TextArea, string> OverrideSpellCheckGlobally;
        public static Func<TextArea, string> OverrideWrapGlobally;

        public static Action<TextArea> OverrideFocusLocally;
        public static Action<TextArea> OverrideHoverLocally;
        public static Func<TextArea, string> OverrideInnerTextLocally;
        public static Func<TextArea, string> OverrideGetTextLocally;
        public static Action<TextArea, string> OverrideSetTextLocally;
        public static Func<TextArea, bool> OverrideIsDisabledLocally;
        public static Func<TextArea, bool> OverrideIsAutoCompleteLocally;
        public static Func<TextArea, bool> OverrideIsReadonlyLocally;
        public static Func<TextArea, bool> OverrideIsRequiredLocally;
        public static Func<TextArea, int?> OverrideMaxLengthLocally;
        public static Func<TextArea, int?> OverrideMinLengthLocally;
        public static Func<TextArea, int?> OverrideRowsLocally;
        public static Func<TextArea, string> OverridePlaceholderLocally;
        public static Func<TextArea, int?> OverrideColsLocally;
        public static Func<TextArea, string> OverrideSpellCheckLocally;
        public static Func<TextArea, string> OverrideWrapLocally;

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
            OverrideGetTextLocally = null;
            OverrideSetTextLocally = null;
            OverrideIsDisabledLocally = null;
            OverrideIsAutoCompleteLocally = null;
            OverrideIsReadonlyLocally = null;
            OverrideIsRequiredLocally = null;
            OverrideMaxLengthLocally = null;
            OverrideMinLengthLocally = null;
            OverrideRowsLocally = null;
            OverridePlaceholderLocally = null;
            OverrideColsLocally = null;
            OverrideSpellCheckLocally = null;
            OverrideWrapLocally = null;
        }

        public override Type ElementType => GetType();

        public string GetText()
        {
            var action = InitializeAction(this, OverrideGetTextGlobally, OverrideGetTextLocally, DefaultGetText);
            return action();
        }

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
        public bool IsDisabled
        {
            get
            {
                var action = InitializeAction(this, OverrideIsDisabledGlobally, OverrideIsDisabledLocally, DefaultIsDisabled);
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
        public int? Rows
        {
            get
            {
                var action = InitializeAction(this, OverrideRowsGlobally, OverrideRowsLocally, DefaultGetRows);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Cols
        {
            get
            {
                var action = InitializeAction(this, OverrideColsGlobally, OverrideColsLocally, DefaultGetCols);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string SpellCheck
        {
            get
            {
                var action = InitializeAction(this, OverrideSpellCheckGlobally, OverrideSpellCheckLocally, DefaultGetSpellChecked);
                return action();
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Wrap
        {
            get
            {
                var action = InitializeAction(this, OverrideWrapGlobally, OverrideWrapLocally, DefaultGetWrap);
                return action();
            }
        }

        protected virtual void DefaultFocus(TextArea textArea) => DefaultFocus(textArea, Focusing, Focused);

        protected virtual void DefaultHover(TextArea textArea) => DefaultHover(textArea, Hovering, Hovered);

        protected virtual string DefaultInnerText(TextArea textArea) => base.DefaultInnerText(textArea);

        protected virtual string DefaultGetText(TextArea textArea) => DefaultGetValue(textArea);

        protected virtual void DefaultSetText(TextArea textArea, string value) => DefaultSetText(textArea, SettingText, TextSet, value);

        protected virtual bool DefaultGetAutoComplete(TextArea textArea) => base.DefaultGetAutoComplete(textArea);

        protected virtual bool DefaultGetReadonly(TextArea textArea) => base.DefaultGetReadonly(textArea);

        protected virtual bool DefaultGetRequired(TextArea textArea) => base.DefaultGetRequired(textArea);

        protected virtual int? DefaultGetMaxLength(TextArea textArea) => base.DefaultGetMaxLength(textArea);

        protected virtual int? DefaultGetMinLength(TextArea textArea) => base.DefaultGetMinLength(textArea);

        protected virtual string DefaultGetPlaceholder(TextArea textArea) => base.DefaultGetPlaceholder(textArea);

        protected virtual bool DefaultIsDisabled(TextArea textArea) => base.DefaultIsDisabled(textArea);

        protected virtual string DefaultGetSpellChecked(TextArea textArea) => string.IsNullOrEmpty(GetAttribute("spellcheck")) ? null : GetAttribute("spellcheck");

        protected virtual int? DefaultGetRows(TextArea textArea) => string.IsNullOrEmpty(GetAttribute("rows")) ? null : (int?)int.Parse(GetAttribute("rows"));

        protected virtual int? DefaultGetCols(TextArea textArea) => string.IsNullOrEmpty(GetAttribute("cols")) ? null : (int?)int.Parse(GetAttribute("cols"));

        protected virtual string DefaultGetWrap(TextArea textArea) => string.IsNullOrEmpty(GetAttribute("wrap")) ? null : GetAttribute("wrap");
    }
}