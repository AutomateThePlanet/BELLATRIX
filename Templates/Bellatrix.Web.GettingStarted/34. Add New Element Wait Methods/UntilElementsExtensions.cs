namespace Bellatrix.Web.GettingStarted.ExtensionMethodsWaits
{
    public static class UntilElementsExtensions
    {
        public static TElementType ToHasSpecificStyle<TElementType>(this TElementType element, string style, int? timeoutInterval = null, int? sleepInterval = null)
            where TElementType : Element
        {
            var until = new UntilHasStyle(style, timeoutInterval, sleepInterval);
            element.EnsureState(until);
            return element;
        }
    }
}