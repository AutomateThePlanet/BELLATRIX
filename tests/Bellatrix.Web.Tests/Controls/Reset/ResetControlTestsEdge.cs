﻿// <copyright file="ResetControlTestsEdge.cs" company="Automate The Planet Ltd.">
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
// <author>Anton Angelov</author>
// <site>https://bellatrix.solutions/</site>
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Reset Control")]
[AllureFeature("Edge Browser")]
public class ResetControlTestsEdge : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ResetPage);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void SetTextToStop_When_UseClickMethod_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");

        buttonElement.Click();

        Assert.AreEqual("Stop", buttonElement.Value);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnRed_When_Hover_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton1");

        buttonElement.Hover();

        Assert.AreEqual("color: red;", buttonElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnBlue_When_Focus_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton2");

        buttonElement.Focus();

        Assert.AreEqual("color: blue;", buttonElement.GetStyle());
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");

        bool isDisabled = buttonElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnTrue_When_DisabledAttributePresent_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton3");

        bool isDisabled = buttonElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnStart_When_ValueAttributePresent_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");

        var actualValue = buttonElement.Value;

        Assert.AreEqual("Start", actualValue);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void ReturnEmpty_When_UseInnerText_Edge()
    {
        var buttonElement = App.Components.CreateById<Reset>("myButton");

        Assert.AreEqual(string.Empty, buttonElement.InnerText);
    }
}