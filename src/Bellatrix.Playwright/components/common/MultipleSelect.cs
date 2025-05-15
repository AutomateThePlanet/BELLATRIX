// <copyright file="MultipleSelect.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;
using Bellatrix.Playwright.Settings;
using Microsoft.TeamFoundation.Common;
using System.Diagnostics;

namespace Bellatrix.Playwright.Components.Common;
public class MultipleSelect : Component, IComponentDisabled, IComponentRequired, IComponentReadonly, IComponentMultiple
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Selecting;
    public static event EventHandler<ComponentActionEventArgs> Selected;
    public static event EventHandler<ComponentActionEventArgs> FailedSelection;
    public static event EventHandler<ComponentActionEventArgs> SelectedNotFound;

    public override Type ComponentType => GetType();

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    public virtual ComponentsList<Option> GetAllSelected()
    {
        string[] optionValues = WrappedElement.Evaluate<string[]>("el => {" +
                "var selectedOptions = [];" +
                "for (var i = 0; i < el.options.length; i++) {" +
                    "if (el.options[i].selected) selectedOptions.push(el.options[i].value);" +
                "}" +
                "return selectedOptions;" +
            "}");

        if (optionValues.IsNullOrEmpty())
        {
            SelectedNotFound?.Invoke(this, new ComponentActionEventArgs(this));
        }

        ComponentsList<Option> options = new ComponentsList<Option>();

        foreach(string value in optionValues)
        {
            options.Add(this.CreateByXpath<Option>($"//option[@value='{value}']"));
        }

        return options;
    }

    public virtual ComponentsList<Option> GetAllOptions()
    {
        return this.CreateAllByXpath<Option>("//option");
    }

    public virtual Option SelectByText(string text)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, text));

        var selectedValue = InternalSelect($"//option[normalize-space()='{text}']");

        Selected?.Invoke(this, new ComponentActionEventArgs(this, text));

        return selectedValue;

    }

    public virtual Option SelectByValue(string value)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, value));

        var selectedValue = InternalSelect($"//option[@value='{value}']");

        Selected?.Invoke(this, new ComponentActionEventArgs(this, value));

        return selectedValue;

    }

    public virtual Option SelectByIndex(int index)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, index.ToString()));

        var selectedValue = InternalSelect($"//option[{index}]");

        Selected?.Invoke(this, new ComponentActionEventArgs(this, selectedValue.Value));

        return selectedValue;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsReadonly => GetReadonlyAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsMultiple
    {
        get
        {
            var multipleAttribute = GetAttribute("multiple");

            // If the <select> element has no such attribute, return false
            if (multipleAttribute == null) return false;

            // If the <select> element has multiple, but this multiple has no value, return true, as it is still 'multiple'
            if (multipleAttribute == string.Empty) return true;

            // Return the value of the 'multiple' attribute, if it has such.
            return bool.Parse(multipleAttribute.ToLower());

        }
    }

    public void DeselectAll()
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect all options if multi-select is supported.");
        }

        var selected = GetAllSelected();

        foreach (var option in selected)
        {
            option.UnSelect();
        }
    }

    public void DeselectByText(string text)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        var optionsToDeselect = this.CreateAllByXpath<Option>($"//option[normalize-space()='{text}']");

        if (optionsToDeselect.IsNullOrEmpty()) throw new ArgumentException($"Couldn't find options with text {text}.");

        foreach (var option in optionsToDeselect)
        {
            option.UnSelect();
        }
    }

    public void DeselectByValue(string value)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        var optionsToDeselect = this.CreateAllByXpath<Option>($"//option[@value='{value}']");

        if (optionsToDeselect.IsNullOrEmpty()) throw new ArgumentException($"Couldn't find options with value {value}.");

        foreach (var option in optionsToDeselect)
        {
            option.UnSelect();
        }
    }

    public void DeselectByIndex(int index)
    {
        if (!IsMultiple)
        {
            throw new InvalidOperationException("You may only deselect option if multi-select is supported");
        }

        try
        {
            this.WrappedElement.Evaluate($"el => el.options[{index}].selected = false;");
        }
        catch (Exception ex)
        {
            DebugInformation.PrintStackTrace(ex);
            throw new ArgumentException("Cannot locate option with index: " + index);
        }
    }

    private Option InternalSelect(string xpath)
    {
        try
        {
            var selectedValue = this.CreateByXpath<Option>(xpath);
            selectedValue.Select();

            return selectedValue;
        }

        catch
        {
            FailedSelection.Invoke(this, new ComponentActionEventArgs(this, xpath));

            throw;
        }
    }
}
