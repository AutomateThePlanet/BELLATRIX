using Bellatrix.LLM;
using Bellatrix.LLM.Settings;
using Bellatrix.Mobile.Core;
using Bellatrix.Mobile.Events;
using Bellatrix.Mobile.Locators.Android;
using Bellatrix.Mobile.Locators.IOS;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.Mobile;

public static class SelfHealingLocatorExtensions
{
    private static bool _initializedForAndroid;
    private static bool _initializedForIOS;

    public static void AddForAndroid()
    {
        if (_initializedForAndroid)
        {
            return;
        }

        Component<AndroidDriver, AppiumElement>.ElementResolved += OnElementResolved;
        Component<AndroidDriver, AppiumElement>.ElementResolveFailed += OnElementResolveFailed;

        _initializedForAndroid = true;
    }
    
    public static void AddForIOS()
    {
        if (_initializedForIOS)
        {
            return;
        }

        Component<IOSDriver, AppiumElement>.ElementResolved += OnElementResolved;
        Component<IOSDriver, AppiumElement>.ElementResolveFailed += OnElementResolveFailed;

        _initializedForIOS = true;
    }

    private static void OnElementResolved<TDriver, TDriverElement>(object sender, ElementResolvedEventArgs<TDriver, TDriverElement> e)
        where TDriver : AppiumDriver
        where TDriverElement : AppiumElement
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        if (settings is not { EnableSelfHealing: true })
        {
            return;
        }
        
        // Optional: override if app uses titles, screens, or activity
        // For iOS: use current screen title; for Android: use current activity
        var locationKey = typeof(TDriver).Name; // fallback to driver type, or use injected service

        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();
        LocatorSelfHealingService.SaveWorkingLocator(e.Component.By.ToString(), snapshot, locationKey);
        
        LocatorSelfHealingService.SaveWorkingLocator(
            e.Component.By.ToString(),
            snapshot,
            ((IHasCapabilities)e.Component.WrappedDriver).Capabilities.GetCapability("appium:app") as string ?? e.Component.WrappedDriver.CurrentWindowHandle);
    }

    private static void OnElementResolveFailed<TDriver, TDriverElement>(object sender, ElementResolveFailedEventArgs<TDriver, TDriverElement> e)
        where TDriver : AppiumDriver
        where TDriverElement : AppiumElement
    {
        var settings = ConfigurationService.GetSection<LargeLanguageModelsSettings>();

        if (settings is not { EnableSelfHealing: true })
        {
            return;
        }

        Logger.LogWarning($"⚠️ Element not found with locator: {e.Component.By}. Trying AI-based healing...");
        
        // Optional: override if app uses titles, screens, or activity
        // For iOS: use current screen title; for Android: use current activity
        var locationKey = typeof(TDriver).Name; // fallback to driver type, or use injected service
        
        var snapshotProvider = ServicesCollection.Current.Resolve<IViewSnapshotProvider>();
        var snapshot = snapshotProvider.GetCurrentViewSnapshot();
        var healedLocator = LocatorSelfHealingService.TryHeal(e.Component.By.ToString(), snapshot, locationKey);

        if (!string.IsNullOrWhiteSpace(healedLocator))
        {
            try
            {
                // Determine strategy based on locator format
                if (healedLocator.StartsWith("uiautomator=", StringComparison.OrdinalIgnoreCase))
                {
                    var expression = healedLocator.Substring("uiautomator=".Length).Trim();
                    e.ResolvedElement = new FindAndroidUIAutomatorStrategy(expression).FindElement(e.Component.WrappedDriver as AndroidDriver) as TDriverElement;
                }
                else if (healedLocator.StartsWith("nspredicate=", StringComparison.OrdinalIgnoreCase))
                {
                    var expression = healedLocator.Substring("nspredicate=".Length).Trim();
                    e.ResolvedElement = new FindIOSNsPredicateStrategy(expression).FindElement(e.Component.WrappedDriver as IOSDriver) as TDriverElement;
                }
            }
            catch
            {
                throw new NotFoundException($"❌ Healing attempt failed for locator: {e.Component.By}", e.Exception);
            }
        }

        throw new NotFoundException($"❌ Healing failed: {e.Component.By}", e.Exception);
    }
}