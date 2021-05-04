// <copyright file="Range.cs" company="Automate The Planet Ltd.">
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
    public class Range : Component, IComponentDisabled, IComponentValue, IComponentRange, IComponentList, IComponentAutoComplete, IComponentRequired, IComponentMax, IComponentMin, IComponentStep
    {
        public static event EventHandler<ComponentActionEventArgs> Hovering;
        public static event EventHandler<ComponentActionEventArgs> Hovered;
        public static event EventHandler<ComponentActionEventArgs> SettingRange;
        public static event EventHandler<ComponentActionEventArgs> RangeSet;

        public override Type ComponentType => GetType();

        public int GetRange()
        {
            return DefaultGetValue().ToInt();
        }

        public void SetRange(int value)
        {
            SetValue(SettingRange, RangeSet, value.ToString());
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
        public string List => GetList();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsRequired => GetRequiredAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Max => GetMaxAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Min => GetMinAttribute();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int? Step => GetStepAttribute();
    }
}