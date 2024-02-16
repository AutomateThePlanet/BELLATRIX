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
using System.Collections.Generic;
using Bellatrix.Layout;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium;

namespace Bellatrix.Mobile.Core;

public partial class Component<TDriver, TDriverElement> : IComponentVisible, ILayoutComponent
{
    internal void Click(EventHandler<ComponentActionEventArgs<TDriverElement>> clicking, EventHandler<ComponentActionEventArgs<TDriverElement>> clicked)
    {
        clicking?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));

        WrappedElement.Click();

        clicked?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this));
    }

    internal bool GetIsCheckedValue()
    {
        string value = WrappedElement.GetAttribute("value");
        if (value == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    internal bool GetIsChecked()
    {
        return bool.Parse(WrappedElement.GetAttribute("checked"));
    }

    internal string GetText()
    {
        return WrappedElement.Text.Replace("\r\n", string.Empty);
    }

    internal string GetValueAttribute()
    {
        return WrappedElement.GetAttribute("value");
    }

    internal bool GetIsDisabled()
    {
        return !WrappedElement.Enabled;
    }

    internal void SetValue(EventHandler<ComponentActionEventArgs<TDriverElement>> settingValue, EventHandler<ComponentActionEventArgs<TDriverElement>> valueSet, string value)
    {
        settingValue?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this, value));
        WrappedElement.Clear();
        WrappedElement.SendKeys(value);
        valueSet?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this, value));
    }

    internal void SetText(EventHandler<ComponentActionEventArgs<TDriverElement>> settingValue, EventHandler<ComponentActionEventArgs<TDriverElement>> valueSet, string value)
    {
        settingValue?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this, value));
        WrappedElement.Clear();
        WrappedElement.SendKeys(value);

        try
        {
            WrappedDriver.HideKeyboard();
        }
        catch
        {
            // ignore
        }

        valueSet?.Invoke(this, new ComponentActionEventArgs<TDriverElement>(this, value));
    }

    private void MobileScroll(ScrollDirection direction)
    {
        var js = (IJavaScriptExecutor)WrappedDriver;
        var swipeObject = new Dictionary<string, string>();
        switch (direction)
        {
            case ScrollDirection.Up:
                swipeObject.Add("direction", "up");
                break;
            case ScrollDirection.Down:
                swipeObject.Add("direction", "down");
                break;
            case ScrollDirection.Right:
                swipeObject.Add("direction", "right");
                break;
            case ScrollDirection.Left:
                swipeObject.Add("direction", "left");
                break;
        }

        swipeObject.Add("element", WrappedElement.Id);
        js.ExecuteScript("mobile:swipe", swipeObject);
    }
}