// <copyright file="UntilFactory.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Untils.Android
{
    public class UntilFactory
    {
        public UntilExist<AndroidDriver<AndroidElement>, AndroidElement> Exists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilExist<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);

        public UntilNotExist<AndroidDriver<AndroidElement>, AndroidElement> NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotExist<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);

        public UntilBeVisible<AndroidDriver<AndroidElement>, AndroidElement> BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeVisible<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);

        public UntilNotBeVisible<AndroidDriver<AndroidElement>, AndroidElement> BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotBeVisible<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);

        public UntilBeClickable<AndroidDriver<AndroidElement>, AndroidElement> BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeClickable<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);

        public UntilHaveContent<AndroidDriver<AndroidElement>, AndroidElement> HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new UntilHaveContent<AndroidDriver<AndroidElement>, AndroidElement>(timeoutInterval, sleepinterval);
    }
}
