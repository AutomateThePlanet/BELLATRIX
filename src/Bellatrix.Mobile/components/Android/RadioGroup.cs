// <copyright file="RadioGroup.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Locators.Android;
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Android;

public class RadioGroup : AndroidComponent
{
    public virtual void ClickByText(string text)
    {
        var allRadioButton = GetAll();
        foreach (var radioButton in allRadioButton)
        {
            if (radioButton.GetText().Equals(text))
            {
                radioButton.Click();
                break;
            }
        }
    }

    public virtual void ClickByIndex(int index)
    {
        var allRadioButton = GetAll();
        if (index > allRadioButton.Count() - 1)
        {
            throw new ArgumentException($"Only {allRadioButton.Count()} radio buttons were present which is less than the specified index = {index}.");
        }

        int currentIndex = 0;
        foreach (var radioButton in allRadioButton)
        {
            if (currentIndex == index)
            {
                radioButton.Click();
                break;
            }

            currentIndex++;
        }
    }

    public virtual RadioButton GetChecked()
    {
        var clickedRadioButton = this.CreateByXPath<RadioButton>("//*[@checked='true']");
        return clickedRadioButton;
    }

    public virtual ComponentsList<RadioButton, FindClassNameStrategy, AndroidDriver, AppiumElement> GetAll()
    {
        var radioButtons = this.CreateAllByClass<RadioButton>("android.widget.RadioButton");
        return radioButtons;
    }
}