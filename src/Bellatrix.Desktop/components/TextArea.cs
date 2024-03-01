// <copyright file="TextArea.cs" company="Automate The Planet Ltd.">
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

public class TextArea : Component, IComponentInnerText, IComponentText, IComponentDisabled
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> SettingText;
    public static event EventHandler<ComponentActionEventArgs> TextSet;

    public virtual string GetText()
    {
        return WrappedElement.Text;
    }

    public virtual void SetText(string value)
    {
        SetText(SettingText, TextSet, value);
    }

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string InnerText => GetInnerText();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();
}