using Bellatrix.Playwright.Enums;

namespace Bellatrix.Playwright;

public class LighthouseAnalysisRunAttribute : BrowserAttribute
{
    public LighthouseAnalysisRunAttribute(bool useHeadless = false)
        : base(useHeadless ? BrowserChoice.ChromeHeadless : BrowserChoice.Chrome, Lifecycle.RestartEveryTime)
    {
        IsLighthouseEnabled = true;
    }
}
