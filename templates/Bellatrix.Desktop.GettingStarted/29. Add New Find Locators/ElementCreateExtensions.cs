using Bellatrix.Desktop.Controls.Core;

namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators;

// 1. To ease the usage of the locator, we need to create extension methods of Element.
// This is everything after that you can use your new locator as it was originally part of Bellatrix.
public static class ElementCreateExtensions
{
    // public static TComponent CreateByNameStartingWith<TComponent>(this Element element, string idPart)
    // where TComponent : Element => element.Create<TComponent, ByIdStartingWith>(new ByIdStartingWith(idPart));
    public static ComponentsList<TComponent> CreateAllByNameStartingWith<TComponent>(this Component element, string tag)
        where TComponent : Component => new ComponentsList<TComponent>(new FindNameStartingWithStrategy(tag), element.WrappedElement);
}
