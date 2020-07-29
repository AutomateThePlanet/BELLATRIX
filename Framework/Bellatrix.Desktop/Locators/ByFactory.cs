// <copyright file="ByFactory.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Desktop.Locators;

namespace Bellatrix.Desktop
{
    public class ByFactory
    {
        public ById Id(string id) => new ById(id);

        public ByAccessibilityId AccessibilityId(string css) => new ByAccessibilityId(css);

        public ByClassName ClassName(string linkText) => new ByClassName(linkText);

        public ByName Name(string name) => new ByName(name);

        public ByTagName TagName(string tag) => new ByTagName(tag);

        public ByXPath XPath(string name) => new ByXPath(name);

        public ByAutomationId AutomationId(string name) => new ByAutomationId(name);
    }
}
