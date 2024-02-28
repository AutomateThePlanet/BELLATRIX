// <copyright file="WaitStrategyFactory.cs" company="Automate The Planet Ltd.">
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
namespace Bellatrix.Desktop.Untils;

public class WaitStrategyFactory
{
    public WaitToExistStrategy Exists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToExistStrategy(timeoutInterval, sleepinterval);

    public WaitNotExistStrategy NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new WaitNotExistStrategy(timeoutInterval, sleepinterval);

    public WaitToBeVisibleStrategy BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeVisibleStrategy(timeoutInterval, sleepinterval);

    public WaitNotBeVisibleStrategy BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new WaitNotBeVisibleStrategy(timeoutInterval, sleepinterval);

    public WaitToBeClickable BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToBeClickable(timeoutInterval, sleepinterval);

    public WaitToHaveContentStrategy HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new WaitToHaveContentStrategy(timeoutInterval, sleepinterval);
}
