// <copyright file="LayoutHeightTestsChrome.cs" company="Automate The Planet Ltd.">
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
public class LayoutHeightTestsChrome : MSTest.WebTest
{
    public override void TestInit()
        => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightEqual28_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeight(29);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightBetween10And30_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeightBetween(10, 30);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightGreaterThan27_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeightGreaterThan(27);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightGreaterThanOrEqual28_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeightGreaterThanOrEqual(28);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightLessThan30_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeightLessThan(30);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightLessThanOrEqual29_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");

        free.AssertHeightLessThanOrEqual(29);
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void HeadingHeightApproximate1PercentDifference_WhenDesktopWindowsSize_1280_1024_Chrome()
    {
        var free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        var pro = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[2]/div[1]/h4");

        free.AssertHeightApproximate(pro, 1);
    }
}