// <copyright file="LayoutNearBottomRightOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutNearBottomRightOfTestsChrome : MSTest.WebTest
{
    private Heading _free;
    private Button _getStarted;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _getStarted = App.Components.CreateByXpath<Button>("//*[text()='Get started']");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFree_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOf(_free);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFree216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOf(_free, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeBetween210To220_70To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfBetween(_free, 210, 220, 70, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeGreaterThan215_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfGreaterThan(_free, 215, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeGreaterThanOrEqual216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfGreaterThanOrEqual(_free, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeLessThan218_73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfLessThan(_free, 218, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeLessThanOrEqual217_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfLessThanOrEqual(_free, 217, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void GetStartedNearBottomRightOfFreeApproximate216_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _getStarted.AssertNearBottomRightOfApproximate(_free, 216, 71, 5);
}