// <copyright file="ListBox.cs" company="Automate The Planet Ltd.">
// Copyright 2022 Automate The Planet Ltd.
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
using Bellatrix.Desktop.Events;

namespace Bellatrix.Desktop;

public class ListItem : Component
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();

    public virtual void Select()
    {
        if (!ConfigurationService.GetSection<ExecutionSettings>().ExperimentalDesktopDriver)
        {
            throw new InvalidOperationException("This option is supported only with ExperimentalDesktopDriver enabled");
        }

        WrappedDriver.ExecuteScript("windows: select", WrappedElement);
    }
    
    public virtual void AddToSelection()
    {
        if (!ConfigurationService.GetSection<ExecutionSettings>().ExperimentalDesktopDriver)
        {
            throw new InvalidOperationException("This option is supported only with ExperimentalDesktopDriver enabled");
        }

        WrappedDriver.ExecuteScript("windows: addToSelection", WrappedElement);
    }
    
    public virtual void RemoveFromSelection()
    {
        if (!ConfigurationService.GetSection<ExecutionSettings>().ExperimentalDesktopDriver)
        {
            throw new InvalidOperationException("This option is supported only with ExperimentalDesktopDriver enabled");
        }

        WrappedDriver.ExecuteScript("windows: removeFromSelection", WrappedElement);
    }
}