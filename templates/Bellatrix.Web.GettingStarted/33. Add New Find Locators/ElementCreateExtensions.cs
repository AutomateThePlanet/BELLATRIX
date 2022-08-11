namespace Bellatrix.Web.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of ComponentCreateService.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementCreateExtensions
{
    // public static TComponent CreateByIdStartingWith<TComponent>(this Element element, string idPrefix)
    // where TComponent : Element => ComponentCreateService.Create<TComponent, FindIdStartingWithStrategy>(new FindIdStartingWithStrategy(idPrefix));
    public static ComponentsList<TComponent> CreateAllByIdStartingWith<TComponent>(this Component element, string idEnding)
        where TComponent : Component => new ComponentsList<TComponent>(new FindIdStartingWithStrategy(idEnding), element.WrappedElement);
}