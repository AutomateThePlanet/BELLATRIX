// <copyright file="LayoutNearTopOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, MobileWindowSize._360_640, Lifecycle.ReuseIfStarted)]
[AllureSuite("Layout")]
public class LayoutNearTopOfTestsChrome : MSTest.WebTest
{
    private Heading _free;
    private Heading _enterprise;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _enterprise = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[3]/div[1]/h4");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterprise_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOf(_enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterprise587_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOf(_enterprise, 587);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseBetween580To595_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfBetween(_enterprise, 580, 595);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseGreaterThan282_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfGreaterThan(_enterprise, 282);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseGreaterThanOrEqual283_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfGreaterThanOrEqual(_enterprise, 283);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseLessThan592_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfLessThan(_enterprise, 592);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseLessThanOrEqual591_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfLessThanOrEqual(_enterprise, 591);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopOfEnterpriseApproximate590_WhenDesktopWindowsSize_360_640_Chrome()
        => _free.AssertNearTopOfApproximate(_enterprise, 590, 5);
}