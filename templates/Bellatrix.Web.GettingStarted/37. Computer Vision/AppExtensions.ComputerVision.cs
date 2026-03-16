using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;

namespace Bellatrix.Web.GettingStarted;

public static partial class AppExtensions
{
    extension(App _)
    {
        public static FormRecognizer FormRecognizer
        {
            get {
                return ServicesCollection.Current.Resolve<FormRecognizer>();
            }
        }

        public static ComputerVision ComputerVision
        {
            get {
                return ServicesCollection.Current.Resolve<ComputerVision>();
            }
        }
    }
}