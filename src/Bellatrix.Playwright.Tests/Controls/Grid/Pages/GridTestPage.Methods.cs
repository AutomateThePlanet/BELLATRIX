namespace Bellatrix.Playwright.Tests.Controls;

public partial class GridTestPage : WebPage
{
    public override string Url => ConfigurationService.GetSection<TestPagesSettings>().GridPage;
}