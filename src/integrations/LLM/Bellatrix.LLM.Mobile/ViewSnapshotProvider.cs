using Bellatrix.Mobile;
using Bellatrix.Mobile.Services;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;

namespace Bellatrix.LLM.Mobile;

public class ViewSnapshotProvider : IViewSnapshotProvider
{
    private AppService<AndroidDriver, AppiumElement> AndroidAppService => ServicesCollection.Current.Resolve<AppService<AndroidDriver, AppiumElement>>();
    private AppService<IOSDriver, AppiumElement> IOSAppService => ServicesCollection.Current.Resolve<AppService<IOSDriver, AppiumElement>>();

    public string GetCurrentViewSnapshot()
    {
        if (AndroidAppService?.WrappedAppiumDriver != null) return AndroidAppService.GetCurrentViewSnapshot();
        if (IOSAppService?.WrappedAppiumDriver != null) return IOSAppService.GetCurrentViewSnapshot();
        throw new Exception("Neither Android nor iOS wrapped appium driver found.");
    }
}