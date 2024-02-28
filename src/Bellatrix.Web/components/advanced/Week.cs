// <copyright file="Week.cs" company="Automate The Planet Ltd.">
// Copyright 2024 Automate The Planet Ltd.
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

namespace Bellatrix.Web;

public class Week : Component, IComponentDisabled, IComponentValue, IComponentWeek, IComponentAutoComplete, IComponentReadonly, IComponentRequired, IComponentMaxText, IComponentMinText, IComponentStep
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingWeek;
    public static event EventHandler<ComponentActionEventArgs> WeekSet;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public override Type ComponentType => GetType();

    public virtual string GetWeek()
    {
        return DefaultGetValue();
    }

    public virtual void SetWeek(int year, int weekNumber)
    {
        DefaultSetWeek(year, weekNumber);
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
    public string Value => DefaultGetValue();

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

    protected virtual void DefaultSetWeek(int year, int weekNumber)
    {
        if (weekNumber <= 0 || weekNumber > 52)
        {
            throw new ArgumentException($"The week number should be between 0 and 53 but you specified: {weekNumber}");
        }

        if (year <= 0)
        {
            throw new ArgumentException($"The year should be a positive number but you specified: {year}");
        }

        string valueToBeSet = weekNumber < 10 ? $"{year}-W0{weekNumber}" : $"{year}-W{weekNumber}";
        SetValue(SettingWeek, WeekSet, valueToBeSet);
    }
}