using Bellatrix.Desktop.NUnit;
using NUnit.Framework;

namespace Bellatrix.Desktop.GettingStarted;

[TestFixture]
public class PageObjectsTests : DesktopTest
{
    // 1. As you most probably noticed this is like the 4th time we use almost the same elements and logic inside our tests.
    // Similar test writing approach leads to unreadable and hard to maintain tests.
    // Because of that people use the so-called Page Object design pattern to reuse their elements and pages' logic.
    // BELLATRIX comes with powerful built-in page objects which are much more readable and maintainable than regular vanilla WinAppDriver ones.
    // 2. On most pages, you need to define elements. Placing them in a single place makes the changing of the locators easy.
    // It is a matter of choice whether to have action methods or not. If you use the same combination of same actions against a group of elements then
    // it may be a good idea to wrap them in a page object action method. In our example, we can wrap the transfer of an item in such a method.
    //
    // 3. In the assertions file, we may place some predefined Validate methods. For example, if you always check the same email or title of a page,
    // there is no need to hard-code the string in each test. Later if the title is changed, you can do it in a single place.
    // The same is true about most of the things you can assert in your tests.
    //
    // This is the same test that doesn't use page objects.
    [Test]
    [Category(Categories.CI)]
    public void ActionsWithoutPageObjects_Wpf()
    {
        var permanentTransfer = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        permanentTransfer.Check();

        App.Assert.IsTrue(permanentTransfer.IsChecked);

        var items = App.Components.CreateByAutomationId<ComboBox>("select");

        items.SelectByText("Item2");

        App.Assert.AreEqual("Item2", items.InnerText);

        var returnItemAfter = App.Components.CreateByName<Component>("DisappearAfterButton1");

        returnItemAfter.ToNotExists().WaitToBe();

        var password = App.Components.CreateByAutomationId<Password>("passwordBox");

        password.SetPassword("topsecret");

        var userName = App.Components.CreateByAutomationId<TextField>("textBox");

        userName.SetText("bellatrix");

        App.Assert.AreEqual("bellatrix", userName.InnerText);

        var keepMeLogged = App.Components.CreateByName<RadioButton>("RadioButton");

        keepMeLogged.Click();

        App.Assert.IsTrue(keepMeLogged.IsChecked);

        var byName = App.Components.CreateByName<Button>("E Button");

        byName.Click();

        var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");

        App.Assert.IsTrue(label.IsPresent);
    }

    [Test]
    [Category(Categories.CI)]
    public void ActionsWithPageObjects_Wpf()
    {
        // 5. You can use the App Create method to get an instance of it.
        var mainPage = App.Create<MainDesktopPage>();

        // 6. After you have the instance, you can directly start using the action methods of the page.
        // As you can see the test became much shorter and more readable.
        // The additional code pays off in future when changes are made to the page, or you need to reuse some of the methods.
        mainPage.TransferItem("Item2", "bellatrix", "topsecret");

        mainPage.AssertKeepMeLoggedChecked();
        mainPage.AssertPermanentTransferIsChecked();
        mainPage.AssertRightItemSelected("Item2");
        mainPage.AssertRightUserNameSet("bellatrix");
    }
}