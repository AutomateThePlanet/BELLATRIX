// <copyright file="LayoutBottomLeftInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutBottomLeftInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesBottomLeftInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigation18_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOf(_navigationDiv, 18, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationBetween10To20_810To820_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfBetween(_navigationDiv, 10, 20, 810, 820);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationGreaterThan16_814_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfGreaterThan(_navigationDiv, 16, 814);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationGreaterThanOrEqual17_815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfGreaterThanOrEqual(_navigationDiv, 17, 815);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationLessThan19_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfLessThan(_navigationDiv, 19, 819);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationLessThanOrEqual18_818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfLessThanOrEqual(_navigationDiv, 18, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomLeftInsideOfNavigationApproximate18_815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomLeftInsideOfApproximate(_navigationDiv, 18, 815, 5);
}