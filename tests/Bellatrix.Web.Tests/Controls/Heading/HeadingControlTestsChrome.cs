// <copyright file="HeadingControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Heading Control")]
public class HeadingControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().HeadingLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Chrome()
    {
        var headingElement = App.Components.CreateById<Heading>("myHeading");

        headingElement.Hover();

        Assert.AreEqual("color: red;", headingElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnAutomateThePlanet_When_InnerText_Chrome()
    {
        var headingElement = App.Components.CreateById<Heading>("myHeading2");

        Assert.AreEqual("Automate The Planet", headingElement.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_InnerTextNotSet_Chrome()
    {
        var headingElement = App.Components.CreateById<Heading>("myHeading4");

        Assert.AreEqual(string.Empty, headingElement.InnerText);
    }
}