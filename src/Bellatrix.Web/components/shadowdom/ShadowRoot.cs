// <copyright file="ShadowRoot.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>

using Bellatrix.Web.Components.ShadowDom;
using Bellatrix.Web.Contracts;
using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace Bellatrix.Web.Components;
public class ShadowRoot : Component, IComponentInnerHtml
{
    public override IWebElement WrappedElement
    {
        get => new ShadowSearchContext((WebElement)base.WrappedElement);
        set => base.WrappedElement = value;
    }

    public string InnerHtml => ShadowDomService.GetShadowHtml(this);

    private class ShadowSearchContext : WebElement
    {
        public ShadowSearchContext(WebElement element) : base(WebDriver(element), ElementId(element))
        {
        }

        public override IWebElement FindElement(By by) => GetShadowRoot().FindElement(by);

        public override ReadOnlyCollection<IWebElement> FindElements(By by) => GetShadowRoot().FindElements(by);

        private static string ElementId(WebElement element) => (string)typeof(WebElement).GetField("elementId", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(element);

        private static WebDriver WebDriver(WebElement element) => (WebDriver)typeof(WebElement).GetField("driver", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(element);
    }
}
