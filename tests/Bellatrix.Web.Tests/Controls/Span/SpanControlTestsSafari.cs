﻿// <copyright file="SpanControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Safari, Lifecycle.ReuseIfStarted)]
[AllureSuite("Span Control")]
public class SpanControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().SpanPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan");

        spanElement.Hover();

        Assert.AreEqual("color: red;", spanElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnAutomateThePlanet_When_InnerText_Safari()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan1");

        Assert.AreEqual("Automate The Planet", spanElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNull_When_InnerTextNotSet_Safari()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan3");

        Assert.IsNotNull(spanElement.InnerText);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNull_When_InnerHtmlNotSet_Safari()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan3");

        Assert.IsNotNull(spanElement.InnerHtml);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnButtonHtml_When_InnerHtmlSet_Safari()
    {
        var spanElement = App.Components.CreateById<Span>("mySpan2");

        Assert.AreEqual("<button name=\"button\">Click me</button>", spanElement.InnerHtml);
    }
}