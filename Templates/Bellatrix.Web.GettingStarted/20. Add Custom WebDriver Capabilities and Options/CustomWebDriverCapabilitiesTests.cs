using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Bellatrix.Web.GettingStarted
{
    [TestClass]
    [SauceLabs(BrowserType.Firefox,
        "50",
        "Windows",
        BrowserBehavior.ReuseIfStarted,
        recordScreenshots: true,
        recordVideo: true)]
    public class CustomWebDriverCapabilitiesTests : WebTest
    {
        // 1. BELLATRIX hides the complexity of initialisation of WebDriver and all related services.
        // In some cases, you need to customise the set up of a browser with using WebDriver options, adding driver capabilities or using browser profile.
        // Using the App service methods you can add all of these with ease. Make sure to call them in the TestsArrange which is called before the
        // execution of the tests placed in the test class. These options are used only for the tests in this particular class.
        // Note: You can use all of these methods no matter which attributes you use- Browser, Remote, SauceLabs, BrowserStack or CrossBrowserTesting.
        public override void TestsArrange()
        {
            var firefoxOptions = new FirefoxOptions
            {
                AcceptInsecureCertificates = true,
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                PageLoadStrategy = PageLoadStrategy.Eager,
            };

            // 2. Add custom WebDriver options.
            App.AddWebDriverOptions(firefoxOptions);

            // 3. Add custom WebDriver capability.
            App.AddAdditionalCapability("disable-popup-blocking", true);

            // 4. Add an existing Firefox profile. You may want to test your application in together with some specific browser extension.
            var profileManager = new FirefoxProfileManager();
            FirefoxProfile profile = profileManager.GetProfile("Bellatrix");

            App.AddWebDriverBrowserProfile(profile);
        }

        [TestMethod]
        [Ignore]
        public void PromotionsPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var promotionsLink = App.ElementCreateService.CreateByLinkText<Anchor>("Promotions");

            promotionsLink.Click();
        }

        [TestMethod]
        [Ignore]
        public void BlogPageOpened_When_PromotionsButtonClicked()
        {
            App.NavigationService.Navigate("http://demos.bellatrix.solutions/");

            var blogLink = App.ElementCreateService.CreateByLinkText<Anchor>("Blog");

            blogLink.Click();
        }
    }
}