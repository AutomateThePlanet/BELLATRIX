﻿// <copyright file="Date.cs" company="Automate The Planet Ltd.">
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

public class Date : Component, IComponentDisabled, IComponentValue, IComponentDate, IComponentAutoComplete, IComponentRequired, IComponentReadonly, IComponentMaxText, IComponentMinText, IComponentStep
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingDate;
    public static event EventHandler<ComponentActionEventArgs> DateSet;

    public override Type ComponentType => GetType();

    public virtual string GetDate()
    {
        return DefaultGetValue();
    }

    public virtual void SetDate(int year, int month, int day)
    {
        DefaultSetDate(year, month, day);
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Value => DefaultGetValue();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsAutoComplete => GetAutoCompleteAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsReadonly => GetReadonlyAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Max => GetMaxAttributeAsString();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Min => GetMinAttributeAsString();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual int? Step => GetStepAttribute();

    protected virtual void DefaultSetDate(int year, int month, int day)
    {
        if (year <= 0)
        {
            throw new ArgumentException($"The year should be a positive number but you specified: {year}");
        }

        if (month <= 0 || month > 12)
        {
            throw new ArgumentException($"The month should be between 0 and 12 but you specified: {month}");
        }

        if (day <= 0 || day > 31)
        {
            throw new ArgumentException($"The day should be between 0 and 31 but you specified: {day}");
        }

        string valueToBeSet = month < 10 ? $"{year}-0{month}" : $"{year}-{month}";
        valueToBeSet = day < 10 ? $"{valueToBeSet}-0{day}" : $"{valueToBeSet}-{day}";
        SetValue(SettingDate, DateSet, valueToBeSet);
    }
}