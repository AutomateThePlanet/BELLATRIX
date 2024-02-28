using System.Collections.Generic;
using System.Linq;
using Bellatrix.Assertions;
using Bellatrix.Web.Assertions;
using NUnit.Framework;
using Assert = Bellatrix.Assertions.Assert;

namespace Bellatrix.Web.GettingStarted;

// The difference between a table and grid is that usually, the grids are more complex than a regular HTML table.
// In them, you can have dynamic data. Moreover, inside the column, you can have other HTML elements such as buttons, text fields, selects,
// other tables. Or you can filter, sort the columns, and have paging for the grid. Instead of getting specific cells or rows by custom locators-
// we have created the Table and Grid controls, which ease the selection of cells/rows and assertion of the data.
// Moreover, in many cases there isn't a unique item which you can use to select the row/cell.
[TestFixture]
public class GridControlTests : NUnit.WebTest
{
    private static GridTestPage _gridTestPage;
    private static List<Employee> _expectedItems;

    // BELLATRIX gives you API for easing the work with HTML grids.
    // Through the SetColumn you map the headers of the table if for some reason you don't want some column, just don't add it.
    // The method returns a list of all rows' data as C# data mapped to the map you provided.
    // You can get the cell converted to the element specified by the grid SetColumn method. Also, since some of this simple controls
    // sometimes are wrapped inside DIV or SPAN elements, you can specify additional locator for finding the ComponentCreateService.
    public Grid TestGrid => App.Components.CreateById<Grid>("sampleGrid")
        .SetColumn("Order", typeof(TextField), Find.By.Tag("input"))
        .SetColumn("Firstname")
        .SetColumn("Lastname")
        .SetColumn("Email Personal")
        .SetColumn("Email Business")
        .SetColumn("Actions", typeof(Button), Find.By.Xpath("./input[@type='button']"));

    public override void TestInit()
    {
        _gridTestPage = App.Create<GridTestPage>();
        App.Navigation.NavigateToLocalPage("TestPages\\Grid\\Grid.html");

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
                BusinessEmail = null,
                PersonalEmail = "mary@hotmail.com",
            },
            new Employee
            {
                Order = "2",
                FirstName = "July",
                LastName = "Dooley",
                BusinessEmail = "july@mscorp.com",
                PersonalEmail = null,
            },
        };
    }

    [Test]
    public void AssertGridCells()
    {
        // Verify all cells using a function with Assert
        TestGrid.ForEachCell(cell => cell.AssertFontSize("14px"));

        // Get Cell by Column name
        TestGrid.GetCell("Firstname", 1).ValidateInnerTextIs("Mary");

        // Get Cell by Cell coordinates
        TestGrid.GetCell(0, 1).ValidateInnerTextIs("John");

        // Get a cell that is equal to an Object property
        TestGrid.GetCell<Employee>(cell => cell.PersonalEmail, 1).ValidateInnerTextIs("mary@hotmail.com");

        // Get all cells that satisfy a condition using a function
        List<TableCell> matchingCells = TestGrid.GetCells<TableCell>(cell => cell.InnerText.StartsWith('J')).ToList();
        Assert.AreEqual(2, matchingCells.Count);

        // Get cell with multiple-row headers
        TestGrid.GetCell("Email Business", 0).ValidateInnerTextIs("jdoe@corp.com");

        // Perform actions in cell elements
        var firstRowEmail = TestGrid.GetRow(0).GetCell("Email Personal");

        TestGrid.GetCell("Actions", 0).As<Button>().Click();

        var firstRowEmailAfterDelete = TestGrid.GetRow(0).GetCell("Email Personal");

        Assert.AreNotEqual(firstRowEmail, firstRowEmailAfterDelete);
    }

    [Test]
    public void AssertObjectsData()
    {
        // You can get all rows as instances of a specific class through the GetItems method.
        var expectedObj = _expectedItems[0];
        var actualObj = TestGrid.GetItems<Employee>()[0];

        EntitiesAsserter.AreEqual(expectedObj, actualObj);

        // Instead of first casting the items and then to get them by index and then assert them manually.
        // You can get specific row through GetRow method and use the built-in AssertRow method to verify the row's data.
        TestGrid.GetRow(0).AssertRow(expectedObj);

        // Compares all grid rows to the expected entities. Each row is internally converted to the type of the expected entities.
        TestGrid.AssertTable(_expectedItems);

        // You can get all header names. Doubled headers are returned as one entry and separated by space.
        Assert.AreEqual("Email Personal", TestGrid.GetHeaderNames().FirstOrDefault(header => header.StartsWith("Email")));
    }

    [Test]
    public void AssertHeaders()
    {
        // You can get all grid header rows through the TableHeaderRows property.
        TestGrid.TableHeaderRows.ForEach(header => header.AssertFontFamily("\"Helvetica Neue\", Helvetica, Arial, sans-serif"));

        // As a shortcut, you can iterate over the header rows through the ForEachHeader method.
        TestGrid.ForEachHeader(row => row.AssertFontFamily("\"Helvetica Neue\", Helvetica, Arial, sans-serif"));

        // You can get all grid header cells through the ColumnHeaders property.
        var headerCells = TestGrid.ColumnHeaders.Where(cell => !string.IsNullOrEmpty(cell.InnerText)).ToElementList();
        headerCells.ForEach(cell => cell.AssertFontSize("14px"));
    }

    [Test]
    public void AssertRows()
    {
        // You can get the grid rows (without the header ones) through the GetRows method.
        Assert.AreEqual(3, TestGrid.GetRows().Count());

        // As a shortcut, you can iterate over the grid rows through the ForEachRow method.
        TestGrid.ForEachRow(row => row.CreateByXpath<Button>(".//input[@type='button']").ValidateIsVisible());

        // You can get a specific row by its index through the GetRow method.
        var secondRow = TestGrid.GetRow(1);
        Assert.AreEqual(1, secondRow.GetCells<TableCell>(cell => cell.InnerText == "Mary").Count);

        // You can get all rows matching a given condition through the GetRows method.
        var firstRow = TestGrid.GetRow(0);
        firstRow = TestGrid.GetRows<TableCell>(cell => cell.InnerText.Contains("J")).First();

        // As a shortcut, you can get the first row matching a given condition through the GetFirstOrDefaultRow method.
        firstRow = TestGrid.GetFirstOrDefaultRow<TableCell>(cell => cell.InnerText.Contains("J"));
    }

    [Test]
    public void AssertSpecificRow()
    {
        var firstRow = TestGrid.GetRow(0);

        // You can get the index of a given row through the Index property.
        Assert.AreEqual(0, firstRow.Index);

        // You can get the html through the InnerHtml property.
        Assert.IsTrue(firstRow.InnerHtml.Contains("</td>"));
        firstRow.ValidateInnerHtmlContains("</td>");

        // There are many ways to get a specific cell through the indexer and the GetCell methods.
        var firstCell = firstRow.GetCell("Order");

        firstCell.As<TextField>().ValidateValueIs("0");

        var secondCell = firstRow[1];
        secondCell = firstRow.GetCell(1);
        secondCell.ValidateInnerTextIs("John");

        // You can get all row cells through the GetCells method.
        IEnumerable<GridCell> cells = firstRow.GetCells();
        Assert.AreEqual(6, cells.Count());

        // You can get the cells matching a condition. Also, they will be returned as elements of a type of your choice.
        List<TableCell> textFields = firstRow.GetCells<TableCell>(cell => cell.InnerText.StartsWith("John") || cell.InnerText.StartsWith("john"));
        Assert.AreEqual(2, textFields.Count);

        // You can get the first cell matching a condition through the GetFirstOrDefaultCell method.
        var firstInputCell = firstRow.GetFirstOrDefaultCell<TextField>(cell => cell.TagName == "input");
        firstInputCell.ValidateValueIs("0");

        // You can convert a row to an instance of a specific class through the GetItem method.
        Assert.AreEqual("John Doe", $"{firstRow.GetItem<Employee>().FirstName} {firstRow.GetItem<Employee>().LastName}");

        // You can compare a row to an instance of a specific class. The row is internally converted to the type of the expected object.
        firstRow.AssertRow(_expectedItems[0]);
    }

    [Test]
    public void AssertSpecificCell()
    {
        var secondCell = TestGrid.GetCell(0, 1);

        // You can get the cell row and column.
        Assert.AreEqual(0, secondCell.Row);
        Assert.AreEqual(1, secondCell.Column);

        // You can get the cell inner text.
        Assert.AreEqual("John", secondCell.InnerText);

        // You can get the cell inner HTML.
        Assert.AreEqual("<b>John</b>", secondCell.InnerHtml);

        // You can get the cell converted to a specific element and use the element's specific API.
        var firstCell = TestGrid.GetCell(0, 0);
        firstCell.As<TextField>().ValidateValueIs("0");

        // You can get the cell converted to the element specified by the grid SetColumn method.
        Assert.AreEqual("0", firstCell.As().Value);
    }

    [Test]
    public void AssertColumns()
    {
        // You can get the cells of a particular column mentioning the column number.
        var firstColumn = TestGrid.GetColumn(0);
        firstColumn[0].As<TextField>().ValidateValueIs("0");

        // You can get the cells of a particular column mentioning the column name.
        firstColumn = TestGrid.GetColumn("Order");
        firstColumn[0].As<TextField>().ValidateValueIs("0");

        // You can get the name of a column mentioning its index.
        Assert.AreEqual("Email Personal", TestGrid.GetGridColumnNameByIndex(3));
    }
}
