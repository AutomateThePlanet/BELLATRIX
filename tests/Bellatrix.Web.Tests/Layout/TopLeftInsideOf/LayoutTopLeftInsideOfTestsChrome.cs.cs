// <copyright file="LayoutTopLeftInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutTopLeftInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesTopLeftInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfViewport16_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOf(SpecialElements.Viewport, 16, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfScreen16_815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOf(SpecialElements.Screen, 16, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigation16_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOf(_navigationDiv, 16, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationBetween10To20_810To820_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfBetween(_navigationDiv, 10, 20, 810, 820);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationGreaterThan15_814_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfGreaterThan(_navigationDiv, 15, 814);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationGreaterThanOrEqual16_815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfGreaterThanOrEqual(_navigationDiv, 16, 815);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationLessThan17_819_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfLessThan(_navigationDiv, 17, 819);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationLessThanOrEqual16_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfLessThanOrEqual(_navigationDiv, 16, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesTopLeftInsideOfNavigationApproximate15_815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertTopLeftInsideOfApproximate(_navigationDiv, 15, 815, 10);
}