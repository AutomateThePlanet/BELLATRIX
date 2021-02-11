namespace Bellatrix.Web.Tests.Controls
{
    public partial class GridTestPage : AssertedNavigatablePage
    {
        public override string Url => ConfigurationService.GetSection<TestPagesSettings>().GridLocalPage;
    }
}