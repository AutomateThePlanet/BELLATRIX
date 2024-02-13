// <copyright file="LayoutAboveOfTestsChrome.cs" company="Automate The Planet Ltd.">
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
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, 600, 1280, Lifecycle.ReuseIfStarted)]
[AllureSuite("Layout")]
public class LayoutAboveOfTestsChrome : MSTest.WebTest
{
    private Heading _free;
    private Heading _enterprise;

    public override void TestInit()
    {
        App.Browser.WrappedDriver.SwitchTo().Window("nameWindow");
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _enterprise = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[3]/div[1]/h4");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterprise_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOf(_enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterprise283_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOf(_enterprise, 281);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseBetween270To290_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfBetween(_enterprise, 270, 290);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseGreaterThan280_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfGreaterThan(_enterprise, 280);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseGreaterThanOrEqual283_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfGreaterThanOrEqual(_enterprise, 281);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseLessThan284_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfLessThan(_enterprise, 284);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseLessThanOrEqual283_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfLessThanOrEqual(_enterprise, 283);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeAboveOfEnterpriseApproximate280_WhenDesktopWindowsSize_1280_600_Chrome()
        => _free.AssertAboveOfApproximate(_enterprise, 280, 5);
}