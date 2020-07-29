namespace Bellatrix.Web.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of ElementCreateService.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByIdStartingWith<TElement>(this ElementCreateService repository, string idPrefix, bool shouldCache = false)
            where TElement : Element => repository.Create<TElement, ByIdStartingWith>(new ByIdStartingWith(idPrefix), shouldCache);

        public static ElementsList<TElement> CreateAllByIdStartingWith<TElement>(this ElementCreateService repository, string idPrefix)
            where TElement : Element => new ElementsList<TElement>(new ByIdStartingWith(idPrefix), null);
    }
}