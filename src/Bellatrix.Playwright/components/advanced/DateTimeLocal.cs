﻿// <copyright file="DateTimeLocal.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public class DateTimeLocal : Component, IComponentDisabled, IComponentValue, IComponentTime, IComponentAutoComplete, IComponentReadonly, IComponentRequired, IComponentMaxText, IComponentMinText, IComponentStep
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingTime;
    public static event EventHandler<ComponentActionEventArgs> TimeSet;

    public override Type ComponentType => GetType();

    public virtual string GetTime()
    {
        return DefaultGetValue();
    }

    public virtual void SetTime(DateTime time)
    {
        SetValue(SettingTime, TimeSet, $"{time.Year}-{time.Month.ToString("00")}-{time.Day.ToString("00")}T{time.Hour.ToString("00")}:{time.Minute.ToString("00")}");
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Value => DefaultGetValue();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsAutoComplete => GetAutoCompleteAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsReadonly => GetReadonlyAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Max => GetMaxAttributeAsString();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Min => GetMinAttributeAsString();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual int? Step => GetStepAttribute();
}