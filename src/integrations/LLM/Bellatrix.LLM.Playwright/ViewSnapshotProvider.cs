using Bellatrix.Playwright;

namespace Bellatrix.LLM.Playwright;

public class ViewSnapshotProvider : IViewSnapshotProvider
{
    private App App => ServicesCollection.Current.Resolve<App>();

    public string GetCurrentViewSnapshot()
    {
        return App.GetCurrentViewSnapshot();
    }
}