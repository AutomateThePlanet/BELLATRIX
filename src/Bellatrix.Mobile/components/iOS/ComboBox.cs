// <copyright file="ComboBox.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS;

public class ComboBox : IOSComponent, IComponentDisabled, IComponentText
{
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Selecting;
    public static event EventHandler<ComponentActionEventArgs<AppiumElement>> Selected;

    public virtual void SelectByText(string value)
    {
        Selecting?.Invoke(this, new ComponentActionEventArgs<AppiumElement>(this, value));

        if (WrappedElement.Text != value)
        {
            WrappedElement.Click();
            var elementCreateService = ServicesCollection.Current.Resolve<ComponentCreateService>();
            var innerElementToClick = elementCreateService.CreateByValueContaining<RadioButton>(value);
            innerElementToClick.Click();
        }

        Selected?.Invoke(this, new ComponentActionEventArgs<AppiumElement>(this, value));
    }

    public new virtual string GetText()
    {
        var result = GetText();
        if (string.IsNullOrEmpty(result))
        {
            var textField = this.CreateByClass<TextField>("android.widget.TextView");
            result = textField.GetText();
        }

        return result;
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public virtual bool IsDisabled => GetIsDisabled();
}