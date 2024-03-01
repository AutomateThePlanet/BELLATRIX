// <copyright file="RadioButtonControlTestsSafari.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Radio Control")]
public class RadioButtonControlTestsSafari : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().RadioLocalPage);

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void Checked_When_UseClickMethod_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio");

        radioElement.Click();

        Assert.AreEqual(true, radioElement.IsChecked);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnRed_When_Hover_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio1");

        radioElement.Hover();

        Assert.AreEqual("color: red;", radioElement.GetStyle());
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnFalse_When_DisabledAttributeNotPresent_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio");

        bool isDisabled = radioElement.IsDisabled;

        Assert.IsFalse(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnTrue_When_DisabledAttributePresent_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio3");

        bool isDisabled = radioElement.IsDisabled;

        Assert.IsTrue(isDisabled);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnOn_When_ValueAttributeNotPresent_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio1");

        var actualValue = radioElement.Value;

        Assert.AreEqual("on", actualValue);
    }

    [TestMethod]
    [Ignore, TestCategory(Categories.Safari), TestCategory(Categories.OSX)]
    public void ReturnNewsletter_When_ValueAttributePresent_Safari()
    {
        var radioElement = App.Components.CreateById<RadioButton>("myRadio2");

        var actualValue = radioElement.Value;

        Assert.AreEqual("newsletter", actualValue);
    }
}