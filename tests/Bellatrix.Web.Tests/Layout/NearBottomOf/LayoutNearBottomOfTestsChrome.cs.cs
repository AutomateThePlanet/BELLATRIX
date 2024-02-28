// <copyright file="LayoutNearBottomOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutNearBottomOfTestsChrome : MSTest.WebTest
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
    public void EnterpriseNearBottomOfFree_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOf(_free);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFree587_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOf(_free, 587);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeBetween585To600_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfBetween(_free, 585, 600);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeGreaterThan586_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfGreaterThan(_free, 586);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeGreaterThanOrEqual587_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfGreaterThanOrEqual(_free, 587);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeLessThan592_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfLessThan(_free, 592);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeLessThanOrEqual591_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfLessThanOrEqual(_free, 591);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearBottomOfFreeApproximate590_WhenDesktopWindowsSize_360_640_Chrome()
        => _enterprise.AssertNearBottomOfApproximate(_free, 590, 5);
}