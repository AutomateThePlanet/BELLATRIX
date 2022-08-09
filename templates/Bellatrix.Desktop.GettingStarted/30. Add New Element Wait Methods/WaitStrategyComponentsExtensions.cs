namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsWaitMethods;

public static class WaitStrategyComponentsExtensions
{
    // 1. The next and final step is to create an extension method for all UI elements.
    // After WaitToHaveSpecificContentStrategy is created, it is important to be passed on to the element’s ValidateState method.
    public static TComponentType ToHaveSpecificContent<TComponentType>(this TComponentType element, string content, int? timeoutInterval = null, int? sleepInterval = null)
     where TComponentType : Component
    {
        var until = new WaitToHaveSpecificContentStrategy(content, timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }
}