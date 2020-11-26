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
using Bellatrix.Web.Untils;

namespace Bellatrix.Web
{
    internal class UntilFactory
    {
        internal UntilExist Exists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilExist(timeoutInterval, sleepinterval);

        internal UntilNotExist NotExists(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotExist(timeoutInterval, sleepinterval);

        internal UntilBeVisible BeVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeVisible(timeoutInterval, sleepinterval);

        internal UntilNotBeVisible BeNotVisible(int? timeoutInterval = null, int? sleepinterval = null) => new UntilNotBeVisible(timeoutInterval, sleepinterval);

        internal UntilBeClickable BeClickable(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeClickable(timeoutInterval, sleepinterval);

        internal UntilHaveContent HasContent(int? timeoutInterval = null, int? sleepinterval = null) => new UntilHaveContent(timeoutInterval, sleepinterval);

        internal UntilBeDisabled BeDisabled(int? timeoutInterval = null, int? sleepinterval = null) => new UntilBeDisabled(timeoutInterval, sleepinterval);
    }
}