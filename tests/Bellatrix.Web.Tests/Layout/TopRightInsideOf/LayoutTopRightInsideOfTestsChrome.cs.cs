// <copyright file="LayoutTopRightInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutTopRightInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesTopRightInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigation16_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOf(_navigationDiv, 16, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationBetween10To20_350To360_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfBetween(_navigationDiv, 10, 20, 350, 360);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationGreaterThan15_354_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfGreaterThan(_navigationDiv, 15, 354);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationGreaterThanOrEqual16_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfGreaterThanOrEqual(_navigationDiv, 16, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationLessThan17_357_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfLessThan(_navigationDiv, 17, 357);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationLessThanOrEqual16_356_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfLessThanOrEqual(_navigationDiv, 16, 356);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopRightInsideOfNavigationApproximate15_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopRightInsideOfApproximate(_navigationDiv, 15, 355, 10);
}