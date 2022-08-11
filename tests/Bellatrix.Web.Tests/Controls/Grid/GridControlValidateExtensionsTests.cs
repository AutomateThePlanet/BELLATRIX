using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.Tests.Controls;

[TestClass]
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]
[AllureSuite("Grid Control")]
public class GridControlValidateExtensionsTests : MSTest.WebTest
{
    private Grid _testGrid;

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
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ValidateInnerText_When_GetCell()
    {
        GridCell cell = _testGrid.GetCell(0, 1);

        cell.ValidateInnerTextIs("John");
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ValidateInnerTextContains_When_GetCell()
    {
        GridCell cell = _testGrid.GetCell(0, 1);

        cell.ValidateInnerTextContains("Jo");
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ValidateInnerHtmlIs_When_GetCell()
    {
        GridCell cell = _testGrid.GetCell(0, 1);

        cell.ValidateInnerHtmlIs("<b>John</b>");
    }

    [TestMethod]
    [TestCategory(Categories.Chrome), TestCategory(Categories.Windows), TestCategory(Categories.OSX)]
    public void ValidateInnerHtmlContains_When_GetCell()
    {
        GridCell cell = _testGrid.GetCell(0, 1);

        cell.ValidateInnerHtmlContains("</b>");
    }
}