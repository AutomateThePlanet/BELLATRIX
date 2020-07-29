using Bellatrix.Desktop.Controls.Core;

namespace Bellatrix.Desktop.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of ElementCreateService.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByNameStartingWith<TElement>(this ElementCreateService repo, string tag)
            where TElement : Element => repo.Create<TElement, ByNameStartingWith>(new ByNameStartingWith(tag));

        public static ElementsList<TElement> CreateAllByNameStartingWith<TElement>(this ElementCreateService repo, string tag)
            where TElement : Element => new ElementsList<TElement>(new ByNameStartingWith(tag), null);
    }
}
