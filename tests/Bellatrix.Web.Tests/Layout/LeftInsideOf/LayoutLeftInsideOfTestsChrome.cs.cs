// <copyright file="LayoutLeftInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutLeftInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesLeftInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigation818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOf(_navigationDiv, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationBetween810To820_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfBetween(_navigationDiv, 810, 820);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationGreaterThan814_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfGreaterThan(_navigationDiv, 814);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationGreaterThanOrEqual815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfGreaterThanOrEqual(_navigationDiv, 815);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationLessThan819_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfLessThan(_navigationDiv, 819);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationLessThanOrEqual818_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfLessThanOrEqual(_navigationDiv, 818);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesLeftInsideOfNavigationApproximate815_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertLeftInsideOfApproximate(_navigationDiv, 815, 5);
}