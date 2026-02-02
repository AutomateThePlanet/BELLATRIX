using Bellatrix.CognitiveServices;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    extension(App _)
    {
        public static FormRecognizer FormRecognizer()
        {
            return ServicesCollection.Current.Resolve<FormRecognizer>();
        }

        public static ComputerVision ComputerVision()
        {
            return ServicesCollection.Current.Resolve<ComputerVision>();
        }
    }
}