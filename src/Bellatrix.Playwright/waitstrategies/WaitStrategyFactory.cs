﻿// <copyright file="WaitStrategyFactory.cs" company="Automate The Planet Ltd.">
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

using Bellatrix.Playwright.WaitStrategies;

namespace Bellatrix.Playwright;

internal class WaitStrategyFactory
{
    internal WaitToExistStrategy Exists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToExistStrategy(timeoutInterval, sleepinterval);

    internal WaitNotToExistStrategy NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitNotToExistStrategy(timeoutInterval, sleepinterval);

    internal WaitToBeVisibleStrategy BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeVisibleStrategy(timeoutInterval, sleepinterval);

    internal WaitToNotBeVisibleStrategy BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToNotBeVisibleStrategy(timeoutInterval, sleepinterval);

    internal WaitToBeClickableStrategy BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeClickableStrategy(timeoutInterval, sleepinterval);

    internal WaitToHaveContentStrategy HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToHaveContentStrategy(timeoutInterval, sleepinterval);

    internal WaitToBeDisabledStrategy BeDisabled(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeDisabledStrategy(timeoutInterval, sleepinterval);
}