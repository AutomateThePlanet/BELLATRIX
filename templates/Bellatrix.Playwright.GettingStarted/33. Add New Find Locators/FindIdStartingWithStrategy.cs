

using Bellatrix.Playwright.SyncPlaywright;
using Microsoft.Playwright;

namespace Bellatrix.Playwright.GettingStarted;

// Here is a sample implementation of the locator for finding all elements starting with ID.
// 1. We need to create the find strategy.
public class FindIdStartingWithStrategy : FindStrategy
{
    public FindIdStartingWithStrategy(string value)
        : base(value)
    {
    }

    // 2. In the Convert method, we use a standard Playwright Locator,
    // and in this case we implement our requirements through a little CSS.
    public override WebElement Resolve(BrowserPage searchContext)
    {
        return searchContext.Locate($"[id^='{Value}']");
    }

    public override WebElement Resolve(WebElement searchContext)
    {
        return searchContext.Locate($"[id^='{Value}']");
    }

    public override string ToString()
    {
        return $"ID starting with {Value}";
    }
}
