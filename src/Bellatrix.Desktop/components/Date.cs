// <copyright file="Date.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop;

public class Date : Component, IComponentDisabled, IComponentDate
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingDate;
    public static event EventHandler<ComponentActionEventArgs> DateSet;

    public virtual string GetDate()
    {
        return WrappedElement.Text;
    }

    public virtual void SetDate(int year, int month, int day)
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

        string valueToBeSet = month < 10 ? $"0{month}\\{year}" : $"{month}\\{year}";
        valueToBeSet = day < 10 ? $"{valueToBeSet}-0{day}" : $"{day}\\{valueToBeSet}";
        SetText(SettingDate, DateSet, valueToBeSet);
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();
}