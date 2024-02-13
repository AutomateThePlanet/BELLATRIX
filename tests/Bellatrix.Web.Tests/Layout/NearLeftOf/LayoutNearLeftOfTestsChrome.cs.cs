// <copyright file="LayoutNearLeftOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutNearLeftOfTestsChrome : MSTest.WebTest
{
    private Heading _free;
    private Heading _pro;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _pro = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[2]/div[1]/h4");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfPro_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOf(_free);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfPro72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOf(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProBetween60To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfBetween(_free, 60, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProGreaterThan71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfGreaterThan(_free, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProGreaterThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfGreaterThanOrEqual(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProLessThan73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfLessThan(_free, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProLessThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfLessThanOrEqual(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeNearLeftOfProApproximate70_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertNearLeftOfApproximate(_free, 70, 5);
}