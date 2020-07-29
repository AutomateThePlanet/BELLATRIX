// <copyright file="RadioGroup.cs" company="Automate The Planet Ltd.">
// Copyright 2020 Automate The Planet Ltd.
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
using Bellatrix.Mobile.Controls.Core;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
    public class RadioGroup : Element
    {
        public static Func<RadioGroup, RadioButton> OverrideGetCheckedGlobally;
        public static Func<RadioGroup, ElementsList<RadioButton, ByClassName, IOSDriver<IOSElement>, IOSElement>> OverrideGetAllGlobally;
        public static Action<RadioGroup, string> OverrideClickByTextGlobally;
        public static Action<RadioGroup, int> OverrideClickByIndexGlobally;

        public static Func<RadioGroup, RadioButton> OverrideGetCheckedLocally;
        public static Func<RadioGroup, ElementsList<RadioButton, ByClassName, IOSDriver<IOSElement>, IOSElement>> OverrideGetAllLocally;
        public static Action<RadioGroup, string> OverrideClickByTextLocally;
        public static Action<RadioGroup, int> OverrideClickByIndexLocally;

        public static new void ClearLocalOverrides()
        {
            OverrideGetCheckedLocally = null;
            OverrideGetAllLocally = null;
            OverrideClickByTextLocally = null;
            OverrideClickByIndexLocally = null;
        }

        public void ClickByText(string text)
        {
            var action = InitializeAction(this, OverrideClickByTextGlobally, OverrideClickByTextLocally, DefaultClickByText);
            action(text);
        }

        public void ClickByIndex(int index)
        {
            var action = InitializeAction(this, OverrideClickByIndexGlobally, OverrideClickByIndexLocally, DefaultClickByIndex);
            action(index);
        }

        public RadioButton GetChecked()
        {
            var action = InitializeAction(this, OverrideGetCheckedGlobally, OverrideGetCheckedLocally, DefaultSelectedValue);
            return action();
        }

        public ElementsList<RadioButton, ByClassName, IOSDriver<IOSElement>, IOSElement> GetAll()
        {
            var action = InitializeAction(this, OverrideGetAllGlobally, OverrideGetAllLocally, DefaultGetAllOptions);
            return action();
        }

        protected virtual ElementsList<RadioButton, ByClassName, IOSDriver<IOSElement>, IOSElement> DefaultGetAllOptions(RadioGroup radioGroup)
        {
            var radioButtons = radioGroup.CreateAllByClass<RadioButton>("XCUIElementTypeSwitch");

            return radioButtons;
        }

        protected virtual RadioButton DefaultSelectedValue(RadioGroup radioGroup)
        {
            var clickedRadioButton = radioGroup.CreateByXPath<RadioButton>("//*[@value='1']");

            return clickedRadioButton;
        }

        protected virtual void DefaultClickByText(RadioGroup radioGroup, string value)
        {
            var allRadioButton = GetAll();
            foreach (var radioButton in allRadioButton)
            {
                if (radioButton.GetText().Equals(value))
                {
                    radioButton.Click();
                    break;
                }
            }
        }

        protected virtual void DefaultClickByIndex(RadioGroup radioGroup, int index)
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
    }
}