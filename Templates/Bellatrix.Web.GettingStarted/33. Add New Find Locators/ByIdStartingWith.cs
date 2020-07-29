namespace Bellatrix.Web.GettingStarted
{
    // Here is a sample implementation of the locator for finding all elements starting with ID.
    // 1. We need to create the By locator.
    public class ByIdStartingWith : By
    {
        public ByIdStartingWith(string value)
            : base(value)
        {
        }

        // 2. In the Convert method, we use a standard WebDriver By locator,
        // and in this case we implement our requirements through a little CSS.
        public override OpenQA.Selenium.By Convert() => OpenQA.Selenium.By.CssSelector($"[id^='{Value}']");
    }
}
