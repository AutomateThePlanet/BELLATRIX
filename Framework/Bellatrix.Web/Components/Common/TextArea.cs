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
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingText;
        public static event EventHandler<ElementActionEventArgs> TextSet;

        public override Type ElementType => GetType();

        public string GetText()
        {
            return base.GetText();
        }

        public void SetText(string value)
        {
            DefaultSetText(SettingText, TextSet, value);
        }

        public void Hover()
        {
            Hover(Hovering, Hovered);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string InnerText => GetInnerText();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled => GetDisabledAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsAutoComplete => GetAutoCompleteAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsReadonly => GetReadonlyAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsRequired => GetRequiredAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Placeholder => GetPlaceholderAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? MaxLength => DefaultGetMaxLength();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? MinLength => DefaultGetMinLength();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Rows => string.IsNullOrEmpty(GetAttribute("rows")) ? null : (int?)int.Parse(GetAttribute("rows"));

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Cols => string.IsNullOrEmpty(GetAttribute("cols")) ? null : (int?)int.Parse(GetAttribute("cols"));

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string SpellCheck => string.IsNullOrEmpty(GetAttribute("spellcheck")) ? null : GetAttribute("spellcheck");

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Wrap => string.IsNullOrEmpty(GetAttribute("wrap")) ? null : GetAttribute("wrap");
    }
}