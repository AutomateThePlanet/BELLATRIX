﻿// <copyright file="Password.cs" company="Automate The Planet Ltd.">
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
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingPassword;
        public static event EventHandler<ElementActionEventArgs> PasswordSet;

        public override Type ElementType => GetType();

        public string GetPassword()
        {
            return DefaultGetValue();
        }

        public void SetPassword(string password)
        {
            DefaultSetText(SettingPassword, PasswordSet, password);
        }

        public void Hover()
        {
            Hover(Hovering, Hovered);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled => GetDisabledAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Value => DefaultGetValue();

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
        public new int? Size => GetSizeAttribute();
    }
}