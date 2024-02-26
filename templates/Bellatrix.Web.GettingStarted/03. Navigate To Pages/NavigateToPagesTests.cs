using Bellatrix.Web.MSTest;
using NUnit.Framework;

namespace Bellatrix.Web.GettingStarted;

// Please notice that we don't use the Browser attribute. A default browser/selenium grid can be specified in the testFrameworkSettings.json file
// under the executionSettings section. There you can specify default lifecycle, version, grid URL, default resolution, and arguments.
// You can still use the Browser attribute on top of classes or tests to override the default behavior.
[TestFixture]
public class NavigateToPagesTests : NUnit.WebTest
{
    // Depending on the types of tests you want to write there are a couple of ways to navigate to specific pages.
    // In later chapters, there are more details about the different test workflow hooks. Find here two of them.
    //
    // 1. If you reuse your browser and want to navigate once to a specific page. You can use the TestsAct method.
    // It executes once for all tests in the class.
    public override void TestsAct() => App.Navigation.Navigate("https://demos.bellatrix.solutions/");

    // 2. If you need each test to navigate each time to the same page, you can use the TestInit method.
    public override void TestInit() => App.Navigation.Navigate("https://demos.bellatrix.solutions/");

    [Test]
    [Category(Categories.CI)]
    public void PromotionsPageOpened_When_PromotionsButtonClicked()
    {
        // 3. You can always navigate in each separate tests, but if all of them go to the same page, you can use the above techniques for code reuse.
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        // Use the element creation service to create an instance of the anchor. There are much more details about this process in the next sections.
        var promotionsLink = App.Components.CreateByLinkText<Anchor>("Promotions");

        promotionsLink.Click();

        // 4. Sometimes, some AJAX async calls are not caught natively by WebDriver. So you can use the BELLATRIX browser service's method.
        // WaitUntilReady which waits for these calls automatically to finish.
        // Keep in mind that usually this is not necessary since BELLATRIX has a complex built-in mechanism for handling element waits.
        App.Browser.WaitUntilReady();
    }

    [Test]
    [Category(Categories.CI)]
    public void BlogPageOpened_When_PromotionsButtonClicked()
    {
        App.Navigation.Navigate("https://demos.bellatrix.solutions/");

        var blogLink = App.Components.CreateByLinkText<Anchor>("Blog");

        blogLink.Click();

        // 5. Sometimes before proceeding with searching and making actions on the next page, we need to wait for something.
        // It is useful in some cases to wait for a partial URL instead hard-coding the whole URL since it can change depending on the environment.
        // Keep in mind that usually this is not necessary since BELLATRIX has a complex built-in mechanism for handling element waits.
        App.Navigation.WaitForPartialUrl("/blog/");
    }

    [Test]
    [Category(Categories.CI)]
    public void TestFileOpened_When_NavigateToLocalPage()
    {
        // 6. Sometimes you may need to navigate to a local HTML file. We make it easier for you since it is complicated depending on the different browsers.
        // Make sure to copy the file to the folder with your tests files. To do it, include it in the project and mark it as Copy Always.
        App.Navigation.NavigateToLocalPage("testPage.html");
    }

    [Test]
    public void GetUrlsBasedOnTestEnvironment()
    {
        // 7. You can use UrlDeterminer class to specify different URLs based on the test environment.
        // In each specific Environment config, you can change the URLs for the different apps under the urlSettings section.
        // To be able, you need to create a simple C# class holding the URL names as public string properties. Similar to UrlSettings class.
        string cartUrl = UrlDeterminer.GetUrl<UrlSettings>(u => u.ShopUrl);

        App.Assert.AreEqual("https://demos.bellatrix.solutions/cart/", cartUrl);
    }
}