// <copyright file="Checkbox.cs" company="Automate The Planet Ltd.">
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
using System.Diagnostics;
using Bellatrix.Mobile.Contracts;
using Bellatrix.Mobile.Controls.IOS;
using Bellatrix.Mobile.Events;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.IOS
{
   public class CheckBox : Element, IElementDisabled, IElementChecked, IElementText
    {
        public static Func<CheckBox, bool> OverrideIsDisabledGlobally;
        public static Func<CheckBox, bool> OverrideIsCheckedGlobally;
        public static Action<CheckBox, bool> OverrideCheckGlobally;
        public static Action<CheckBox> OverrideUncheckGlobally;
        public static Func<CheckBox, string> OverrideGetTextGlobally;

        public static Func<CheckBox, bool> OverrideIsDisabledLocally;
        public static Func<CheckBox, bool> OverrideIsCheckedLocally;
        public static Action<CheckBox, bool> OverrideCheckLocally;
        public static Action<CheckBox> OverrideUncheckLocally;
        public static Func<CheckBox, string> OverrideGetTextLocally;

        public static event EventHandler<ElementActionEventArgs<IOSElement>> Checking;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> Checked;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> Unchecking;
        public static event EventHandler<ElementActionEventArgs<IOSElement>> Unchecked;

        public static new void ClearLocalOverrides()
        {
            OverrideIsDisabledLocally = null;
            OverrideIsCheckedLocally = null;
            OverrideCheckLocally = null;
            OverrideUncheckLocally = null;
            OverrideGetTextLocally = null;
        }

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