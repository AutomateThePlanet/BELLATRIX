using Bellatrix.Playwright.Enums;

namespace Bellatrix.Playwright;

public class LighthouseAnalysisRunAttribute : BrowserAttribute
{
    public LighthouseAnalysisRunAttribute(bool useHeadless = false)
        : base(useHeadless ? BrowserTypes.ChromeHeadless : BrowserTypes.Chrome, Lifecycle.RestartEveryTime)
    {
        IsLighthouseEnabled = true;
    }
}
