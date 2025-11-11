using System.Collections.Generic;
using System.Linq;
using Bellatrix.Web.Assertions;
using NUnit.Framework;
using Assert = Bellatrix.Assertions.Assert;

namespace Bellatrix.Web.GettingStarted;

[TestFixture]
public class TableControlTests : NUnit.WebTest
{
    private List<User> _expectedUsers;

    // The difference between a table and grid is that usually, the grids are more complex than a regular HTML table.
    // In them, you can have dynamic data. Moreover, inside the column, you can have other HTML elements such as buttons, text fields, selects,
    // other tables. Or you can filter, sort the columns, and have paging for the grid. Instead of getting specific cells or rows by custom locators-
    // we have created the Table and Grid controls, which ease the selection of cells/rows and assertion of the data.
    // Moreover, in many cases there isn't a unique item which you can use to select the row/cell.
    //
    // BELLATRIX gives you API for easing the work with HTML tables.
    // Through the SetColumn you map the headers of the table if for some reason you don't want some column, just don't add it.
    // The method returns a list of all rows' data as C# data mapped to the map you provided.
    private Table Table => App.Components.CreateById<Table>("table1")
        .SetColumn("Last Name")
        .SetColumn("First Name")
        .SetColumn("Email")
        .SetColumn("Due")
        ////.SetColumn("Web Site") // this property won't be asserted if you use the AssertTable method.
        .SetColumn("Action");

    public override void TestInit()
    {
        App.Navigation.NavigateToLocalPage("TestPages\\Table\\table.html");

        // In order GetItems to be able to work you need to map the properties to headers through the HeaderName attribute
        // this is how we handle differences between the property name, spaces in the headers and such.
        _expectedUsers = new List<User>
        {
            new User
            {
                FirstName = "John",
                LastName = "Smith",
                Email = "jsmith@gmail.com",
                WebSite = "http://www.jsmith.com",
                Due = "$50.00",
            },
            new User
            {
                FirstName = "Frank",
                LastName = "Bach",
                Email = "fbach@yahoo.com",
                WebSite = "http://www.frank.com",
                Due = "$51.00",
            },
            new User
            {
                FirstName = "Jason",
                LastName = "Doe",
                Email = "jdoe@hotmail.com",
                WebSite = "http://www.jdoe.com",
                Due = "$100.00",
            },
            new User
            {
                FirstName = "Tim",
                LastName = "Conway",
                Email = "tconway@earthlink.net",
                WebSite = "http://www.timconway.com",
                Due = "$50.00",
            },
        };
    }

    [Test]
    public void AssertMiscData()
    {
        // You can get all rows as instances of a specific class through the GetItems method.
        Assert.AreEqual(_expectedUsers[0].Email, Table.GetItems<User>()[0].Email);

        // Compares all table rows to the expected entities. Each row is internally converted to the type of the expected entities.
        Table.AssertTable(_expectedUsers);

        // You can get all header names. Doubled headers are returned as one entry and separated by space.
        Assert.AreEqual("Action", Table.GetHeaderNames().Last());
    }

    [Test]
    [Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
    public void AssertCells()
    {
        // As a shortcut, you can iterate over all table cells through the ForEachCell method.
        Table.ForEachCell(cell => Assert.AreEqual("14px", cell.GetCssValue("font-size")));

        // You can get a particular cell as BELLATRIX element mentioning the column header and row number.
        Table.GetCell("First Name", 1).ValidateInnerTextIs("Frank");

        // You can get a particular cell as BELLATRIX element mentioning the row and column number.
        Table.GetCell(1, 2).ValidateInnerTextIs("Jason");

        // You can get a particular cell by header expression and row number.
        Table.GetCell<User>(cell => cell.Email, 1).ValidateInnerTextIs("fbach@yahoo.com");

        // You can get particular cells by a selector.
        List<TableCell> cells = Table.GetCells(cell => cell.InnerText.ToLower().StartsWith('j'));
        Assert.AreEqual(4, cells.Count);

        // As a shortcut, you can get the first cell matching a given condition through the GetFirstOrDefaultCell method.
        var matchingCell = Table.GetFirstOrDefaultCell(cell => cell.InnerText.ToLower().StartsWith('j'));
        matchingCell.ValidateInnerTextIs("John");
    }

    [Test]
    [Browser(BrowserType.FirefoxHeadless, Lifecycle.ReuseIfStarted)]
    public void AssertSpecificRow()
    {
      // You can get a specific row using the GetRow method by the index of the row.
        var firstRow = Table.GetRow(0);

        // You can get the index of a given row through the Index property.
        Assert.AreEqual(0, firstRow.Index);

        // You can get the HTML through the InnerHtml property.
        Assert.IsTrue(firstRow.InnerHtml.Contains("</td>"));

        // If you only need to assert the inner HTML you can use the built-in BELLATRIX Validate methods.
        firstRow.ValidateInnerHtmlContains("</td>");

        // There are many ways to get a specific cell through the indexer and the GetCell methods.
        var firstCell = Table.GetRow(0).GetCell(0);

        // You can again use directly the built-in BELLATRIX Validate methods.
        firstCell.ValidateInnerTextIs("Smith");

        // You can get a cell by header name
        var secondCell = firstRow.GetCell("Email");
        secondCell.ValidateInnerTextIs("jsmith@gmail.com");

        // You can get all row cells through the GetCells method.
        IEnumerable<TableCell> cells = firstRow.GetCells();
        Assert.AreEqual(6, cells.Count());

        // You can get the cells matching a condition.
        List<TableCell> matchingCells = firstRow.GetCells(cell => cell.InnerText.ToLower().Contains("smith"));
        Assert.AreEqual(3, matchingCells.Count);

        // You can get the first cell matching a condition through the GetFirstOrDefaultCell method.
        var matchingCell = firstRow.GetFirstOrDefaultCell(cell => cell.InnerText.ToLower().Contains("smith"));
        matchingCell.ValidateInnerTextIs("Smith");

        // You can convert a row to an instance of a specific class through the GetItem method.
        Assert.AreEqual("jsmith@gmail.com", firstRow.GetItem<User>().Email);

        // You can compare a row to an instance of a specific class.
        // The row is internally converted to the type of the expected object.
        firstRow.AssertRow(_expectedUsers[0]);
    }

    [Test]
    public void AssertHeaders()
    {
        // You can get all table header rows through the TableHeaderRows property.
        Table.TableHeaderRows.ForEach(header => header.AssertFontFamily("\"Times New Roman\""));

        // As a shortcut, you can iterate over the header rows through the ForEachHeader method.
        Table.ForEachHeader(row => row.AssertFontFamily("\"Times New Roman\""));

        // You can get all table header cells through the ColumnHeaders property.
        List<Label> headerCells = Table.ColumnHeaders;
        headerCells.ForEach(cell => cell.AssertFontSize("16px"));
    }

    [Test]
    public void AssertSpecificCell()
    {
        var firstCell = Table.GetCell(0, 0);

        // You can get the cell row and column.
        Assert.AreEqual(0, firstCell.Row);
        Assert.AreEqual(0, firstCell.Column);

        // You can get the cell innerText.
        Assert.AreEqual("Smith", firstCell.InnerText);

        // You can use built-in BELLATRIX Validate methods to assert the cell attributes.
        firstCell.ValidateInnerTextIs("Smith");

        // You can get the cell innerHtml.
        var thirdCell = Table.GetCell(0, 2);
        Assert.AreEqual("Doe", thirdCell.InnerHtml);
    }

    [Test]
    public void AssertColumns()
    {
        // You can get the cells of a particular column mentioning the column number.
        var secondColumn = Table.GetColumn(1);
        Assert.AreEqual("John", secondColumn[0].InnerText);

        // You can use built-in BELLATRIX Validate methods to assert the cell attributes.
        secondColumn[0].ValidateInnerTextIs("John");

        // You can get the cells of a particular column mentioning the column name.
        secondColumn = Table.GetColumn("First Name");
        Assert.AreEqual("John", secondColumn[0].InnerText);

        secondColumn[0].ValidateInnerTextIs("John");
    }
}
