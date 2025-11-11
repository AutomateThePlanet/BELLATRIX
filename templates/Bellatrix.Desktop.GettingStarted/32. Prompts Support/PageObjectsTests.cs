using Bellatrix.Desktop.LLM.Extensions;
using Bellatrix.Desktop.NUnit;
using NUnit.Framework;
using static Bellatrix.AiValidator;
using static Bellatrix.AiAssert;

namespace Bellatrix.Desktop.GettingStarted.LLM;

[TestFixture]
public class PageObjectsTests : DesktopTest
{
    [Test]
    public void ActionsWithoutPageObjects_Wpf_LLM()
    {
        var permanentTransfer = App.Components.CreateByName<CheckBox>("BellaCheckBox");

        permanentTransfer.Check();

        App.Assert.IsTrue(permanentTransfer.IsChecked);

        //var items = App.Components.CreateByAutomationId<ComboBox>("select");
        var items = App.Components.CreateByPrompt<ComboBox>("find the select under meissa check box");


        items.SelectByText("Item1");
        ValidateByPrompt("verify select under meissa check box displayed");

        var returnItemAfter = App.Components.CreateByName<Component>("DisappearAfterButton1");

        returnItemAfter.ToNotExists().WaitToBe();

        var password = App.Components.CreateByAutomationId<Password>("passwordBox");
        //var password = App.Components.CreateByAutomationId<Password>("passwordBoxUpdated");

        password.SetPassword("topsecret");

        var userName = App.Components.CreateByAutomationId<TextField>("textBox");

        userName.SetText("bellatrix");

        App.Assert.AreEqual("bellatrix", userName.InnerText);

        var keepMeLogged = App.Components.CreateByName<RadioButton>("RadioButton");

        keepMeLogged.Click();

        App.Assert.IsTrue(keepMeLogged.IsChecked);

        var byName = App.Components.CreateByName<Button>("E Button");

        byName.Click();

        //var label = App.Components.CreateByAutomationId<Label>("ResultLabelId");
        var label = App.Components.CreateByPrompt<Label>("find the label above the calendar");

        ValidateByPrompt("validate that the label says that the button was clicked");
        //App.Assert.IsTrue(label.IsPresent);
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