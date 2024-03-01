// <copyright file="Checkbox.cs" company="Automate The Planet Ltd.">
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

public class CheckBox : Component, IComponentDisabled, IComponentChecked
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Checking;
    public static event EventHandler<ComponentActionEventArgs> Checked;
    public static event EventHandler<ComponentActionEventArgs> Unchecking;
    public static event EventHandler<ComponentActionEventArgs> Unchecked;

    public virtual void Check(bool isChecked = true)
    {
        if (isChecked && !WrappedElement.Selected || !isChecked && WrappedElement.Selected)
        {
            Click(Checking, Checked);
        }
    }

    public virtual void Uncheck()
    {
        if (WrappedElement.Selected)
        {
            Click(Unchecking, Unchecked);
        }
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => WrappedElement.Selected;
}