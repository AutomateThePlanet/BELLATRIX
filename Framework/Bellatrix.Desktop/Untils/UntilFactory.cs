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
namespace Bellatrix.Desktop.Untils
{
    public class UntilFactory
    {
        public UntilExist Exists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilExist(timeoutInterval, sleepinterval);

        public UntilNotExist NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotExist(timeoutInterval, sleepinterval);

        public UntilBeVisible BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeVisible(timeoutInterval, sleepinterval);

        public UntilNotBeVisible BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotBeVisible(timeoutInterval, sleepinterval);

        public UntilBeClickable BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeClickable(timeoutInterval, sleepinterval);

        public UntilHaveContent HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new UntilHaveContent(timeoutInterval, sleepinterval);
    }
}
