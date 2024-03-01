// <copyright file="HeadingControlEventsTests.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Web.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Heading Control")]
[AllureFeature("ControlEvents")]
public class HeadingControlEventsTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().HeadingLocalPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveringCalled_BeforeActuallyHover()
    {
        Heading.Hovering += AssertStyleAttributeEmpty;

        var headingElement = App.Components.CreateById<Heading>("myHeading");

        headingElement.Hover();

        Assert.AreEqual("color: red;", headingElement.GetStyle());

        Heading.Hovering -= AssertStyleAttributeEmpty;

        void AssertStyleAttributeEmpty(object sender, ComponentActionEventArgs args)
        {
            Assert.AreEqual(string.Empty, args.Element.WrappedElement.GetAttribute("style"));
        }
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void HoveredCalled_AfterHover()
    {
        Heading.Hovered += AssertStyleAttributeContainsNewValue;

        var headingElement = App.Components.CreateById<Heading>("myHeading");

        headingElement.Hover();

        Heading.Hovered -= AssertStyleAttributeContainsNewValue;

        void AssertStyleAttributeContainsNewValue(object sender, ComponentActionEventArgs args)
        {
            App.Components.CreateById<Heading>("myHeading").ValidateStyleIs("color: red;");
        }
    }
}