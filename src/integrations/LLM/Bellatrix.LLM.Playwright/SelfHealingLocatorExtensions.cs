using Bellatrix.LLM;
using Bellatrix.LLM.Settings;
using Bellatrix.Playwright.Events;

namespace Bellatrix.Playwright;

public static class SelfHealingLocatorExtensions
{
    private static bool _initialized;

    public static void Add()
    {
        if (_initialized)
        {
            return;
        }

        Component.ElementResolved += OnElementResolved;
        Component.ElementResolveFailed += OnElementResolveFailed;

        _initialized = true;
    }

    private static void OnElementResolved(object sender, ElementResolvedEventArgs e)
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        if (settings is not { EnableSelfHealing: true })
        {
            return;
        }

        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();

        LocatorSelfHealingService.SaveWorkingLocator(
            e.Component.By.ToString(),
            snapshot,
            e.Component.WrappedBrowser.CurrentPage.Url);
    }

    private static void OnElementResolveFailed(object sender, ElementResolveFailedEventArgs e)
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        if (settings == null || !settings.EnableSelfHealing)
        {
            return;
        }

        Logger.LogWarning(
            $"⚠️ Element not found with locator: {e.Component.By}. Trying AI-based healing...");

        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();

        var healedLocator = LocatorSelfHealingService.TryHeal(e.Component.By.ToString(), snapshot, e.Component.WrappedBrowser.CurrentPage.Url);

        if (!string.IsNullOrWhiteSpace(healedLocator))
        {
            try
            {
                e.ResolvedElement = new FindXpathStrategy(healedLocator).Resolve(e.Component.WrappedBrowser.CurrentPage);
            }
            catch
            {
                throw new InvalidOperationException($"Healing attempt failed: {e.Component.By.Value}", e.Exception);
            }
        }

        throw new InvalidOperationException($"Original and healed locators failed: {e.Component.By.Value}", e.Exception);
    }
}