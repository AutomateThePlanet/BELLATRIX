using Bellatrix.AWS;

namespace Bellatrix.Desktop;

public static class AppExtensions
{
    extension(App _)
    {
        public AWSServicesFactory AWS => ServicesCollection.Current.Resolve<AWSServicesFactory>();
    }
}