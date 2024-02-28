// <copyright file="LayoutNearBottomLeftOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, 1280, 600, Lifecycle.ReuseIfStarted)]
[AllureSuite("Layout")]
public class LayoutNearBottomLeftOfTestsChrome : MSTest.WebTest
{
    private Heading _enterprise;
    private Button _getStarted;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _enterprise = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[3]/div[1]/h4");
        _getStarted = App.Components.CreateByXpath<Button>("//*[text()='Get started']");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterprise_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOf(_enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterprise216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOf(_enterprise, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseBetween210To220_70To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfBetween(_enterprise, 210, 220, 70, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseGreaterThan215_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfGreaterThan(_enterprise, 215, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseGreaterThanOrEqual216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfGreaterThanOrEqual(_enterprise, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseLessThan218_73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfLessThan(_enterprise, 218, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseLessThanOrEqual217_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfLessThanOrEqual(_enterprise, 217, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomLeftOfEnterpriseApproximate216_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomLeftOfApproximate(_enterprise, 216, 71, 5);
}