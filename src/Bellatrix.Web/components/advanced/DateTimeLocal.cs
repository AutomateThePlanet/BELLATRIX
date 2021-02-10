// <copyright file="DateTimeLocal.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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
    public class DateTimeLocal : Element, IElementDisabled, IElementValue, IElementTime, IElementAutoComplete, IElementReadonly, IElementRequired, IElementMaxText, IElementMinText, IElementStep
    {
        public static event EventHandler<ElementActionEventArgs> Hovering;
        public static event EventHandler<ElementActionEventArgs> Hovered;
        public static event EventHandler<ElementActionEventArgs> SettingTime;
        public static event EventHandler<ElementActionEventArgs> TimeSet;

        public override Type ElementType => GetType();

        public string GetTime()
        {
            return DefaultGetValue();
        }

        public void SetTime(DateTime time)
        {
            SetValue(SettingTime, TimeSet, $"{time.Year}-{time.Month}-{time.Day}T{time.Hour}:{time.Minute}");
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
        public string Max => GetMaxAttributeAsString();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string Min => GetMinAttributeAsString();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Step => GetStepAttribute();
    }
}