namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods
{
    public static class UntilElementsExtensions
    {
        // 1. The next and final step is to create an extension method for all UI elements.
        // After UntilHaveSpecificContent is created, it is important to be passed on to the element’s EnsureState method.
        public static TElementType ToHaveSpecificContent<TElementType>(this TElementType element, string content, int? timeoutInterval = null, int? sleepInterval = null)
         where TElementType : Element
        {
            var until = new UntilHaveSpecificContent(content, timeoutInterval, sleepInterval);
            element.EnsureState(until);
            return element;
        }
    }
}