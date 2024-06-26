﻿// <copyright file="Option.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using System.Diagnostics;
using Bellatrix.Playwright.Contracts;

namespace Bellatrix.Playwright;

public class Option : Component, IComponentInnerText, IComponentValue, IComponentDisabled, IComponentSelected
{
    public override Type ComponentType => GetType();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string InnerText => GetInnerText();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual string Value => DefaultGetValue();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsSelected => WrappedElement.Evaluate<bool>("el => el.selected");

    public virtual void Select()
    {
        WrappedElement.Evaluate("el => el.selected = true");
    }

    public virtual void UnSelect()
    {
        WrappedElement.Evaluate("el => el.selected = false");
    }
}