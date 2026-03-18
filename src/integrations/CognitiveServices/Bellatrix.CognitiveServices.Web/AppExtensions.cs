using Bellatrix.CognitiveServices;

namespace Bellatrix.Web;

public static class AppExtensions
{
    extension(App _)
    {
        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
        public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    }
}