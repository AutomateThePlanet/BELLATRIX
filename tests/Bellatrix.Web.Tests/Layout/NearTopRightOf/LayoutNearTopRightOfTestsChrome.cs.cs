// <copyright file="LayoutNearTopRightOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutNearTopRightOfTestsChrome : MSTest.WebTest
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
    public void EnterpriseNearTopRightOfGetStarted_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOf(_getStarted);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStarted216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOf(_getStarted, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedBetween210To220_70To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfBetween(_getStarted, 210, 220, 70, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedGreaterThan215_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfGreaterThan(_getStarted, 215, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedGreaterThanOrEqual216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfGreaterThanOrEqual(_getStarted, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedLessThan218_73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfLessThan(_getStarted, 218, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedLessThanOrEqual217_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfLessThanOrEqual(_getStarted, 217, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void EnterpriseNearTopRightOfGetStartedApproximate216_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _enterprise.AssertNearTopRightOfApproximate(_getStarted, 216, 71, 5);
}