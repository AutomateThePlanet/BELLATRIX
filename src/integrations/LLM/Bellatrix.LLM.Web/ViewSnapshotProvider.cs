using Bellatrix.Web;

namespace Bellatrix.LLM.Web;

public class ViewSnapshotProvider : IViewSnapshotProvider
{
    private App App => ServicesCollection.Current.Resolve<App>();

    public string GetCurrentViewSnapshot()
    {
        return App.GetCurrentViewSnapshot();
    }
}