﻿// <copyright file="LayoutRightOfTestsEdge.cs" company="Automate The Planet Ltd.">
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
// <author>Miriam Kyoseva</author>
// <site>https://bellatrix.solutions/</site>
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Playwright.Tests.Controls;

[TestClass]
[Browser(BrowserTypes.Edge, 1280, 600, Lifecycle.ReuseIfStarted)]
[AllureSuite("Layout")]
[AllureFeature("Edge Browser")]
public class LayoutRightOfTestsEdge : MSTest.WebTest
{
    private Heading _free;
    private Heading _pro;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().LayoutPricingPage);
        _free = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[1]/div[1]/h4");
        _pro = App.Components.CreateByXpath<Heading>("/html/body/div[3]/div/div[2]/div[1]/h4");
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfPro_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOf(_pro);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfPro72_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOf(_pro, 72);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProBetween60To80_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfBetween(_pro, 60, 80);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProGreaterThan71_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfGreaterThan(_pro, 71);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProGreaterThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfGreaterThanOrEqual(_pro, 72);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProLessThan73_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfLessThan(_pro, 73);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProLessThanOrEqual72_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfLessThanOrEqual(_pro, 72);

    [TestMethod]
    [TestCategory(Categories.CI)]
    [TestCategory(Categories.Layout)]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    public void FreeRightOfProApproximate70_WhenDesktopWindowsSize_1280_1024_Edge() => _free.AssertRightOfApproximate(_pro, 70, 5);
}