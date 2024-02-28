// <copyright file="ProgressControlTestsChrome.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Progress Control")]
public class ProgressControlTestsChrome : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().ProgressLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void Return70_When_UseGetValueMethod_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress");

        Assert.AreEqual("70", progressElement.Value);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnNull_When_NoValueAttributeAttributePresent_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress2");

        Assert.IsNotNull(progressElement.Value);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void Return100_When_UseGetMaxMethod_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress");

        Assert.AreEqual("100", progressElement.Max);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void Return1_When_NoMaxAttributePresent_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress1");

        var actualMax = progressElement.Max;

        Assert.AreEqual("1", actualMax);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void Return70_When_UseGetInnerTextMethod_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress");

        Assert.AreEqual("70 %", progressElement.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnEmpty_When_NoInnerTextPresent_Chrome()
    {
        var progressElement = App.Components.CreateById<Progress>("myProgress3");

        var actualInnerText = progressElement.InnerText;

        Assert.AreEqual(string.Empty, actualInnerText);
    }
}