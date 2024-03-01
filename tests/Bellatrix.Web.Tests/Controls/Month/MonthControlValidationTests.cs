// <copyright file="MonthControlValidationTests.cs" company="Automate The Planet Ltd.">
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
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Edge, Lifecycle.ReuseIfStarted)]
[AllureSuite("Month Control")]
public class MonthControlValidationTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().MonthLocalPage);

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void MonthSetThrowsArgumentException_When_Month0_Edge()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.ThrowsException<ArgumentException>(() => monthElement.SetMonth(2017, 0));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void MonthSetThrowsArgumentException_When_MonthMinus1_Edge()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.ThrowsException<ArgumentException>(() => monthElement.SetMonth(2017, -1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void MonthSetThrowsArgumentException_When_YearMinus1_Edge()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.ThrowsException<ArgumentException>(() => monthElement.SetMonth(-1, 2));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void MonthSetThrowsArgumentException_When_Year0_Edge()
    {
        var monthElement = App.Components.CreateById<Month>("myMonth");

        Assert.ThrowsException<ArgumentException>(() => monthElement.SetMonth(0, 1));
    }
}