using NUnit.Framework;

namespace Bellatrix.Mobile.IOS.GettingStarted;

[TestFixture]
[IOS(Constants.IOSNativeAppPath,
    Constants.AppleCalendarBundleId,
    Constants.IOSDefaultVersion,
    Constants.IOSDefaultDeviceName,
    Lifecycle.RestartEveryTime)]
public class PageObjectsTests : NUnit.IOSTest
{
    // 1. As you most probably noticed this is like the 4th time we use almost the same elements and logic inside our tests.
    // Similar test writing approach leads to unreadable and hard to maintain tests.
    // Because of that people use the so-called Page Object design pattern to reuse their elements and pages' logic.
    // BELLATRIX comes with powerful built-in page objects which are much more readable and maintainable than regular vanilla WebDriver ones.

    // 2. To create a new page object, you have a couple of options. You can create it manually. However, why wasting time?
    // BELLATRIX comes with ready-to-go page object templates. How to create a new page object?
    // 2.1. Create a new folder for your page and name it properly.
    // 2.2. Open the context menu and click 'New Item...'
    // 2.3. Choose one of the 2 iOS page objects templates
    // - Bellatrix-AssertedIOSPage - contains 3 files- one for actions, one for element declarations and one for assertions (all of them make one-page object)
    // - Bellatrix-IOSPage- one for actions and one for elements (all of them make a one-page object), don't have methods for navigation
    //
    // 3. On most pages, you need to define elements. Placing them in a single place makes the changing of the locators easy.
    // It is a matter of choice whether to have action methods or not. If you use the same combination of same actions against a group of elements then
    // it may be a good idea to wrap them in a page object action method. In our example, we can wrap the transfer of an item in such a method.
    //
    // 4. In the assertions file, we may place some predefined Validate methods. For example, if you always check the same email or title of a screen,
    // there is no need to hard-code the string in each test. Later if the title is changed, you can do it in a single place.
    // The same is true about most of the things you can assert in your tests.
    //
    // This is the same test that doesn't use page objects.
    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ActionsWithoutPageObjectsFirst()
    {
        var numberOne = App.Components.CreateById<TextField>("IntegerA");
        var numberTwo = App.Components.CreateById<TextField>("IntegerB");
        var compute = App.Components.CreateByName<Button>("ComputeSumButton");
        var answer = App.Components.CreateByName<Label>("Answer");

        numberOne.SetText("5");
        numberTwo.SetText("6");
        compute.Click();

        Assert.That("11".Equals(answer.GetText()));
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ActionsWithoutPageObjectsSecond()
    {
        var numberOne = App.Components.CreateById<TextField>("IntegerA");
        var numberTwo = App.Components.CreateById<TextField>("IntegerB");
        var compute = App.Components.CreateByName<Button>("ComputeSumButton");
        var answer = App.Components.CreateByName<Label>("Answer");

        numberOne.SetText("20");
        numberTwo.SetText("30");
        compute.Click();

        Assert.That("50".Equals(answer.GetText()));
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ActionsWithPageObjectsFirst()
    {
        // 5. You can use the App Create method to get an instance of it.
        var mainPage = App.Create<CalculatorPage>();

        // 7. After you have the instance, you can directly start using the action methods of the page.
        // As you can see the test became much shorter and more readable.
        // The additional code pays off in future when changes are made to the page, or you need to reuse some of the methods.
        mainPage.Sum(5, 6);

        mainPage.AssertAnswer(11);
    }

    [Test]
    [CancelAfter(180000)]
    [Category(Categories.CI)]
    public void ActionsWithPageObjectsSecond()
    {
        var mainPage = App.Create<CalculatorPage>();

        mainPage.Sum(20, 30);

        mainPage.AssertAnswer(50);
    }
}