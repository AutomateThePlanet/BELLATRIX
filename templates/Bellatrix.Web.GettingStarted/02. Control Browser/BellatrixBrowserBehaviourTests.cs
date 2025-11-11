using Bellatrix.Web.MSTest;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// 1. This is the main attribute that you need to mark each class that contains NUnit tests.
[TestFixture]

// 2. This is the attribute for automatic start/control of WebDriver browsers by Bellatrix. If you have to do it manually properly, you will need thousands of lines of code.
// 2.1. BrowserType controls which browser is used. Available options are Chrome, Firefox, Edge, InternetExplorer, Opera, Chrome in headless mode, Firefox in headless mode.
// (Headless mode = executed in the browser but the browser's UI is not rendered, in theory, should be faster. In practice the time gain is little.)
// 2.2. Lifecycle enum controls when the browser is started and stopped. This can drastically increase or decrease the tests execution time, depending on your needs.
// However you need to be careful because in case of tests failures the browser may need to be restarted.
// Available options:
// RestartEveryTime- for each test a separate WebDriver instance is created and the previous browser is closed. The new browser comes with new cookies and cache.
// RestartOnFail- the browser is only restarted if the previous test failed. Alternatively, if the previous test's browser was different.
// ReuseIfStarted- the browser is only restarted if the previous test's browser was different. In all other cases, the browser is reused if possible.
// Note: However, use this option with caution since in some rare cases if you have not properly setup your tests you may need to restart the browser if the test fails otherwise all other tests may fail too.
//
// There are even more things you can do with this attribute, but we look into them in the next sections.
//
// If you place attribute over the class all tests inherit the lifecycle. It is possible to place it over each test and this way it overrides the class lifecycle only for this particular test.
//
// If you don't use the attribute, the default information from the configuration will be used placed under the executionSettings section.
// Also, you can add additional driver arguments under the arguments section array in the configuration file.
[Browser(BrowserType.Chrome, Lifecycle.ReuseIfStarted)]

// 2.2. All web BELLATRIX test classes should inherit from the WebTest base class. This way you can use all built-in BELLATRIX tools and functionalities.
public class BellatrixBrowserLifecycleTests : NUnit.WebTest
{
    // 2.3. All MSTest tests should be marked with the Test attribute.
    [Test]
    [Category(Categories.CI)]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        // There is more about the App class in the next sections. However, it is the primary point where you access the BELLATRIX services.
        // It comes from the WebTest class as a property. Here we use the BELLATRIX navigation service to navigate to the demo page.
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // Use the element creation service to create an instance of the anchor. There are much more details about this process in the next sections.
        var promotionsLink = App.Components.CreateByLinkText<Anchor>("Promotions");

        promotionsLink.Click();
    }

    [Test]
    [Category(Categories.CI)]

    // 2.4. As mentioned above you can override the browser lifecycle for a particular test. The global lifecycle for all tests in the class is to reuse an instance of Edge browser.
    // Only for this particular test, BELLATRIX opens Chrome and restarts it only on fail.
    [Browser(BrowserType.Chrome, Lifecycle.RestartOnFail)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();
    }
}