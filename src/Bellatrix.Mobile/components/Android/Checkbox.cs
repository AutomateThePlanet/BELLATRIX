// <copyright file="Checkbox.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.Android;
using Bellatrix.Mobile.Events;

namespace Bellatrix.Mobile.Android
{
   public class CheckBox : Element, IElementDisabled, IElementChecked, IElementText
    {
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Checking;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Checked;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Unchecking;
        public static event EventHandler<ElementActionEventArgs<OpenQA.Selenium.Appium.Android.AndroidElement>> Unchecked;

        public void Check(bool isChecked = true)
        {
            bool isElementChecked = GetIsChecked();
            if (isChecked && !isElementChecked || !isChecked && isElementChecked)
            {
                Click(Checking, Checked);
            }
        }

        public void Uncheck()
        {
            bool isChecked = GetIsChecked();
            if (isChecked)
            {
                Click(Unchecking, Unchecked);
            }
        }

        public string GetText()
        {
            return GetText();
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsDisabled => GetIsDisabled();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public bool IsChecked => GetIsChecked();
    }
}