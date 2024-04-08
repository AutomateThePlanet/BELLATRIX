// <copyright file="Component.DefaultActions.cs" company="Automate The Planet Ltd.">
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
using System.Drawing;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public partial class Component
{
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Point Location => new((int)WrappedElement.BoundingBox().X, (int)WrappedElement.BoundingBox().Y);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public Size Size => new((int)WrappedElement.BoundingBox().Width, (int)WrappedElement.BoundingBox().Height);

    public string GetCssValue(string propertyName) => WrappedElement.Evaluate<string>($"element => window.getComputedStyle(element).getPropertyValue('{propertyName}')");

    protected virtual void DefaultSetAttribute(string attributeName, string attributeValue)
    {
        SettingAttribute?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Evaluate($"el => el.{attributeName} = '{attributeValue}'");

        AttributeSet?.Invoke(this, new ComponentActionEventArgs(this));
    }

    protected virtual string GetClassAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("class")) ? null : GetAttribute("class");
    }

    internal void DefaultClick(EventHandler<ComponentActionEventArgs> clicking, EventHandler<ComponentActionEventArgs> clicked, LocatorClickOptions options = default)
    {
        clicking?.Invoke(this, new ComponentActionEventArgs(this));
        if (options != null)
        {
            if ((bool)options.Force) PerformJsClick();
            else WrappedElement.Click(options);
        }
        
        else WrappedElement.Click();

        clicked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    private void PerformJsClick() => WrappedElement.Evaluate("el => el.click()");

    internal void DefaultCheck(EventHandler<ComponentActionEventArgs> checking, EventHandler<ComponentActionEventArgs> @checked, LocatorCheckOptions options = default)
    {
        checking?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Check(options);

        @checked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    internal void DefaultUncheck(EventHandler<ComponentActionEventArgs> unchecking, EventHandler<ComponentActionEventArgs> @unchecked, LocatorUncheckOptions options = default)
    {
        unchecking?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Uncheck(options);


        @unchecked?.Invoke(this, new ComponentActionEventArgs(this));
    }

    internal void Hover(EventHandler<ComponentActionEventArgs> hovering, EventHandler<ComponentActionEventArgs> hovered)
    {
        hovering?.Invoke(this, new ComponentActionEventArgs(this));

        WrappedElement.Hover();

        hovered?.Invoke(this, new ComponentActionEventArgs(this));
    }

    internal string GetInnerText()
    {
        return WrappedElement.InnerText().Trim().Replace("\r\n", string.Empty);
    }

    internal void SetValue(EventHandler<ComponentActionEventArgs> gettingValue, EventHandler<ComponentActionEventArgs> gotValue, string value)
    {
        gettingValue?.Invoke(this, new ComponentActionEventArgs(this, value));
        SetAttribute("value", value);
        gotValue?.Invoke(this, new ComponentActionEventArgs(this, value));
    }

    internal string DefaultGetValue()
    {
        return GetAttribute("value");
    }

    internal int? DefaultGetMaxLength()
    {
        int? result = string.IsNullOrEmpty(GetAttribute("maxlength")) ? null : (int?)int.Parse(GetAttribute("maxlength"));
        if (result != null && (result == 2147483647 || result == -1))
        {
            result = null;
        }

        return result;
    }

    internal int? DefaultGetMinLength()
    {
        int? result = string.IsNullOrEmpty(GetAttribute("minlength")) ? null : (int?)int.Parse(GetAttribute("minlength"));

        if (result != null && result == -1)
        {
            result = null;
        }

        return result;
    }

    internal int? GetSizeAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("size")) ? null : (int?)int.Parse(GetAttribute("size"));
    }

    internal int? GetHeightAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("height")) ? null : (int?)int.Parse(GetAttribute("height"));
    }

    internal int? GetWidthAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("width")) ? null : (int?)int.Parse(GetAttribute("width"));
    }

    internal string GetInnerHtmlAttribute()
    {
        return WrappedElement.InnerHTML();
    }

    internal string GetForAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("for")) ? null : GetAttribute("for");
    }

    protected bool GetDisabledAttribute()
    {
        return WrappedElement.IsDisabled();
    }

    internal string GetText()
    {
        return WrappedElement.InnerText();
    }

    internal int? GetMinAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("min")) ? null : (int?)int.Parse(GetAttribute("min"));
    }

    internal int? GetMaxAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("max")) ? null : (int?)int.Parse(GetAttribute("max"));
    }

    internal string GetMinAttributeAsString()
    {
        return string.IsNullOrEmpty(GetAttribute("min")) ? null : GetAttribute("min");
    }

    internal string GetMaxAttributeAsString()
    {
        return string.IsNullOrEmpty(GetAttribute("max")) ? null : GetAttribute("max");
    }

    internal int? GetStepAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("step")) ? null : (int?)int.Parse(GetAttribute("step"));
    }

    internal string GetPlaceholderAttribute()
    {
        return string.IsNullOrEmpty(GetAttribute("placeholder")) ? null : GetAttribute("placeholder");
    }

    internal bool GetAutoCompleteAttribute()
    {
        return GetAttribute("autocomplete") == "on";
    }

    internal bool GetReadonlyAttribute()
    {
        return !string.IsNullOrEmpty(GetAttribute("readonly"));
    }

    internal bool GetRequiredAttribute()
    {
        return !string.IsNullOrEmpty(GetAttribute("required"));
    }

    internal string GetList()
    {
        return string.IsNullOrEmpty(GetAttribute("list")) ? null : GetAttribute("list");
    }

    internal void DefaultSetText(EventHandler<ComponentActionEventArgs> settingValue, EventHandler<ComponentActionEventArgs> valueSet, string value, LocatorFillOptions options = default)
    {
        settingValue?.Invoke(this, new ComponentActionEventArgs(this, value));

        WrappedElement.Fill(value, options);

        valueSet?.Invoke(this, new ComponentActionEventArgs(this, value));
    }

    private void ScrollToVisible(bool shouldWait = true)
    {
        ScrollingToVisible?.Invoke(this, new ComponentActionEventArgs(this));

        try
        {
            WrappedElement.ScrollIntoViewIfNeeded();
        }
        catch (Exception)
        {
            // ignore
        }

        ScrolledToVisible?.Invoke(this, new ComponentActionEventArgs(this));
    }
}