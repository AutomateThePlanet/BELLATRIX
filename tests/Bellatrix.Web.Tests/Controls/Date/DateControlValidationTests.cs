// <copyright file="DateControlValidationTests.cs" company="Automate The Planet Ltd.">
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
[AllureSuite("Date Control")]
public class DateControlValidationTests : MSTest.WebTest
{
    public override void TestInit() => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().DateLocalPage);

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_Month0_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(2017, 0, 1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_MonthMinus1_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(2017, -1, 1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_YearMinus1_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(-1, 2, 1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_Year0_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(0, 1, 1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_DayMinus1_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(2017, 1, -1));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_Day0_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(2007, 2, 0));
    }

    [TestMethod]
    [TestCategory(Categories.Edge), TestCategory(Categories.Windows)]
    [TestCategory(Categories.CI)]
    public void DateSetThrowsArgumentException_When_Day32_Edge()
    {
        var dateElement = App.Components.CreateById<Date>("myDate");

        Assert.ThrowsException<ArgumentException>(() => dateElement.SetDate(2007, 1, 32));
    }
}