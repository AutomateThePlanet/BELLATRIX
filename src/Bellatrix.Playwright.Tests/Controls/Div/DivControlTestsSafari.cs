﻿// <copyright file="DivControlTestsSafari.cs" company="Automate The Planet Ltd.">
// Copyright 2025 Automate The Planet Ltd.
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Webkit, Lifecycle.ReuseIfStarted)]
[AllureSuite("Div Control")]
public class DivControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DivPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var divElement = App.Components.CreateById<Div>("myDiv");

        divElement.Hover();

        Assert.AreEqual("color: red;", divElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnAutomateThePlanet_When_InnerText_Safari()
    {
        var divElement = App.Components.CreateById<Div>("myDiv1");

        Assert.AreEqual("Automate The Planet", divElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNull_When_InnerTextNotSet_Safari()
    {
        var divElement = App.Components.CreateById<Div>("myDiv3");

        Assert.IsNotNull(divElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNull_When_InnerHtmlNotSet_Safari()
    {
        var divElement = App.Components.CreateById<Div>("myDiv3");

        Assert.IsNotNull(divElement.InnerHtml);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnButtonHtml_When_InnerHtmlSet_Safari()
    {
        var divElement = App.Components.CreateById<Div>("myDiv2");

        Assert.AreEqual("<button name=\"button\">Click me</button>", divElement.InnerHtml);
    }
}