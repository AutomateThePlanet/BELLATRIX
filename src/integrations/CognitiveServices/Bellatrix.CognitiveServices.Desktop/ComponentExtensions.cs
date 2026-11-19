using Bellatrix.CognitiveServices;
using Bellatrix.CognitiveServices.services;

namespace Bellatrix.Desktop;

public static class ComponentExtensions
{
    extension(Component component)
    {
        public AssertedFormPage AIAnalyze()
        {
            string currentComponentScreenshot = component.TakeScreenshot();
            var formRecognizer = ServicesCollection.Current.Resolve<FormRecognizer>();
            var analyzedComponent = formRecognizer.Analyze(currentComponentScreenshot);
            return analyzedComponent;
        }
    }
}