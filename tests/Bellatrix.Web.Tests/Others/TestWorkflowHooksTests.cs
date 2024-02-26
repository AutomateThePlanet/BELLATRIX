// <copyright file="TestWorkflowHooksTests.cs" company="Automate The Planet Ltd.">
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

using System.Diagnostics;
using System.Threading;
using Bellatrix.Layout;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[Browser(OS.OSX, BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("TestWorkflowHooks")]
public class TestWorkflowHooksTests : MSTest.WebTest
{
    private static Select _sortDropDown;
    private static Anchor _protonRocketAnchor;

    public override void TestsArrange()
    {
        _sortDropDown = App.Components.CreateByXpath<Select>("//*[@id='main']/div[1]/form/select");
        _protonRocketAnchor = App.Components.CreateByXpath<Anchor>("//*[@id='main']/div[2]/ul/li[1]/a[1]");
    }

    public override void TestsAct()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");
        _sortDropDown.SelectByText("Sort by price: low to high");
    }

    public override void TestInit()
    {
        // Executes a logic before each test in the test class.
    }

    public override void TestCleanup()
    {
        // Executes a logic after each test in the test class.
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SortDropDownIsAboveOfProtonRocketAnchor()
    {
        _sortDropDown.AssertAboveOf(_protonRocketAnchor);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SortDropDownIsAboveOfProtonRocketAnchor_41px()
    {
        _sortDropDown.AssertAboveOf(_protonRocketAnchor, 41);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SortDropDownIsAboveOfProtonRocketAnchor_GreaterThan40px()
    {
        _sortDropDown.AssertAboveOfGreaterThan(_protonRocketAnchor, 40);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SortDropDownIsAboveOfProtonRocketAnchor_GreaterThanOrEqual41px()
    {
        _sortDropDown.AssertAboveOfGreaterThanOrEqual(_protonRocketAnchor, 41);
    }

    [TestMethod]
    [TestCategory(Categories.CI)]
    public void SortDropDownIsNearTopOfProtonRocketAnchor_GreaterThan40px()
    {
        _sortDropDown.AssertNearTopOfGreaterThan(_protonRocketAnchor, 40);
    }
}