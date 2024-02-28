// <copyright file="LayoutVerticallyAlignedTestsChrome.cs.cs" company="Automate The Planet Ltd.">
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
[Browser(BrowserType.Chrome, MobileWindowSize._414_736, Lifecycle.ReuseIfStarted)]
[AllureSuite("Layout")]
public class LayoutVerticallyAlignedTestsChrome : MSTest.WebTest
{
    private Heading _free;
    private Heading _pro;
    private Heading _enterprise;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _pro = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[2]/div[1]/h4");
        _enterprise = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[3]/div[1]/h4");
    }

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void AlignedVerticallyAll_WhenDesktopWindowsSize_414_736_Chrome()
        => LayoutAssert.AssertAlignedVerticallyAll(_free, _pro, _enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void AlignedVerticallyLeft_WhenDesktopWindowsSize_414_736_Chrome()
        => LayoutAssert.AssertAlignedVerticallyLeft(_free, _pro, _enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void AlignedVerticallyRight_WhenDesktopWindowsSize_414_736_Chrome()
        => LayoutAssert.AssertAlignedVerticallyRight(_free, _pro, _enterprise);

    [TestMethod]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void AlignedVerticallyCentered_WhenDesktopWindowsSize_414_736_Chrome()
        => LayoutAssert.AssertAlignedVerticallyCentered(_free, _pro, _enterprise);
}