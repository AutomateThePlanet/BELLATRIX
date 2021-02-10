// <copyright file="Element.DefaultActions.cs" company="Automate The Planet Ltd.">
// Copyright 2021 Automate The Planet Ltd.
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

namespace Bellatrix.Desktop
{
    public partial class Element : IElementVisible, IElement, ILayoutElement
    {
        internal void Click(EventHandler<ElementActionEventArgs> clicking, EventHandler<ElementActionEventArgs> clicked)
        {
            clicking?.Invoke(this, new ElementActionEventArgs(this));

            WrappedElement.Click();

            clicked?.Invoke(this, new ElementActionEventArgs(this));
        }

        internal void Hover(EventHandler<ElementActionEventArgs> hovering, EventHandler<ElementActionEventArgs> hovered)
        {
            hovering?.Invoke(this, new ElementActionEventArgs(this));

            WrappedDriver.Mouse.MouseMove(WrappedElement.Coordinates);

            hovered?.Invoke(this, new ElementActionEventArgs(this));
        }

        internal string GetInnerText()
            => WrappedElement.Text.Replace("\r\n", string.Empty);

        internal bool GetIsDisabled()
            => !WrappedElement.Enabled;

        internal void SetText(EventHandler<ElementActionEventArgs> settingValue, EventHandler<ElementActionEventArgs> valueSet, string value)
        {
            settingValue?.Invoke(this, new ElementActionEventArgs(this, value));
            WrappedElement.Clear();
            WrappedElement.SendKeys(value);
            valueSet?.Invoke(this, new ElementActionEventArgs(this, value));
        }
    }
}