// <copyright file="AppiumDriverWait.cs" company="Automate The Planet Ltd.">
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
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;

namespace Bellatrix.Mobile.Untils;

public class AppiumDriverWait<TDriver, TDriverElement> : DefaultWait<TDriver>
    where TDriver : AppiumDriver
    where TDriverElement : AppiumElement
{
    public AppiumDriverWait(TDriver driver, IClock clock, TimeSpan timeout, TimeSpan sleepInterval)
        : base(driver, clock)
    {
        Timeout = timeout;
        PollingInterval = sleepInterval;
    }

    public AppiumDriverWait(TDriver driver, IClock clock)
           : base(driver, clock)
    {
    }
}
