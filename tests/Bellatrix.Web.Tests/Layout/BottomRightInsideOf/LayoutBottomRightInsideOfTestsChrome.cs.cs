// <copyright file="LayoutBottomRightInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutBottomRightInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesBottomRightInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigation18_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOf(_navigationDiv, 18, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationBetween10To20_350To360_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfBetween(_navigationDiv, 10, 20, 350, 360);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationGreaterThan16_354_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfGreaterThan(_navigationDiv, 16, 354);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationGreaterThanOrEqual17_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfGreaterThanOrEqual(_navigationDiv, 17, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationLessThan19_357_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfLessThan(_navigationDiv, 19, 357);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationLessThanOrEqual18_356_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfLessThanOrEqual(_navigationDiv, 18, 356);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomRightInsideOfNavigationApproximate18_355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomRightInsideOfApproximate(_navigationDiv, 18, 355, 5);
}