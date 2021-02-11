namespace Bellatrix.Web.Tests.Controls
{
    public partial class GridTestPage : AssertedNavigatablePage
    {
        public override string Url => SettingsService.GetSection<TestPagesSettings>().GridLocalPage;
    }
}