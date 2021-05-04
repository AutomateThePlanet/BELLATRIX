namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods
{
    public static class WaitStrategyElementsExtensions
    {
        // 1. The next and final step is to create an extension method for all UI elements.
        // After WaitToHaveSpecificContentStrategy is created, it is important to be passed on to the element’s ValidateState method.
        public static TElementType ToHaveSpecificContent<TElementType>(this TElementType element, string content, int? timeoutInterval = null, int? sleepInterval = null)
         where TElementType : Component
        {
            var until = new WaitToHaveSpecificContentStrategy(content, timeoutInterval, sleepInterval);
            element.EnsureState(until);
            return element;
        }
    }
}