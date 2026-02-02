using Bellatrix.CognitiveServices;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    public static LighthouseService Lighthouse(this App app)
    {
        return ServicesCollection.Current.Resolve<LighthouseService>();
    }
}