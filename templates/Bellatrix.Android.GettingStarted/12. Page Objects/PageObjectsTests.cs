using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bellatrix.Mobile.Android.GettingStarted
{
    [TestClass]
    [Android(Constants.AndroidNativeAppPath,
        Constants.AndroidDefaultAndroidVersion,
        Constants.AndroidDefaultDeviceName,
        Constants.AndroidNativeAppAppExamplePackage,
        ".view.Controls1",
        Lifecycle.RestartEveryTime)]
    public class PageObjectsTests : MSTest.AndroidTest
    {
        // 1. As you most probably noticed this is like the 4th time we use almost the same elements and logic inside our tests.
        // Similar test writing approach leads to unreadable and hard to maintain tests.
        // Because of that people use the so-called Page Object design pattern to reuse their elements and pages' logic.
        // BELLATRIX comes with powerful built-in page objects which are much more readable and maintainable than regular vanilla WebDriver ones.

        // 2. To create a new page object, you have a couple of options. You can create it manually. However, why wasting time?
        // BELLATRIX comes with ready-to-go page object templates. How to create a new page object?
        // 2.1. Create a new folder for your page and name it properly.
        // 2.2. Open the context menu and click 'New Item...'
        // 2.3. Choose one of the 4 Android page objects templates
        // - Bellatrix-AssertedAndroidPage - contains 3 files- one for actions, one for element declarations and one for assertions (all of them make one-page object)
        // - Bellatrix-AndroidPage- one for actions and one for elements (all of them make a one-page object), don't have methods for navigation
        // - Bellatrix-AndroidAssertedNavigatablePage- one for actions and one for elements (all of them make a one-page object), one for assertions and contains methods for navigating to specific activity.
        // - Bellatrix-AndroidNavigatablePage- one for actions and one for elements (all of them make a one-page object) and contains methods for navigating to specific activity.
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
        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ActionsWithoutPageObjects()
        {
            var button = App.ComponentCreateService.CreateByIdContaining<Button>("button_disabled");
            button.ValidateIsDisabled();
            var checkBox = App.ComponentCreateService.CreateByIdContaining<CheckBox>("check1");
            checkBox.Check();
            checkBox.ValidateIsChecked();
            var comboBox = App.ComponentCreateService.CreateByIdContaining<ComboBox>("spinner1");
            comboBox.SelectByText("Jupiter");
            comboBox.ValidateTextIs("Jupiter");
            var label = App.ComponentCreateService.CreateByText<Label>("textColorPrimary");
            label.ValidateIsVisible();
            var radioButton = App.ComponentCreateService.CreateByIdContaining<RadioButton>("radio2");
            radioButton.Click();
            radioButton.ValidateIsChecked(timeout: 30, sleepInterval: 2);
        }

        [TestMethod]
        [TestCategory(Categories.CI)]
        public void ActionsWithPageObjects()
        {
            // 5. You can use the App Create method to get an instance of it.
            var mainPage = App.Create<MainAndroidPage>();

            // 6. Opens the specified Android activity screen.
            mainPage.GoTo();

            // 7. After you have the instance, you can directly start using the action methods of the page.
            // As you can see the test became much shorter and more readable.
            // The additional code pays off in future when changes are made to the page, or you need to reuse some of the methods.
            mainPage.TransferItem("Jupiter", "bellatrix", "topsecret");

            mainPage.AssertKeepMeLoggedChecked();
            mainPage.AssertPermanentTransferIsChecked();
            mainPage.AssertRightItemSelected("Jupiter");
            mainPage.AssertRightUserNameSet("bellatrix");
        }
    }
}