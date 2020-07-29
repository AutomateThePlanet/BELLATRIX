// <copyright file="LoadTestLocator.cs" company="Automate The Planet Ltd.">
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
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace Bellatrix.LoadTesting.Model.Locators
{
    public abstract class LoadTestLocator
    {
        public abstract string LocatorType { get; }
        public abstract LoadTestElement LocateElement(HtmlDocument htmlDoc, string locatorValue);

        protected void ThrowNewNotFoundElementException(IEnumerable<HtmlNode> htmlNodes, string locatorValue)
        {
            if (htmlNodes == null || !htmlNodes.Any())
            {
                // TODO: add new special exception type.
                throw new System.Exception($"Element with locator {LocatorType}={locatorValue} was not found.");
            }
        }

        protected void ThrowNewNotFoundElementException(HtmlNode htmlNode, string locatorValue)
        {
            if (htmlNode == null)
            {
                // TODO: add new special exception type.
                throw new System.Exception($"Element with locator {LocatorType}={locatorValue} was not found.");
            }
        }
    }
}
