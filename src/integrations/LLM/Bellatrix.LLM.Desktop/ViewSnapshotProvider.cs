using Bellatrix.Desktop;

namespace Bellatrix.LLM.Desktop;

public class ViewSnapshotProvider : IViewSnapshotProvider
{
    private App App => ServicesCollection.Current.Resolve<App>();

    public string GetCurrentViewSnapshot()
    {
        return App.GetCurrentViewSnapshot();
    }
}