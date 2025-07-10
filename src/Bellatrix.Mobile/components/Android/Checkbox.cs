﻿// <copyright file="Checkbox.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using System;
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Events;

namespace Bellatrix.Mobile.Android;

public class CheckBox : AndroidComponent, IComponentDisabled, IComponentChecked, IComponentText
{
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Checking;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Checked;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Unchecking;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Unchecked;

    public virtual void Check(bool isChecked = true)
    {
        bool isElementChecked = GetIsChecked();
        if (isChecked && !isElementChecked || !isChecked && isElementChecked)
        {
            Click(Checking, Checked);
        }
    }

    public virtual void Uncheck()
    {
        bool isChecked = GetIsChecked();
        if (isChecked)
        {
            Click(Unchecking, Unchecked);
        }
    }

    public new virtual string GetText()
    {
        return GetText();
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsChecked => GetIsChecked();
}