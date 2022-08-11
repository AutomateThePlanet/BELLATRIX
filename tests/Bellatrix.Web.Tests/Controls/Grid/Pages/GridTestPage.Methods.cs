namespace Bellatrix.Web.Tests.Controls;

public partial class GridTestPage : WebPage
{
    public override string Url => ConfigurationService.GetSection<TestPagesSettings>().GridLocalPage;
}