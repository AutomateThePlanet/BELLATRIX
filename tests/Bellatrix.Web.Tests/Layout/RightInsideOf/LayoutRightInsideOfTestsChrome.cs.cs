// <copyright file="LayoutRightInsideOfTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
public class LayoutRightInsideOfTestsChrome : MSTest.WebTest
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
    public void FeaturesRightInsideOfNavigation_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOf(_navigationDiv);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigation355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOf(_navigationDiv, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationBetween350To360_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfBetween(_navigationDiv, 350, 360);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationGreaterThan354_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfGreaterThan(_navigationDiv, 354);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationGreaterThanOrEqual355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfGreaterThanOrEqual(_navigationDiv, 355);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationLessThan357_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfLessThan(_navigationDiv, 357);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationLessThanOrEqual356_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfLessThanOrEqual(_navigationDiv, 356);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void FeaturesRightInsideOfNavigationApproximate355_WhenDesktopWindowsSize_1280_600_Chrome()
        => _features.AssertRightInsideOfApproximate(_navigationDiv, 355, 5);
}