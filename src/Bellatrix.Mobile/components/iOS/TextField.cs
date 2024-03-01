// <copyright file="TextField.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Events;

namespace Bellatrix.Mobile.IOS;

public class TextField : IOSComponent, IComponentDisabled, IComponentText
{
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> SettingText;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> TextSet;

    public virtual void SetText(string value)
    {
        SetValue(SettingText, TextSet, value);
    }

    public new virtual string GetText()
    {
        string textValue = GetValueAttribute();
        return textValue ?? string.Empty;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();
}