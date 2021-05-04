namespace Bellatrix.Web.GettingStarted.ExtensionMethodsLocators
{
    // 1. To ease the usage of the locator, we need to create extension methods of ElementCreateService.
    // This is everything after that you can use your new locator as it was originally part of Bellatrix.
    public static class ElementRepositoryExtensions
    {
        public static TElement CreateByIdStartingWith<TElement>(this ElementCreateService repository, string idPrefix, bool shouldCache = false)
            where TElement : Component => repository.Create<TElement, FindIdStartingWithStrategy>(new FindIdStartingWithStrategy(idPrefix), shouldCache);

        public static ComponentsList<TElement> CreateAllByIdStartingWith<TElement>(this ElementCreateService repository, string idPrefix)
            where TElement : Component => new ComponentsList<TElement>(new FindIdStartingWithStrategy(idPrefix), null);
    }
}