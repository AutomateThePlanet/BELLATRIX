using System.Diagnostics;
using Bellatrix.Playwright.MSTest;
using NUnit.Framework;

namespace Bellatrix.Playwright.GettingStarted;

[TestFixture]
public class BrowserServiceTests : NUnit.WebTest
{
    // 1. BELLATRIX gives you an interface to most common operations for controlling the started browser through the BrowserService class.
    // We already saw one of them WaitUntilReady waiting for all Ajax calls to complete and WaitForLoadState.
    [Test]
    [Category(Categories.CI)]
    public void GetCurrentUri()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 2. Get the current tab URL.
        Debug.WriteLine(App.Browser.Url);
    }

    [Test]
    [Category(Categories.CI)]
    public void ControlBrowser()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 3. Maximizes the browser.
        App.Browser.Maximize();

        // 4. Simulates clicking the browser's Back button.
        App.Browser.Back();

        // 5. Simulates clicking the browser's Forward button.
        App.Browser.Forward();

        // 6. Simulates clicking the browser's Refresh button.
        App.Browser.Refresh();
    }

    [Test]
    [Category(Categories.CI)]
    public void GetTabTitle()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 7. Get the current tab Title.
        Assert.That("Bellatrix Demos – Bellatrix is a cross-platform, easily customizable and extendable .NET test automation framework that increases tests’ reliability.".Equals(App.Browser.Title));
    }

    [Test]
    [Category(Categories.CI)]
    public void PrintCurrentPageHtml()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 8. Get the current page HTML.
        Debug.WriteLine(App.Browser.HtmlSource);
    }

    [Test]
    [Ignore("no need to run")]
    public void SwitchToFrame()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // 9. To work with elements inside a frame, you can just create a normal Frame component with the available find strategies.
        var frame = App.Components.CreateById<Frame>("myFrameId");

        var myButton = frame.CreateById<Button>("purchaseBtnId");

        myButton.Click();
    }
}