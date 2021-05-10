using System.Diagnostics;
using Bellatrix.Web.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [Browser(BrowserType.Chrome, Lifecycle.RestartEveryTime)]
    public class BrowserServiceTests : MSTest.WebTest
    {
        // 1. BELLATRIX gives you an interface to most common operations for controlling the started browser through the BrowserService class.
        // We already saw one of them WaitUntilReady waiting for all Ajax calls to complete.
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void GetCurrentUri()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 2. Get the current tab URL.
            Debug.WriteLine(App.BrowserService.Url);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ControlBrowser()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 3. Maximizes the browser.
            App.BrowserService.Maximize();

            // 4. Simulates clicking the browser's Back button.
            App.BrowserService.Back();

            // 5. Simulates clicking the browser's Forward button.
            App.BrowserService.Forward();

            // 6. Simulates clicking the browser's Refresh button.
            App.BrowserService.Refresh();
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void GetTabTitle()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 7. Get the current tab Title.
            Assert.AreEqual("Bellatrix Demos – Bellatrix is a cross-platform, easily customizable and extendable .NET test automation framework that increases tests’ reliability.", App.BrowserService.Title);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void PrintCurrentPageHtml()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 8. Get the current page HTML.
            Debug.WriteLine(App.BrowserService.HtmlSource);
        }

        [TestMethod]
        [Ignore]
        public void SwitchToFrame()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            // 9. To work with elements inside a frame, you should switch to it first.
            var frame = App.ElementCreateService.CreateById<Frame>("myFrameId");
            App.BrowserService.SwitchToFrame(frame);

            // Search for the button inside the frame ElementCreateService. Of course, once you switched to frame, you can create the element through ElementCreateService too.
            var myButton = frame.CreateById<Button>("purchaseBtnId");

            myButton.Click();

            // 10. To continue searching in the whole page, you need to switch to default again. It is the same process of how you work with WebDriver.
            App.BrowserService.SwitchToDefault();
        }
    }
}