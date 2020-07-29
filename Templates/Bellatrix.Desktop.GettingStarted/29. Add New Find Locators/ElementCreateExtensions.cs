using Bellatrix.Desktop.Controls.Core;

namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of Element.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementCreateExtensions
    {
        // public static TElement CreateByNameStartingWith<TElement>(this Element element, string idPart)
        // where TElement : Element => element.Create<TElement, ByIdStartingWith>(new ByIdStartingWith(idPart));
        public static ElementsList<TElement> CreateAllByNameStartingWith<TElement>(this Element element, string tag)
            where TElement : Element => new ElementsList<TElement>(new ByNameStartingWith(tag), element.WrappedElement);
    }
}
