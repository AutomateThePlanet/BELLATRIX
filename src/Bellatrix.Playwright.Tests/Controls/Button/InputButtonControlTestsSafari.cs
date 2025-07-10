﻿// <copyright file="InputButtonControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Input Button Control")]
public class InputButtonControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ButtonPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void SetTextToStop_When_UseClickMethod_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton");

        buttonElement.Click();

        Assert.AreEqual("Stop", buttonElement.Value);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton1");

        buttonElement.Hover();

        Assert.AreEqual("color: red;", buttonElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnBlue_When_Focus_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton2");

        buttonElement.Focus();

        buttonElement.ValidateStyleIs("color: blue;");
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton");

        bool isDisabled = buttonElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton3");

        bool isDisabled = buttonElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnStart_When_ValueAttributePresent_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton");

        var actualValue = buttonElement.Value;

        Assert.AreEqual("Start", actualValue);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_UseInnerText_Safari()
    {
        var buttonElement = App.Components.CreateById<Button>("myButton");

        Assert.AreEqual(string.Empty, buttonElement.InnerText);
    }
}