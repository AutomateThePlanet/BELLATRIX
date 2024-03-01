// <copyright file="LayoutTopInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutTopInsideOfTestsChrome : MSTest.WebTest
{
    private Div _navigationDiv;
    private Anchor _features;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _navigationDiv = App.Components.CreateByXpath<Div>("/html/body/div[1]");
        _features = App.Components.CreateByXpath<Anchor>("/html/body/div[1]/nav/a[1]");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigation36_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOf(_navigationDiv, 16);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationBetween10To20_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfBetween(_navigationDiv, 10, 20);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationGreaterThan15_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfGreaterThan(_navigationDiv, 15);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationGreaterThanOrEqual16_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfGreaterThanOrEqual(_navigationDiv, 16);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationLessThan17_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfLessThan(_navigationDiv, 17);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationLessThanOrEqual16_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfLessThanOrEqual(_navigationDiv, 16);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopInsideOfNavigationApproximate15_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopInsideOfApproximate(_navigationDiv, 15, 10);
}