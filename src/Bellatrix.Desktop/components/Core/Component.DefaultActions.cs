// <copyright file="Element.DefaultActions.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Contracts;
using Bellatrix.Desktop.Events;
using Bellatrix.Layout;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Bellatrix.Desktop;

public partial class Component : IComponentVisible, IComponent, ILayoutComponent
{
    internal virtual void Click(EventHandler<ComponentActionEventArgs> clicking, EventHandler<ComponentActionEventArgs> clicked)
    {
        clicking?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Click();

        clicked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    internal virtual void Hover(EventHandler<ComponentActionEventArgs> hovering, EventHandler<ComponentActionEventArgs> hovered)
    {
        hovering?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedDriver.Mouse.MouseMove(WrappedElement.Coordinates);

        hovered?.Invoke(this, new ComponentActionEventArgs(this));
    }

    internal virtual string GetInnerText()
    {
        return WrappedElement.Text.Replace("\r\n", string.Empty);
    }

    internal virtual bool GetIsDisabled()
    {
        return !WrappedElement.Enabled;
    }

    internal virtual void SetText(EventHandler<ComponentActionEventArgs> settingValue, EventHandler<ComponentActionEventArgs> valueSet, string value)
    {
        settingValue?.Invoke(this, new ComponentActionEventArgs(this, value));
        WrappedElement.Clear();
        WrappedElement.SendKeys(value);
        valueSet?.Invoke(this, new ComponentActionEventArgs(this, value));
    }
}