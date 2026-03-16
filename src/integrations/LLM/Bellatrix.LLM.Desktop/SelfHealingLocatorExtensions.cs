using Bellatrix.Desktop.Events;
using Bellatrix.Desktop.Locators;
using Bellatrix.LLM;
using Bellatrix.LLM.Settings;
using OpenQA.Selenium;

namespace Bellatrix.Desktop;

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
            e.Component.WrappedDriver.Capabilities.GetCapability("appium:app") as string ?? e.Component.WrappedDriver.CurrentWindowHandle);
    }

    private static void OnElementResolveFailed(object sender, ElementResolveFailedEventArgs e)
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        if (settings is not { EnableSelfHealing: true })
        {
            return;
        }

        Logger.LogWarning(
            $"⚠️ Element not found with locator: {e.Component.By}. Trying AI-based healing...");

        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();

        var healedXpath = LocatorSelfHealingService.TryHeal(
            e.Component.By?.ToString(),
            snapshot,
            e.Component.WrappedDriver.Capabilities.GetCapability("appium:app") as string ?? e.Component.WrappedDriver.CurrentWindowHandle);

        if (!string.IsNullOrEmpty(healedXpath))
        {
            try
            {
                e.ResolvedElement =
                    new FindXPathStrategy(healedXpath)
                        .FindElement(e.Component.WrappedDriver);
            }
            catch
            {
                throw new NotFoundException(
                    $"❌ Healing attempt failed for locator: {e.Component.By}",
                    e.Exception);
            }
        }
        else
        {
            throw new NotFoundException(
                $"❌ Healing failed: {e.Component.By}",
                e.Exception);
        }
    }
}