// <copyright file="TableControlTestsChrome.cs" company="Automate The Planet Ltd.">
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

using System.Collections.Generic;
using System.Linq;
using Bellatrix.Web.Tests.Controls.Table;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[Browser(OS.OSX, BrowserType.Safari, Lifecycle.ReuseIfStarted)]
[AllureSuite("Table Control")]
public class TableControlTestsChrome : MSTest.WebTest
{
    public override void TestInit()
        => App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().TableLocalPage);

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void Table_GetItems()
    {
        var table = App.Components.CreateById<Web.Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        var dataTableExampleOnes = table.GetItems<DataTableExampleOne>();

        Assert.AreEqual("Smith", dataTableExampleOnes.First().LastName);
        Assert.AreEqual("John", dataTableExampleOnes.First().FirstName);
        Assert.AreEqual("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void BasicTable_Has_Header()
    {
        var table = App.Components.CreateById<Web.Table>("table1");
        var headerNames = table.GetHeaderNames();
        var tableCell = table.GetCell(3, 1);
        Assert.IsTrue(headerNames.Contains("Due"));
        Assert.AreEqual("$51.00", tableCell.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void TableWithHeader_GetItems()
    {
        var table = App.Components.CreateById<Web.Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        var dataTableExampleOnes = table.GetItems<DataTableExampleOne>();

        Assert.AreEqual("Smith", dataTableExampleOnes.First().LastName);
        Assert.AreEqual("John", dataTableExampleOnes.First().FirstName);
        Assert.AreEqual("http://www.timconway.com", dataTableExampleOnes.Last().WebSite);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void TableWithHeader_Returns_Value()
    {
        var table = App.Components.CreateById<Web.Table>("table1");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");
        table.SetColumn("Action");

        var tableCell = table.GetCell("Email", 1);

        Assert.AreEqual("fbach@yahoo.com", tableCell.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void SimpleTable_Returns_AllRowsAndCells()
    {
        var table = App.Components.CreateById<Web.Table>("simpleTable");
        var firstRowCellsCount = table.GetRow(0).GetCells().Count();
        var secondRowCellsCount = table.GetRow(1).GetCells().Count();

        Assert.AreEqual(firstRowCellsCount, secondRowCellsCount);
        Assert.AreEqual(4, firstRowCellsCount);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void NestedTables_Returns_TableCellsCountEqualToTableHeadersCount()
    {
        var table = App.Components.CreateById<Web.Table>("nestedTable");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");

        var tableHeadersCount = table.GetHeaderNames().Count;
        var tableCellsCount = table.GetRow(0).GetCells().Count();

        Assert.AreEqual(tableHeadersCount, tableCellsCount);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows)]
    public void NestedTables_Returns_TableCellsCountEqualBetweenRows()
    {
        var table = App.Components.CreateById<Web.Table>("nestedTable");
        table.SetColumn("Last Name");
        table.SetColumn("First Name");
        table.SetColumn("Email");
        table.SetColumn("Due");
        table.SetColumn("Web Site");

        var firstRowCellsCount = table.GetRow(0).GetCells().Count();
        var secondRowCellsCount = table.GetRow(1).GetCells().Count();

        Assert.AreEqual(firstRowCellsCount, secondRowCellsCount);
    }
}