// <copyright file="Month.cs" company="Automate The Planet Ltd.">
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

public class Month : Component, IComponentDisabled, IComponentValue, IComponentMonth, IComponentAutoComplete, IComponentReadonly, IComponentRequired, IComponentMaxText, IComponentMinText, IComponentStep
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingMonth;
    public static event EventHandler<ComponentActionEventArgs> MonthSet;

    public override Type ComponentType => GetType();

    public virtual string GetMonth()
    {
        return DefaultGetValue();
    }

    public virtual void SetMonth(int year, int monthNumber)
    {
        DefaultSetMonth(year, monthNumber);
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

    protected virtual void DefaultSetMonth(int year, int monthNumber)
    {
        if (monthNumber <= 0 || monthNumber > 12)
        {
            throw new ArgumentException($"The month number should be between 0 and 12 but you specified: {monthNumber}");
        }

        if (year <= 0)
        {
            throw new ArgumentException($"The year should be a positive number but you specified: {year}");
        }

        string valueToBeSet = monthNumber < 10 ? $"{year}-0{monthNumber}" : $"{year}-{monthNumber}";
        SetValue(SettingMonth, MonthSet, valueToBeSet);
    }
}