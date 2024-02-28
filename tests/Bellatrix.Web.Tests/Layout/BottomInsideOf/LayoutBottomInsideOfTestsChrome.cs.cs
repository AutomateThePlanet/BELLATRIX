// <copyright file="LayoutBottomInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutBottomInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesBottomInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigation18_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOf(_navigationDiv, 18);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationBetween10To20_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfBetween(_navigationDiv, 10, 20);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationGreaterThan16_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfGreaterThan(_navigationDiv, 16);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationGreaterThanOrEqual17_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfGreaterThanOrEqual(_navigationDiv, 17);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationLessThan19_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfLessThan(_navigationDiv, 19);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationLessThanOrEqual18_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfLessThanOrEqual(_navigationDiv, 18);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesBottomInsideOfNavigationApproximate18_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertBottomInsideOfApproximate(_navigationDiv, 18, 5);
}