using System.Collections.Generic;
using System.Linq;
using Bellatrix.Web.Assertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Grid Control")]
public class GridControlTestsChrome : MSTest.WebTest
{
    private Grid _testGrid;
    private List<Employee> _expectedItems;

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage(ConfigurationService.GetSection<TestPagesSettings>().GridLocalPage);
        _testGrid = new ComponentCreateService().CreateById<Grid>("sampleGrid")
            .SetColumn("Order", typeof(TextField), Find.By.Tag("input"))
            .SetColumn("Firstname")
            .SetColumn("Lastname")
            .SetColumn("Email Personal")
            .SetColumn("Email Business")
            .SetColumn("Actions", typeof(Button), Find.By.Xpath("./input[@type='button']"));

        _expectedItems = new List<Employee>
        {
            new Employee
            {
                Order = "0",
                FirstName = "John",
                LastName = "Doe",
                PersonalEmail = "john.doe@gmail.com",
                BusinessEmail = "jdoe@corp.com",
            },
            new Employee
            {
                Order = "1",
                FirstName = "Mary",
                LastName = "Moe",
                BusinessEmail = string.Empty,
                PersonalEmail = "mary@hotmail.com",
            },
            new Employee
            {
                Order = "2",
                FirstName = "July",
                LastName = "Dooley",
                BusinessEmail = "july@mscorp.com",
                PersonalEmail = string.Empty,
            },
        };
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void VerifyFontSize_When_ForEachCell_Chrome()
    {
        _testGrid.ForEachCell(cell => cell.AssertFontSize("14px"));
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetCell_WithIntForColumn_Chrome()
    {
        GridCell cell = _testGrid.GetCell(0, 1);

        Assert.AreEqual(0, cell.Row);
        Assert.AreEqual(1, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetCell_WithStringForColumn_Chrome()
    {
        GridCell cell = _testGrid.GetCell("Firstname", 1);

        Assert.AreEqual(1, cell.Row);
        Assert.AreEqual(1, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetCell_WithExpressionForColumn_Chrome()
    {
        GridCell cell = _testGrid.GetCell<Employee>(cell => cell.PersonalEmail, 1);

        Assert.AreEqual(1, cell.Row);
        Assert.AreEqual(3, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetCell_WithSplitColumn_Chrome()
    {
        var cell = _testGrid.GetCell("Email Business", 0);

        Assert.AreEqual(0, cell.Row);
        Assert.AreEqual(4, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetRow_Then_GetCell_WithStringColumn_Chrome()
    {
        var cell = _testGrid.GetRow(0).GetCell("Firstname");

        Assert.AreEqual(0, cell.Row);
        Assert.AreEqual(1, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetRow_Then_GetCell_WithIntColumn_Chrome()
    {
        var cell = _testGrid.GetRow(0).GetCell(1);

        Assert.AreEqual(0, cell.Row);
        Assert.AreEqual(1, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetRow_Then_GetCell_WithSplitColumn_Chrome()
    {
        var cell = _testGrid.GetRow(0).GetCell("Email Business");

        Assert.AreEqual(0, cell.Row);
        Assert.AreEqual(4, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetRow_Then_GetCell_WithExpressionForColumn_Chrome()
    {
        GridCell cell = _testGrid.GetRow(1).GetCell<Employee>(cell => cell.PersonalEmail);

        Assert.AreEqual(1, cell.Row);
        Assert.AreEqual(3, cell.Column);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperObject_When_GetCell_As_Chrome()
    {
        TextField cell = _testGrid.GetCell(0, 0).As<TextField>();

        cell.SetText("test");

        Assert.AreEqual("test", cell.Value);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCells_When_GetCells_WithExpressionForColumn_Chrome()
    {
        var matchingCells = _testGrid.GetCells<TableCell>(cell => cell.InnerText.StartsWith('J'));

        Assert.AreEqual(2, matchingCells.Count());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetFirstOrDefaultCell_Chrome()
    {
        var cell = _testGrid.GetFirstOrDefaultCell<TableCell>(cell => cell.InnerText.StartsWith('J'));

        Assert.AreEqual("John", cell.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperCell_When_GetLastOrDefaultCell_Chrome()
    {
        var cell = _testGrid.GetLastOrDefaultCell<TableCell>(cell => cell.InnerText.StartsWith('J'));

        Assert.AreEqual("July", cell.InnerText);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void AssertRow_When_Chrome()
    {
        _testGrid.GetRow(0).AssertRow(_expectedItems[0]);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRow_When_GetRowByIndex_Chrome()
    {
        var row = _testGrid.GetRow(0);

        Assert.AreEqual(0, row.Index);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRows_When_GetRows_Chrome()
    {
        var rows = _testGrid.GetRows().ToList();

        Assert.AreEqual(3, rows.Count);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void VerifyRowElement_When_ForEachRow_Chrome()
    {
        _testGrid.ForEachRow(row => row.CreateByXpath<Button>(".//input[@type='button']").ValidateIsVisible());
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnRow_When_GetRowByExpression_Chrome()
    {
        var row = _testGrid.GetFirstOrDefaultRow<GridRow>(r => r.InnerHtml.Contains("July"));

        Assert.AreEqual(2, row.Index);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnColumnIndex_When_GetByName_Chrome()
    {
        var index = _testGrid.GetGridColumnIndexByName("Firstname");

        Assert.AreEqual(1, index);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnColumnName_When_GetByIndex_Chrome()
    {
        var name = _testGrid.GetGridColumnNameByIndex(1);

        Assert.AreEqual("Firstname", name);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperNames_When_GetHeaderNames_Chrome()
    {
        var expectedheaderNames = new List<string> { "Order", "Firstname", "Lastname", "Email Personal", "Email Business", "Actions" };

        var headerNames = _testGrid.GetHeaderNames().ToList();

        CollectionAssert.AreEqual(expectedheaderNames, headerNames);
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void VerifyHeadersFont_When_GetHeaderNames_Chrome()
    {
        _testGrid.TableHeaderRows.ForEach(header => header.AssertFontFamily("\"Helvetica Neue\", Helvetica, Arial, sans-serif"));
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void VerifyHeadersFont_When_ForeachHeaderNames_Chrome()
    {
        _testGrid.ForEachHeader(row => row.AssertFontFamily("\"Helvetica Neue\", Helvetica, Arial, sans-serif"));
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnColumnCells_When_GetColumnByIndex_Chrome()
    {
        var column = _testGrid.GetColumn(0);

        Assert.AreEqual(3, column.Count);
        Assert.IsTrue(column.Select(c => c.Column).All(v => v == 0));
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ReturnProperColumn_When_GetColumnByIndex_Chrome()
    {
        var column = _testGrid.GetColumn(0);

        Assert.IsTrue(column.Select(c => c.Column).All(v => v == 0));
    }
}
