// <copyright file="LayoutLeftOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutLeftOfTestsChrome : MSTest.WebTest
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
    public void FreeLeftOfPro_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOf(_free);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfPro72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOf(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProBetween60To80_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfBetween(_free, 60, 80);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProGreaterThan71_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfGreaterThan(_free, 71);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProGreaterThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfGreaterThanOrEqual(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProLessThan73_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfLessThan(_free, 73);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProLessThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfLessThanOrEqual(_free, 72);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FreeLeftOfProApproximate70_WhenDesktopWindowsSize_1280_1024_Chrome()
        => _pro.AssertLeftOfApproximate(_free, 70, 5);
}