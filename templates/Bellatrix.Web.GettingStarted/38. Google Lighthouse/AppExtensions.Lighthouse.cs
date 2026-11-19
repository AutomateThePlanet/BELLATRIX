using Bellatrix.CognitiveServices;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    extension(App _)
    {
        public static LighthouseService Lighthouse
        {
            get {
                return ServicesCollection.Current.Resolve<LighthouseService>();
            }
        }
    }
}