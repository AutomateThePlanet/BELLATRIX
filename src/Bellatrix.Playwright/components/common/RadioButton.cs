// <copyright file="RadioButton.cs" company="Automate The Planet Ltd.">
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

public class RadioButton : Component, IComponentDisabled, IComponentValue, IComponentChecked
{
    public static event EventHandler<ComponentActionEventArgs> Clicking;
    public static event EventHandler<ComponentActionEventArgs> Clicked;
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;

    public override Type ComponentType => GetType();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Value => DefaultGetValue();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => WrappedElement.IsChecked();

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    /// <summary>
    /// Automatically performs a validation if the component is already selected.
    /// </summary>
    /// <param name="options"></param>
    public virtual void Click(LocatorCheckOptions options = default)
    {
        var tempOptions = options ?? new LocatorCheckOptions();
        tempOptions.Timeout = 1;

        Clicking?.Invoke(this, new ComponentActionEventArgs(this));

        try
        {
            DefaultCheck(null, null, tempOptions);
        }
        catch
        {
            // Fallback to JsClick, radio button may be custom element with hidden input
            var clickOptions = new LocatorClickOptions
            {
                Force = true,
                Timeout = options?.Timeout,
            };

            DefaultClick(null, null, clickOptions);
        }

        Clicked?.Invoke(this, new ComponentActionEventArgs(this));
    }
}