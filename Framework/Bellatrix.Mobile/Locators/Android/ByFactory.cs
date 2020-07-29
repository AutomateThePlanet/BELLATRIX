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
using Bellatrix.Mobile.Locators.Android;

namespace Bellatrix.Mobile.Android
{
    public class ByFactory
    {
        public ById Id(string id) => new ById(id);

        public ByIdContaining IdContaining(string id) => new ByIdContaining(id);

        public ByDescription Description(string description) => new ByDescription(description);

        public ByDescriptionContaining DescriptionContaining(string description) => new ByDescriptionContaining(description);

        public ByText Text(string text) => new ByText(text);

        public ByTextContaining TextContaining(string text) => new ByTextContaining(text);

        public ByClassName ClassName(string className) => new ByClassName(className);

        public ByName Name(string name) => new ByName(name);

        public ByTagName TagName(string tag) => new ByTagName(tag);

        public ByXPath XPath(string name) => new ByXPath(name);

        public ByAndroidUIAutomator AndroidUIAutomator(string name) => new ByAndroidUIAutomator(name);
    }
}
