﻿// <copyright file="CheckBox.cs" company="Automate The Planet Ltd.">
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

public class CheckBox : Component, IComponentDisabled, IComponentChecked, IComponentValue
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Checking;
    public static event EventHandler<ComponentActionEventArgs> Checked;
    public static event EventHandler<ComponentActionEventArgs> Unchecking;
    public static event EventHandler<ComponentActionEventArgs> Unchecked;

    public override Type ComponentType => GetType();

    /// <summary>
    /// Automatically performs a validation if the component is already checked.
    /// </summary>
    /// <param name="options"></param>
    public virtual void Check(LocatorCheckOptions options = default)
    {
        var tempOptions = options ?? new LocatorCheckOptions();
        tempOptions.Timeout = 1;

        Checking?.Invoke(this, new ComponentActionEventArgs(this));
        this.ValidateIsPresent();

        try
        {
            DefaultCheck(null, null, tempOptions);
        }
        catch
        {
            // Fallback to JsClick, checkbox may be custom element with hidden input

            if (!IsChecked)
            {
                var clickOptions = new LocatorClickOptions
                {
                    Force = true,
                    Timeout = options?.Timeout,
                };
                DefaultClick(null, null, clickOptions);
            }
        }

        Checked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    /// <summary>
    /// Automatically performs a validation if the component is already unchecked.
    /// </summary>
    /// <param name="options"></param>
    public virtual void Uncheck(LocatorUncheckOptions options = default)
    {
        var tempOptions = options ?? new LocatorUncheckOptions();
        tempOptions.Timeout = 1;

        Unchecking?.Invoke(this, new ComponentActionEventArgs(this));
        this.ValidateIsPresent();

        try
        {
            DefaultUncheck(null, null, tempOptions);
        }
        catch
        {
            // Fallback to JsClick, checkbox may be custom element with hidden input

            if (IsChecked)
            {
                var clickOptions = new LocatorClickOptions
                {
                    Force = true,
                    Timeout = options?.Timeout,
                };
                DefaultClick(null, null, clickOptions);
            }

        }

        Unchecked?.Invoke(this, new ComponentActionEventArgs(this));
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
    public virtual bool IsChecked => WrappedElement.IsChecked();
}