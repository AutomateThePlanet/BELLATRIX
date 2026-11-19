using Bellatrix.LLM;
using Bellatrix.LLM.Settings;
using Bellatrix.Web.Events;
using OpenQA.Selenium;

namespace Bellatrix.Web;

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
            e.Component.WrappedDriver.Url);
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
        
        var nativeElementFinderService = e.Component.ParentWrappedElement == null
            ? new NativeElementFinderService(e.Component.WrappedDriver)
            : new NativeElementFinderService(e.Component.ParentWrappedElement);

        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();

        var healedXpath = LocatorSelfHealingService.TryHeal(
            e.Component.By?.ToString(),
            snapshot,
            e.Component.WrappedDriver.Url);

        if (!string.IsNullOrEmpty(healedXpath))
        {
            try
            {
                var healedElement = nativeElementFinderService.FindAll(new FindXpathStrategy(healedXpath)).ElementAt(e.Component.ElementIndex);
                Logger.LogInformation("🧠 Using AI-suggested fallback locator. Original not updated.");
                e.ResolvedElement = healedElement;
            }
            catch
            {
                throw new NotFoundException($"❌ Healing attempt failed: {e.Component.By.Value}", e.Exception);
            }
        }

        throw new NotFoundException($"❌ Original and healed locators failed: {e.Component.By.Value}", e.Exception);
    }
}