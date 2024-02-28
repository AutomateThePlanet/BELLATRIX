// <copyright file="LayoutNearTopLeftOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutNearTopLeftOfTestsChrome : MSTest.WebTest
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
    public void FreeNearTopLeftOfGetStarted_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOf(_getStarted);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStarted216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOf(_getStarted, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedBetween210To220_70To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfBetween(_getStarted, 210, 220, 70, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedGreaterThan215_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfGreaterThan(_getStarted, 215, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedGreaterThanOrEqual216_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfGreaterThanOrEqual(_getStarted, 216, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedLessThan218_73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfLessThan(_getStarted, 218, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedLessThanOrEqual217_72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfLessThanOrEqual(_getStarted, 217, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearTopLeftOfGetStartedApproximate216_71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _free.AssertNearTopLeftOfApproximate(_getStarted, 216, 71, 5);
}