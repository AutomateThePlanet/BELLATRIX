using Bellatrix.CognitiveServices;

namespace Bellatrix.Desktop;

public static class AppExtensions
{
    extension(App _)
    {
        public ComputerVision ComputerVision => ServicesCollection.Current.Resolve<ComputerVision>();
        public FormRecognizer FormRecognizer => ServicesCollection.Current.Resolve<FormRecognizer>();
    }
}