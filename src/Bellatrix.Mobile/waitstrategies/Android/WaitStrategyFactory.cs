// <copyright file="UntilFactory.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium.Android;

namespace Bellatrix.Mobile.Untils.Android;

public class WaitStrategyFactory
{
    public WaitToExistStrategy<AndroidDriver, AppiumElement> Exists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToExistStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);

    public WaitNotExistStrategy<AndroidDriver, AppiumElement> NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitNotExistStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);

    public WaitToBeVisibleStrategy<AndroidDriver, AppiumElement> BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeVisibleStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);

    public WaitNotBeVisibleStrategy<AndroidDriver, AppiumElement> BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitNotBeVisibleStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);

    public WaitToBeClickableStrategy<AndroidDriver, AppiumElement> BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeClickableStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);

    public WaitToHaveContentStrategy<AndroidDriver, AppiumElement> HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToHaveContentStrategy<AndroidDriver, AppiumElement>(timeoutInterval, sleepinterval);
}
