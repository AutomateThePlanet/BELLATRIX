// <copyright file="Select.cs" company="Automate The Planet Ltd.">
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
using System.Globalization;
using System.Text;
using Bellatrix.Playwright.Contracts;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public class Select : Component, IComponentDisabled, IComponentRequired, IComponentReadonly
{
    public static event EventHandler<ComponentActionEventArgs> Hovering;
    public static event EventHandler<ComponentActionEventArgs> Hovered;
    public static event EventHandler<ComponentActionEventArgs> Selecting;
    public static event EventHandler<ComponentActionEventArgs> Selected;
    public static event EventHandler<ComponentActionEventArgs> FailedSelection;

    public override Type ComponentType => GetType();

    public virtual void Hover()
    {
        Hover(Hovering, Hovered);
    }

    public virtual Option GetSelected()
    {
        string optionValue = WrappedElement.Evaluate<string>("selectElement => {" +
         "    const selectedOption = selectElement.options[selectElement.selectedIndex];" +
         "    return selectedOption.getAttribute('value');" +
         "}");

        if (string.IsNullOrEmpty(optionValue))
        {
            throw new ArgumentException("Couldn't find the selected option's value.");
        }


        return this.CreateByXpath<Option>($"//option[@value='{optionValue}']");
    }

    public virtual ComponentsList<Option> GetAllOptions()
    {
        return this.CreateAllByXpath<Option>(".//option");
    }

    public virtual void SelectByText(string text)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, text));

        InternalSelect(new SelectOptionValue() { Label = text }, text);

        Selected?.Invoke(this, new ComponentActionEventArgs(this, text));

    }

    public virtual void SelectByValue(string value)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, value));

        InternalSelect(new SelectOptionValue() { Value = value }, value);

        Selected?.Invoke(this, new ComponentActionEventArgs(this, value));

    }

    public virtual void SelectByIndex(int index)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs(this, index.ToString()));

        InternalSelect(new SelectOptionValue() { Index = index }, index);

        Selected?.Invoke(this, new ComponentActionEventArgs(this, GetSelected().GetInnerText()));
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetDisabledAttribute();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsRequired => GetRequiredAttribute();
    public virtual bool IsReadonly => GetReadonlyAttribute();

    private void InternalSelect(SelectOptionValue option, string by)
    {
        try
        {
            var optionValue = WrappedElement.SelectOption(option)[0];
            if (string.IsNullOrEmpty(optionValue)) throw new ArgumentException("Returning option value was empty, something went wrong during selection.");
        }

        catch
        {
            FailedSelection.Invoke(this, new ComponentActionEventArgs(this, by));

            throw;
        }
    }

    private void InternalSelect(SelectOptionValue option, int by)
    {
        try
        {
            var optionValue = WrappedElement.SelectOption(option)[0];
            if (string.IsNullOrEmpty(optionValue)) throw new ArgumentException("Returning option value was empty, something went wrong during selection.");
        }

        catch
        {
            FailedSelection.Invoke(this, new ComponentActionEventArgs(this, by.ToString()));

            throw;
        }
    }
}