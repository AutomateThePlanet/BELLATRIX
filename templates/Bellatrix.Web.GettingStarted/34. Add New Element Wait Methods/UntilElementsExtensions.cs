namespace Bellatrix.Web.GettingStarted.ExtensionMethodsWaits;

public static class UntilElementsExtensions
{
    public static TComponentType ToHasSpecificStyle<TComponentType>(this TComponentType element, string style, int? timeoutInterval = null, int? sleepInterval = null)
        where TComponentType : Component
    {
        var until = new WaitToHasStyleStrategy(style, timeoutInterval, sleepInterval);
        element.EnsureState(until);
        return element;
    }
}