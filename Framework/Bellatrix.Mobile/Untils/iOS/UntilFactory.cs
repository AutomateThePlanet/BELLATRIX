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
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile.Untils.IOS
{
    public class UntilFactory
    {
        public UntilExist<IOSDriver<IOSElement>, IOSElement> Exists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilExist<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);

        public UntilNotExist<IOSDriver<IOSElement>, IOSElement> NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotExist<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);

        public UntilBeVisible<IOSDriver<IOSElement>, IOSElement> BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeVisible<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);

        public UntilNotBeVisible<IOSDriver<IOSElement>, IOSElement> BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotBeVisible<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);

        public UntilBeClickable<IOSDriver<IOSElement>, IOSElement> BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeClickable<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);

        public UntilHaveContent<IOSDriver<IOSElement>, IOSElement> HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new UntilHaveContent<IOSDriver<IOSElement>, IOSElement>(timeoutInterval, sleepinterval);
    }
}
